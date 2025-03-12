namespace DungeonExplorer
{
    public class Enemy
    {
        public string name;
        public string description;
        public int health { get; private set; }
        public int armour { get; private set; }
        public int damage { get; private set; }
        public string type { get; private set; }


        public Enemy(string name, string description, int health, int armour, int damage, string type)
        {
            this.name = name;
            this.description = description;
            this.health = health;
            this.armour = armour;
            this.damage = damage;
            this.type = type;
        }
        
        public string getName() { return name; }
        public string getDescription() { return description; }
    }
}