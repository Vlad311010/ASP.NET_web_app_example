using app.Utils;

namespace app.Models
{
    public enum UserType
    {
        User = 0,
        Admin = 1
    }

    public enum HeroClass
    {
        [Color("#0352fc")]
        Saber = 0,
        [Color("#fc030f")]
        Archer = 1,
        [Color("#6b02a8")]
        Caster = 2,
        [Color("#4f2c01")]
        Berserk = 3
    }
}
