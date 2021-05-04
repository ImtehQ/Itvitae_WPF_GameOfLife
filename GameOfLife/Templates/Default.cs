using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameOfLife.Templates
{
    public class Default : Template
    {
        public override Color color_Alive { get { return Color.LightSeaGreen ; } set => throw new NotImplementedException(); }
        public override Color color_Dead { get { return Color.Black; } set => throw new NotImplementedException(); }
        public override Color color_Corrupt { get { return Color.Yellow; } set => throw new NotImplementedException(); }
        public override Color color_UsedToBeAlive { get { return Color.DarkGreen; } set => throw new NotImplementedException(); }
        public override Color color_UsedToBeDead { get { return Color.DarkGray; } set => throw new NotImplementedException(); }
    }
}
