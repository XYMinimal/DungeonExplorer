using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public Game()
        {
            // Initialisation code
            Console.WriteLine("Enter player name: ");
            string playerName = Console.ReadLine();
            _player = new Player(playerName, 100);
            _depth = 0;
            _currentRoom = new Room("You are in an empty room.\n There is");
            

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
            bool treasureSpawn = _random.Next(100) < 30;
            if (treasureSpawn)
            {
                string type = itemTypes[_random.Next(itemTypes.Length)];
                Switch (type)
                {
                    case "Weapon":
                        _player.PickUpItem(new Weapon());
                }
            }


            return room;
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



                _depth += 1;
            }
        }
    }
}