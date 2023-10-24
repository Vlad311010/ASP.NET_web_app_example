using app.Models;
using app.Repositories;
using app.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages
{
    public class ShopModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        public ShopModel(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User? User { get; set; }    

        public void OnGet()
        {
            string login = HttpContext.User.GetLogin();
            User = _userRepo.GetByLogin(login);
        }

        public void OnGetAddAP()
        {

        }
    }
}
