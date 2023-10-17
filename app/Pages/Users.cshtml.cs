using app.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using app.Repositories;

namespace app.Pages
{

    public class UsersListModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IHeroRepository _heroRepo;
        private readonly IHeroInstance _heroInstanceRepo;

        public UsersListModel(IUserRepository userRepo, IHeroRepository heroRepo, IHeroInstance contractRepo)
        {
            _userRepo = userRepo;
            _heroRepo = heroRepo;
            _heroInstanceRepo = contractRepo;
        }

        public List<User> Users { get; set; }
        public List<Hero> Heroes { get; set; }
        public List<HeroInstance> Instances { get; set; }

        public void OnGet()
        {
            Users = _userRepo.All.ToList();
            Heroes = _heroRepo.All.ToList();
            Instances = _heroInstanceRepo.All.ToList();
        }

        public Hero GetHero(int id) { return _heroRepo.Get(id); }

    }
}
