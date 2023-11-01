using app.Models;

namespace app.ViewModels
{
    public class HeroInstanceFullInfo
    {
        public HeroClass Class { get; private set; }
        public string Name { get; private set; }
        public int MaxHP { get; private set; }
        public int MaxMP { get; private set; }
        public int CurrentHP { get; private set; }
        public int CurrentMP { get; private set; }
        public int Level { get; private set; }

        public HeroInstanceFullInfo(Hero hero, HeroInstance instance)
        {
            Class = hero.Class;
            Name = hero.Name;
            MaxHP = hero.MaxHP;
            MaxMP = hero.MaxMP;
            CurrentHP = instance.CurrentHP;
            CurrentMP = instance.CurrentMP;
            Level = instance.Level;
        }

        public float GetHPPercent() { return CurrentHP / (float)MaxHP * 100f; }
        public float GetMPPercent() { return CurrentMP / (float)MaxMP * 100f; }
    }
}
