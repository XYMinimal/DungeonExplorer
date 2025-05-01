using System;
using System.Collections.Generic;
using DungeonExplorer.Items;

namespace DungeonExplorer.Entities
{
    public class Player : Creature
    {
        public float DamageModifier { get; set; }
        public int WeaponDamage { get; set; }
        public int DamageResistance { get; set; }
        public float GuardEffectiveness { get; set; }
        
        public Inventory Inventory { get; } = new Inventory();
        
        private int _treasureCount = 0;

        public Player(string name, int health) : base(name, health)
        {
            DamageModifier = 0;
        }
        public void PickUpItem(Item item)
        {
            Inventory.AddItem(item);
        }

        public void DropItem(Item item)
        {
            Inventory.RemoveItem(item);
            AddTreasure();
        }

        public void AddTreasure()
        {
            _treasureCount++;
        }

        public void SetDamageModifier(float modifier)
        {
            DamageModifier = modifier;
        }

        public override int Attack(float modifier)
        {
            return (int) Math.Floor((3 + WeaponDamage) * DamageModifier * modifier);
        }

        public override void Heal(int amount)
        {
            Health += amount;
        }

        public override void Guard()
        {
            Console.WriteLine($"{Name} guards, damage taken will be reduced");
        }
    }
}