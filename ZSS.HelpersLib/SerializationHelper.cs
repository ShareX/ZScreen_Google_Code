using System.Drawing;

namespace HelpersLib
{
    public class XmlFont
    {
        public string FontFamily { get; set; }
        public GraphicsUnit GraphicsUnit { get; set; }
        public float Size { get; set; }
        public FontStyle Style { get; set; }

        public XmlFont()
        {
        }

        public XmlFont(Font font)
        {
            FontFamily = font.FontFamily.Name;
            GraphicsUnit = font.Unit;
            Size = font.Size;
            Style = font.Style;
        }

        public static implicit operator Font(XmlFont font)
        {
            return new Font(font.FontFamily, font.Size, font.Style, font.GraphicsUnit);
        }

        public static implicit operator XmlFont(Font font)
        {
            return new XmlFont(font);
        }
    }

    public class XmlColor
    {
        public int Argb { get; set; }

        public XmlColor()
        {
        }

        public XmlColor(Color color)
        {
            Argb = color.ToArgb();
        }

        public static implicit operator Color(XmlColor color)
        {
            return Color.FromArgb(color.Argb);
        }

        public static implicit operator XmlColor(Color color)
        {
            return new XmlColor(color);
        }
    }
}