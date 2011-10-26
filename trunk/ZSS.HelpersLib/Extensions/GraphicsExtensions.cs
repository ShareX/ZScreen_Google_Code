using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HelpersLib
{
    public static class GraphicsExtensions
    {
        public static void DrawRectangleProper(this Graphics g, Pen pen, Rectangle rect)
        {
            rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            if (rect.Width > 1 && rect.Height > 1)
            {
                g.DrawRectangle(pen, rect);
            }
        }

        public static void DrawCrossRectangle(this Graphics g, Pen pen, Rectangle rect, int crossSize)
        {
            rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            if (rect.Width > 0 && rect.Height > 0)
            {
                // Top
                g.DrawLine(pen, rect.X - crossSize, rect.Y, rect.Right + crossSize, rect.Y);

                // Right
                g.DrawLine(pen, rect.Right, rect.Y - crossSize, rect.Right, rect.Bottom + crossSize);

                // Bottom
                g.DrawLine(pen, rect.X - crossSize, rect.Bottom, rect.Right + crossSize, rect.Bottom);

                // Left
                g.DrawLine(pen, rect.X, rect.Y - crossSize, rect.X, rect.Bottom + crossSize);
            }
        }
    }
}