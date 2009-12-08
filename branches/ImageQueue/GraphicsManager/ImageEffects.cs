using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GraphicsManager
{
    public class ImageEffects
    {
        public static Image DrawBorder(Image img, Color color, int size)
        {
            Image result = new Bitmap(img.Size.Width + (size * 2), img.Size.Height + (size * 2));
            Graphics g = Graphics.FromImage(result);

            using (Pen p = new Pen(color, size))
            {
                g.DrawRectangle(p, 1, 1, result.Size.Width - size, result.Size.Height - size);
            }
            g.DrawImage(img, size, size);

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
            Graphics g = Graphics.FromImage(result);
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            g.DrawImage(reflection, new Point(0, img.Height + offset));
            return result;
        }

        private static Bitmap AddSkew(Bitmap bmp, int skew)
        {
            Bitmap result = new Bitmap(bmp.Width + skew, bmp.Height);
            Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Point[] destinationPoints = { new Point(0, 0), new Point(bmp.Width, 0), new Point(skew, bmp.Height - 1) };
            g.DrawImage(bmp, destinationPoints);
            return result;
        }

        private static Bitmap AddReflection(Image img, int percentage, int transparency)
        {
            Bitmap b = new Bitmap(img);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            b = b.Clone(new Rectangle(0, 0, b.Width, (int)(b.Height * ((float)percentage / 100))), PixelFormat.Format32bppArgb);
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            byte alpha;
            int nOffset = bmData.Stride - b.Width * 4;
            transparency.Between(0, 255);

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
    }
}