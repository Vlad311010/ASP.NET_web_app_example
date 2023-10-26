using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    [PrimaryKey("OwnerId", "HeroId")]
    public class HeroInstance
    {
        public int OwnerId { get; set; }
        public int HeroId { get; set; }
        
        public int Level { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
    }
}
