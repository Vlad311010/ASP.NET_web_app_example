using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.Repositories;

namespace app.Pages.Authenticated.HeroesShop
{
    public class EditHeroModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditHeroModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Hero Hero { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? heroId)
        {
            if (heroId == null || _context.Heroes == null)
            {
                return NotFound();
            }

            var hero =  await _context.Heroes.FirstOrDefaultAsync(m => m.Id == heroId);
            if (hero == null)
            {
                return NotFound();
            }
            Hero = hero;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Hero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(Hero.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./HeroesShop");
        }

        private bool HeroExists(int id)
        {
          return (_context.Heroes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
