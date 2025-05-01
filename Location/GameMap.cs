using System;
using System.Collections;
using System.Collections.Generic;
using DungeonExplorer.Entities;

namespace DungeonExplorer.Location
{
    public class GameMap
    {
        public Room[,] Rooms = new Room[10, 10];
        private Location _playerLoc;
        private Location _exitLoc;

        public GameMap()
        {
            _playerLoc = RoomGenerator.GetRandomLocation(new Location(1, 1));
            _exitLoc = RoomGenerator.GetRandomLocation(_playerLoc);
            /* Did all this code only to find out one line could do the same
             
             for (int i = 0; i < 10; i++)
            {
                Rooms.Add(new ArrayList(10));
            }

            foreach (ArrayList row in Rooms)
            {
                for (int i = 0; i < 10; i++)
                {
                    row.Add(null);
                }
            }*/
        }

        public Room SetUp(string[] itemTypes, string[] potionTypes, int depth, Player player)
        {
            return Rooms[_playerLoc.X, _playerLoc.Y] = RoomGenerator.GenerateRoom(itemTypes, potionTypes, depth, player);
        }
        
        // This could be made better with LinQ filtering and some reorganising
        public Room MovePlayer(Player player, string[] itemtypes, string[] potiontypes, int depth)
        {
            List<string> directions = new List<string>(){ "forward", "backwards", "left", "right" };
            if (_playerLoc.X < 1) { directions.Remove("left");}
            if (_playerLoc.X > 8) { directions.Remove("right");}
            if (_playerLoc.Y < 1) { directions.Remove("forward");}
            if (_playerLoc.Y > 8) { directions.Remove("backwards");}
            
            bool invalid = true;
            string choice = "";
            Console.WriteLine($"You can move: {string.Join(", ", directions)}");

            while (invalid)
            { 
                choice = Console.ReadLine();
                if (!directions.Contains(choice?.ToLower()))
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }
                invalid = false;    
            }
            
            Rooms[_playerLoc.X, _playerLoc.Y].Explored = true;
            switch (choice)
            {
                case "forward":
                    _playerLoc.Y--;
                    break;
                case "backwards":
                    _playerLoc.Y++;
                    break;
                case "left":
                    _playerLoc.X--;
                    break;
                case "right":
                    _playerLoc.X++;
                    break;
                
            }

            if (_playerLoc.X == _exitLoc.X && _playerLoc.Y == _exitLoc.Y)
            {
                Console.WriteLine("You have found the exit!\nYou win!!!");
                Environment.Exit(0);
            }

            if (Rooms[_playerLoc.X, _playerLoc.Y] == null)
            {
                Rooms[_playerLoc.X, _playerLoc.Y] = RoomGenerator.GenerateRoom(itemtypes, potiontypes, depth, player);
            }
            return Rooms[_playerLoc.X, _playerLoc.Y];
        }
    }
}