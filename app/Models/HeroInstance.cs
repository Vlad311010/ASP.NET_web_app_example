namespace app.Models
{
    public class HeroInstance
    {
        public int OwnerId { get; set; }
        public int HeroId { get; set; }
        
        public int Level { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
    }
}
