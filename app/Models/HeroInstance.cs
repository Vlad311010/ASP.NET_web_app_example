using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    [PrimaryKey("OwnerId", "HeroId")]
    public class HeroInstance
    {
        public int HeroId { get; set; }
        public virtual Hero Hero { get; set; } = null!;
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; } = null!;

        public int Level { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
    }
}
