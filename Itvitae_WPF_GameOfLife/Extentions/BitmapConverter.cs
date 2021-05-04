using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Itvitae_WPF_GameOfLife.Extentions
{
    public static class BitmapConverter
    {
        /// <summary>
        /// REALLY SLOW!
        /// </summary>
        /// <param name="src"></param>
        /// <param name="fromSize"></param>
        /// <param name="toSize"></param>
        /// <returns></returns>
        public static BitmapImage Convert(this Bitmap src, int fromSize, int toSize)
        {
            //src = src.Rescale(fromSize, toSize);
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
           
            return image;
        }

        public static Bitmap Rescale(this Bitmap src, int fromSize, int toSize)
        {
            var resultImage = new Bitmap(toSize, toSize);
            var resultGraphics = Graphics.FromImage(resultImage);
            int factor = toSize / fromSize;
            for (int y = 0; y < fromSize; y++)
            {
                for (int x = 0; x < fromSize; x++)
                {
                    resultGraphics.FillRectangle(new SolidBrush(src.GetPixel(x,y)), x, y, factor, factor);
                }
            }
            return resultImage;
        } 
    }
}
