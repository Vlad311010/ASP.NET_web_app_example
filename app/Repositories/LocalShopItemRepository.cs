using app.Models;

namespace app.Repositories
{
    public class LocalShopItemRepository : IShopItemRepository
    {
        List<ShopItem> _items = new List<ShopItem>()
        {
            new ShopItem { Id = 1, Name = "AP", Description = "Recieve 5 AP" , Price = 250, Image="images/icon.png", Handler = "?",  Page = "?" }
        };

        public IEnumerable<ShopItem> All => _items;

        public ShopItem Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
