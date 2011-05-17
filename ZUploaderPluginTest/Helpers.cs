using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ZUploaderCropPlugin
{
    public static class Helpers
    {
        public static Rectangle FixRectangle(int x, int y, int width, int height)
        {
            if (width < 0) x += width;
            if (height < 0) y += height;

            return new Rectangle(x, y, Math.Abs(width), Math.Abs(height));
        }

        public static Rectangle GetScreenBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        public static Bitmap GetScreenshot()
        {
            Rectangle bounds = GetScreenBounds();

            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size, CopyPixelOperation.SourceCopy);
            }

            return screenshot;
        }

        public static Image CropImage(Image img, Rectangle rect)
        {
            Image bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            return bmp;
        }
    }
}