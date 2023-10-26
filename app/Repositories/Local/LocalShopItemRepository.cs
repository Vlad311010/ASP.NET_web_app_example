using app.Models;

namespace app.Repositories
{
    public class LocalShopItemRepository : IShopItemRepository
    {
        List<Item> _items = new List<Item>()
        {
            new Item { Id = 1, Name = "AP", Description = "Recieve 5 AP" , Price = 250, Image="images/icon.png", Handler = "?",  Page = "?" }
        };

        public IEnumerable<Item> All => _items;

        public Item Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
