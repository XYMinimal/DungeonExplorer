namespace DungeonExplorer
{
    public class Armour : Item
    {
        private readonly int armour;
        public Armour(string name, string description, int armour) : base(name, description)
        {
            this.armour = armour;
        }
        
        public int getArmour() { return armour; }
    }
}