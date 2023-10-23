using app.Repositories;
using app.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace app.ViewComponents
{
    public class NavBarViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepo;
        public NavBarViewComponent(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string login = HttpContext.User.GetLogin();
            var user = _userRepo.GetByLogin(login);
            return View(user);
        }
    }
}
