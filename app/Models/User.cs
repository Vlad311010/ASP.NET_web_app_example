using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class User
    {
        // user data
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your login"), MaxLength(50)]
        public string Login { get; set; }
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please enter your email"), MaxLength(255)]
        public string Email { get; set; }
        public UserType Type { get; set; }
        
        // f keys
        public virtual ICollection<HeroInstance> OwnedHeroes { get; } = new List<HeroInstance>();

        // player data
        public int Money { get; set; }
        public int Score { get; set; }
        public int ActionPoints { get; set; }

        public User(string Login, string Password, string Email, UserType type=UserType.User) 
        {
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
            this.Type = type;
        }


    }
}
