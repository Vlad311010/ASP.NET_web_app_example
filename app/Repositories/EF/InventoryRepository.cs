using app.Models;
using app.ViewModels;
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
            return await _context.Inventories
                .Where(i => i.OwnerId == userId)
                .Select(i => _context.Items.SingleOrDefault(item => item.Id == i.ItemId)).ToListAsync();
        }

        public async Task<List<OwnedItemView>> GetAsOwnedItem(int userId)
        {
            return await _context.Inventories
                .Where(i => i.OwnerId == userId)
                .Select(i => 
                    new OwnedItemView(_context.Items.SingleOrDefault(item => item.Id == i.ItemId), i.Amount))
                .ToListAsync();
        }

        public async Task Add(Inventory item)
        {
            var ownedItem = await _context.Inventories.SingleOrDefaultAsync(i => i.ItemId == item.Item.Id && i.OwnerId == item.Owner.Id);
            if (ownedItem != null)
            {
                ownedItem.Amount += 1;
                await _context.SaveChangesAsync();
            }
            else
            {
                item.Amount = 1;
                await _context.Inventories.AddAsync(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}
