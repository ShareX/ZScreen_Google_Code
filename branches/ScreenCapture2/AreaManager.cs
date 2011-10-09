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

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class AreaManager
    {
        public List<Rectangle> Areas { get; private set; }
        public int SelectedAreaIndex { get; private set; }

        public Rectangle CurrentArea
        {
            get
            {
                if (SelectedAreaIndex > -1)
                {
                    return Areas[SelectedAreaIndex];
                }

                return Rectangle.Empty;
            }
            set
            {
                if (SelectedAreaIndex > -1)
                {
                    Areas[SelectedAreaIndex] = value;
                }
            }
        }

        public bool IsMouseDown { get; private set; }
        public bool IsMouseDragging { get; private set; }

        public bool IsFixedSize { get; set; }
        public Size FixedSize { get; set; }

        private Surface surface;
        private Point currentPosition;
        private Point positionOnClick;

        public AreaManager(Surface surface)
        {
            this.surface = surface;

            Areas = new List<Rectangle>();

            surface.MouseDown += new MouseEventHandler(surface_MouseDown);
            surface.MouseUp += new MouseEventHandler(surface_MouseUp);
            surface.MouseDoubleClick += new MouseEventHandler(surface_MouseDoubleClick);
            surface.MouseMove += new MouseEventHandler(surface_MouseMove);
            surface.KeyUp += new KeyEventHandler(surface_KeyUp);
        }

        private void surface_MouseDown(object sender, MouseEventArgs e)
        {
            int areaIndex = AreaIntersect(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (areaIndex > -1) // Select area
                {
                    IsMouseDragging = true;
                    SelectedAreaIndex = areaIndex;
                }
                else if (!IsMouseDown) // Create new area
                {
                    IsMouseDown = true;
                    Rectangle rect;

                    if (IsFixedSize)
                    {
                        rect = new Rectangle(new Point(e.Location.X - FixedSize.Width / 2, e.Location.Y - FixedSize.Height / 2), FixedSize);
                    }
                    else
                    {
                        rect = new Rectangle(e.Location, new Size(1, 1));
                    }

                    Areas.Add(rect);
                    SelectedAreaIndex = Areas.Count - 1;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (areaIndex > -1)
                {
                    Areas.RemoveAt(areaIndex);
                }
                else
                {
                    surface.Close(false);
                }
            }
        }

        private void surface_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!CurrentArea.IsEmpty)
                {
                    IsMouseDown = false;
                }

                if (IsMouseDragging)
                {
                    IsMouseDragging = false;
                }
            }
        }

        private void surface_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            surface.Close(true);
        }

        private void surface_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDragging)
            {
                Rectangle rect = CurrentArea;
                rect.X += e.X - positionOnClick.X;
                rect.Y += e.Y - positionOnClick.Y;
                positionOnClick = e.Location;
                CurrentArea = rect;
            }

            if (IsMouseDown && !CurrentArea.IsEmpty /*&& !CurrentArea.Selected*/)
            {
                //Rectangle rect = new Rectangle(positionOnClick.X, positionOnClick.Y, currentPosition.X - positionOnClick.X, currentPosition.Y - positionOnClick.Y);
                //CurrentArea = CaptureHelpers.FixRectangle(rect);

                CurrentArea = CaptureHelpers.CreateRectangle(positionOnClick, currentPosition);
            }
        }

        private void surface_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                surface.Close(false);
            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                surface.Close(true);
            }
        }

        public int AreaIntersect(Point mousePosition)
        {
            for (int i = Areas.Count - 1; i >= 0; i--)
            {
                if (Areas[i].Contains(mousePosition))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool IsAreaIntersect()
        {
            return AreaIntersect(CaptureHelpers.GetZeroBasedMousePosition()) > -1;
        }

        public Rectangle CombineAreas()
        {
            Rectangle rect;

            foreach (Rectangle area in Areas)
            {
                Rectangle.Union(rect, area);
            }

            return rect;
        }
    }
}