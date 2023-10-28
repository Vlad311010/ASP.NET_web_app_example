using app.Models;

namespace app.Repositories
{
    public class LocalHeroInstanceRepository : IHeroInstanceRepository
    {
        public IEnumerable<HeroInstance> All => new List<HeroInstance>()
        {
            // new HeroInstance() { OwnerId=2, HeroId=1, CurrentHP = 50, CurrentMP = 50, Level = 10 },
            // new HeroInstance() { OwnerId=2, HeroId=2, CurrentHP = 30, CurrentMP = 100, Level = 2},
            // new HeroInstance() { OwnerId=3, HeroId=2, CurrentHP = 10, CurrentMP = 130, Level = 4},
        };

        public HeroInstance Get(int userId, int id)
        {
            return All.Where(m => m.OwnerId == userId && m.Hero.Id == id).SingleOrDefault();
        }
    }
}
