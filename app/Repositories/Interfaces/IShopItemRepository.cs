using app.Models;

namespace app.Repositories
{
    public interface IShopItemRepository
    {
        Task<IEnumerable<Item>> All();
        Item Get(int id);
        Task<Item> GetAsync(int id);
    }
}
