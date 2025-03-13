using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private float _damageModifier;
        public Weapon Weapon { get; set; }
        public int Armor { get; set; }
        private List<Item> _inventory = new List<Item>();
        private int _treasureCount = 0;

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
            _damageModifier = 0;
        }
        public void PickUpItem(Item item)
        {
            _inventory.Add(item);
        }
        public List<Item> InventoryContents()
        {
            return _inventory;
        }

        public void AddTreasure()
        {
            _treasureCount++;
        }

        public void SetDamageModifier(float modifier)
        {
            _damageModifier = modifier;
        }

        public void SetHealth(int health)
        {
            Health = health;
        }

        public float GetDamageModifier()
        {
            return _damageModifier;
        }
    }
}