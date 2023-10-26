using app.Models;

namespace app.Repositories
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> All { get; }
        List<Item> Get(int userId);
    }
}
