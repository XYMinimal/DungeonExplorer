using System;
using System.Collections.Generic;
using System.Linq;
using DungeonExplorer.Entities;
using DungeonExplorer.Items;

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

        private void PlayerTurn()
        {
            bool invalid = true;
            int choice = 1;
            Console.WriteLine($"Your Health: {_player.Health}\nWhat do you do: \n" +
                              $"\t1. Attack\n\t2. Guard\n\t3. Use potion\n:");

            while (invalid)
            {
                choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                if (choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }
                invalid = false;    
            }

            switch (choice)
            {
                case 1:
                    int damageDealt = Math.Min(CalculateDamage(_player.Attack(1f), 
                        _enemy.IsGuarding), _enemy.Health);
                    Console.WriteLine($"You successfully dealt {damageDealt} damage.");
                    _enemy.Health -= damageDealt;
                    break;
                case 2:
                    _player.Guard();
                    break;
                case 3:
                    List<Potion> potions = _player.Inventory.GetPotions();
                    int index = 1;
                    foreach (Potion potion in potions)
                    {
                        Console.WriteLine($"{index}: {potion.Name}");
                    }
                    break;
                default:
                    throw new Exception("This shouldn't happen.");
            }
                
        }

        private void UsePotion()
        {
            bool invalid = true;
            int choice = 1;
            Console.WriteLine($"Which potion would you like to use: ");
            

            while (invalid)
            {
                choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                if (choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }
                invalid = false;    
            }
            Potion selectedPotion = _player.Inventory.GetPotions()[choice];
            if (selectedPotion.Type == "Health")
            {
                int healed = Math.Min(100 - _player.Health, 50);
                _player.Health += healed;
                Console.WriteLine($"You drank a Health Potion and healed {healed} health!");
            }
            else if (selectedPotion.Type == "Strength")
            {
                _player.DamageModifier += 0.5f;
                Console.WriteLine("You drank a Strength Potion and will do more damage until the fight ends!");
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void Fight()
        {
            // Display enemy health
            GetEnemyInfo();
            
            //Player's turn
            PlayerTurn();
            
            // Enemy's turn
            int enemyDamage = _enemy.Strategy.Enact(_enemy, _player);
            
            if (enemyDamage == -1) { return; }
            
            int finalDamage = Math.Min(_player.Health, CalculateDamage(enemyDamage, _player.IsGuarding));
            Console.WriteLine($"You took {finalDamage} damage!");
            _player.Health -= finalDamage;
        }

        public bool engage()
        {
            while (true)
            {
                Fight();
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
                
            }
            
        }
        
    }
}