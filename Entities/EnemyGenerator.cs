using System;
using System.Collections.Generic;
using DungeonExplorer.Logic.Strategies;

namespace DungeonExplorer.Entities
{
    public static class EnemyGenerator
    {
        private static readonly Random Random = new Random();

        private static readonly List<Enemy> Templates = new List<Enemy>
        {
            new Enemy("Goblin", 7, new Aggresive(), 5),
            new Enemy("Giant", 30, new Defensive(), 10),
            new Enemy("Dragon", 25, new Desperate(), 12),
            new Enemy("Crypt Ghoul", 15, new Aggresive(), 3),
            new Enemy("Wandering Ooze", 50, new Defensive(), 2),
            new Enemy("Glass Golem", 10, new Aggresive(), 20),
            new Enemy("Grave Robber", 12, new Desperate(), 6)
        };

        public static Enemy GetRandomEnemy(int depthModifier)
        {
            Enemy template = Templates[Random.Next(0, Templates.Count)];
            return new Enemy(
                template.Name, 
                template.Health * depthModifier, 
                template.Strategy, 
                template.Damage * depthModifier / 2
                );
        }
    }
}