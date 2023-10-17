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
        internal static void MapApi(WebApplication app)
        {
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


            app.MapGet("api/users", (IUserRepository db) =>
            {
                var users = db.All.ToList();
                return Results.Ok(users);
            }).RequireAuthorization("AdminOnly");

            app.MapGet("api/users/{userId}", (int userId, IUserRepository db) =>
            {
                User? user = db.GetById(userId);
                return Results.Ok(user);
            }).RequireAuthorization("AdminOnly");


            app.MapGet("api/users/create", ([FromBody]User user, IUserRepository db) =>
            {
                User? newUser = db.Add(user);
                if (newUser != null)
                    return Results.Ok(newUser);
                else 
                    return Results.BadRequest();
            }).RequireAuthorization("AdminOnly");


        }
    }
}
