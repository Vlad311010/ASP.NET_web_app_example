using app.Models;

namespace app.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;
        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Inventory> All => _context.Inventories;

        public List<Item> Get(int userId)
        {
            return _context.Inventories.Where(i => i.OwnerId == userId)
                .Select(i => _context.Items.SingleOrDefault(item => item.Id == i.ItemId)).ToList();
        }
    }
}
