using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly AppDbContext _context;
        public HeroRepository(AppDbContext context)
        {
            this._context = context;
        }
        
        public async Task<IEnumerable<Hero>> All()
        {
            return await _context.Heroes.ToListAsync();
        }

        public Hero? Get(int id)
        {
            return _context.Heroes.SingleOrDefault(h => h.Id == id);
        }

        public async Task<Hero?> GetAsync(int id)
        {
            return await _context.Heroes.SingleOrDefaultAsync(h => h.Id == id);
        }
    }
}
