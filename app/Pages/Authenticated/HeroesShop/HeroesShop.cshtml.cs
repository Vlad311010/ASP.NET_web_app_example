using app.Models;
using app.Repositories;
using app.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages
{
    public class HeroesShopModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IHeroRepository _heroRepo;

        public HeroesShopModel(IUserRepository userRepo, IHeroRepository heroRepo)
        {
            _userRepo = userRepo;
            _heroRepo = heroRepo;
        }

        public List<int> OwnedHeroes { get; set; }
        public List<Hero> Heroes { get; set; }

        public void OnGet()
        {
            string userLogin = HttpContext.User.GetLogin();
            User? user = _userRepo.GetByLogin(userLogin);
            if (user == null) RedirectToPage("/Login");

            Heroes = _heroRepo.All.ToList();
            OwnedHeroes = user.OwnedHeroes.Select(h => h.HeroId).ToList();
        }

    }
}
