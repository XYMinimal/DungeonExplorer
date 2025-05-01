using System.Collections.Generic;
using DungeonExplorer.Entities;
using DungeonExplorer.Items;

namespace DungeonExplorer.Entities
{
    public class Player : Creature
    {
        private float _damageModifier;
        public Weapon Weapon { get; set; }
        private List<Item> _inventory = new List<Item>();
        private int _treasureCount = 0;

        public Player(string name, int health) : base(name, health)
        {
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

        public override void Heal(int amount)
        {
            Health += amount;
        }
    }
}