using app.Models;

namespace app.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> All();
        User? Authenticate(string login, string password);
        Task<User?> GetByLogin(string login);
        Task<User?> GetById(int id);
        Task<User?> Add(User user);
        Task<User?> Update(User user);
        Task<bool> WithdrawMoney(User user, int amount);
        Task<User?> Remove(int id);
    }
}
