using app.Models;

namespace app.ViewModels
{
    public class OwnedItemView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public int Amount { get; set; }

        public OwnedItemView(Item item, int amount) 
        {
            Name = item.Name;
            Description = item.Description;
            Image = item.Image;
            Amount = amount;
        }
    }
}
