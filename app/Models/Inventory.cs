using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace app.Models
{
    [PrimaryKey("OwnerId", "ItemId")]
    public class Inventory
    {
        public int ItemId { get; set; }
        public int OwnerId { get; set; }
        
        [JsonIgnore]
        public virtual Item Item { get; set; } = null!;
        
        [JsonIgnore]
        public virtual User Owner { get; set; } = null!;

        public int Amount { get; set; }
    }
}
