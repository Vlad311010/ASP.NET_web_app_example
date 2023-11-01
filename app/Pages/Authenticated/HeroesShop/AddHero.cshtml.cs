using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using app.Models;
using app.Repositories;

namespace app.Pages.Authenticated.HeroesShop
{
    public class AddHeroModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddHeroModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Hero Hero { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Heroes == null || Hero == null)
            {
                return Page();
            }

            _context.Heroes.Add(Hero);
            await _context.SaveChangesAsync();

            return RedirectToPage("./HeroesShop");
        }
    }
}
