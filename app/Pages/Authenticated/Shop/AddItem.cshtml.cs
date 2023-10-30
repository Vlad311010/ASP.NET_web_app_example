using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using app.Models;
using app.Repositories;

namespace app.Pages.Authenticated.Shop
{
    public class AddItemModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddItemModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Items == null || Item == null)
            {
                return Page();
            }

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Authenticated/Shop/Shop");
        }
    }
}
