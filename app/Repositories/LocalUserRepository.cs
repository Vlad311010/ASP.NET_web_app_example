using app.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace app.Repositories
{
    public class LocalUserRepository : IUserRepository
    {
        private const string usersJsonPath = "./Data/UsersJson.json";
        private JsonSerializerOptions serializationOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);


        private List<User> _users = new List<User>
        {
            // new User("Admin", "Admin", "admin@mail.com", UserType.Admin),
            // new User("Azter", "11111", "azter@mail.com"),
            // new User("Ruf", "11111", "ruf@mail.com")
        };

        public IEnumerable<User> All 
        { 
            get 
            { 
                LoadData(); 
                return _users; 
            } 
        }

        public User? Authenticate(string login, string password)
        {
            User? user = _users.SingleOrDefault(x => x.Login == login && x.Password == password);
            return user;
        }

        public User? GetByLogin(string login)
        {
            return _users.FirstOrDefault(x => x.Login == login);
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        private void LoadData()
        {
            string jsonText = File.ReadAllText(usersJsonPath);
            _users = JsonSerializer.Deserialize<List<User>>(jsonText, serializationOptions);
        }

        private void SaveData()
        {
            string usersJson = JsonSerializer.Serialize(_users, serializationOptions);
            File.WriteAllText(usersJsonPath, usersJson);
        }

        private int GetNewUserId()
        {
            return _users.Max(x => x.Id);
        }

        public User? Add(User user)
        {
            LoadData();

            if (_users.Exists(u => u.Login == user.Login))
            {
                return null;
            }
            user.Id = GetNewUserId();
            user.Money = 1000;
            user.ActionPoints = 10;
            user.Score = 0;

            _users.Add(user);
            SaveData();
            return user;
        }
    }
}
