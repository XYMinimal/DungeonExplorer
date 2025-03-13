namespace DungeonExplorer
{
    public class Room
    {
        private string _description;
        private string[] _paths =  new string[]{"forward", "left", "right"};
        public Enemy Enemy {get;set;}

        public Room(string description, int pathCount)
        {
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
    }
}