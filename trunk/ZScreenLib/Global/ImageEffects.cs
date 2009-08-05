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

namespace ZScreenLib
{
    public static class ImageEffects
    {
        /// <summary>Get Image with Watermark</summary>
        public static Image ApplyWatermark(Image img)
        {
            return ApplyWatermark(img, Program.conf.WatermarkMode);
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
                    return DrawWatermark(img, NameParser.Convert(new NameParserInfo(NameParserType.Watermark) { IsPreview = true, Picture = img }));
                case WatermarkType.IMAGE:
                    return DrawImageWatermark(img, Program.conf.WatermarkImageLocation);
            }
        }

        private static Image DrawWatermark(Image img, string drawText)
        {
            if (!string.IsNullOrEmpty(drawText))
            {
                try
                {
                    int offset = (int)Program.conf.WatermarkOffset;
                    Font font = XMLSettings.DeserializeFont(Program.conf.WatermarkFont);
                    Size textSize = TextRenderer.MeasureText(drawText, font);
                    Size labelSize = new Size(textSize.Width + 10, textSize.Height + 10);
                    Point labelPosition = FindPosition(Program.conf.WatermarkPositionMode, offset, img.Size,
                        new Size(textSize.Width + 10, textSize.Height + 10), 1);
                    if (Program.conf.WatermarkAutoHide && ((img.Width < labelSize.Width + offset) ||
                        (img.Height < labelSize.Height + offset)))
                    {
                        return img;
                        //throw new Exception("Image size smaller than watermark size.");
                    }
                    Rectangle labelRectangle = new Rectangle(Point.Empty, labelSize);
                    GraphicsPath gPath = RoundedRectangle.Create(labelRectangle, (int)Program.conf.WatermarkCornerRadius);

                    int backTrans = (int)Program.conf.WatermarkBackTrans;
                    int fontTrans = (int)Program.conf.WatermarkFontTrans;
                    Color fontColor = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
                    Bitmap bmp = new Bitmap(labelRectangle.Width + 1, labelRectangle.Height + 1);
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    g.FillPath(new LinearGradientBrush(labelRectangle, Color.FromArgb(backTrans, XMLSettings.DeserializeColor(Program.conf.WatermarkGradient1)),
                        Color.FromArgb(backTrans, XMLSettings.DeserializeColor(Program.conf.WatermarkGradient2)), Program.conf.WatermarkGradientType), gPath);
                    g.DrawPath(new Pen(Color.FromArgb(backTrans, XMLSettings.DeserializeColor(Program.conf.WatermarkBorderColor))), gPath);
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(drawText, font, new SolidBrush(Color.FromArgb(fontTrans, fontColor)), bmp.Width / 2, bmp.Height / 2, sf);
                    Graphics gImg = Graphics.FromImage(img);
                    gImg.SmoothingMode = SmoothingMode.HighQuality;
                    gImg.DrawImage(bmp, labelPosition);
                    if (Program.conf.WatermarkAddReflection)
                    {
                        Bitmap bmp2 = AddReflection(bmp, 50, 200);
                        gImg.DrawImage(bmp2, new Rectangle(labelPosition.X, labelPosition.Y + bmp.Height - 1, bmp2.Width, bmp2.Height));
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
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
                    int offset = (int)Program.conf.WatermarkOffset;
                    Image img2 = Image.FromFile(imgPath);
                    img2 = GraphicsMgr.ChangeImageSize((Bitmap)img2, (float)Program.conf.WatermarkImageScale);
                    Point imgPos = FindPosition(Program.conf.WatermarkPositionMode, offset, img.Size, img2.Size, 0);
                    if (Program.conf.WatermarkAutoHide && ((img.Width < img2.Width + offset) ||
                        (img.Height < img2.Height + offset)))
                    {
                        return img;
                        //throw new Exception("Image size smaller than watermark size.");
                    }

                    Graphics g = Graphics.FromImage(img);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(img2, imgPos);
                    if (Program.conf.WatermarkAddReflection)
                    {
                        Bitmap bmp = AddReflection((Bitmap)img2, 50, 200);
                        g.DrawImage(bmp, new Rectangle(imgPos.X, imgPos.Y + img2.Height - 1, bmp.Width, bmp.Height));
                    }
                    if (Program.conf.WatermarkUseBorder)
                    {
                        g.DrawRectangle(new Pen(Color.Black), new Rectangle(imgPos.X, imgPos.Y, img2.Width - 1, img2.Height - 1));
                    }
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
            return img;
        }

        public static Image ApplyScreenshotEffects(Image img)
        {
            if (Program.conf.BevelEffect)
            {
                img = BevelImage(img, Program.conf.BevelEffectOffset);
            }
            if (Program.conf.DrawReflection)
            {
                img = DrawReflection(img);
            }
            return img;
        }

        private static Image DrawReflection(Image bmp)
        {
            Bitmap reflection = AddReflection(bmp, Program.conf.ReflectionPercentage, Program.conf.ReflectionTransparency);
            if (Program.conf.ReflectionSkew)
            {
                reflection = AddSkew(reflection, Program.conf.ReflectionSkewSize);
            }
            Bitmap result = new Bitmap(reflection.Width, bmp.Height + reflection.Height + Program.conf.ReflectionOffset);
            Graphics g = Graphics.FromImage(result);
            g.DrawImage(bmp, new Point(0, 0));
            g.DrawImage(reflection, new Point(0, bmp.Height + Program.conf.ReflectionOffset));
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
            switch (Program.conf.BevelFilterType)
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
            switch (Program.conf.ImageSizeType)
            {
                case ImageSizeType.FIXED:
                    img = GraphicsMgr.ChangeImageSize(img, Program.conf.ImageSizeFixedWidth, Program.conf.ImageSizeFixedHeight);
                    break;
                case ImageSizeType.RATIO:
                    img = GraphicsMgr.ChangeImageSize(img, Program.conf.ImageSizeRatioPercentage);
                    break;
            }
            return img;
        }
    }
}