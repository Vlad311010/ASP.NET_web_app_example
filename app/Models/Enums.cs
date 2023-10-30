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

    [AttributeUsage(AttributeTargets.Field)]
    public class ColorAttribute : Attribute
    {
        public string HexColor { get; private set; }
        public ColorAttribute(string hexColor)
        {
            HexColor = hexColor;
        }
    }

    public static class AttributeGetter
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
    }
}
