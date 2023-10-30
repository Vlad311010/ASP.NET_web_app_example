using app;
using app.Repositories;
using app.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    cookieOptions.LoginPath = "/Login";
    cookieOptions.AccessDeniedPath = "/Forbidden";

    // cookieOptions.Events.OnRedirectToAccessDenied = 
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
    options.Conventions.AllowAnonymousToFolder("/Public");
    options.Conventions.AuthorizeFolder("/Authenticated");
    
    // Admin Only
    options.Conventions.AuthorizePage("/Authenticated/Users", "AdminOnly");
    options.Conventions.AuthorizePage("/Authenticated/Shop/EditItem", "AdminOnly");
    options.Conventions.AuthorizePage("/Authenticated/Shop/AddItem", "AdminOnly");
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

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);


var app = builder.Build();

Api.MapApi(app);

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapDefaultControllerRoute();


if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        var context = service.GetService<AppDbContext>();
        if (!await context.Users.AnyAsync(u => u.Login == "Admin"))
        {
            User Admin = new User("Admin", "Admin", "admin@mail.com", UserType.Admin);
            context.Users.Add(Admin);
            await context.SaveChangesAsync();
        }
    }
}

app.Run();
