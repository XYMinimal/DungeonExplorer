using System;

namespace DungeonExplorer
{
    public class Enemy
    {
        public string name;
        public int health { get; private set; }
        public int armour { get; private set; }
        public int damage { get; private set; }


        public Enemy(string name, int health, int armour, int damage)
        {
            this.name = name;
            this.health = health;
            this.armour = armour;
            this.damage = damage;
        }
        
        public string getName() { return name; }

        public int takeDamage(int damage)
        {
            int taken = Math.Max(damage - armour, 0);
            health -= taken;
            return taken;
        }
    }
}