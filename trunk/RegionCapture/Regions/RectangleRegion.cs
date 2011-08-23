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
using System.Linq;

namespace RegionCapture
{
    public class RectangleRegion : Surface
    {
        protected DrawableObject region;
        protected NodeObject[] nodes;

        protected bool isNodesCreated;

        private Point pos, pos2;

        public RectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            region = new DrawableObject();
            DrawableObjects.Add(region);
        }

        protected override void Update()
        {
            base.Update();

            if (isMouseDown && !isNodesCreated)
            {
                pos = pos2 = mousePosition;

                nodes = new NodeObject[8];

                for (int i = 0; i < 8; i++)
                {
                    nodes[i] = new NodeObject(borderPen, nodeBackgroundBrush);
                    nodes[i].Position = ClientMousePosition;
                    nodes[i].Visible = true;
                    DrawableObjects.Add(nodes[i]);
                }

                nodes[(int)NodePosition.BottomRight].IsHolding = true;

                isNodesCreated = true;
            }

            if (nodes != null && isNodesCreated)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (nodes[i].IsHolding)
                    {
                        if (i <= 2) // Top row
                        {
                            pos.Y += mousePosition.Y - oldMousePosition.Y;
                        }
                        else if (i >= 4 && i <= 6) // Bottom row
                        {
                            pos2.Y += mousePosition.Y - oldMousePosition.Y;
                        }

                        if (i >= 2 && i <= 4) // Right row
                        {
                            pos2.X += mousePosition.X - oldMousePosition.X;
                        }
                        else if (i >= 6 || i == 0) // Left row
                        {
                            pos.X += mousePosition.X - oldMousePosition.X;
                        }

                        break;
                    }
                }

                if (region.IsHolding && DrawableObjects.OfType<NodeObject>().All(x => !x.IsHolding && !x.IsMouseHover))
                {
                    int x = mousePosition.X - oldMousePosition.X;
                    int y = mousePosition.Y - oldMousePosition.Y;

                    pos.X += x;
                    pos2.X += x;
                    pos.Y += y;
                    pos2.Y += y;
                }

                area = Helpers.CreateRectangle(pos, pos2);
                region.Rectangle = area;

                UpdateNodePositions();
            }
        }

        protected override void Draw(Graphics g)
        {
            if (Area.Width > 0 && Area.Height > 0)
            {
                g.ExcludeClip(Area);
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                g.ResetClip();
                g.DrawRectangle(borderPen, Area.X, Area.Y, Area.Width - 1, Area.Height - 1);
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }

            base.Draw(g);
        }

        private void UpdateNodePositions()
        {
            int xStart = area.X;
            int xMid = area.X + area.Width / 2;
            int xEnd = area.X + area.Width;

            int yStart = area.Y;
            int yMid = area.Y + area.Height / 2;
            int yEnd = area.Y + area.Height;

            nodes[(int)NodePosition.TopLeft].Position = new Point(xStart, yStart);
            nodes[(int)NodePosition.Top].Position = new Point(xMid, yStart);
            nodes[(int)NodePosition.TopRight].Position = new Point(xEnd, yStart);
            nodes[(int)NodePosition.Right].Position = new Point(xEnd, yMid);
            nodes[(int)NodePosition.BottomRight].Position = new Point(xEnd, yEnd);
            nodes[(int)NodePosition.Bottom].Position = new Point(xMid, yEnd);
            nodes[(int)NodePosition.BottomLeft].Position = new Point(xStart, yEnd);
            nodes[(int)NodePosition.Left].Position = new Point(xStart, yMid);
        }
    }
}