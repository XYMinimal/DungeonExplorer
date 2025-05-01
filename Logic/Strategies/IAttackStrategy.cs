namespace DungeonExplorer.Entities
{
    public interface IAttackStrategy
    {
        int Enact(Enemy enemy, Player player);
        string Name { get; }
        string Description { get; }
    }
}