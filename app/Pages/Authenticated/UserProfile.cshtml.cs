using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly IUserRepository _usersRepo;

        public UserProfileModel(IUserRepository context)
        {
            _usersRepo = context;
        }

        [BindProperty(SupportsGet = true)]
        public User CurrentUser { get; set; }

        public void OnGet()
        {
            CurrentUser = _usersRepo.All.Where(m => m.Login == User.Identity?.Name).Single();
        }
    }
}
