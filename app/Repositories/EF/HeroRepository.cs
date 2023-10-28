using app.Models;

namespace app.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly AppDbContext _context;
        public HeroRepository(AppDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Hero> All => _context.Heroes;

        public Hero? Get(int id)
        {
            return _context.Heroes.SingleOrDefault(h => h.Id == id);
        }
    }
}
