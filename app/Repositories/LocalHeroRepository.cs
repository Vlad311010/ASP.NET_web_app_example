using app.Models;
using System.Security.Claims;
using System.Xml.Linq;

namespace app.Repositories
{
    public class LocalHeroRepository : IHeroRepository
    {
        private List<Hero>  _heroes = new List<Hero>()
        {
            new Hero() { Id = 1, Name = "Artoria", Class = HeroClass.Saber, MaxHP = 100, MaxMP = 100 },
            new Hero() { Id = 2, Name = "Medea", Class = HeroClass.Caster, MaxHP = 30, MaxMP = 300 }
        };

        public IEnumerable<Hero> All => _heroes;

        public Hero Get(int id) { return _heroes.Where(m => m.Id == id).First(); }
    }
}
