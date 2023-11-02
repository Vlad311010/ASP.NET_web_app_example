using app.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using app.Repositories;

namespace app.Pages
{

    public class UsersListModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IHeroRepository _heroRepo;
        private readonly IHeroInstanceRepository _heroInstanceRepo;

        public UsersListModel(IUserRepository userRepo, IHeroRepository heroRepo, IHeroInstanceRepository contractRepo)
        {
            _userRepo = userRepo;
            _heroRepo = heroRepo;
            _heroInstanceRepo = contractRepo;
        }

        public List<User> Users { get; set; }
        public List<Hero> Heroes { get; set; }
        public List<HeroInstance> Instances { get; set; }

        public async Task OnGet()
        {
            Users = (List<User>)await _userRepo.All();
            Heroes = (List<Hero>)await _heroRepo.All();
            Instances = (List<HeroInstance>)await _heroInstanceRepo.All();
        }

        public async Task<Hero> GetHero(int id) 
        { 
            return await _heroRepo.GetAsync(id);
        }

    }
}
