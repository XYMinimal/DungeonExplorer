using System.Collections.Generic;
using System.Linq;


namespace DungeonExplorer
{
    public class Room
    {
        private string _description;
        private List<Item> _items;
        private string[] _paths =  new string[]{"forward", "left", "right"};
        private int pathCount;
        public Enemy Enemy {get;set;}

        public Room(string description, int pathCount)
        {
            this.pathCount = pathCount;
            this._description = description + $"\nThere are {pathCount} paths: ";
            for (int i = 0; i < pathCount; i++)
            {
                this._description += $"\n\t{i}. {_paths[i]}";
            }

        }

        public string getDescription()
        {
            return _description;
        }

        public void addItem(Item item)
        {
            this._items.Add(item);
        }

        public List<string> getPaths()
        {
            return _paths.Take(pathCount).ToList();
        }
        
    }
}