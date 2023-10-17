using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Principal;

namespace app.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _context;

        public string userName { get; set; }

        public IndexModel(IUserRepository context)
        {
            _context = context;
        }

        public void OnGet()
        {
            IIdentity identitie = User.Identity;
            if (identitie != null )
            {
                userName = identitie.Name ?? "";
            }
        }


        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
