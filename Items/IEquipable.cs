using DungeonExplorer.Entities;

namespace DungeonExplorer.Items
{
    public interface IEquipable
    {
        bool IsEquipped { get; }
        void Equip(Player player);
        void Unequip(Player player);
    }
}