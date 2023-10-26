using app.Models;

namespace app.Repositories
{
    public interface IShopItemRepository
    {
        IEnumerable<Item> All { get; }
        Item Get(int id);
    }
}
