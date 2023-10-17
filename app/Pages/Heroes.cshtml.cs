using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages
{
    public class HeroesModel : PageModel
    {
        private readonly IHeroRepository _heroRepo;

        public HeroesModel(IHeroRepository heroRepo)
        {
            _heroRepo = heroRepo;
        }

        public List<Hero> Heroes { get; set; }

        public void OnGet()
        {
            Heroes = _heroRepo.All.ToList();
        }
    }
}
