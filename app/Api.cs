using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace app
{
    

    public static class Api
    {
        private class LoginRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
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
                return Results.Ok();
            });

            app.MapGet("api/users", (IUserRepository db) =>
            {
                var users = db.All.ToList();
                return Results.Ok(users);
            }).RequireAuthorization("AdminOnly");

            app.MapGet("api/users/{userId}", ([FromRoute] int userId, IUserRepository db) =>
            {
                User? user = db.GetById(userId);
                return user != null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapPost("api/users/create", ([FromBody] User user, IUserRepository db) =>
            {
                User? newUser = db.Add(user);
                return newUser != null ? Results.Ok(newUser) : Results.BadRequest();
            }).RequireAuthorization("AdminOnly");

            app.MapPost("api/users/update", ([FromBody] User user, IUserRepository db) =>
            {
                User? updatedUser = db.Update(user);
                return updatedUser != null ? Results.Ok(updatedUser) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapDelete("api/users/remove/{userId}", ([FromRoute] int userId, IUserRepository db) =>
            {
                User? user = db.Remove(userId);
                return user != null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapPost("api/users/giveAdminPrivilege/{login}", ([FromRoute] string login, IUserRepository db) =>
            {
                User? user = db.GetByLogin(login);
                if (user == null) 
                    return Results.NotFound();
                
                user.Type = UserType.Admin;
                User? updatedUser = db.Update(user);
                return Results.Ok(updatedUser);
            }).RequireAuthorization("AdminOnly");

            app.MapGet("api/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
                string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
        }
    }
}
