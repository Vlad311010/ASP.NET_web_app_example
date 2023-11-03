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

        public async Task<IEnumerable<User>> All()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> Add(User user)
        {
            if (!ValidateUser(user, true))
                return null;

            user.Money = 1000;
            user.ActionPoints = 10;
            user.Score = 0;

            User newUser = _context.Users.Add(user).Entity;
            await _context.SaveChangesAsync();
            return newUser;
        }

        public User? Authenticate(string login, string password)
        {
            User? user = _context.Users.SingleOrDefault(x => x.Login == login);
            if (user == null) return null;

            bool isPasswordCorrect = PasswordHashing.VerifyPassword(password, user.PasswordHash, user.Salt);
            return isPasswordCorrect ? user : null;
            
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.Where(u => u.Id == id).Include(u => u.OwnedHeroes).SingleOrDefaultAsync();
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users.Where(u => u.Login == login).Include(u => u.OwnedHeroes).Include(u => u.OwnedItems).SingleOrDefaultAsync();
        }

        public async Task<User?> Remove(int id)
        {
            User? userToRemove = await GetById(id);
            _context.Users.Remove(userToRemove);

            await _context.SaveChangesAsync();
            return userToRemove;
        }

        public async Task<User?> Update(User user)
        {
            if (!ValidateUser(user, false)) return null;

            User originalEntity = _context.Entry(user).Entity;
            user.Id = originalEntity.Id;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Users.Entry(user).Entity;
        }

        public async Task<bool> WithdrawMoney(User user, int amount)
        {
            if (user.Money < amount) 
                return false;
            else
            {
                user.Money -= amount;
                await _context.SaveChangesAsync();
                return true;
            }

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
