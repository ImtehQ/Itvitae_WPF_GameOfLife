using System.Collections.Generic;

namespace GameOfLife.Models
{
    class Cell
    {
        /// <summary>
        /// Used to see if the cell is valid
        /// </summary>
        public bool Corrupt;

        /// <summary>
        /// The surounding cells
        /// </summary>
        public List<Cell> neighbors = new List<Cell>();

        public bool aliveLastFrame;
        public bool aliveCurrentFrame;
    }
}
