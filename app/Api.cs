using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using app.ApiModels;


namespace app
{
    public static class Api
    {
        private static bool AuthenticateUserRequest(ClaimsPrincipal requster, int allowedId)
        {
            return requster.HasClaim(ClaimTypes.Role, "Admin") || requster.HasClaim("UserId", allowedId.ToString());
        }

        private static bool AuthenticateUserRequest(ClaimsPrincipal requster, string owner)
        {
            return requster.HasClaim(ClaimTypes.Role, "Admin") || requster.HasClaim(ClaimTypes.Name, owner);
        }

        internal static void MapApi(WebApplication app)
        {
            if (app.Environment.IsDevelopment()) {
                app.MapGet("api/admin", async (HttpContext ctx) =>
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Admin")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                    };

                    await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return Results.Ok();
                });
            }

            app.MapPost("api/login", async (HttpContext ctx, IUserRepository db, [FromBody] LoginRequest loginRequest) =>
            {
                User? user = db.Authenticate(loginRequest.Login, loginRequest.Password);

                if (user == null)
                {
                    Results.Text("Invalid login or password");
                    return Results.StatusCode(401);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Type == UserType.Admin? "Admin" : "Player"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                };


                await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return Results.Ok(new { user.Login, ap = user.ActionPoints, user.Money });
            });

            app.MapGet("api/users", async (IUserRepository db) =>
            {
                var users = await db.All();
                return Results.Ok(users);
            }).RequireAuthorization("AdminOnly");

