using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class ShopItemRepository : IShopItemRepository
    {
        private readonly AppDbContext _context;
        public ShopItemRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> All()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> Get(int id)
        {
            return await _context.Items.SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
