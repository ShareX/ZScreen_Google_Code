using System.Drawing;
using System.Drawing.Imaging;

namespace GraphicsMgrLib
{
    public static class Extensions
    {
        public static void DrawShadow(this Graphics g, Bitmap shadowBitmap, int x, int y, int width, int height)
        {
            using (Brush brush = new TextureBrush(shadowBitmap))
            using (Bitmap bmpTemp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            using (Graphics gTemp = Graphics.FromImage(bmpTemp))
            {
                // Draw on a temp bitmap with (0,0) offset, because the texture starts at (0,0)
                gTemp.FillRectangle(brush, 0, 0, width, height);
                g.DrawImage(bmpTemp, x, y);
            }
        }
    }
}