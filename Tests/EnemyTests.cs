using System.Diagnostics;
using DungeonExplorer.Items;

namespace DungeonExplorer.Tests
{
    public static class EnemyTests
    {
        public static void TestEnemyAttacked()
        {
            Enemy enemy = new Enemy("Test Enemy", 20, 5, 10);

            enemy.takeDamage(15);
            Debug.Assert(enemy.health == 10, "Health should be 10");
            
            enemy.takeDamage(1000000);
            Debug.Assert(enemy.health == 0, "Health should not go below 0");
        }

        public static void TestPlayerAttacked()
        {
            Player player = new Player("Test Player", 100);
            player.Armor = 5;
            Weapon weapon = new Weapon("Test Weapon", 10);
            
            player.SetHealth(player.Health - weapon.CalculateDamage(player.Armor));
            
            Debug.Assert(player.Health == 95, "Health should be 95");
        }
    }
}