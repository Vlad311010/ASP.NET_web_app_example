namespace app.Utils
{
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
