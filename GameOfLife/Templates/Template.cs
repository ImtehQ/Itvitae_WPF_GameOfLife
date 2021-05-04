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

        public  SolidBrush GetBrush(bool aliveLastFrame, bool aliveCurrentFrame, bool corrupt, bool useLastFrame = false)
        {
            return new SolidBrush(GetColor(aliveLastFrame, aliveCurrentFrame, corrupt, useLastFrame));
        }

        public Color GetColor(bool aliveLastFrame, bool aliveCurrentFrame, bool corrupt, bool useLastFrame = false)
        {
            if (corrupt)
                return color_Corrupt;

            if (useLastFrame)
            {
                if (aliveLastFrame && aliveCurrentFrame == false)
                    return color_UsedToBeAlive;
                if (aliveLastFrame == false && aliveCurrentFrame == true)
                    return color_UsedToBeDead;
            }

            if (aliveCurrentFrame)
                return color_Alive;
            if (aliveCurrentFrame == false)
                return color_Dead;

            return Color.Transparent;
        }
    }
}
