namespace DungeonExplorer
{
    public class Weapon : Item
    {
        private int damage;
        private string type;
        
        public Weapon(string name, string description, int damage, string type) : base(name, description)
        {
            this.damage = damage;
            this.type = type;
            
        }

        public int getDamage() { return damage; }
        public string getType() { return type; }

        private int calculateDamage(int armour, string type)
        {
            return armour * 10;
        }
    }
}