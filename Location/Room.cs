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
        public bool Explored { get; set; } = false;
        public Enemy Enemy {get;set;}

        public Room()
        {
            _items = new List<Item>();
            _description = Explored ? "You enter an empty room" : "You discover a new room!";

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