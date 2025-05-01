using System;

namespace DungeonExplorer.Entities
{
    public class Enemy : Creature
    {
        public IAttackStrategy Strategy { get; }
        public int Damage { get; }


        public Enemy(string name, int health, IAttackStrategy strategy, int damage) : base(name, health)
        { 
            Strategy = strategy;
            Damage = damage;
        }

        public override int Attack(float modifier)
        {
            return (int) Math.Floor(Damage * modifier);
        }

        public override void Heal(int amount)
        {
            int healValue = Math.Max(MaxHealth, amount);
            Console.WriteLine($"{Name} heals {healValue} health!");
            Health += healValue;
        }

        public override void Guard()
        {
            Console.WriteLine($"{Name} is guarding and will take reduced damage");
            base.Guard();
        }
    }
}