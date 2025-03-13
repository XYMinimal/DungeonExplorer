using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public Weapon Weapon { get; set; }
        public int Armor { get; set; }
        private List<Item> _inventory = new List<Item>();
        private int treasureCount = 0;
        public float DamageModifier { get; private set; }

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }
        public void PickUpItem(Item item)
        {
            _inventory.Add(item);
        }
        public List<Item> InventoryContents()
        {
            return _inventory;
        }

        public void addTreasure()
        {
            treasureCount++;
        }

        public void setDamageModifier(float modifier)
        {
            DamageModifier = modifier;
        }

        public void setHealth(int health)
        {
            Health = health;
        }
    }
}