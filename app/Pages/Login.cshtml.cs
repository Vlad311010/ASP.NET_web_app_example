using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace app.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _usersRepo;

        [BindProperty] public string Login { get; set; }
        [BindProperty] public string Password { get; set; }
        public string Message { get; set; }

        public LoginModel(IUserRepository context)
        {
            _usersRepo = context;
        }

        public async Task<IActionResult> OnPost() 
        {
            User? user = _usersRepo.Authenticate(Login, Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                Message = "Invalid login or password";
                return Page();
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

            Message = "OK";
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // success
            return LocalRedirect(Url.Page("/Index"));
        }
    }
}
