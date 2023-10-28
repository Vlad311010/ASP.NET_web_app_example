using app.Models;
using app.Repositories;
using app.Utils;
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

        public void OnGet(string userName)
        {
            User? user = _userRepo.GetByLogin(userName);
            if (user == null) RedirectToPage("/Login");

            OwnedHeroes = _heroInstanceRepo.All.Where(h => h.OwnerId == user.Id).Select(h => new HeroInstanceFullInfo(GetHeroFromInstance(h), h)).ToList();
        }

        private Hero GetHeroFromInstance(HeroInstance instance)
        {
            return _heroRepo.Get(instance.Hero.Id);
        }
    }
}
