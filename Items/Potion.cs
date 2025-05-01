namespace DungeonExplorer.Items
{
    public class Potion : Item
    {
        public string Type { get; private set; }

        public Potion(string name, string type) : base(name)
        {
            Type = type;
        }
    }
}