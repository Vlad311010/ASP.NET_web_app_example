using app.Models;

namespace app.Repositories
{
    public interface IHeroRepository
    {
        IEnumerable<Hero> All { get; }
        Hero Get(int id);
    }
}
