using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class Item
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        [Required]
        [Range(0,99999)]
        public int Price { get; set; }

        public virtual ICollection<Item> OwnedItems { get; } = new List<Item>();
    }
}
