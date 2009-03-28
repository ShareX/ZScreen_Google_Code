using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
            return Watermark(img);
        }

        private static Image Watermark(Image img)
        {
            return DrawWatermark(img, NameParser.Convert(NameParser.NameType.Watermark, true), XMLSettings.DeserializeFont(Program.conf.WatermarkFont),
                XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor), (int)Program.conf.WatermarkFontTrans, (int)Program.conf.WatermarkOffset,
                (int)Program.conf.WatermarkBackTrans, XMLSettings.DeserializeColor(Program.conf.WatermarkGradient1),
                XMLSettings.DeserializeColor(Program.conf.WatermarkGradient2), XMLSettings.DeserializeColor(Program.conf.WatermarkBorderColor),
                Program.conf.WatermarkPositionMode, (int)Program.conf.WatermarkCornerRadius, Program.conf.WatermarkGradientType);
        }

        private static Image DrawWatermark(Image img, string drawText, Font font, Color fontColor, int fontTrans, int offset, int backTrans,
            Color backColor1, Color backColor2, Color borderColor, WatermarkPositionType position, int cornerRadius, string gradientType)
        {
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.HighQuality;
            Size textSize = TextRenderer.MeasureText(drawText, font);
            Rectangle labelRect = Rectangle.Empty;
            try
            {
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
                }
                if ((img.Width < labelRect.Width + offset) || (img.Height < labelRect.Height + offset))
                {
                    throw new Exception("Image size smaller than watermark size.");
                }
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
                g.FillPath(new LinearGradientBrush(labelRect, Color.FromArgb(backTrans, backColor1), Color.FromArgb(backTrans, backColor2),
                   (LinearGradientMode)Enum.Parse(typeof(LinearGradientMode), gradientType)), gPath);
                g.DrawPath(new Pen(Color.FromArgb(backTrans, borderColor)), gPath);
                g.DrawString(drawText, font, new SolidBrush(Color.FromArgb(fontTrans, fontColor)), labelRect.X + 5, labelRect.Y + 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return img;
        }
    }
}