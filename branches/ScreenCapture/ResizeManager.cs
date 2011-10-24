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
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class ResizeManager
    {
        private bool visible;

        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;

                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i].Visible = visible;
                }
            }
        }

        public int MaxMoveSpeed { get; set; }
        public int MinMoveSpeed { get; set; }
        public bool IsBottomRightResizing { get; set; }

        private Surface surface;
        private AreaManager areaManager;

        private NodeObject[] nodes;
        private Rectangle tempRect;

        public ResizeManager(Surface surface, AreaManager areaManager)
        {
            this.surface = surface;
            this.areaManager = areaManager;

            surface.KeyDown += new KeyEventHandler(surface_KeyDown);

            nodes = new NodeObject[8];

            for (int i = 0; i < 8; i++)
            {
                nodes[i] = surface.MakeNode();
            }

            nodes[(int)NodePosition.BottomRight].Order = 10;
        }

        public void Update()
        {
            if (Visible && nodes != null)
            {
                if (surface.IsLeftMouseDown)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (nodes[i].IsDragging)
                        {
                            if (!surface.IsBeforeLeftMouseDown)
                            {
                                tempRect = areaManager.CurrentArea;
                            }

                            int x = surface.CurrentMousePosition.X - surface.BeforeMousePosition.X;

                            if (i >= 2 && i <= 4) // Right
                            {
                                tempRect.Width += x;
                            }
                            else if (i >= 6 || i == 0) // Left
                            {
                                tempRect.X += x;
                                tempRect.Width -= x;
                            }

                            int y = surface.CurrentMousePosition.Y - surface.BeforeMousePosition.Y;

                            if (i <= 2) // Top
                            {
                                tempRect.Y += y;
                                tempRect.Height -= y;
                            }
                            else if (i >= 4 && i <= 6) // Bottom
                            {
                                tempRect.Height += y;
                            }

                            areaManager.CurrentArea = CaptureHelpers.FixRectangle(tempRect);

                            break;
                        }
                    }
                }

                UpdateNodePositions();
            }
        }

        private void surface_KeyDown(object sender, KeyEventArgs e)
        {
            int speed;

            if (e.Control)
            {
                speed = MaxMoveSpeed;
            }
            else
            {
                speed = MinMoveSpeed;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (e.Shift) { MoveCurrentArea(-speed, 0); } else { ResizeCurrentArea(-speed, 0, IsBottomRightResizing); }
                    break;
                case Keys.Right:
                    if (e.Shift) { MoveCurrentArea(speed, 0); } else { ResizeCurrentArea(speed, 0, IsBottomRightResizing); }
                    break;
                case Keys.Up:
                    if (e.Shift) { MoveCurrentArea(0, -speed); } else { ResizeCurrentArea(0, -speed, IsBottomRightResizing); }
                    break;
                case Keys.Down:
                    if (e.Shift) { MoveCurrentArea(0, speed); } else { ResizeCurrentArea(0, speed, IsBottomRightResizing); }
                    break;
                case Keys.Tab:
                    IsBottomRightResizing = !IsBottomRightResizing;
                    break;
            }
        }

        public bool IsCursorOnNode()
        {
            foreach (NodeObject node in nodes)
            {
                if (node.IsMouseHover)
                {
                    return true;
                }
            }

            return false;
        }

        public void Show()
        {
            UpdateNodePositions();

            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void UpdateNodePositions()
        {
            UpdateNodePositions(areaManager.CurrentArea);
        }

        private void UpdateNodePositions(Rectangle rect)
        {
            float xStart = rect.X;
            float xMid = rect.X + rect.Width / 2;
            float xEnd = rect.X + rect.Width - 1;

            float yStart = rect.Y;
            float yMid = rect.Y + rect.Height / 2;
            float yEnd = rect.Y + rect.Height - 1;

            nodes[(int)NodePosition.TopLeft].Position = new PointF(xStart, yStart);
            nodes[(int)NodePosition.Top].Position = new PointF(xMid, yStart);
            nodes[(int)NodePosition.TopRight].Position = new PointF(xEnd, yStart);
            nodes[(int)NodePosition.Right].Position = new PointF(xEnd, yMid);
            nodes[(int)NodePosition.BottomRight].Position = new PointF(xEnd, yEnd);
            nodes[(int)NodePosition.Bottom].Position = new PointF(xMid, yEnd);
            nodes[(int)NodePosition.BottomLeft].Position = new PointF(xStart, yEnd);
            nodes[(int)NodePosition.Left].Position = new PointF(xStart, yMid);
        }

        public void MoveCurrentArea(int x, int y)
        {
            areaManager.CurrentArea = new Rectangle(new Point(areaManager.CurrentArea.X + x, areaManager.CurrentArea.Y + y), areaManager.CurrentArea.Size);
        }

        public void ResizeCurrentArea(int x, int y, bool isBottomRightMoving)
        {
            if (isBottomRightMoving)
            {
                areaManager.CurrentArea = new Rectangle(areaManager.CurrentArea.X, areaManager.CurrentArea.Y,
                    areaManager.CurrentArea.Width + x, areaManager.CurrentArea.Height + y);
            }
            else
            {
                areaManager.CurrentArea = new Rectangle(areaManager.CurrentArea.X + x, areaManager.CurrentArea.Y + y,
                    areaManager.CurrentArea.Width - x, areaManager.CurrentArea.Height - y);
            }
        }
    }
}