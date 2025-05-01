using System;
using System.Collections.Generic;
using DungeonExplorer.Entities;
using DungeonExplorer.Items;
using DungeonExplorer.Location;
using DungeonExplorer.Logic;

namespace DungeonExplorer
{
    internal class Game
    {
        private readonly Player _player;
        private GameMap _gameMap =  new GameMap();

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
            _player.PickUpItem(new Potion("Free Health Potion", "Health"));
            _depth = 3;
            

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
            Room room = _gameMap.SetUp(_itemTypes, _potionTypes, _depth, _player);
            while (_playing)
            {
                PrintInfo(room);
                // If an enemy is present begin the fight
                if (room.Enemy != null)
                {
                    bool result = new FightLoop(room.Enemy, _player).Engage();
                    if (!result)
                    {
                        Console.WriteLine($"You lose! You collected {_player.TreasureCount} and reached depth {_depth}");
                        Environment.Exit(0);
                    }
                }
                
                
                List<Item>  items = room.GetItems();
                Console.WriteLine("What do you want to do?");
                bool invalidChoice = true;
                int choice = 0;
                if (items.Count != 0)
                {
                    Console.WriteLine("1. Pick up items\n2. Pickup items and change equipment: ");
                    while (invalidChoice)
                    {
                        choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                        if (choice != 1 && choice != 2)
                        {
                            Console.WriteLine("Invalid choice. Try again.");
                            continue;
                        }
                        invalidChoice = false;
                    }
                    
                    foreach (Item item in items)
                    {
                        _player.PickUpItem(item);
                    }
                    
                    if (choice == 2)
                    {
                        List<Item> unequiped = _player.Inventory.Sort(_player.Inventory.GetUnequiped());
                        Console.WriteLine(string.Join(", ", unequiped));
                        for (int i = 0; i < unequiped.Count; i++)
                        {
                            if (unequiped[i].GetType() == typeof(Armour))
                            {
                                Armour armour = (Armour)unequiped[i];
                                Console.WriteLine(
                                    $"\t{i + 1}. Name: {armour.Name}, Guard Effect: {armour.GuardEffectiveness}%, Damage Reduction: {armour.DamageResistance}");
                            }
                            else
                            {
                                Weapon weapon = (Weapon)unequiped[i];
                                Console.WriteLine($"\t{i + 1}. Name: {weapon.Name}, Damage: {weapon.Damage}");
                            }
                        }

                        invalidChoice = true;
                            choice = 0;
                            if (unequiped.Count != 0)
                            {
                                Console.WriteLine("Select Item:  ");
                                while (invalidChoice)
                                {
                                    choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                                    if (choice < 1 || choice > unequiped.Count + 1)
                                    {
                                        Console.WriteLine("Invalid choice. Try again.");
                                        continue;
                                    }

                                    invalidChoice = false;
                                }
                                choice -= 1;

                                if (choice.GetType() == typeof(Armour))
                                {
                                    Armour old = (Armour)_player.Inventory.GetEquipped(typeof(Armour));
                                    old.Unequip(_player);
                                    Armour current = (Armour)unequiped[choice];
                                    current.Equip(_player);
                                }
                                else if (choice.GetType() == typeof(Weapon))
                                {
                                    Weapon old = (Weapon)_player.Inventory.GetEquipped(typeof(Weapon));
                                    old.Unequip(_player);
                                    Weapon current = (Weapon)unequiped[choice];
                                    current.Equip(_player);
                                }
                            
                        }
                        
                        
                    } 
                    
                }

                room = _gameMap.MovePlayer(_player, _itemTypes, _potionTypes, _depth);
                _depth += 1;
            }
        }
    }
}