using app.Models;
using app.Repositories;
using app.Utils;
using app.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages.Authenticated
{
    public class InventoryModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IInventoryRepository _inventoryRepo;
        private readonly IShopItemRepository _shopItemRepo;

        public InventoryModel(IUserRepository userRepo, IInventoryRepository inventoryRepo, IShopItemRepository shopItemRepo)
        {
            _userRepo = userRepo;
            _inventoryRepo = inventoryRepo;
            _shopItemRepo = shopItemRepo;
        }

        [BindProperty] public List<OwnedItemView> OwnedItems { get; set; }

        public async Task<IActionResult> OnGetAsync(string userLogin)
        {
            User? loggedUser = await _userRepo.GetByLogin(HttpContext.User.GetLogin());
            User? user = await _userRepo.GetByLogin(userLogin);
            if (user == null) RedirectToPage("/Login");

            if (loggedUser.Type != UserType.Admin && loggedUser != user)
                return RedirectToPage("/Public/Login");

            // OwnedItems = await _inventoryRepo.Get(user.Id);
            OwnedItems = await _inventoryRepo.GetAsOwnedItem(user.Id);
            return Page();
        }
    }
}
