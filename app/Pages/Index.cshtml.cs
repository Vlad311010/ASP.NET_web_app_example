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
        public string userName { get; set; }

        public void OnGet()
        {
            IIdentity identitie = User.Identity;
            if (identitie != null)
            {
                userName = identitie.Name ?? "";
            }
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
