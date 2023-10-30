using app.Models;
using app.Utils;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> All => _context.Users;

        public User? Add(User user)
        {
            if (!ValidateUser(user, true))
                return null;

            user.Money = 1000;
            user.ActionPoints = 10;
            user.Score = 0;

            User newUser = _context.Users.Add(user).Entity;
            _context.SaveChanges();
            return newUser;
        }

        public User? Authenticate(string login, string password)
        {
            User? user = _context.Users.SingleOrDefault(x => x.Login == login);
            if (user == null) return null;

            bool isPasswordCorrect = PasswordHashing.VerifyPassword(password, user.PasswordHash, user.Salt);
            return isPasswordCorrect ? user : null;
            
        }

        public User? GetById(int id)
        {
            return _context.Users.Where(u => u.Id == id).Include(u => u.OwnedHeroes).SingleOrDefault();
        }

        public User? GetByLogin(string login)
        {
            return _context.Users.Where(u => u.Login == login).SingleOrDefault();
        }

        public User? Remove(int id)
        {
            User? userToRemove = GetById(id);
            _context.Users.Remove(userToRemove);
            
            _context.SaveChanges();
            return userToRemove;
        }

        public User? Update(User user)
        {
            if (!ValidateUser(user, false)) return null;

            User originalEntity = _context.Entry(user).Entity;
            user.Id = originalEntity.Id;

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return _context.Users.Entry(user).Entity;
        }

        private bool ValidateUser(User user, bool checkForUniqueLogin = false)
        {
            return
                (!checkForUniqueLogin || !_context.Users.Any(u => u.Login == user.Login))
                &&
                (!String.IsNullOrEmpty(user.Login) && !String.IsNullOrEmpty(user.PasswordHash) && !String.IsNullOrEmpty(user.Email));
        }
    }
}
