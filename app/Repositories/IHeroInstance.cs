using app.Models;

namespace app.Repositories
{
    public interface IHeroInstance
    {
        IEnumerable<HeroInstance> All { get; }

        HeroInstance Get(int userId, int heroId);
    }
}
