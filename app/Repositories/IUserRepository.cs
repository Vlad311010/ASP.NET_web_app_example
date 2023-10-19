using app.Models;

namespace app.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> All { get; }
        User? Authenticate(string login, string password);
        User? GetByLogin(string login);
        User? GetById(int id);
        User? Add(User user);
        User? Update(User user);
        User? Remove(string login);
    }
}
