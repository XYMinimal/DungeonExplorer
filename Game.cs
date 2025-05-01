using System;
using System.Collections.Generic;
using DungeonExplorer.Entities;
using DungeonExplorer.Items;
using DungeonExplorer.Location;

namespace DungeonExplorer
{
    internal class Game
    {
        private readonly Player _player;
        private readonly Random _random =  new Random();

        private bool _playing = true;
        // depth is used to increase difficulty and escape chance as game progresses
        private int _depth;
        // List of possible items you can find
        private readonly string[] _itemTypes = {
            "Weapon", "Armour", "Potion", "Treasure"
        };
        // List of possible potion types
        private readonly string[] _potionTypes = {
            "Health", "Strength"
        };

        public Game()
        {
            // Initialisation code
            Console.WriteLine("Enter player name: ");
            string playerName = Console.ReadLine();
            _player = new Player(playerName, 100);
            _player.PickUpItem(new Weapon("Common Sword", 5));
            _depth = 0;
            

        }
        
        // Basic description of the room
        private void PrintInfo(Room room)
        {
            Console.WriteLine($"Name: {_player.Name}\tHealth: {_player.Health}\nInfo: {room.GetDescription()}");
            if (room.GetItems().Count == 0) {return;}
            Console.WriteLine("You find item(s) in the room: \t");
            foreach (Item item in room.GetItems())
            {
                Console.WriteLine($"\t{item.Name}");
            }
        }
        
        // Main game loop, can be split into 3 sections: fight, loot, navigate
        public void Start()
        {
            while (_playing)
            {
                // If empty room then escape, line 47 determines the probability
                Room room = RoomGenerator.GenerateRoom(_itemTypes, _potionTypes, _depth, _player);
                if (room.GetPaths().Count == 0)
                {
                    _playing = false;
                    return;
                }
                PrintInfo(room);
                // If an enemy is present begin the fight
                if (room.Enemy != null) {  Console.WriteLine($"You have encountered a {room.Enemy.Name}"); }
                while (room.Enemy != null)
                {
                    bool invalid = true;
                    int choice = 1;
                    Console.WriteLine($"Your Health: {_player.Health}\tEnemy Health: {room.Enemy.Health}\n\nWhat do you use: \n" +
                                      $"{string.Join(", ", _player.Inventory..Select(p => p.GetName()))}");

                    while (invalid)
                    {
                        choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                        if (choice <= 0 || choice > _player.InventoryContents().Count)
                        {
                            Console.WriteLine("Invalid choice. Try again.");
                            continue;
                        }
                        invalid = false;    
                    }
                    
                    Item chosen =  _player.InventoryContents()[choice - 1];
                    if (chosen.GetType() == typeof(Weapon))
                    {
                        Weapon weapon = (Weapon)chosen;
                        int taken = room.Enemy.takeDamage(
                            (int) Math.Floor(weapon.GetDamage() * (1 + _player.GetDamageModifier())));
                        Console.WriteLine($"You attack and the enemy loses {taken} health!");
                    }
                    else if (chosen.GetType() == typeof(Potion))
                    {
                        Potion potion = (Potion)chosen;
                        if (potion.Type == "Health")
                        {
                            int healed = Math.Min(100 - _player.Health, 50);
                            _player.SetHealth(healed);
                            Console.WriteLine($"You drank a Health Potion and healed {healed} health!");
                        }
                        else if (potion.Type == "Strength")
                        {
                            _player.SetDamageModifier(_player.GetDamageModifier() + 0.5f);
                            Console.WriteLine("You drank a Strength Potion and will do more damage until the fight ends!");
                        }
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    if (room.Enemy.health <= 0)
                    {
                        Console.WriteLine("You have defeated the " + room.Enemy.name);
                        room.Enemy = null;
                        break;
                    }
                    
                    int dealt = Math.Max(room.Enemy.Damage - _player.Armor, 0);
                    Console.WriteLine($"The enemy attacks and deals {dealt} damage!\n");
                    if (_player.Health <= 0)
                    {
                        Console.WriteLine("You have lost all health!");
                        Environment.Exit(0);
                    }
                   
                    
                }
                
                List<Item>  items = room.GetItems();
                Console.WriteLine("What do you want to do?");
                bool invalidChoice = true;
                if (items.Count != 0)
                {
                    Console.WriteLine("1. Pick up items\n: ");
                    while (invalidChoice)
                    {
                        int choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                        if (choice != 1)
                        {
                            Console.WriteLine("Invalid choice. Try again.");
                            continue;
                        }
                        invalidChoice = false;
                    }

                    foreach (Item item in items)
                    {
                        if (item.GetName() == "Armour")
                        {
                            _player.Armor += 4;
                        }
                        else
                        {
                            _player.PickUpItem(item);
                        }
                    }
                }
                
                PrintInfo(room);
                Console.WriteLine("Where would you like to go? : ");
                bool invalidX = true;
                while (invalidX)
                {
                    int choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                    if (choice < 0 || choice > room.GetItems().Count)
                    {
                        Console.WriteLine("Invalid choice. Try again.");
                        continue;
                    }
                    invalidX = false;
                }
                
                


                _depth += 1;
            }
        }
    }
}