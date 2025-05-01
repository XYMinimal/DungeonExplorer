namespace DungeonExplorer.Entities
{
    public abstract class Creature
    {
        public string Name;
        public int Health {get; set;}
        public int MaxHealth  {get;}

        public bool IsGuarding { get; private set; } = false;

        protected Creature(string name, int health)
        {
            Name = name;
            Health = MaxHealth =  health;
        }

        public abstract int Attack(float  modifier);
        public abstract void Heal(int amount);
        
        // Felt like I had to find somewhere to use virtual
        public virtual void Guard()
        {
            IsGuarding = true;
        }

        public void CancelGuard()
        {
            IsGuarding = false;
        }
        
    }
}