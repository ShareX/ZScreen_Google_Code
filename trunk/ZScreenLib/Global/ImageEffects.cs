#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Text;
using ZSS;
using System.Collections.Generic;
using System.Linq;
using Greenshot.Drawing;
using GradientTester;
using HelpersLib;

namespace ZScreenLib
{
    public static class ImageEffects
    {
        private static List<int> mLogoRandomList = new List<int>(5);

        public static Bitmap GetRandomLogo(Bitmap logo)
        {
            Bitmap bmp = new Bitmap(logo);
            if (mLogoRandomList.Count == 0)
            {
                List<int> numbers = Enumerable.Range(1, 7).ToList();

                int count = numbers.Count;

                for (int x = 0; x < count; x++)
                {
                    int r = Adapter.RandomNumber(0, numbers.Count - 1);
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
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(Adapter.RandomNumber(100, 300)));
                    break;
                case 5:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(Adapter.RandomNumber(-300, -100)));
                    break;
                case 6:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(Adapter.RandomNumber(150, 300)));
                    break;
                case 7:
                    logo = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(Adapter.RandomNumber(-300, -150)));
                    break;
            }

            mLogoRandomList.RemoveAt(0);

            return logo;
        }

        /// <summary>Get Image with Watermark</summary>
        public static Image ApplyWatermark(Image img)
        {
            return ApplyWatermark(img, Engine.conf.WatermarkMode);
        }

        /// <summary>Get Image with Watermark</summary>
        public static Image ApplyWatermark(Image img, WatermarkType watermarkType)
        {
            switch (watermarkType)
            {
                default:
                case WatermarkType.NONE:
                    return img;
                case WatermarkType.TEXT:
                    return DrawWatermark(img, NameParser.Convert(new NameParserInfo(NameParserType.Watermark, Engine.conf.WatermarkText) { IsPreview = true, Picture = img }));
                case WatermarkType.IMAGE:
                    return DrawImageWatermark(img, Engine.conf.WatermarkImageLocation);
            }
        }

        private static Image DrawWatermark(Image img, string drawText)
        {
            if (!string.IsNullOrEmpty(drawText))
            {
                if (0 == Engine.conf.WatermarkFont.Size)
                {
                    Adapter.ShowFontDialog();
                }
                try
                {
                    int offset = (int)Engine.conf.WatermarkOffset;
                    Font font = XMLSettings.DeserializeFont(Engine.conf.WatermarkFont);
                    Size textSize = TextRenderer.MeasureText(drawText, font);
                    Size labelSize = new Size(textSize.Width + 10, textSize.Height + 10);
                    Point labelPosition = FindPosition(Engine.conf.WatermarkPositionMode, offset, img.Size,
                        new Size(textSize.Width + 10, textSize.Height + 10), 1);
                    if (Engine.conf.WatermarkAutoHide && ((img.Width < labelSize.Width + offset) ||
                        (img.Height < labelSize.Height + offset)))
                    {
                        return img;
                        //throw new Exception("Image size smaller than watermark size.");
                    }
                    Rectangle labelRectangle = new Rectangle(Point.Empty, labelSize);
                    GraphicsPath gPath = RoundedRectangle.Create(labelRectangle, (int)Engine.conf.WatermarkCornerRadius);

                    int backTrans = (int)Engine.conf.WatermarkBackTrans;
                    int fontTrans = (int)Engine.conf.WatermarkFontTrans;
                    Color fontColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkFontColor);
                    Bitmap bmp = new Bitmap(labelRectangle.Width + 1, labelRectangle.Height + 1);
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    LinearGradientBrush brush;
                    if (Engine.conf.WatermarkUseCustomGradient)
                    {
                        brush = GradientMaker.CreateGradientBrush(labelRectangle.Size, Engine.conf.GradientMakerOptions.GetBrushDataActive());
                    }
                    else
                    {
                        brush = new LinearGradientBrush(labelRectangle, Color.FromArgb(backTrans, XMLSettings.DeserializeColor(Engine.conf.WatermarkGradient1)),
                            Color.FromArgb(backTrans, XMLSettings.DeserializeColor(Engine.conf.WatermarkGradient2)), Engine.conf.WatermarkGradientType);
                    }
                    g.FillPath(brush, gPath);
                    g.DrawPath(new Pen(Color.FromArgb(backTrans, XMLSettings.DeserializeColor(Engine.conf.WatermarkBorderColor))), gPath);
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(drawText, font, new SolidBrush(Color.FromArgb(fontTrans, fontColor)), bmp.Width / 2, bmp.Height / 2, sf);
                    Graphics gImg = Graphics.FromImage(img);
                    gImg.SmoothingMode = SmoothingMode.HighQuality;
                    gImg.DrawImage(bmp, labelPosition);
                    if (Engine.conf.WatermarkAddReflection)
                    {
                        Bitmap bmp2 = AddReflection(bmp, 50, 200);
                        gImg.DrawImage(bmp2, new Rectangle(labelPosition.X, labelPosition.Y + bmp.Height - 1, bmp2.Width, bmp2.Height));
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug("Errow while drawing watermark", ex);
                }
            }

            return img;
        }

        private static Image DrawImageWatermark(Image img, string imgPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                {
                    int offset = (int)Engine.conf.WatermarkOffset;
                    Image img2 = Image.FromFile(imgPath);
                    img2 = GraphicsMgr.ChangeImageSize((Bitmap)img2, (float)Engine.conf.WatermarkImageScale);
                    Point imgPos = FindPosition(Engine.conf.WatermarkPositionMode, offset, img.Size, img2.Size, 0);
                    if (Engine.conf.WatermarkAutoHide && ((img.Width < img2.Width + offset) ||
                        (img.Height < img2.Height + offset)))
                    {
                        return img;
                        //throw new Exception("Image size smaller than watermark size.");
                    }

                    Graphics g = Graphics.FromImage(img);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(img2, imgPos);
                    if (Engine.conf.WatermarkAddReflection)
                    {
                        Bitmap bmp = AddReflection((Bitmap)img2, 50, 200);
                        g.DrawImage(bmp, new Rectangle(imgPos.X, imgPos.Y + img2.Height - 1, bmp.Width, bmp.Height));
                    }
                    if (Engine.conf.WatermarkUseBorder)
                    {
                        g.DrawRectangle(new Pen(Color.Black), new Rectangle(imgPos.X, imgPos.Y, img2.Width - 1, img2.Height - 1));
                    }
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Error while drwaing image watermark", ex);
            }
            return img;
        }

        public static Image ApplyScreenshotEffects(Image img)
        {
            if (Engine.conf.BevelEffect)
            {
                img = BevelImage(img, Engine.conf.BevelEffectOffset);
            }
            if (Engine.conf.DrawReflection)
            {
                img = DrawReflection(img);
            }
            if (Engine.conf.BorderEffect)
            {
                img = DrawBorder(img, Engine.conf.BorderEffectColor, Engine.conf.BorderEffectSize);
            }
            return img;
        }

        private static Image DrawBorder(Image img, Color color, int size)
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

        private static Image DrawReflection(Image bmp)
        {
            Bitmap reflection = AddReflection(bmp, Engine.conf.ReflectionPercentage, Engine.conf.ReflectionTransparency);
            if (Engine.conf.ReflectionSkew)
            {
                reflection = AddSkew(reflection, Engine.conf.ReflectionSkewSize);
            }
            Bitmap result = new Bitmap(reflection.Width, bmp.Height + reflection.Height + Engine.conf.ReflectionOffset);
            Graphics g = Graphics.FromImage(result);
            g.DrawImage(bmp, new Point(0, 0));
            g.DrawImage(reflection, new Point(0, bmp.Height + Engine.conf.ReflectionOffset));
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

        private static Bitmap AddReflection(Image bmp, int percentage, int transparency)
        {
            Bitmap b = new Bitmap(bmp);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            b = b.Clone(new Rectangle(0, 0, b.Width, (int)(b.Height * ((float)percentage / 100))), PixelFormat.Format32bppArgb);
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            byte alpha;
            int nOffset = bmData.Stride - b.Width * 4;
            transparency.Mid(0, 255);

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

        private static Point FindPosition(WatermarkPositionType positionType, int offset, Size img, Size img2, int add)
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

        private static Image BevelImage(Image image, int offset)
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

        private static Image PrepareBevel(Image image, Point[] points, int filterValue)
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
            switch (Engine.conf.BevelFilterType)
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

        public static Image ApplySizeChanges(Image img)
        {
            switch (Engine.conf.ImageSizeType)
            {
                case ImageSizeType.FIXED:
                    img = GraphicsMgr.ChangeImageSize(img, Engine.conf.ImageSizeFixedWidth, Engine.conf.ImageSizeFixedHeight);
                    break;
                case ImageSizeType.RATIO:
                    img = GraphicsMgr.ChangeImageSize(img, Engine.conf.ImageSizeRatioPercentage);
                    break;
            }
            return img;
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

        public class TurnImage
        {
            public event ImageEventHandler ImageTurned;
            public delegate void ImageEventHandler(Image img);
            public bool IsTurning;
            private Image image1, image2;
            private Timer timer;
            private Bitmap bitmap;
            private Graphics g;
            private int progress = 1;
            private int speed = 5;
            private int step = 1;

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

            public void StartTurn()
            {
                if (!IsTurning && !timer.Enabled)
                {
                    IsTurning = timer.Enabled = true;
                }
            }

            public void StopTurn()
            {
                IsTurning = timer.Enabled = false;
            }

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
        }
    }
}