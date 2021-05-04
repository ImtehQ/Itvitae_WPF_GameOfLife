using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameOfLife.Models;

namespace GameOfLife.Controllers
{
    public class CellController
    {
        //The cell this controller will handle
        private Cell cell = new Cell();

        //Saves us some count calls.
        private int neighborsAliveCount = 0;
       
        private void CheckCoruption()
        {
            if (cell.neighbors.Count < 8) //8 is the magic number
                cell.Corrupt = true;
        }
        /// <summary>
        /// Add surounding cells to the cell
        /// </summary>
        /// <param name="cellController"></param>
        public void AddNeighbor(CellController cellController)
        {
            cell.neighbors.Add(cellController.cell);
        }

        /// <summary>
        /// Check the current state of the cell and updates the cell
        /// </summary>
        /// <param name="autoUpdate"></param>
        public void UpdateAlive(bool autoUpdate = true)
        {
            CheckCoruption();

            if (cell.Corrupt)
                return;

            //Will be called lots of time, so its a global variable
            neighborsAliveCount = cell.neighbors.Count(n => n.aliveLastFrame == true);

            if (neighborsAliveCount <= 1 && cell.aliveLastFrame)
                cell.aliveCurrentFrame = false; //dies
            else if (neighborsAliveCount >= 4 && cell.aliveLastFrame)
                cell.aliveCurrentFrame = false; //dies
            else if (neighborsAliveCount == 3 && cell.aliveLastFrame ==false)
                cell.aliveCurrentFrame = true; //Born
            else if (neighborsAliveCount >= 2 && cell.aliveLastFrame)
                cell.aliveCurrentFrame = true; // stays alive
            else if (neighborsAliveCount >= 2 && cell.aliveLastFrame == false)
                cell.aliveCurrentFrame = false; // stays dead
            //else
            //    cell.Corrupt = true;

            if (autoUpdate)
                UpdateStatus();
        }

        /// <summary>
        /// Updates the alive status
        /// </summary>
        public void UpdateStatus()
        {
            CheckCoruption();

            if (cell.Corrupt)
                return;

            if (cell.aliveLastFrame == true && cell.aliveCurrentFrame == false)
            {
                //It died
            }
            if (cell.aliveLastFrame == false && cell.aliveCurrentFrame == true)
            {
                //It was born
            }
            if (cell.aliveLastFrame == true && cell.aliveCurrentFrame == true)
            {
                //It was was not changed
            }
            if (cell.aliveLastFrame == false && cell.aliveCurrentFrame == false)
            {
                //It was was not changed
            }

            cell.aliveLastFrame = cell.aliveCurrentFrame;
        }

        /// <summary>
        /// Only used to overwrite cell status
        /// </summary>
        /// <param name="value"></param>
        public void SetAlive(bool value, bool includeLastFrame)
        {
            cell.aliveCurrentFrame = value;
            if (includeLastFrame)
                cell.aliveLastFrame = value;
        }

        public bool GetAlive()
        {
            return cell.aliveCurrentFrame;
        }
        public bool GetAliveLastFrame()
        {
            return cell.aliveLastFrame;
        }

        /// <summary>
        /// Mark the cell as corrupt or not
        /// </summary>
        /// <param name="value"></param>
        public void SetCorrupt(bool value)
        {
            cell.Corrupt = value;
        }

        public bool GetCorrupt()
        {
            return cell.Corrupt;
        }
    }
}
