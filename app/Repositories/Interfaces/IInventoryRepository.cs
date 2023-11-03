using app.Models;
using app.ViewModels;

namespace app.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> All();
        Task<List<Item>> Get(int userId);
        Task<List<OwnedItemView>> GetAsOwnedItem(int userId);
        Task Add(Inventory item);
    }
}
