using System;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private Random random =  new Random();

        public Game()
        {
            Console.WriteLine("Enter player name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName, 100);
            currentRoom = new Room("You are in an empty room.\n There is");
            

        }
        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                // generate room
            }
        }
    }
}