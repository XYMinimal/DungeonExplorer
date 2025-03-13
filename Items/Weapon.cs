namespace DungeonExplorer
{
    public class Weapon : Item
    {
        private int damage;
        private string type;
        
        public Weapon(string name, int damage) : base(name)
        {
            this.damage = damage;
            
        }

        public int getDamage() { return damage; }

        private int calculateDamage(int armour)
        {
            return damage - armour;
        }
    }
}