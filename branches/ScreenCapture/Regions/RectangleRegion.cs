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
using HelpersLib;

namespace ScreenCapture
{
    public class RectangleRegion : DragableRegion
    {
        protected NodeObject[] nodes;

        private Rectangle tempRect;

        public RectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            nodes = new NodeObject[8];

            for (int i = 0; i < 8; i++)
            {
                nodes[i] = new NodeObject(borderPen, nodeBackgroundBrush);
                DrawableObjects.Add(nodes[i]);
            }

            nodes[(int)NodePosition.BottomRight].Order = 10;
        }

        protected override void Update()
        {
            base.Update();

            if (isMouseDown && !IsAreaCreated)
            {
                if (Config.IsFixedSize)
                {
                    CurrentArea = new Rectangle(new Point(mousePosition.X - Config.FixedSize.Width / 2, mousePosition.Y - Config.FixedSize.Height / 2), Config.FixedSize);
                    areaObject.IsDragging = true;
                }
                else
                {
                    CurrentArea = new Rectangle(mousePosition, new Size(1, 1));
                    ShowNodes();
                    nodes[(int)NodePosition.BottomRight].IsDragging = true;
                }

                IsAreaCreated = true;
            }

            if (IsAreaCreated && nodes != null)
            {
                if (isMouseDown)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (nodes[i].IsDragging)
                        {
                            if (!oldIsMouseDown)
                            {
                                tempRect = CurrentArea;
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

                            CurrentArea = CaptureHelpers.FixRectangle(tempRect);

                            break;
                        }
                    }
                }

                UpdateNodePositions();
            }
        }

        protected override void Draw(Graphics g)
        {
            if (CurrentArea.Width > 0 && CurrentArea.Height > 0)
            {
                g.ExcludeClip(CurrentArea);
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                DrawObjects(g);
                g.ResetClip();

                if (areaObject.IsDragging || areaObject.IsMouseHover)
                {
                    g.FillRectangle(lightBrush, CurrentArea);
                }

                g.DrawRectangle(borderPen, CurrentArea.X, CurrentArea.Y, CurrentArea.Width - 1, CurrentArea.Height - 1);
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }
        }

        private void UpdateNodePositions()
        {
            float xStart = CurrentArea.X;
            float xMid = CurrentArea.X + CurrentArea.Width / 2;
            float xEnd = CurrentArea.X + CurrentArea.Width - 1;

            float yStart = CurrentArea.Y;
            float yMid = CurrentArea.Y + CurrentArea.Height / 2;
            float yEnd = CurrentArea.Y + CurrentArea.Height - 1;

            nodes[(int)NodePosition.TopLeft].Position = new PointF(xStart, yStart);
            nodes[(int)NodePosition.Top].Position = new PointF(xMid, yStart);
            nodes[(int)NodePosition.TopRight].Position = new PointF(xEnd, yStart);
            nodes[(int)NodePosition.Right].Position = new PointF(xEnd, yMid);
            nodes[(int)NodePosition.BottomRight].Position = new PointF(xEnd, yEnd);
            nodes[(int)NodePosition.Bottom].Position = new PointF(xMid, yEnd);
            nodes[(int)NodePosition.BottomLeft].Position = new PointF(xStart, yEnd);
            nodes[(int)NodePosition.Left].Position = new PointF(xStart, yMid);
        }
    }
}