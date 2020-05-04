using System.Drawing;

namespace Pacman.Ai.AStar
{
    public class SearchLocation
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        public bool[,] Map { get; set; }
    }
}
