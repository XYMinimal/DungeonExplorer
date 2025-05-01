using DungeonExplorer.Entities;

namespace DungeonExplorer.Items
{
    public class Weapon : Item, IEquipable
    {
        private readonly int _damage;
        public bool IsEquipped { get; private set; }
        
        public Weapon(string name, int damage) : base(name)
        {
            _damage = damage;
        }
        
        public void Equip(Player player)
        {
            player.WeaponDamage =  _damage;
            IsEquipped = true;
        }

        public void Unequip(Player player)
        {
            player.WeaponDamage = 0;
            IsEquipped = false;
        }
    }
}