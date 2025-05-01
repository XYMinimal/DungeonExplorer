using System.Collections.Generic;
using System.Linq;
using DungeonExplorer.Entities;
using DungeonExplorer.Items;


namespace DungeonExplorer
{
    public class Room
    {
        private readonly string _description;
        private readonly List<Item> _items;
        private readonly string[] _paths =  {"forward", "left", "right"};
        private readonly int _pathCount;
        public Enemy Enemy {get;set;}

        public Room(string description, int pathCount)
        {
            _items = new List<Item>();
            _pathCount = pathCount;
            _description = description + $"\nThere are {pathCount} paths: ";
            for (int i = 0; i < pathCount; i++)
            {
                _description += $"\n\t{i}. {_paths[i]}";
            }

        }

        public string GetDescription()
        {
            return _description;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public List<string> GetPaths()
        {
            return _paths.Take(_pathCount).ToList();
        }

        public List<Item> GetItems()
        {
            return _items;
        }
        
    }
}