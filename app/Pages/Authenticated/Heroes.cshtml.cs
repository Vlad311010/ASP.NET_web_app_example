using app.Models;
using app.Repositories;
using app.Utils;
using app.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages
{
    public class HeroesModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IHeroRepository _heroRepo;
        private readonly IHeroInstanceRepository _heroInstanceRepo;

        public HeroesModel(IUserRepository userRepo, IHeroRepository heroRepo, IHeroInstanceRepository heroInstanceRepo)
        {
            _userRepo = userRepo;
            _heroRepo = heroRepo;
            _heroInstanceRepo = heroInstanceRepo;
        }

        public List<HeroInstanceFullInfo> OwnedHeroes { get; set; }

        public async Task OnGetAsync()
        {
            string userLogin = HttpContext.User.GetLogin();
            User? user = await _userRepo.GetByLogin(userLogin);
            if (user == null) RedirectToPage("/Login");

            OwnedHeroes = (await _heroInstanceRepo.All()).ToList()
                .Where(h => h.OwnerId == user.Id)
                .Select(h => new HeroInstanceFullInfo(GetHeroFromInstance(h), h)).ToList();
        }

        private Hero GetHeroFromInstance(HeroInstance instance)
        {
            return _heroRepo.Get(instance.Hero.Id);
        }
    }
}
