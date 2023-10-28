using app.Models;

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
            return _context.Users.Add(user).Entity;
        }

        public User? Authenticate(string login, string password)
        {
            User? user = _context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);
            return user;
            
        }

        public User? GetById(int id)
        {
            return _context.Users.Where(u => u.Id == id).SingleOrDefault();
        }

        public User? GetByLogin(string login)
        {
            return _context.Users.Where(u => u.Login == login).SingleOrDefault();
        }

        public User? Remove(int id)
        {
            User? userToRemove = GetById(id);
            return _context.Users.Remove(userToRemove).Entity;
        }

        public User? Update(User user)
        {
            throw new NotImplementedException();
        }

        private bool ValidateUser(User user, bool checkForUniqueLogin = false)
        {
            return
                (!checkForUniqueLogin || !_context.Users.Any(u => u.Login == user.Login))
                &&
                (!String.IsNullOrEmpty(user.Login) && !String.IsNullOrEmpty(user.Password) && !String.IsNullOrEmpty(user.Email));
        }
    }
}
