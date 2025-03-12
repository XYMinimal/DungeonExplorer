using System;
using System.Collections;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public static class LootTable
    {
        private static Random _random = new Random();

        private static readonly Dictionary<string, float> lootTable = new Dictionary<string, float>
        {
            { "Common", 0.58f },
            { "Rare", 0.3f },
            { "Epic", 0.1f },
            { "Legendary", 0.02f }
        };

        public static String generateRarity()
        {
            float f = (float)_random.NextDouble();
            foreach (KeyValuePair<string, float> entry in lootTable)
            {
                f -= entry.Value;
                if (f <= 0)
                {
                    return entry.Key;
                }
            }
            throw new Exception("Rarity doesn't exist");
        }
    }
}