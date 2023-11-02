using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;
        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> All()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<List<Item>> Get(int userId)
        {
            return await _context.Inventories.Where(i => i.OwnerId == userId)
                .Select(i => _context.Items.SingleOrDefault(item => item.Id == i.ItemId)).ToListAsync();
        }
    }
}
