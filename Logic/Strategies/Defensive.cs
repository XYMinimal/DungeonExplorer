using System;
using DungeonExplorer.Entities;

namespace DungeonExplorer.Logic.Strategies
{
    public class Defensive : IAttackStrategy
    {
        public string Name => "Defensive";
        public string Description => "This enemy will heal and block damage, only attacking when on high hp!";

        public int Enact(Enemy enemy, Player player)
        {
            if (enemy.Health <= (enemy.MaxHealth / 2))
            {
                enemy.Heal((int)Math.Floor(enemy.Health * 1.3));
            }
            else if (enemy.Health <= enemy.MaxHealth * 0.7)
            {
                enemy.Guard();
            }
            else
            {   
                Console.WriteLine($"{enemy.Name} attacks!");
                return enemy.Attack(1);
            }

            return -1;
        }
        
    }
}