using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    [PrimaryKey("OwnerId", "ItemId")]
    public class Inventory
    {   
        public int OwnerId { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }
    }
}
