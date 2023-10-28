using Microsoft.Extensions.Hosting;

namespace app.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HeroClass Class { get; set; }
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public virtual ICollection<HeroInstance> Instances { get; } = new List<HeroInstance>();
    }
}
