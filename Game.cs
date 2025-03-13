using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player _player;
        private Room _currentRoom;
        private readonly Random _random =  new Random();
        // depth is used to increase difficulty and escape chance as game progresses
        private int _depth;
        // List of possible enemy names, in a bigger project this would be store in a separate file
        private String[] _enemies = new String[]
        {
            "Evil Bat", 
            "Zombie", 
            "Giant Spider", 
            "Living Ooze"
        };

        private string[] itemTypes = new string[]
        {
            "Weapon", "Armour", "Potion", "Treasure"
        };

        private string[] potionTypes = new String[]
        {
            "Health", "Strength", "Fortitude"
        };

        public Game()
        {
            // Initialisation code
            Console.WriteLine("Enter player name: ");
            string playerName = Console.ReadLine();
            _player = new Player(playerName, 100);
            _depth = 0;
            _currentRoom = new Room("You are in an empty room.\n There is", 3);
            

        }
        // Main function for creating rooms, items and enemies
        public Room generateRoom()
        {
            Room room = new Room("", 0);
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
                double health = Math.Floor(Math.Pow(_depth, _random.Next(8, 14) / 10.0)
                int intHealth = (int) Math.Floor(health);
                Enemy enemy = new Enemy(
                    _enemies[_random.Next(_enemies.Length)],
                    intHealth,
                    (int)Math.Floor(intHealth / 10.0),
                    (int)Math.Floor(_player.Health / (10 - (_depth * 0.5))));
                room.Enemy = enemy;
            }

            for (int i = 0; i < _random.Next(100) % 33; i++)
            {
                bool treasureSpawn = _random.Next(100) < 30;
                if (treasureSpawn)
                {
                    string type = itemTypes[_random.Next(itemTypes.Length)];
                    switch (type)
                    {
                        case "Weapon":
                            var weaponDetails = LootTable.generateRarity();
                            room.addItem(new Weapon(weaponDetails.Value + "Weapon", 
                                weaponDetails.Key * _depth * 2));
                            break;
                        // Logic here should likely be altered
                        case "Armour":
                            var armourDetails = LootTable.generateRarity();
                            room.addItem(new Weapon(armourDetails.Value + "Weapon", 
                                armourDetails.Key * _depth * 2));
                            break;
                        case "Potion":
                            string potionType = potionTypes[_random.Next(potionTypes.Length)];
                            room.addItem(new Item(potionType + "Potion");
                            break;
                        case "Treasure":
                            _player.addTreasure();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }


            return room;
        }

        public void printInfo(Room room)
        {
            Console.WriteLine($"Name: {_player.Name}\tHealth: {_player.Health}\nInfo: {room.getDescription()}");
        }
        
        
        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                Room room = generateRoom();
                if (room.getDescription() == "")
                {
                    playing = false;
                    return;
                }
                printInfo(room);
                while (room.Enemy != null)
                {
                    bool invalid = true;
                    int choice = 1;
                    Console.WriteLine($"You have encountered a {room.Enemy.name}\n\nWhat do you use: \n" +
                                      $"{string.Join(", ", _player.InventoryContents().Select(p => p.getName()))}");

                    while (invalid)
                    {
                        choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
                        if (choice <= 0 || choice > _player.InventoryContents().Count)
                        {
                            Console.WriteLine("Invalid choice. Try again.");
                        }
                        invalid = false;    
                    }
                    
                    Item chosen =  _player.InventoryContents()[choice];
                    if (chosen.GetType() == typeof(Weapon))
                    {
                        Weapon weapon = (Weapon)chosen;
                        int taken = room.Enemy.takeDamage(
                            (int) Math.Floor(weapon.getDamage() * (1 + _player.damageModifier)));
                        Console.WriteLine($"You attack and the enemy loses {taken} health!");
                    }
                    else if (chosen.GetType() == typeof(Potion))
                    {
                        Potion potion = (Potion)chosen;
                        if (potion.Type == "Health")
                        {
                            int healed = Math.Min(100 - _player.Health, 50);
                            _player.setHealth(healed);
                            Console.WriteLine($"You drank a Health Potion and healed {healed} health!");
                        }
                        else if (potion.Type == "Strength")
                        {
                            _player.setDamageModifier(_player.damageModifier += 0.5);
                        }
                    }
                   
                    
                }
                


                _depth += 1;
            }
        }
    }
}