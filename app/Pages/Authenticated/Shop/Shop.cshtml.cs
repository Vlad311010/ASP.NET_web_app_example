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

        public void OnGet()
        {
            Items = _shopItemRepo.All.ToList();
        }

        public void OnPostAP()
        {
            Items = _shopItemRepo.All.ToList();

            string login = HttpContext.User.GetLogin();
            User = _userRepo.GetByLogin(login);
            User.Money = -250;
            User.ActionPoints += 5;
            _userRepo.Update(User);
        }

        public void OnPostBuyItem(int itemId)
        {
            Items = _shopItemRepo.All.ToList();

            string login = HttpContext.User.GetLogin();
            User = _userRepo.GetByLogin(login);

            Item item = _shopItemRepo.Get(itemId);
            User.Money -= item.Price;


        }
    }
}
