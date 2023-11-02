using app.Models;

namespace app.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> All();
        Task<List<Item>> Get(int userId);
    }
}
