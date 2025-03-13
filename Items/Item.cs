namespace DungeonExplorer
{
    public class Item
    {
        private readonly string name;

        public Item(string name)
        {
            this.name = name;
        }
        
        public string GetName
            () {return name;}
    }
}