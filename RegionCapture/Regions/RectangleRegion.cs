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

namespace RegionCapture
{
    public class RectangleRegion : DragableRegion
    {
        public bool IsFixedSize { get; set; }

        protected NodeObject[] nodes;
        protected bool isNodesCreated;

        private Rectangle tempRect;

        public RectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
        }

        protected override void Update()
        {
            base.Update();

            if (isMouseDown && !isNodesCreated)
            {
                area = new Rectangle(mousePosition, new Size(1, 1));

                nodes = new NodeObject[8];

                for (int i = 0; i < 8; i++)
                {
                    nodes[i] = new NodeObject(borderPen, nodeBackgroundBrush);
                    nodes[i].Position = ClientMousePosition;
                    nodes[i].Visible = true;
                    DrawableObjects.Add(nodes[i]);
                }

                nodes[(int)NodePosition.BottomRight].Order = 10;
                nodes[(int)NodePosition.BottomRight].IsDragging = true;

                isNodesCreated = true;
            }

            if (nodes != null && isNodesCreated && isMouseDown)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (nodes[i].IsDragging)
                    {
                        if (!oldIsMouseDown)
                        {
                            tempRect = area;
                        }

                        if (i <= 2) // Top row
                        {
                            tempRect.Y += mousePosition.Y - oldMousePosition.Y;
                            tempRect.Height -= mousePosition.Y - oldMousePosition.Y;
                        }
                        else if (i >= 4 && i <= 6) // Bottom row
                        {
                            tempRect.Height += mousePosition.Y - oldMousePosition.Y;
                        }

                        if (i >= 2 && i <= 4) // Right row
                        {
                            tempRect.Width += mousePosition.X - oldMousePosition.X;
                        }
                        else if (i >= 6 || i == 0) // Left row
                        {
                            tempRect.X += mousePosition.X - oldMousePosition.X;
                            tempRect.Width -= mousePosition.X - oldMousePosition.X;
                        }

                        area = Helpers.FixRectangle(tempRect);

                        break;
                    }
                }
            }

            UpdateNodePositions();
        }

        protected override void Draw(Graphics g)
        {
            if (Area.Width > 0 && Area.Height > 0)
            {
                g.ExcludeClip(Area);
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                g.ResetClip();

                if (areaObject.IsDragging || areaObject.IsMouseHover)
                {
                    g.FillRectangle(lightBrush, Area);
                }

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
            if (isNodesCreated)
            {
                int xStart = area.X;
                int xMid = area.X + area.Width / 2;
                int xEnd = area.X + area.Width - 1;

                int yStart = area.Y;
                int yMid = area.Y + area.Height / 2;
                int yEnd = area.Y + area.Height - 1;

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
}