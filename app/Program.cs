using app;
using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Net;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    cookieOptions.LoginPath = "/Login";
    cookieOptions.AccessDeniedPath = "/Forbidden";

    cookieOptions.Events.OnRedirectToAccessDenied = 
        cookieOptions.Events.OnRedirectToLogin = ctx =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else
            {
                ctx.Response.Redirect(cookieOptions.LoginPath);
            }
            return Task.FromResult(0);
        };
});


builder.Services.AddRazorPages(options =>
{
    // Autorization access
    options.Conventions.AuthorizeFolder("/")
    .AllowAnonymousToPage("/Login")
    .AllowAnonymousToPage("/Index")
    .AllowAnonymousToPage("/Register");
    // Admin Only
    options.Conventions.AuthorizePage("/Users", "AdminOnly");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(new string[] { "Admin" } )
    );
});


builder.Services.AddSingleton<IUserRepository, LocalUserRepository>();
builder.Services.AddSingleton<IHeroRepository, LocalHeroRepository>();
builder.Services.AddSingleton<IHeroInstanceRepository, LocalHeroInstanceRepository>();

var app = builder.Build();

Api.MapApi(app);

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
