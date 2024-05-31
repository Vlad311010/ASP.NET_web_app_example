using app.Models;
using app.Repositories;

namespace app.ApiModels
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class UserRequestData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class ItemResponceData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Amount { get; set; }

        public ItemResponceData(IShopItemRepository itemRepository, Inventory inventoryItem)
        {
            Item item = itemRepository.Get(inventoryItem.ItemId);
            this.Id = inventoryItem.ItemId;
            this.Name = item.Name;
            this.Description = item.Description;
            this.Image = item.Image;
            this.Amount = inventoryItem.Amount;
        }
    }

    public class HeroResponceData
    {
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; }
        public int HeroId { get; set; }
        public int OwnerId { get; set; }
        public int Level { get; set; }
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }


        public HeroResponceData(IHeroRepository db, HeroInstance heroInstance)
        {
            Hero hero = db.Get(heroInstance.HeroId);
            this.Name = hero.Name;
            this.Class = hero.Class.ToString();
            this.HeroId = heroInstance.HeroId;
            this.OwnerId = heroInstance.OwnerId;
            this.Level = heroInstance.Level;
            this.MaxHP = hero.MaxHP;
            this.MaxMP = hero.MaxMP;
            this.CurrentHP = heroInstance.CurrentHP;
            this.CurrentMP = heroInstance.CurrentMP;
        }

        public HeroResponceData(Hero hero)
        {
            this.Name = hero.Name;
            this.Class = hero.Class.ToString();
            this.HeroId = hero.Id;
            this.MaxHP = hero.MaxHP;
            this.MaxMP = hero.MaxMP;
            this.CurrentHP = hero.MaxHP;
            this.CurrentMP = hero.MaxMP;
        }
    }

}
