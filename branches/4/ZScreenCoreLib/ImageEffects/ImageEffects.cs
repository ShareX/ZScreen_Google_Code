using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GradientTester;
using GraphicsMgrLib;
using HelpersLib;
using ZScreenCoreLib;

#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

namespace ZScreenCoreLib
{
    public class ImageEffects
    {
        #region 0 Properties

        private ImageEffectsConfig Config = null;
        private static List<int> mLogoRandomList = new List<int>(5);

        #endregion 0 Properties

        #region 0 Constructors

        protected ImageEffects() { }

        public ImageEffects(ImageEffectsConfig cfg)
        {
            this.Config = cfg;
        }

        #endregion 0 Constructors

        protected static Bitmap AddSkew(Bitmap bmp, int skew)
        {
            Bitmap result = new Bitmap(bmp.Width + skew, bmp.Height);
            Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Point[] destinationPoints = { new Point(0, 0), new Point(bmp.Width, 0), new Point(skew, bmp.Height - 1) };
            g.DrawImage(bmp, destinationPoints);
            return result;
        }

        public Image ApplyScreenshotEffects(Image img)
        {
            if (Config.BevelEffect)
            {
                img = BevelImage(img, Config.BevelEffectOffset);
            }
            if (Config.DrawReflection)
            {
                img = DrawReflection(img);
            }
            if (Config.BorderEffect)
            {
                img = DrawBorder(img, Config.BorderEffectColor, Config.BorderEffectSize);
            }
            return img;
        }

        public Image ApplySizeChanges(Image img)
        {
            switch (Config.ImageSizeType)
            {
                case ImageSizeType.FIXED:
                    img = GraphicsMgr.ChangeImageSize(img, Config.ImageSizeFixedWidth, Config.ImageSizeFixedHeight, true, true);
                    break;
                case ImageSizeType.RATIO:
                    img = GraphicsMgr.ChangeImageSize(img, Config.ImageSizeRatioPercentage);
                    break;
            }
            return img;
        }

        protected Image BevelImage(Image image, int offset)
        {
            Image defaultImage = (Image)image.Clone();

            Point[] topPoints = {
                                    new Point(0, 0), //Top left
                                    new Point(image.Width, 0), //Top right
                                    new Point(image.Width - offset, offset), //Bottom right
                                    new Point(offset, offset) //Bottom left
                                };
            Point[] leftPoints = {
                                     new Point(0, 0), //Top left
                                     new Point(offset, offset), //Top right
                                     new Point(offset, image.Height - offset), //Bottom right
                                     new Point(0, image.Height) //Bottom left
                                 };
            Point[] bottomPoints = {
                                       new Point(offset, image.Height - offset), //Top left
                                       new Point(image.Width - offset, image.Height - offset), //Top right
                                       new Point(image.Width, image.Height), //Bottom right
                                       new Point(0, image.Height) //Bottom left
                                   };
            Point[] rightPoints = {
                                      new Point(image.Width - offset, offset), //Top left
                                      new Point(image.Width, 0), //Top right
                                      new Point(image.Width, image.Height), //Bottom right
                                      new Point(image.Width - offset, image.Height - offset) //Bottom left
                                  };

            PrepareBevel(defaultImage, topPoints, 25);
            PrepareBevel(defaultImage, leftPoints, 50);
            PrepareBevel(defaultImage, bottomPoints, -25);
            PrepareBevel(defaultImage, rightPoints, -50);

            return defaultImage;
        }

        protected static Image DrawBorder(Image img, Color color, int size)
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

        public static Image DrawCheckers(Image img)
        {
            return DrawCheckers(img, Color.White, Color.LightGray, 10);
        }

        public static Image DrawCheckers(Image img, Color color1, Color color2, int boxSize)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Brush[] brush = new Brush[2] { new SolidBrush(color1), new SolidBrush(color2) };

            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int y = 0; y <= img.Height / boxSize; y++)
                {
                    for (int x = 0; x <= img.Width / boxSize; x++)
                    {
                        g.FillRectangle(brush[(x + y) % 2], x * boxSize, y * boxSize, boxSize, boxSize);
                    }
                }

