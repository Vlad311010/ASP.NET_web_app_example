using app.Models;

namespace app.Repositories
{
    public class ShopItemRepository : IShopItemRepository
    {
        private readonly AppDbContext _context;
        public ShopItemRepository(AppDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<Item> All => _context.Items;

        public Item Get(int id)
        {
            return _context.Items.SingleOrDefault(i => i.Id == id);
        }
    }
}
