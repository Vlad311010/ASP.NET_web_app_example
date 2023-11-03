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
        private readonly IInventoryRepository _invertoryRepo;
        public ShopModel(IUserRepository userRepo, IShopItemRepository shopItemRepo, IInventoryRepository invertoryRepo)
        {
            _userRepo = userRepo;
            _shopItemRepo = shopItemRepo;
            _invertoryRepo = invertoryRepo;
        }

        public User? User { get; set; }
        
        public List<Item> Items { get; set; }
        
        public string Message { get ; set; }

        public async Task OnGetAsync()
        {
            Items = (List<Item>)await _shopItemRepo.All();
        }

        public async Task OnPostAPAsync()
        {
            Items = (List<Item>)await _shopItemRepo.All();

            string login = HttpContext.User.GetLogin();
            User = await _userRepo.GetByLogin(login);
            if (await _userRepo.WithdrawMoney(User, 250))
            {
                User.ActionPoints += 5;
                await _userRepo.Update(User);
            }
            else
                Message = "Not enought money";
        }

        public async Task OnPostBuyItemAsync(int itemId)
        {
            Items = (List<Item>)await _shopItemRepo.All();

            string login = HttpContext.User.GetLogin();
            User = await _userRepo.GetByLogin(login);

            Item item = await _shopItemRepo.Get(itemId);
            if (await _userRepo.WithdrawMoney(User, item.Price))
            {
                Inventory ownedItem = new Inventory();
                ownedItem.Owner = User;
                ownedItem.Item = item;

                await _invertoryRepo.Add(ownedItem);
                await _userRepo.Update(User);
            }
            else
                Message = "Not enought money";
        }
    }
}