                g.DrawImage(img, Point.Empty);
            }

            return bmp;
        }

        protected Image DrawReflection(Image bmp)
        {
            Bitmap reflection = WatermarkEffects.AddReflection(bmp, Config.ReflectionPercentage, Config.ReflectionTransparency);
            if (Config.ReflectionSkew)
            {
                reflection = AddSkew(reflection, Config.ReflectionSkewSize);
            }
            Bitmap result = new Bitmap(reflection.Width, bmp.Height + reflection.Height + Config.ReflectionOffset);
            Graphics g = Graphics.FromImage(result);
            g.DrawImage(bmp, new Point(0, 0));
            g.DrawImage(reflection, new Point(0, bmp.Height + Config.ReflectionOffset));
            return result;
        }

        /// <summary>
        /// Creates a new image with the transparency removed by capturing
        /// the given image over a background of the given color.
        /// </summary>
        public static Image FillBackground(Image img, Color color)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(color);
                g.DrawImage(img, Point.Empty);
            }

            return bmp;
        }

        protected static Point FindPosition(WatermarkPositionType positionType, int offset, Size img, Size img2, int add)
        {
            Point position;
            switch (positionType)
            {
                case WatermarkPositionType.TOP_LEFT:
                    position = new Point(offset, offset);
                    break;
                case WatermarkPositionType.TOP_RIGHT:
                    position = new Point(img.Width - img2.Width - offset - add, offset);
                    break;
                case WatermarkPositionType.BOTTOM_LEFT:
                    position = new Point(offset, img.Height - img2.Height - offset - add);
                    break;
                case WatermarkPositionType.BOTTOM_RIGHT:
                    position = new Point(img.Width - img2.Width - offset - add, img.Height - img2.Height - offset - add);
                    break;
                case WatermarkPositionType.CENTER:
                    position = new Point(img.Width / 2 - img2.Width / 2 - add, img.Height / 2 - img2.Height / 2 - add);
                    break;
                case WatermarkPositionType.LEFT:
                    position = new Point(offset, img.Height / 2 - img2.Height / 2 - add);
                    break;
                case WatermarkPositionType.TOP:
                    position = new Point(img.Width / 2 - img2.Width / 2 - add, offset);
                    break;
                case WatermarkPositionType.RIGHT:
                    position = new Point(img.Width - img2.Width - offset - add, img.Height / 2 - img2.Height / 2 - add);
                    break;
                case WatermarkPositionType.BOTTOM:
                    position = new Point(img.Width / 2 - img2.Width / 2 - add, img.Height - img2.Height - offset - add);
                    break;
                default:
                    position = Point.Empty;
                    break;
            }
            return position;
        }

        public static Bitmap GetRandomLogo(Bitmap logo)
        {
            Bitmap bmp = new Bitmap(logo);
            if (mLogoRandomList.Count == 0)
            {
                List<int> numbers = Enumerable.Range(1, 7).ToList();

                int count = numbers.Count;

                for (int x = 0; x < count; x++)
                {
                    int r = ZAppHelper.Random.Next(0, numbers.Count - 1);
                    mLogoRandomList.Add(numbers[r]);
                    numbers.RemoveAt(r);
                }
            }

            switch (mLogoRandomList[0])
            {
                case 1:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    break;
                case 2:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.GrayscaleFilter());
                    break;
                case 3:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.GrayscaleFilter());
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    break;
                case 4:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(ZAppHelper.Random.Next(100, 300)));
                    break;
                case 5:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(ZAppHelper.Random.Next(-300, -100)));
                    break;
                case 6:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(ZAppHelper.Random.Next(150, 300)));
                    break;
                case 7:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(ZAppHelper.Random.Next(-300, -150)));
                    break;
            }

            mLogoRandomList.RemoveAt(0);

            return logo;
        }

        private Image PrepareBevel(Image image, Point[] points, int filterValue)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(bmp);
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(points);
            g.Clear(Color.Transparent);
            g.SetClip(gp);
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
            g.Dispose();
            ColorMatrix cm;
            switch (Config.BevelFilterType)
            {
                default:
                case FilterType.Brightness:
                    cm = ColorMatrices.BrightnessFilter(filterValue);
                    break;
                case FilterType.Contrast:
                    cm = ColorMatrices.ContrastFilter(filterValue);
                    break;
                case FilterType.Saturation:
                    cm = ColorMatrices.SaturationFilter(filterValue);
                    break;
            }
            bmp = ColorMatrices.ApplyColorMatrix(bmp, cm);
            Graphics g2 = Graphics.FromImage(image);
            g2.DrawImage(bmp, new Rectangle(0, 0, image.Width, image.Height));
            g2.Dispose();
            return image;
        }

        public class TurnImage
        {
            private Bitmap bitmap;
            private Graphics g;
            private Image image1, image2;
            public delegate void ImageEventHandler(Image img);
            public event ImageEventHandler ImageTurned;
            public bool IsTurning;
            private int progress = 1;

            private void ReportImage(Image img)
            {
                if (ImageTurned != null)
                {
                    ImageTurned(bitmap);
                }
            }

            private void ResetSettings()
            {
                IsTurning = timer.Enabled = false;
                progress = step = 1;

                ReportImage(image1);
            }

            private int speed = 5;

            public void StartTurn()
            {
                if (!IsTurning && !timer.Enabled)
                {
                    IsTurning = timer.Enabled = true;
                }
            }

            private int step = 1;

            public void StopTurn()
            {
                IsTurning = timer.Enabled = false;
            }

            private Timer timer;

            private void timer_Tick(object sender, EventArgs e)
            {
                if (progress > bitmap.Width + speed)
                {
                    if (step == 1)
                    {
                        step = 2;
                        progress = 0;
                    }
                    else
                    {
                        ResetSettings();
                        return;
                    }
                }

                g.Clear(Color.Transparent);

                if (step == 1)
                {
                    if (progress < bitmap.Width / 2)
                    {
                        g.DrawImage(image1, new Rectangle(progress, 0, bitmap.Width - progress * 2, bitmap.Height));
                    }
                    else
                    {
                        g.DrawImage(image2, new Rectangle(bitmap.Width - progress, 0, (progress - bitmap.Width / 2) * 2, bitmap.Height));
                    }
                }
                else
                {
                    if (progress < bitmap.Width / 2)
                    {
                        g.DrawImage(image2, new Rectangle(progress, 0, bitmap.Width - progress * 2, bitmap.Height));
                    }
                    else
                    {
                        g.DrawImage(image1, new Rectangle(bitmap.Width - progress, 0, (progress - bitmap.Width / 2) * 2, bitmap.Height));
                    }
                }

                progress += speed;

                ReportImage(bitmap);
            }

            public TurnImage(Image img)
            {
                image1 = (Image)img.Clone();
                image2 = (Image)img.Clone();
                image2.RotateFlip(RotateFlipType.RotateNoneFlipX);

                bitmap = new Bitmap(image1.Width, image2.Height);

                g = Graphics.FromImage(bitmap);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                timer = new Timer { Interval = 25 };
                timer.Tick += new EventHandler(timer_Tick);
            }
        }
    }
}