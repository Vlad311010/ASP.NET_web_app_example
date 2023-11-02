using app.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace app.Repositories
{
    public class HeroInstanceRepository : IHeroInstanceRepository
    {
        private readonly AppDbContext _context;

        public HeroInstanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeroInstance>> All()
        {
            return await _context.HeroInstances.Include(h => h.Hero).ToListAsync();
        }   


        public async Task<HeroInstance> Get(int userId, int heroId)
        {
            return await _context.HeroInstances.Where(m => m.OwnerId == userId && m.Hero.Id == userId).SingleOrDefaultAsync();
        }
        public async Task<HeroInstance> Add(HeroInstance entity)
        {
            await _context.HeroInstances.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
