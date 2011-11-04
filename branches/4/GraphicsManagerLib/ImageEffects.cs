using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GraphicsMgrLib
{
    public class GraphicsMgrImageEffects
    {
        public enum BorderStyle
        {
            Inside, Outside
        }

        public static Image DrawBorder(BorderStyle style, Image img, Color color, int size)
        {
            if (style == BorderStyle.Inside)
            {
                return DrawBorderInside(img, color, size);
            }

            return DrawBorderOutside(img, color, size);
        }

        public static Image DrawBorderInside(Image img, Color color, int size)
        {
            Image result = (Image)img.Clone();

            using (Graphics g = Graphics.FromImage(result))
            using (Pen p = new Pen(color, size))
            {
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawRectangle(p, p.Width / 2, p.Width / 2, result.Size.Width - p.Width, result.Size.Height - p.Width);
            }

            return result;
        }

        public static Image DrawBorderOutside(Image img, Color color, int size)
        {
            Image result = new Bitmap(img.Size.Width + size * 2, img.Size.Height + size * 2);

            using (Graphics g = Graphics.FromImage(result))
            using (Pen p = new Pen(color, size))
            {
                g.DrawImage(img, size, size, img.Size.Width, img.Size.Height);
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawRectangle(p, p.Width / 2, p.Width / 2, result.Size.Width - p.Width, result.Size.Height - p.Width);
            }

            return result;
        }

        public static Image DrawReflection(Image img, int percentage, int transparency, int offset, bool skew, int skewSize)
        {
            Bitmap reflection = AddReflection(img, percentage, transparency);

            if (skew)
            {
                reflection = AddSkew(reflection, skewSize);
            }

            Bitmap result = new Bitmap(reflection.Width, img.Height + reflection.Height + offset);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
                g.DrawImage(reflection, new Point(0, img.Height + offset));
            }

            return result;
        }

        private static Bitmap AddSkew(Bitmap bmp, int skew)
        {
            Bitmap result = new Bitmap(bmp.Width + skew, bmp.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Point[] destinationPoints = { new Point(0, 0), new Point(bmp.Width, 0), new Point(skew, bmp.Height - 1) };
                g.DrawImage(bmp, destinationPoints);
            }

            return result;
        }

        private static Bitmap AddReflection(Image img, int percentage, int transparency)
        {
            Bitmap b = new Bitmap(img);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            b = b.Clone(new Rectangle(0, 0, b.Width, (int)(b.Height * (float)percentage / 100)), PixelFormat.Format32bppArgb);
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            byte alpha;
            int nOffset = bmData.Stride - b.Width * 4;

            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        alpha = (byte)(transparency - transparency * (y + 1) / b.Height);
                        if (p[3] > alpha) p[3] = alpha;
                        p += 4;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return b;
        }

        public static Image Zoom(Image img, int zoom, int size)
        {
            Bitmap bmp = new Bitmap(zoom * size, zoom * size);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(0, 0, size, size), GraphicsUnit.Pixel);
            }

            return DrawGrids(bmp, zoom, Color.Gray, 1);
        }

        public static Image DrawGrids(Image img, int size, Color penColor, int penSize)
        {
            Image result = (Image)img.Clone();

            using (Graphics g = Graphics.FromImage(result))
            using (Pen p = new Pen(new SolidBrush(penColor), penSize))
            {
                for (int x = size; x < img.Width; x += size)
                {
                    g.DrawLine(p, new Point(x, 0), new Point(x, img.Height));
                }

                for (int y = size; y < img.Height; y += size)
                {
                    g.DrawLine(p, new Point(0, y), new Point(img.Width, y));
                }
            }

            return result;
        }

        public static Image FillBackground(Image img, Color color)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(new SolidBrush(color), 0, 0, img.Width, img.Height);
                g.DrawImage(img, Point.Empty);
            }

            return bmp;
        }
    }
}