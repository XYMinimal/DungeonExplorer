using System.Diagnostics;
using DungeonExplorer.Entities;
using DungeonExplorer.Items;
using DungeonExplorer.Logic;

namespace DungeonExplorer.Tests
{
    public static class EnemyTests
    {
        public static void TestEnemyAttacked()
        {
            Enemy enemy = new Enemy("Test Enemy", 20, new Aggresive(), 10);

            enemy.Health -= FightLoop.CalculateDamage(15, true, 0.4f, 0);
            Debug.Assert(enemy.Health == 12, $"Health should be 12, is {enemy.Health}");

            enemy.Health -= FightLoop.CalculateDamage(15, false, 0.4f, 6);
            Debug.Assert(enemy.Health == 3, $"Health should be 3, is {enemy.Health}");
        }

        public static void TestPlayerAttacked()
        {
            Player player = new Player("Test Player", 100);
            new Armour("Test Armour", 5, 0.3f).Equip(player);
            new Weapon("Test Weapon", 15).Equip(player);
            player.Guard();
            
            player.Health -= FightLoop.CalculateDamage(player.Attack(1.5f), player.IsGuarding, player.GuardEffectiveness, player.DamageResistance);
            
            Debug.Assert(player.Health == 74, $"Health should be 74, is {player.Health}");
            
            player.Heal(50);
            Debug.Assert(player.Health < 100, "Health should not exceed 100");
        }
    }
}