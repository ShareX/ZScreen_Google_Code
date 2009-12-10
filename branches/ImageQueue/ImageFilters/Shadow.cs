using System.Drawing;
using Plugins;
using GraphicsManager;
using System.Drawing.Imaging;
using System.ComponentModel;
using System;

namespace ImageFilters
{
    public class Shadow : IPluginItem
    {
        public override string Name { get { return "Shadow"; } }

        public override string Description { get { return "Shadow"; } }

        private int radius;

        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value.Between(0, 25);
            }
        }

        private int offsetX;

        public int OffsetX
        {
            get
            {
                return offsetX;
            }
            set
            {
                offsetX = value;
            }
        }

        private int offsetY;

        public int OffsetY
        {
            get
            {
                return offsetY;
            }
            set
            {
                offsetY = value;
            }
        }

        private int transparency;

        public int Transparency
        {
            get
            {
                return transparency;
            }
            set
            {
                transparency = value;
            }
        }

        public Shadow()
        {
            radius = 5;
            offsetX = 3;
            offsetY = 3;
            transparency = 50;
        }

        public override Image ApplyEffect(Image img)
        {
            ColorMatrix matrix = ColorMatrixMgr.IdentityMatrix;
            matrix[4, 0] = matrix[4, 1] = matrix[4, 2] = -1.0f;

            using (Image blackImage = ColorMatrixMgr.ApplyColorMatrix(img, matrix))
            using (Image shadow = Blur.ApplyBlur((Bitmap)blackImage, radius, true))
            using (Image shadow2 = ColorMatrixMgr.ApplyColorMatrix(shadow, ColorMatrixMgr.Alpha(transparency, 0)))
            {
                Bitmap result = new Bitmap(shadow.Width + Math.Abs(offsetX), shadow.Height + Math.Abs(offsetY));

                using (Graphics g = Graphics.FromImage(result))
                {
                    Rectangle shadowRect = new Rectangle(0, 0, shadow.Width, shadow.Height);
                    Rectangle imageRect = new Rectangle(radius, radius, img.Width, img.Height);

                    if (offsetX < 0)
                    {
                        imageRect.X += -offsetX + Math.Min(radius, -offsetX);
                    }
                    else
                    {
                        imageRect.X = Math.Max(0, radius - offsetX);
                        shadowRect.X = offsetX;
                    }

                    if (offsetY < 0)
                    {
                        imageRect.Y += -offsetY + Math.Min(radius, -offsetX);
                    }
                    else
                    {
                        imageRect.Y = Math.Max(0, radius - offsetY);
                        shadowRect.Y = offsetY;
                    }

                    g.DrawImage(shadow2, shadowRect);
                    g.DrawImage(img, imageRect);
                }

                return result;
            }
        }
    }
}