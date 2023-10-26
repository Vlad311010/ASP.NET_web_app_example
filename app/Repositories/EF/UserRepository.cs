using app.Models;

namespace app.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext context;
        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> All => context.Users;

        public User? Add(User user)
        {
            if (!ValidateUser(user, true))
                return null;

            user.Money = 1000;
            user.ActionPoints = 10;
            user.Score = 0;
            return context.Users.Add(user).Entity;
        }

        public User? Authenticate(string login, string password)
        {
            User? user = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);
            return user;
            
        }

        public User? GetById(int id)
        {
            return context.Users.Where(u => u.Id == id).SingleOrDefault();
        }

        public User? GetByLogin(string login)
        {
            return context.Users.Where(u => u.Login == login).SingleOrDefault();
        }

        public User? Remove(int id)
        {
            User? userToRemove = GetById(id);
            return context.Users.Remove(userToRemove).Entity;
        }

        public User? Update(User user)
        {
            throw new NotImplementedException();
        }

        private bool ValidateUser(User user, bool checkForUniqueLogin = false)
        {
            return
                (!checkForUniqueLogin || !context.Users.Any(u => u.Login == user.Login))
                &&
                (!String.IsNullOrEmpty(user.Login) && !String.IsNullOrEmpty(user.Password) && !String.IsNullOrEmpty(user.Email));
        }
    }
}
