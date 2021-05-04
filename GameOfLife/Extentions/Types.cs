using System;
namespace GameOfLife.Extentions
{
    public class Types
    {

    }

    public class Point2
    {
        public int X { get; }
        public int Y { get; }

        public Point2(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Point2(double x, double y)
        {
            this.X = Convert.ToInt32(x);
            this.Y = Convert.ToInt32(y);
        }
    }
}
