using DungeonExplorer.Entities;

namespace DungeonExplorer.Items
{
    public class Armour : Item, IEquipable
    {
        private readonly int _damageResistance;
        private readonly float _guardEffectiveness;
        public bool IsEquipped { get; private set; }

        public Armour(string name, int damageResistance, float guardEffectiveness) : base(name)
        {
            _damageResistance = damageResistance;
            _guardEffectiveness = guardEffectiveness;
        }

        public void Equip(Player player)
        {
            IsEquipped = true;
            player.GuardEffectiveness = _guardEffectiveness;
            player.DamageResistance = _damageResistance;
        }

        public void Unequip(Player player)
        {
            IsEquipped = false;
            player.GuardEffectiveness = 0;
            player.DamageResistance = 0;
        }
        
        public int DamageResistance => _damageResistance;
        public float GuardEffectiveness => _guardEffectiveness;
        
    }
}