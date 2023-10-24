using app.Models;

namespace app.Repositories
{
    public class LocalShopItemRepository : IShopItemRepository
    {
        List<ShopItem> _items = new List<ShopItem>()
        {

        };

        public IEnumerable<ShopItem> All => _items;

        public ShopItem Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
