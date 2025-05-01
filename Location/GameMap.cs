using System;
using System.Collections;
using DungeonExplorer.Entities;

namespace DungeonExplorer.Location
{
    public class GameMap
    {
        public Room[,] Rooms = new Room[10, 10];
        private  Location _playerLoc = RoomGenerator.GetRandomLocation(new Location(0, 0));
        private Location ExitLoc = RoomGenerator.GetRandomLocation(new Location(Player));

        public GameMap()
        {
            /* Did all this code only to find out one line could do the same
             
             for (int i = 0; i < 10; i++)
            {
                Rooms.Add(new ArrayList(10));
            }

            foreach (ArrayList row in Rooms)
            {
                for (int i = 0; i < 10; i++)
                {
                    row.Add(null);
                }
            }*/
        }

        public void MovePlayer(Player player)
        {
            ArrayList directions = new ArrayList(){ "Forward", "Backwards", "Left", "Right" };
            if (player)
        }
    }
}