namespace DungeonExplorer.Entities
{
    public class Aggresive : IAttackStrategy
    {
        public string Name => "Aggresive";
        public string Description => "Enemy will go all out on attacks and cause as much damage as possible!";
        public int Enact(Enemy enemy, Player player)
        {
            return enemy.Attack(1.3f);
        }
    }
}