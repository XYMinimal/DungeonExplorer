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
            Room room = new Room();
            // I've changed how escaping works to now have a predetermined random exit point, see GameMap.cs
            
            if (Random.Next(100) < 50)
            {
                int depthModifier = (int) Math.Floor(1 + (0.15 * depth));
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
                            var weaponQuality = LootTable.generateRarity();
                            room.AddItem(new Weapon(weaponQuality.Value + " Sword", 
                                (weaponQuality.Key + 1) * depth * 2));
                            break;
                        // For now Armour is a static increase
                        case "Armour":
                            var armourQuality = LootTable.generateRarity();
                            room.AddItem(new Armour(armourQuality.Value + " Armour",
                                (armourQuality.Key + 1) * depth * 2, 
                                Random.Next(5 * (armourQuality.Key) + 1) - 10 + depth)); 
                            break;
                        // Generate random type
                        case "Potion":
                            string potionType = potionTypes[Random.Next(potionTypes.Length)];
                            room.AddItem(new Potion($"{potionType} Potion", potionType));
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