namespace WebDevProj.Api.Models
{
    public class Valera
    {
        //primary key
        public int Id { get; set; }
        //Properties
        public const int MaxHealth = 100;
        public const int MinHealth = 0;
        public const int MaxAlcohol = 100;
        public const int MinAlcohol = 0;
        public const int MaxCheer = 10;
        public const int MinCheer = -10;
        public const int MaxFatigue = 100;
        public const int MinFatigue = 0;

        private int _health = MaxHealth;

        public int Health
        {
            get => _health;
            set => _health = Math.Clamp(value, MinHealth, MaxHealth);
        }

        private int _alcohol = 0;
        public int Alcohol
        {
            get => _alcohol;
            set => _alcohol = Math.Clamp(value, MinAlcohol, MaxAlcohol);
        }

        private int _cheer = 5;

        public int Cheer
        {
            get => _cheer;
            set => _cheer = Math.Clamp(value, MinCheer, MaxCheer);
        }

        private int _fatigue = 0;
        public int Fatigue
        {
            get => _fatigue;
            set => _fatigue = Math.Clamp(value, MinFatigue, MaxFatigue);
        }

        private int _money = 100;

        public int Money
        {
            get => _money;
            set => _money = Math.Max(value, 0);
        }

        //Public methods
        public void GoToWork()
        {
            if(Alcohol >= 50 || Fatigue >= 10 )
            {
                throw new InvalidOperationException("Too drunk or tired");
            }
            Cheer -= 5;
            Alcohol -= 30;
            Money += 100;
            Fatigue += 70;
        }
        public void EnjoyNature()
        {
            Cheer += 1;
            Alcohol -= 10;
            Fatigue += 10;
        }
        public void DrinkVineWatchTv()
        {
            Cheer -= 1;
            Alcohol += 30;
            Fatigue += 10;
            Money -= 20;
            Health -= 5;
        }
        public void GoToBar()
        {
            Cheer += 1;
            Alcohol += 60;
            Fatigue += 40;
            Health -= 10;
            Money -= 100;
        }
        public void DrinkWithMarginals()
        {
            Cheer += 5;
            Health -= 80;
            Alcohol += 90;
            Fatigue += 80;
            Money -= 150;
        }
        public void SingInSubway()
        {
            if(Alcohol > 40 && Alcohol < 70)
            {
                Money += 50;
            }
            Cheer += 1;
            Alcohol += 10;
            Money += 10;
            Fatigue += 20;
        }
        public void Sleep()
        {
            if(Alcohol < 30)
            {
                Health += 90;
            }
            if(Alcohol > 70)
            {
                Cheer -= 3;
            }
            Alcohol -= 50;
            Fatigue -= 70;
        }
    }
}
