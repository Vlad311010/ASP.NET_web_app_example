using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace app.Models
{
    [PrimaryKey("OwnerId", "HeroId")]
    public class HeroInstance
    {
        public int HeroId { get; set; }
        public int OwnerId { get; set; }
        
        [JsonIgnore]
        public virtual Hero Hero { get; set; } = null!;
        
        [JsonIgnore]
        public virtual User Owner { get; set; } = null!;

        public int Level { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
    }
}
