namespace DungeonExplorer
{
    public class Armour : Item
    {
        private readonly int armour;
        public Armour(string name, int armour) : base(name)
        {
            this.armour = armour;
        }
        
        public int getArmour() { return armour; }
    }
}