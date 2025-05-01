using System;
using DungeonExplorer.Entities;

namespace DungeonExplorer.Logic.Strategies
{
    public class Desperate : IAttackStrategy
    {
        public string Name => "Desperate";
        public string Description => "Enemy is initially defensive. The closer to death, the greater the damage!";
        private readonly Random _random = new Random();
        public int Enact(Enemy enemy, Player player)
        {
            if (enemy.Health <= enemy.MaxHealth * 0.6)
            {
                return enemy.Attack((float) enemy.MaxHealth / enemy.Health);
            }

            if (_random.NextDouble() < 0.5)
            {
                enemy.Guard();
                return -1;
            }
            
            enemy.Heal((int)Math.Floor(enemy.Health * 0.1 * enemy.HealCount));
            return -1;
        }
    }
}