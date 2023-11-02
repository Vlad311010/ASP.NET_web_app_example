using app.Models;

namespace app.Repositories
{
    public interface IHeroInstanceRepository
    {
        Task<IEnumerable<HeroInstance>> All();

        Task<HeroInstance> Get(int userId, int heroId);

        Task<HeroInstance> Add(HeroInstance entity);
    }
}
