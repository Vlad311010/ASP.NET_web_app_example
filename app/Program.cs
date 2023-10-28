using app;
using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

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
    /*options.Conventions.AuthorizeFolder("/")
    .AllowAnonymousToPage("/Login")
    .AllowAnonymousToPage("/Index")
    .AllowAnonymousToPage("/Register");
    // Admin Only
    options.Conventions.AuthorizePage("/Users", "AdminOnly");*/
    options.Conventions.AllowAnonymousToFolder("/Public");
    options.Conventions.AuthorizeFolder("/Authenticated");
    
    // Admin Only
    options.Conventions.AuthorizePage("/Authenticated/Users", "AdminOnly");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(new string[] { "Admin" } )
    );
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<IHeroInstanceRepository, HeroInstanceRepository>();
builder.Services.AddScoped<IShopItemRepository, ShopItemRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
);


var app = builder.Build();

Api.MapApi(app);

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
