using app.Models;

namespace app.Repositories
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Hero>> All();
        Hero? Get(int id);
        Task<Hero?> GetAsync(int id);
    }
}
