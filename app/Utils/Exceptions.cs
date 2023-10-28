namespace app.Utils
{
    [System.Serializable]
    public class NotEnoughtMoney : Exception
    {
        public NotEnoughtMoney() { }
        public NotEnoughtMoney(string message) : base(message) { }
        public NotEnoughtMoney(string message, Exception inner) : base(message, inner) { }
        protected NotEnoughtMoney(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
