using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _usersRepo;

        [BindProperty] public string Login { get; set; }
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public bool IsAdmin { get; set; }
        public string Message { get; private set; }
        public bool WrongLogin { get; private set; }
        public bool WrongPassword { get; private set; }
        public bool WrongEmail { get; private set; }

        public RegisterModel(IUserRepository context)
        {
            _usersRepo = context;
        }

        public async Task<IActionResult> OnPost()
        {
            UserType newUserType = IsAdmin ? UserType.Admin : UserType.User;
            User user = new User(Login, Password, Email, newUserType);
            WrongLogin = String.IsNullOrEmpty(Login);
            WrongPassword = String.IsNullOrEmpty(Password);
            WrongEmail = String.IsNullOrEmpty(Email);
            if (WrongLogin || WrongPassword || WrongEmail) 
            {
                ModelState.AddModelError(string.Empty, "Invalid registration data.");
                Message = String.Format("Invalid registration data", Login);
                return Page();
            }
            if (_usersRepo.GetByLogin(Login) != null)
            {
                ModelState.AddModelError(string.Empty, "User with such login already exists.");
                Message = String.Format("Login '{0}' already claimed", Login);
                return Page();
            }

            _usersRepo.Add(user);
            
            return LocalRedirect(Url.Page("/Login"));
        }
    }
}
