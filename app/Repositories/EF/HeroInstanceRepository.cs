using app.Models;

namespace app.Repositories
{
    public class HeroInstanceRepository : IHeroInstanceRepository
    {
        private readonly AppDbContext _context;

        public HeroInstanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HeroInstance> All => _context.HeroInstances;

        public HeroInstance Get(int userId, int heroId)
        {
            return _context.HeroInstances.Where(m => m.OwnerId == userId && m.Hero.Id == userId).SingleOrDefault();
        }
    }
}
