using System;
using DungeonExplorer.Entities;
using DungeonExplorer.Items;

namespace DungeonExplorer.Location
{
    public static class RoomGenerator
    {
        private static readonly Random Random = new Random();

        // An example of recursion, ensures you don't spawn on the exit and insta-win
        public static Location GetRandomLocation(Location avoid)
        {
            Location location = new Location(Random.Next(10), Random.Next(10));
            if (location.X == avoid.X && location.Y == avoid.Y)
            {
                return GetRandomLocation(avoid);
            }
            return location;
        }
        
        // Main function for creating rooms, items and enemies
        public static Room GenerateRoom(string[] itemTypes, string[] potionTypes, int depth, Player player)
        {
            Room room = new Room("", Random.Next(2) + 1);
            // This code determines if the player has found an exit, the chance increases the more rooms you explore
            bool escape = Random.Next(100) < (5 * Math.Log(depth));
            if (escape)
            {
                Console.WriteLine("You escaped in " + depth + " rooms!");
                return room;
            }
            // This code is responsible for determining if an enemy will spawn
            // as well as the stats, Stats increase with depth
            if (Random.Next(100) < 50)
            {
                int depthModifier = (int) Math.Floor(Math.Pow(3, depth / 10.0));
                room.Enemy = EnemyGenerator.GetRandomEnemy(depthModifier);
            }
            // This code determines if you will find any items and how many
            for (int i = 0; i < Random.Next(100) % 33; i++)
            {
                bool treasureSpawn = Random.Next(100) < 30;
                if (treasureSpawn)
                {
                    string type = itemTypes[Random.Next(itemTypes.Length)];
                    switch (type)
                    {
                        case "Weapon":
                            // Generates a weapon with a weighted random strength
                            var weaponDetails = LootTable.generateRarity();
                            room.AddItem(new Weapon(weaponDetails.Value + " Sword", 
                                weaponDetails.Key * depth * 2));
                            break;
                        // For now Armour is a static increase
                        case "Armour":
                            room.AddItem(new Item("Armour"));
                            break;
                        // Generate random type
                        case "Potion":
                            string potionType = potionTypes[Random.Next(potionTypes.Length)];
                            room.AddItem(new Item(potionType + "Potion"));
                            break;
                        // Treasure is just a generic collectable
                        case "Treasure":
                            player.AddTreasure();
                            break;
                        // This shouldn't be thrown ever
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            return room;
        }
        
    }
}