using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Crop
{
    public class Area : IDisposable
    {
        public Region Region { get; set; }

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
                Region = new Region(rectangle);
            }
        }

        public bool Selected { get; set; }

        public Brush RectangleBrush { get; set; }
        public Pen RectanglePen { get; set; }
        public Font TextFont { get; set; }
        public Brush TextBrush { get; set; }
        public Brush TextShadowBrush { get; set; }
        public int TextOffset { get; set; }

        public Area()
        {
            RectangleBrush = new SolidBrush(Color.FromArgb(100, Color.CornflowerBlue));
            RectanglePen = new Pen(Color.Black, 1);
            TextFont = new Font("Arial", 14);
            TextBrush = new SolidBrush(Color.White);
            TextShadowBrush = new SolidBrush(Color.Black);
            TextOffset = 5;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(RectangleBrush, Rectangle);

            if (Region != null) g.Clip = Region;

            string info = string.Format("X:{0} Y:{1}\n{2} x {3}", Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            g.DrawString(info, TextFont, TextShadowBrush, Rectangle.X + TextOffset + 1, Rectangle.Y + TextOffset + 1);
            g.DrawString(info, TextFont, TextBrush, Rectangle.X + TextOffset, Rectangle.Y + TextOffset);

            g.Clip = new Region();

            Rectangle rect = Rectangle;
            rect.Width--;
            rect.Height--;
            g.DrawRectangle(RectanglePen, rect);
        }

        public void Dispose()
        {
            if (RectanglePen != null) RectanglePen.Dispose();
            if (RectangleBrush != null) RectangleBrush.Dispose();
            if (TextFont != null) TextFont.Dispose();
            if (TextBrush != null) TextBrush.Dispose();
            if (TextShadowBrush != null) TextShadowBrush.Dispose();
        }
    }
}