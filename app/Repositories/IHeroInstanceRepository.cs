using app.Models;

namespace app.Repositories
{
    public interface IHeroInstanceRepository
    {
        IEnumerable<HeroInstance> All { get; }

        HeroInstance Get(int userId, int heroId);
    }
}
