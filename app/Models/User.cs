using app.Utils;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class User
    {
        // user data
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your login"), MaxLength(50)]
        public string Login { get; set; }
        public string PasswordHash { get; private set; }
        
        [MaxLength(64)]
        public byte[] Salt { get; private set; }

        [Required(ErrorMessage = "Please enter your email"), MaxLength(255)]
        public string Email { get; set; }
        public UserType Type { get; set; }
        
        // f keys
        public virtual ICollection<HeroInstance> OwnedHeroes { get; } = new List<HeroInstance>();
        public virtual ICollection<Inventory> OwnedItems { get; } = new List<Inventory>();

        // player data
        public int Money { get; set; }
        public int Score { get; set; }
        public int ActionPoints { get; set; }

        public User() { }
        public User(string Login, string Password, string Email, UserType type=UserType.User) 
        {
            string passwordHash;
            byte[] salt;
            if (string.IsNullOrEmpty(Password))
            {
                passwordHash = "";
                salt = new byte[0];
            }
            else
                passwordHash = PasswordHashing.HashPasword(Password, out salt);

            this.Login = Login;
            this.PasswordHash = passwordHash;
            this.Salt = salt;
            this.Email = Email;
            this.Type = type;
        }


    }
}
