using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ZSS
{
    public static class WatermarkMaker
    {
        /// <summary>
        /// Get Image with Watermark
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Image GetImage(Image img)
        {
            if (Program.conf.WatermarkUseImage)
            {
                return DrawImageWatermark(img, Program.conf.WatermarkImageLocation, Program.conf.WatermarkPositionMode, (int)Program.conf.WatermarkOffset);
            }
            else
            {
                return DrawWatermark(img, NameParser.Convert(NameParser.NameType.Watermark, true), XMLSettings.DeserializeFont(Program.conf.WatermarkFont),
                    XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor), (int)Program.conf.WatermarkFontTrans, (int)Program.conf.WatermarkOffset,
                    (int)Program.conf.WatermarkBackTrans, XMLSettings.DeserializeColor(Program.conf.WatermarkGradient1),
                    XMLSettings.DeserializeColor(Program.conf.WatermarkGradient2), XMLSettings.DeserializeColor(Program.conf.WatermarkBorderColor),
                    Program.conf.WatermarkPositionMode, (int)Program.conf.WatermarkCornerRadius, Program.conf.WatermarkGradientType);
            }
        }

        private static Image DrawImageWatermark(Image img, string imgPath, WatermarkPositionType position, int offset)
        {
            try
            {
                if (File.Exists(imgPath))
                {
                    using (Image img2 = Image.FromFile(imgPath))
                    {
                        Graphics g = Graphics.FromImage(img);
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        Rectangle imgRect = Rectangle.Empty;
                        switch (position)
                        {
                            case WatermarkPositionType.TOP_LEFT:
                                imgRect = new Rectangle(offset, offset, img2.Width, img2.Height);
                                break;
                            case WatermarkPositionType.TOP_RIGHT:
                                imgRect = new Rectangle(img.Width - img2.Width - offset, offset, img2.Width, img2.Height);
                                break;
                            case WatermarkPositionType.BOTTOM_LEFT:
                                imgRect = new Rectangle(offset, img.Height - img2.Height - offset, img2.Width, img2.Height);
                                break;
                            case WatermarkPositionType.BOTTOM_RIGHT:
                                imgRect = new Rectangle(img.Width - img2.Width - offset, img.Height - img2.Height - offset, img2.Width, img2.Height);
                                break;
                            case WatermarkPositionType.CENTER:
                                imgRect = new Rectangle(img.Width / 2 - img2.Width / 2, img.Height / 2 - img2.Height / 2, img2.Width, img2.Height);
                                break;
                        }
                        if ((img.Width < imgRect.Width + offset) || (img.Height < imgRect.Height + offset))
                        {
                            throw new Exception("Image size smaller than watermark size.");
                        }
                        else
                        {
                            g.DrawImage(img2, imgRect);
                            if (Program.conf.WatermarkUseBorder)
                            {
                                g.DrawRectangle(new Pen(Color.Black), new Rectangle(imgRect.X - 1, imgRect.Y - 1, imgRect.Width, imgRect.Height));
                            }
                            if (Program.conf.WatermarkAddReflection)
                            {
                                Bitmap img3 = Transparent((Bitmap)img2);
                                g.DrawImage(img3, new Rectangle(imgRect.X, imgRect.Y + imgRect.Height, img3.Width, img3.Height));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return img;
        }

        private static Image DrawWatermark(Image img, string drawText, Font font, Color fontColor, int fontTrans, int offset, int backTrans,
            Color backColor1, Color backColor2, Color borderColor, WatermarkPositionType position, int cornerRadius, LinearGradientMode gradientType)
        {
            try
            {
                Graphics g = Graphics.FromImage(img);
                g.SmoothingMode = SmoothingMode.HighQuality;
                Size textSize = TextRenderer.MeasureText(drawText, font);
                Rectangle labelRect = Rectangle.Empty;
                switch (position)
                {
                    case WatermarkPositionType.TOP_LEFT:
                        labelRect = new Rectangle(offset, offset, textSize.Width + 10, textSize.Height + 10);
                        break;
                    case WatermarkPositionType.TOP_RIGHT:
                        labelRect = new Rectangle(img.Width - textSize.Width - 10 - offset - 1, offset,
                            textSize.Width + 10, textSize.Height + 10);
                        break;
                    case WatermarkPositionType.BOTTOM_LEFT:
                        labelRect = new Rectangle(offset, img.Height - textSize.Height - 10 - offset - 1,
                            textSize.Width + 10, textSize.Height + 10);
                        break;
                    case WatermarkPositionType.BOTTOM_RIGHT:
                        labelRect = new Rectangle(img.Width - textSize.Width - 10 - offset - 1,
                            img.Height - textSize.Height - 10 - offset - 1, textSize.Width + 10, textSize.Height + 10);
                        break;
                    case WatermarkPositionType.CENTER:
                        labelRect = new Rectangle(img.Width / 2 - (textSize.Width + 10) / 2 - 1,
                            img.Height / 2 - (textSize.Height + 10) / 2 - 1, textSize.Width + 10, textSize.Height + 10);
                        break;
                }
                if ((img.Width < labelRect.Width + offset) || (img.Height < labelRect.Height + offset))
                {
                    throw new Exception("Image size smaller than watermark size.");
                }
                else
                {
                    GraphicsPath gPath;
                    if (cornerRadius > 0)
                    {
                        gPath = MyGraphics.RoundedRectangle(labelRect, cornerRadius);
                    }
                    else
                    {
                        gPath = new GraphicsPath();
                        gPath.AddRectangle(labelRect);
                    }
                    g.FillPath(new LinearGradientBrush(labelRect, Color.FromArgb(backTrans, backColor1),
                        Color.FromArgb(backTrans, backColor2), gradientType), gPath);
                    g.DrawPath(new Pen(Color.FromArgb(backTrans, borderColor)), gPath);
                    g.DrawString(drawText, font, new SolidBrush(Color.FromArgb(fontTrans, fontColor)), labelRect.X + 5, labelRect.Y + 5);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return img;
        }

        public static Bitmap Transparent(Bitmap b)
        {
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            b = b.Clone(new Rectangle(0, 0, b.Width, b.Height / 2), PixelFormat.Format32bppArgb);
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
                        alpha = (byte)(255 - 255 * (y + 1) / b.Height);
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