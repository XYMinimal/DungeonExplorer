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

        public static KeyValuePair<int, string> generateRarity()
        {
            float f = (float)_random.NextDouble();
            int x = 0;
            foreach (KeyValuePair<string, float> entry in lootTable)
            {
                f -= entry.Value;
                if (f <= 0)
                {
                    return new KeyValuePair<int, string>(x, entry.Key);
                }

                x++;
            }
            throw new Exception("Rarity doesn't exist");
        }
    }
}