            app.MapGet("api/users/{userId}", async (HttpContext ctx, [FromRoute] int userId, IUserRepository db) =>
            {
                User? user = await db.GetById(userId);
                return user != null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapPost("api/users/create", async ([FromBody] User user, IUserRepository db) =>
            {
                User? newUser = await db.Add(user);
                return newUser != null ? Results.Ok(newUser) : Results.BadRequest();
            }).RequireAuthorization("AdminOnly");

            app.MapPost("api/users/update", async ([FromBody] User user, IUserRepository db) =>
            {
                User? updatedUser = await db.Update(user);
                return updatedUser != null ? Results.Ok(updatedUser) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapDelete("api/users/remove/{userId}", async ([FromRoute] int userId, IUserRepository db) =>
            {
                User? user = await db.Remove(userId);
                return user != null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapPost("api/users/giveAdminPrivilege/{login}", async ([FromRoute] string login, IUserRepository db) =>
            {
                User? user = await db.GetByLogin(login);
                if (user == null) 
                    return Results.NotFound();
                
                user.Type = UserType.Admin;
                User? updatedUser = await db.Update(user);
                return Results.Ok(updatedUser);
            }).RequireAuthorization("AdminOnly");


            // frontend api

            app.MapGet("api/userData/{userLogin}", async (HttpContext ctx, [FromRoute] string userLogin, IUserRepository db) =>
            {
                User? user = await db.GetByLogin(userLogin);
                if (AuthenticateUserRequest(ctx.User, userLogin))
                    return user != null ? Results.Ok(user) : Results.NotFound();
                else
                    return Results.StatusCode(403);
            });

            app.MapGet("api/userData/heroes/{userLogin}", async (HttpContext ctx, [FromRoute] string userLogin, IUserRepository usersRepo, IHeroRepository heroesRepo) =>
            {
                User? user = await usersRepo.GetByLogin(userLogin);
                if (AuthenticateUserRequest(ctx.User, userLogin))
                    return user != null ? Results.Ok(user.OwnedHeroes.Select(h => new HeroResponceData(heroesRepo, h))) : Results.NotFound();
                else
                    return Results.StatusCode(403);
            });

            app.MapGet("api/userData/heroes", async (HttpContext ctx, IHeroRepository heroesRepo) =>
            {
                var heroes = await heroesRepo.All();
                return Results.Ok(heroes.Select(h => new HeroResponceData(h)));
            });

            app.MapGet("api/userData/items/{userLogin}", async (HttpContext ctx, [FromRoute] string userLogin, IUserRepository usersRepo, IShopItemRepository itemsRepo) =>
            {
                User? user = await usersRepo.GetByLogin(userLogin);
                if (AuthenticateUserRequest(ctx.User, userLogin))
                    return Results.Ok(user.OwnedItems.Select(item => new ItemResponceData(itemsRepo, item)));
                else
                    return Results.StatusCode(403);
            });

            app.MapGet("api/userData/items/", async (HttpContext ctx, IUserRepository usersRepo, IShopItemRepository itemRepo) =>
            {
                var items = await itemRepo.All();
                return Results.Ok(items);
            });

            app.MapPost("api/registrate/", async (HttpContext ctx, [FromBody] UserRequestData newUserRequestData, IUserRepository usersRepo) =>
            {
                User? newUser = new User(newUserRequestData.Login, newUserRequestData.Password, newUserRequestData.Email);
                newUser = await usersRepo.Add(newUser);
                return newUser != null ? Results.Json(new { login = newUser.Login, email = newUser.Email }, statusCode: 201) : Results.Json(new { error = "Such user already exists" }, statusCode: 409);
            });

            app.MapPost("api/logout/", async (HttpContext ctx ) =>
            {
                await ctx.SignOutAsync();
                return Results.Ok();
            });

            app.MapPost("api/heroesShop/randomContract", async (HttpContext ctx, [FromBody] UserRequestData userRequestData, IUserRepository usersRepo, IHeroRepository heroRepo, IHeroInstanceRepository heroInstanceRepo) =>
            {
                string login = userRequestData.Login;
                User? user = await usersRepo.GetByLogin(login);
                if (user == null) return Results.Json(new { error = "Can't find user " + login }, statusCode: 400);
                if (!AuthenticateUserRequest(ctx.User, login)) return Results.StatusCode(403);


                List<Hero> Heroes = (List<Hero>)await heroRepo.All();
                List<int> OwnedHeroes = user.OwnedHeroes.Select(h => h.HeroId).ToList();

                var notOwnedHeroes = Heroes.Where(h => !OwnedHeroes.Contains(h.Id));
                if (!notOwnedHeroes.Any())
                    return Results.Json(new { error = "You already own all available heroes" }, statusCode: 409);

                var rand = new Random();
                Hero hero = notOwnedHeroes.ElementAt(rand.Next(notOwnedHeroes.Count()));
                HeroInstance instance = new HeroInstance(hero, user);
                await heroInstanceRepo.Add(instance);
                return Results.Json(new { hero.Id, hero.Name, heroClass = hero.Class.ToString() }, statusCode: 200);
            });

            app.MapPost("api/shop/buyItem/{itemId}", async (HttpContext ctx, [FromRoute] int itemId, [FromBody] UserRequestData userRequestData, IUserRepository usersRepo, IShopItemRepository shopItemsRepo, IInventoryRepository inventoryRepo) =>
            {
                string login = userRequestData.Login;
                User? user = await usersRepo.GetByLogin(login);
                if (user == null) return Results.Json(new { error = "Can't find user " + login }, statusCode: 400);
                if (!AuthenticateUserRequest(ctx.User, login)) return Results.StatusCode(403);

                Item item = await shopItemsRepo.GetAsync(itemId);
                if (item == null) return Results.Json(new { error = "Can't find item " + itemId}, statusCode: 400);

                if (await usersRepo.WithdrawMoney(user, item.Price))
                {
                    Inventory ownedItem = new Inventory();
                    ownedItem.Owner = user;
                    ownedItem.Item = item;

                    await inventoryRepo.Add(ownedItem);
                    await usersRepo.Update(user);
                }
                else
                    return Results.Json(new { error = "insufficient funds" }, statusCode: 409);

                return Results.Json(new { moneyLeft = user.Money, item.Name, item.Id }, statusCode: 200);
            });

            app.MapPost("api/shop/buyAP", async (HttpContext ctx, [FromBody] UserRequestData userRequestData, IUserRepository usersRepo) =>
            {
                string login = userRequestData.Login;
                User? user = await usersRepo.GetByLogin(login);
                if (user == null) return Results.Json(new { error = "Can't find user " + login }, statusCode: 400);
                if (!AuthenticateUserRequest(ctx.User, login)) return Results.StatusCode(403);

                if (await usersRepo.WithdrawMoney(user, 250))
                {
                    user.ActionPoints += 5;
                    await usersRepo.Update(user);
                }
                else
                    return Results.Json(new { error = " " }, statusCode: 409);

                return Results.Json(new { moneyLeft = user.Money, AP=user.ActionPoints }, statusCode: 200);
            });


            app.MapGet("api/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
                string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));


        }
    }
}
