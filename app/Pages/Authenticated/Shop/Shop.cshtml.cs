using app.Models;
using app.Repositories;
using app.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages
{
    public class ShopModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IShopItemRepository _shopItemRepo;
        public ShopModel(IUserRepository userRepo, IShopItemRepository shopItemRepo)
        {
            _userRepo = userRepo;
            _shopItemRepo = shopItemRepo;
        }

        public User? User { get; set; }
        
        [BindProperty]
        public List<Item> Items { get; set; }

        public async Task OnGet()
        {
            Items = (List<Item>)await _shopItemRepo.All();
        }

        public async Task OnPostAPAsync()
        {
            Items = (List<Item>)await _shopItemRepo.All();

            string login = HttpContext.User.GetLogin();
            User = await _userRepo.GetByLogin(login);
            User.Money -= 250;
            User.ActionPoints += 5;
            await _userRepo.Update(User);
        }

        public async Task OnPostBuyItemAsync(int itemId)
        {
            Items = (List<Item>)await _shopItemRepo.All();

            string login = HttpContext.User.GetLogin();
            User = await _userRepo.GetByLogin(login);

            Item item = await _shopItemRepo.Get(itemId);
            // Inventory ownedItem = new Inventory();
            // User.OwnedItems.Add(item);
            User.Money -= item.Price;

        }
    }
}
