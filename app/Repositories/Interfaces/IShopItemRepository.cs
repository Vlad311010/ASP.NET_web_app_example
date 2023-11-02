using app.Models;

namespace app.Repositories
{
    public interface IShopItemRepository
    {
        Task<IEnumerable<Item>> All();
        Task<Item> Get(int id);
    }
}
