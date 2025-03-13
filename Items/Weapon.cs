namespace DungeonExplorer.Items
{
    public class Weapon : Item
    {
        private readonly int _damage;
        
        public Weapon(string name, int damage) : base(name)
        {
            _damage = damage;
            
        }

        public int GetDamage() { return _damage; }

        public int CalculateDamage(int armour)
        {
            return _damage - armour;
        }
    }
}