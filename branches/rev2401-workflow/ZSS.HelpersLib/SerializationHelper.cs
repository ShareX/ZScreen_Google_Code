using System.Drawing;

namespace HelpersLib
{
    public class XFont
    {
        public string FontFamily { get; set; }
        public float Size { get; set; }
        public FontStyle Style { get; set; }
        public GraphicsUnit GraphicsUnit { get; set; }

        public XFont() { }

        public XFont(Font font)
        {
            Init(font);
        }

        public XFont(string fontName, float fontSize, FontStyle fontStyle = FontStyle.Regular)
        {
            Font font = CreateFont(fontName, fontSize, fontStyle);
            Init(font);
        }

        private void Init(Font font)
        {
            FontFamily = font.FontFamily.Name;
            GraphicsUnit = font.Unit;
            Size = font.Size;
            Style = font.Style;
        }

        private Font CreateFont(string fontName, float fontSize, FontStyle fontStyle)
        {
            try
            {
                return new Font(fontName, fontSize, fontStyle);
            }
            catch
            {
                return new Font(SystemFonts.DefaultFont.FontFamily, fontSize, fontStyle);
            }
        }

        public static implicit operator Font(XFont font)
        {
            return new Font(font.FontFamily, font.Size, font.Style, font.GraphicsUnit);
        }

        public static implicit operator XFont(Font font)
        {
            return new XFont(font);
        }
    }

    public class XColor
    {
        public int Argb { get; set; }

        public XColor() { }

        public XColor(Color color)
        {
            Argb = color.ToArgb();
        }

        public XColor(byte a, byte r, byte g, byte b)
        {
            Argb = (a << 24) | (r << 16) | (g << 8) | b;
        }

        public static implicit operator Color(XColor color)
        {
            return Color.FromArgb(color.Argb);
        }

        public static implicit operator XColor(Color color)
        {
            return new XColor(color);
        }
    }
}