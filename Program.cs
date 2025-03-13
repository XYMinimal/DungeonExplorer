using System;
using DungeonExplorer.Tests;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
           
            EnemyTests.TestEnemyAttacked();
            EnemyTests.TestPlayerAttacked();
        
            Console.WriteLine("Tests finished");
            Game game = new Game();
            game.Start();
            Console.WriteLine("Waiting for your Implementation");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
