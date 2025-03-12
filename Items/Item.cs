namespace DungeonExplorer
{
    public class Item
    {
        private readonly string name;
        private readonly string description;

        public Item(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
        
        public string getName() {return name;}
        public string getDescription(){ return description;}
    }
}