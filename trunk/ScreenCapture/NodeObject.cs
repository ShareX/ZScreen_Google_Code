#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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

using System.Drawing;

namespace ScreenCapture
{
    public class NodeObject : DrawableObject
    {
        private Point position;

        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateRectangle();
            }
        }

        public int Size { get; private set; }

        private Pen borderPen;
        private Brush backgroundBrush;

        public NodeObject(Pen borderPen, Brush backgroundBrush, int x = 0, int y = 0, int size = 6)
        {
            this.borderPen = borderPen;
            this.backgroundBrush = backgroundBrush;
            Size = size;
            Position = new Point(x, y);
            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
            Rectangle = new Rectangle(Position.X - Size, Position.Y - Size, Size * 2 + 1, Size * 2 + 1);
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(backgroundBrush, Rectangle.X, Rectangle.Y, Rectangle.Width - 1, Rectangle.Height - 1);
            g.DrawLine(borderPen, Rectangle.X + Size - 1, Rectangle.Y + Size, Rectangle.X + Size + 1, Rectangle.Y + Size);
            g.DrawLine(borderPen, Rectangle.X + Size, Rectangle.Y + Size - 1, Rectangle.X + Size, Rectangle.Y + Size + 1);
            g.DrawEllipse(borderPen, Rectangle.X, Rectangle.Y, Rectangle.Width - 1, Rectangle.Height - 1);
        }
    }
}