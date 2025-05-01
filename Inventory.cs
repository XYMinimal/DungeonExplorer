using System;
using System.Collections.Generic;
using System.Linq;
using DungeonExplorer.Items;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Item> _contents = new List<Item>();
        private const int MaxSize = 20;

        public void AddItem(Item item)
        {
            _contents.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _contents.Remove(item);
        }

        // This method is unused now, but I thought I would leave it in because I haven't used a big LinQ ordering anywhere else
        public void Sort()
        {
            _contents = _contents.OrderBy(item =>
            {
                if (item is Weapon weapon && weapon.IsEquipped) return 0;
                if (item is Armour armour && armour.IsEquipped) return 1;
                if (item is Potion) return 2;
                return 3;
            }).ThenBy(item => item.Name).ToList();
        }
        

        public List<Potion> GetPotions()
        {
            return _contents.OfType<Potion>().OrderBy(Item => Item.Name).ToList();
        }

        public void DisplayContents()
        {
            Weapon equippedWeapon = (Weapon)_contents.Find(item => item is Weapon weapon && weapon.IsEquipped);
            Armour equippedArmour = (Armour)_contents.Find(item => item is Armour armour && armour.IsEquipped);
            Console.WriteLine($"Inventory: {_contents.Count} Items /  Max size: {MaxSize}");
            // I didn't actually know this operator existed
            Console.WriteLine($"Equipped: {equippedWeapon?.Name ?? "None"}, {equippedArmour?.Name ?? "None"}");
            Console.WriteLine($"Potions: {_contents.FindAll(item => item is Potion)}");
            // I know a line this long is bad form but IDK what to do here
            Console.WriteLine(
                $"Items: {_contents.FindAll(item => (item is Weapon weapon && !weapon.IsEquipped) || (item is Armour armour && !armour.IsEquipped))}");

        }
    }
}