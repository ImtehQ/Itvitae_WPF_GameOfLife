using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameOfLife.Templates
{
    public abstract class Template
    {
        public abstract Color color_Alive { get; set; }
        public abstract Color color_Dead { get; set; }
        public abstract Color color_Corrupt { get; set; }
        public abstract Color color_UsedToBeAlive { get; set; }
        public abstract Color color_UsedToBeDead { get; set; }

        public  SolidBrush GetColor(bool aliveLastFrame, bool aliveCurrentFrame, bool corrupt, bool useLastFrame = false)
        {
            if (corrupt)
                return new SolidBrush(color_Corrupt);

            if(useLastFrame)
            {
                if (aliveLastFrame && aliveCurrentFrame == false)
                    return new SolidBrush(color_UsedToBeAlive);
                if (aliveLastFrame == false && aliveCurrentFrame == true)
                    return new SolidBrush(color_UsedToBeDead);
            }

            if (aliveCurrentFrame)
                return new SolidBrush(color_Alive);
            if(aliveCurrentFrame == false)
                return new SolidBrush(color_Dead);

            return new SolidBrush(Color.Transparent);
        }
    }
}
