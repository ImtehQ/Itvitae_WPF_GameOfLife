using System;
using System.Drawing;
using GameOfLife.Extentions;

namespace GameOfLife.Controllers
{
    public class MapController
    {

        private Random randomFill;
        private int cellWidth = 100;
        public CellController[,] CellControllers;

        private Bitmap mapData;
        private Graphics mapDataGraphics;



        public Bitmap GenerateBitMapData()
        {
            mapData = new Bitmap(cellWidth, cellWidth);
            mapDataGraphics = Graphics.FromImage(mapData);
            mapDataGraphics.FillRectangle(Brushes.Black, 0, 0, cellWidth, cellWidth);
            return mapData;
        }

        public void UpdateBitMapData()
        {
            for (int y = 0; y < cellWidth; y++)
            {
                for (int x = 0; x < cellWidth; x++)
                {
                    if (CellControllers[x, y].GetCorrupt() == true)
                    {
                        mapDataGraphics.FillRectangle(Brushes.Red, x, y, 1, 1);
                        continue;
                    }
                    if (CellControllers[x, y].GetAlive() == true)
                        mapDataGraphics.FillRectangle(Brushes.White, x, y, 1, 1);
                    else
                        mapDataGraphics.FillRectangle(Brushes.Black, x, y, 1, 1);
                }
            }
        }

        public Bitmap GetBitMapData()
        {
            return mapData;
        }

        /// <summary>
        /// Change the cell alive status
        /// </summary>
        /// <param name="PointOnScreen"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="newState"></param>
        public void ChangeCellAliveStatus(Point2 PointOnScreen, int screenWidth, int screenHeight, bool newState)
        {
            Point2 arrayPoint = GetArrayLocation(PointOnScreen, screenWidth, screenHeight);

            CellControllers[arrayPoint.X, arrayPoint.Y].SetAlive(newState, true);

        }

        /// <summary>
        /// Change the cell corrupt status
        /// </summary>
        /// <param name="PointOnScreen"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="newState"></param>
        public void ChangeCellCorruptStatus(Point2 PointOnScreen, int screenWidth, int screenHeight, bool newState)
        {
            Point2 arrayPoint = GetArrayLocation(PointOnScreen, screenWidth, screenHeight);
            CellControllers[arrayPoint.X, arrayPoint.Y].SetCorrupt(newState);
        }

        public MapController(int mapSize)
        {
            cellWidth = mapSize;
        }

        /// <summary>
        /// Generate a sleeping world
        /// </summary>
        public void GenerateWorldMap()
        {
            CellControllers = new CellController[cellWidth, cellWidth];

            for (int y = 0; y < cellWidth; y++)
            {
                for (int x = 0; x < cellWidth; x++)
                {
                    CellControllers[x, y] = new CellController();
                }
            }
        }

        /// <summary>
        /// Wake up each cell and tell them about there own neighbors
        /// </summary>
        public void PopulateWorldMap()
        {
            for (int y = 1; y < cellWidth - 1; y++)
            {
                for (int x = 1; x < cellWidth - 1; x++)
                {
                    CellControllers[x, y].AddNeighbor(CellControllers[x, y - 1]);
                    CellControllers[x, y].AddNeighbor(CellControllers[x + 1, y - 1]);
                    CellControllers[x, y].AddNeighbor(CellControllers[x + 1, y]);
                    CellControllers[x, y].AddNeighbor(CellControllers[x + 1, y + 1]);
                    CellControllers[x, y].AddNeighbor(CellControllers[x, y + 1]);
                    CellControllers[x, y].AddNeighbor(CellControllers[x - 1, y + 1]);
                    CellControllers[x, y].AddNeighbor(CellControllers[x - 1, y]);
                    CellControllers[x, y].AddNeighbor(CellControllers[x - 1, y - 1]);
                    CellControllers[x, y].SetCorrupt(false);
                }
            }
        }

        /// <summary>
        /// Check each cell if death or alive
        /// </summary>
        public void CheckWorldMap()
        {
            for (int y = 0; y < cellWidth; y++)
            {
                for (int x = 0; x < cellWidth; x++)
                {
                    CellControllers[x, y].UpdateAlive();
                }
            }
        }

        /// <summary>
        /// Update the intire worldmap all at once
        /// </summary>
        public void UpdateWorldMap()
        {
            for (int y = 0; y < cellWidth; y++)
            {
                for (int x = 0; x < cellWidth; x++)
                {
                    CellControllers[x, y].UpdateStatus();
                }
            }
        }

        /// <summary>
        /// Randomly fill in the world
        /// chance range 1-100
        /// </summary>
        public void RandomAlive(int chance = 10, int seed = 3456)
        {
            if (chance <= 1 || chance > 100)
                return;

            if (seed > 0)
                randomFill = new Random(seed);

            for (int y = 0; y < cellWidth; y++)
            {
                for (int x = 0; x < cellWidth; x++)
                {
                    CellControllers[x, y].SetAlive(randomFill.Next(0, chance) == 1 ? true : false, true);
                }
            }
        }

        /// <summary>
        /// Converts the on screen location to a array location
        /// </summary>
        /// <param name="PointOnScreen"></param>
        /// <returns></returns>
        private Point2 GetArrayLocation(Point2 PointOnScreen, int screenWidth, int screenHeight)
        {
            return new Point2(PointOnScreen.X.Remap(0, cellWidth, 0, screenWidth), PointOnScreen.Y.Remap(0, cellWidth, 0, screenHeight));
        }
    }
}
