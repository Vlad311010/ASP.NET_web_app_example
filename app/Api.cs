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
                return user != null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapGet("api/users/{login}", (string login, IUserRepository db) =>
            {
                User? user = db.GetByLogin(login);
                return user != null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");


            app.MapGet("api/users/create", ([FromBody]User user, IUserRepository db) =>
            {
                User? newUser = db.Add(user);
                return newUser != null ? Results.Ok(newUser) : Results.BadRequest();
            }).RequireAuthorization("AdminOnly");

            app.MapGet("api/users/update", ([FromBody] User user, IUserRepository db) =>
            {
                User? updatedUser = db.Update(user);
                return updatedUser != null ? Results.Ok(updatedUser) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");

            app.MapGet("api/users/remove/{login}", ([FromRoute] string login, IUserRepository db) =>
            {
                User? user = db.Remove(login);
                return user != null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization("AdminOnly");
        }
    }
}
