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
        private readonly IHeroInstanceRepository _heroInstanceRepo;

        public HeroesShopModel(IUserRepository userRepo, IHeroRepository heroRepo, IHeroInstanceRepository heroInstanceRepo)
        {
            _userRepo = userRepo;
            _heroRepo = heroRepo;
            _heroInstanceRepo = heroInstanceRepo;
        }

        public List<int> OwnedHeroes { get; set; }
        public List<Hero> Heroes { get; set; }
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            string userLogin = HttpContext.User.GetLogin();
            User? user = await _userRepo.GetByLogin(userLogin);
            if (user == null) RedirectToPage("/Login");

            Heroes = (List<Hero>)await _heroRepo.All();
            OwnedHeroes = user.OwnedHeroes.Select(h => h.HeroId).ToList();
        }

        public async Task<IActionResult> OnPostGetRandomHeroAsync()
        {
            string userLogin = HttpContext.User.GetLogin();
            User? user = await _userRepo.GetByLogin(userLogin);
            if (user == null) RedirectToPage("/Login");

            Heroes = (List<Hero>)await _heroRepo.All();
            OwnedHeroes = user.OwnedHeroes.Select(h => h.HeroId).ToList();

            var notOwnedHeroes = Heroes.Where(h => !OwnedHeroes.Contains(h.Id));
            if (!notOwnedHeroes.Any())
            {
                Message = "You already own all available heroes";
                return Page();
            }
            
            var rand = new Random();
            Hero hero = notOwnedHeroes.ElementAt(rand.Next(notOwnedHeroes.Count()));
            HeroInstance instance = new HeroInstance(hero, user);
            await _heroInstanceRepo.Add(instance);
            return RedirectToPage("./HeroesShop");
        }
    }
}
