using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class HeroInstanceRepository : IHeroInstanceRepository
    {
        private readonly AppDbContext _context;

        public HeroInstanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HeroInstance> All => _context.HeroInstances.Include(h => h.Hero).ToList();

        public HeroInstance Get(int userId, int heroId)
        {
            return _context.HeroInstances.Where(m => m.OwnerId == userId && m.Hero.Id == userId).SingleOrDefault();
        }
    }
}
