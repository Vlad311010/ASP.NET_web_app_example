using app.Models;

namespace app.Repositories
{
    public interface IShopItemRepository
    {
        IEnumerable<ShopItem> All { get; }
        ShopItem Get(int id);
    }
}
