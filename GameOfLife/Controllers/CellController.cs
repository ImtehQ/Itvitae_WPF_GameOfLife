using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameOfLife.Models;
using GameOfLife.Rules;

namespace GameOfLife.Controllers
{
    public class CellController
    {
        //The cell this controller will handle
        private Cell cell = new Cell();

        //Saves us some count calls.
        private int neighborsAliveCount = 0;

        private Rule rule = new Default();


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
        public void UpdateAlive(bool autoUpdate)
        {
            CheckCoruption();

            if (cell.Corrupt)
                return;

            //Will be called lots of time, so its a global variable
            neighborsAliveCount = cell.neighbors.Count(n => n.aliveLastFrame == true);

            //Before we set the cells new state, we save its last state
            cell.aliveLastFrame = cell.aliveCurrentFrame;
            //Its not needed for the game, but does add a ton of extra options.
            cell.aliveCurrentFrame = rule.NewState(cell.aliveLastFrame, neighborsAliveCount);

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
        }

        /// <summary>
        /// Only used to overwrite cell status
        /// </summary>
        /// <param name="value"></param>
        public void SetAlive(bool value, bool includeLastFrame, bool toggle = false)
        {
            if (toggle)
                cell.aliveCurrentFrame = !cell.aliveCurrentFrame;
            else
                cell.aliveCurrentFrame = value;

            if (includeLastFrame)
                cell.aliveLastFrame = cell.aliveCurrentFrame;
        }

        public bool GetAliveCurrentFrame()
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
