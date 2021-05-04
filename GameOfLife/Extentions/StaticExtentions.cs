namespace GameOfLife.Extentions
{
    public static class StaticExtentions
    {
        /// <summary>
        /// Remap 1 value to another
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from1"></param>
        /// <param name="to1"></param>
        /// <param name="from2"></param>
        /// <param name="to2"></param>
        /// <returns></returns>
        public static int Remap(this int value, int toMin, int toMax, int inputMin, int inputMax)
        {
            return toMin + (value - inputMin) * (toMax - toMin) / (inputMax - inputMin);
        }

    }
}
