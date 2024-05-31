using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using app.ApiModels;
using System.Security.Claims;

namespace app.ApiControllers
{
    [Route("api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public AuthenticationController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            User? user = _userRepo.Authenticate(loginRequest.Login, loginRequest.Password);

            if (user == null)
            {
                Results.Text("Invalid login or password");
                return StatusCode(StatusCodes.Status401Unauthorized);
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


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return Ok(new { user.Login, ap = user.ActionPoints, user.Money });
        }
    }
}
