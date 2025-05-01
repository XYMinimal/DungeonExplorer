using System;
using DungeonExplorer.Entities;

namespace DungeonExplorer.Logic
{
    public class FightLoop
    {
        private Enemy _enemy;
        private Player _player;

        public FightLoop(Enemy enemy, Player player)
        {
            _enemy = enemy;
            _player = player;
            
            Console.WriteLine($"You have encountered {enemy.Name}!");
            Console.WriteLine($"{_enemy.Strategy.Name}:\n\t{_enemy.Strategy.Description}");
        }

        private void GetEnemyInfo()
        {
            Console.WriteLine($"{_enemy.Name}:\t{_enemy.Health} / {_enemy.MaxHealth}");
        }

        private int CalculateDamage(int damage, bool isGuarding)
        {
            return (int) Math.Floor(damage * (isGuarding ? 0.4 : 1));
        }
        
        private void PlayerTurn() {}

        public bool engage()
        {
            // Manage win / loss cons, false == you lose, true == you won
            if (_enemy.Health <= 0)
            {
                Console.WriteLine($"You defeated {_enemy.Name}!");
                return true;
            }

            if (_player.Health <= 0)
            {
                Console.WriteLine($"You were defeated by {_enemy.Name}!");
                return false;
            }
            // Display enemy health
            GetEnemyInfo();
            
            //Player's turn
            PlayerTurn();
            
            // Enemy's turn
            int enemyDamage = _enemy.Strategy.Enact(_enemy, _player);
            if (enemyDamage != -1)
            {
                int finalDamage = Math.Min(_player.Health, CalculateDamage(enemyDamage, _player.IsGuarding))
                Console.WriteLine();
            }
            
            
            
            
            
            
            return 0;
        }
        
    }
}