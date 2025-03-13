using System;
using System.Collections.Generic;
using System.Linq;
using DungeonExplorer.Items;

namespace DungeonExplorer
{
    internal class Game
    {
        private readonly Player _player;
        private readonly Random _random =  new Random();

        private bool _playing = true;
        // depth is used to increase difficulty and escape chance as game progresses
        private int _depth;
        // List of possible enemy names, in a bigger project this would be store in a separate file
        private readonly string[] _enemies = {
            "Evil Bat", 
            "Zombie", 
            "Giant Spider", 
            "Living Ooze"
        };
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
        // Main function for creating rooms, items and enemies
        private Room GenerateRoom()
        {
            Room room = new Room("", _random.Next(2) + 1);
            // This code determines if the player has found an exit, the chance increases the more rooms you explore
            bool escape = _random.Next(100) < (5 * Math.Log(_depth));
            if (escape)
            {
                Console.WriteLine("You escaped in " + _depth + " rooms!");
                return room;
            }
            // This code is responsible for determining if an enemy will spawn
            // as well as the stats, Stats increase with depth
            bool enemySpawn = _random.Next(100) < 50;
            if (enemySpawn)
            {
                double health = Math.Floor(Math.Pow(_depth + 3, _random.Next(8, 14) / 10.0));
                int intHealth = (int) Math.Floor(health);
                Enemy enemy = new Enemy(
                    _enemies[_random.Next(_enemies.Length)],
                    intHealth,
                    (int)Math.Floor(intHealth / 10.0),
                    (int)Math.Floor(_player.Health / (10 - (_depth * 0.5))));
                room.Enemy = enemy;
            }
            // This code determines if you will find any items and how many
            for (int i = 0; i < _random.Next(100) % 33; i++)
            {
                bool treasureSpawn = _random.Next(100) < 30;
                if (treasureSpawn)
                {
                    string type = _itemTypes[_random.Next(_itemTypes.Length)];
                    switch (type)
                    {
                        case "Weapon":
                            // Generates a weapon with a weighted random strength
                            var weaponDetails = LootTable.generateRarity();
                            room.addItem(new Weapon(weaponDetails.Value + " Sword", 
                                weaponDetails.Key * _depth * 2));
                            break;
                        // For now Armour is a static increase
                        case "Armour":
                            room.addItem(new Item("Armour"));
                            break;
                        // Generate random type
                        case "Potion":
                            string potionType = _potionTypes[_random.Next(_potionTypes.Length)];
                            room.addItem(new Item(potionType + "Potion"));
                            break;
                        // Treasure is just a generic collectable
                        case "Treasure":
                            _player.AddTreasure();
                            break;
                        // This shouldn't be thrown ever
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }


            return room;
        }
        // Basic description of the room
        private void PrintInfo(Room room)
        {
            Console.WriteLine($"Name: {_player.Name}\tHealth: {_player.Health}\nInfo: {room.getDescription()}");
            if (room.getItems().Count == 0) {return;}
            Console.WriteLine("You find item(s) in the room: \t");
            foreach (Item item in room.getItems())
            {
                Console.WriteLine($"\t{item.GetName()}");
            }
        }
        
        // Main game loop, can be split into 3 sections: fight, loot, navigate
        public void Start()
        {
            while (_playing)
            {
                // If empty room then escape, line 47 determines the probability
                Room room = GenerateRoom();
                if (room.getPaths().Count == 0)
                {
                    _playing = false;
                    return;
                }
                PrintInfo(room);
                // If an enemy is present begin the fight
                if (room.Enemy != null) {  Console.WriteLine($"You have encountered a {room.Enemy.name}"); }
                while (room.Enemy != null)
                {
                    bool invalid = true;
                    int choice = 1;
                    Console.WriteLine($"Your Health: {_player.Health}\tEnemy Health: {room.Enemy.health}\n\nWhat do you use: \n" +
                                      $"{string.Join(", ", _player.InventoryContents().Select(p => p.GetName()))}");

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
                    
                    int dealt = Math.Max(room.Enemy.damage - _player.Armor, 0);
                    Console.WriteLine($"The enemy attacks and deals {dealt} damage!\n");
                    if (_player.Health <= 0)
                    {
                        Console.WriteLine("You have lost all health!");
                        Environment.Exit(0);
                    }
                   
                    
                }
                
                List<Item>  items = room.getItems();
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
                    if (choice < 0 || choice > room.getItems().Count)
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