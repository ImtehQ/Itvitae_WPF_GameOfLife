using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Itvitae_WPF_GameOfLife.Controllers
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?redirectedfrom=MSDN&view=net-5.0

    public class MapDisplayController
    {
        private WriteableBitmap writeableBitmap;

        public MapDisplayController(int width, int height)
        {
            writeableBitmap = new WriteableBitmap(
                width,
                height,
                96,
                96,
                PixelFormats.Bgr32,
                null);
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b, byte a)
        {
            try
            {
                // Reserve the back buffer for updates.
                writeableBitmap.Lock();
                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = writeableBitmap.BackBuffer;

                    // Find the address of the pixel to draw.
                    pBackBuffer += x * writeableBitmap.BackBufferStride;
                    pBackBuffer += y * 4;

                    // Compute the pixel's color.
                    int color_data = r << 16; // R
                    color_data |= g << 8;   // G
                    color_data |= b << 0;   // B

                    // Assign the color data to the pixel.
                    *((int*)pBackBuffer) = color_data;
                }
                // Specify the area of the bitmap that changed.
                writeableBitmap.AddDirtyRect(new Int32Rect(x, y, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                writeableBitmap.Unlock();
            }
        }

        public WriteableBitmap GetMap()
        {
            return writeableBitmap;
        }
    }
}
