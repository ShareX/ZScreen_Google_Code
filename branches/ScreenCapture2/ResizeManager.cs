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
        public bool IsMouseDown { get; private set; }

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
        private Label[] nodes = new Label[8];
        private int mx, my;
        private Rectangle tempRect;

        public ResizeManager(Surface surface, AreaManager areaManager)
        {
            this.surface = surface;
            this.areaManager = areaManager;

            surface.KeyDown += new KeyEventHandler(surface_KeyDown);

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new Label();
                nodes[i].Tag = i;
                nodes[i].Width = nodes[i].Height = 10;
                nodes[i].BackColor = Color.White;
                nodes[i].BorderStyle = BorderStyle.FixedSingle;
                nodes[i].MouseDown += new MouseEventHandler(ResizeManager_MouseDown);
                nodes[i].MouseUp += new MouseEventHandler(ResizeManager_MouseUp);
                nodes[i].MouseMove += new MouseEventHandler(ResizeManager_MouseMove);
                nodes[i].Visible = false;
                surface.Controls.Add(nodes[i]);
            }

            nodes[0].Cursor = Cursors.SizeNWSE;
            nodes[1].Cursor = Cursors.SizeNS;
            nodes[2].Cursor = Cursors.SizeNESW;
            nodes[3].Cursor = Cursors.SizeWE;
            nodes[4].Cursor = Cursors.SizeNWSE;
            nodes[5].Cursor = Cursors.SizeNS;
            nodes[6].Cursor = Cursors.SizeNESW;
            nodes[7].Cursor = Cursors.SizeWE;
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

        private void ResizeManager_MouseDown(object sender, MouseEventArgs e)
        {
            mx = e.X;
            my = e.Y;
            tempRect = areaManager.CurrentArea;
            IsMouseDown = true;
        }

        private void ResizeManager_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void ResizeManager_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                Label resizer = (Label)sender;
                int index = (int)resizer.Tag;

                if (index <= 2) // Top row
                {
                    tempRect.Y += e.Y - my;
                    tempRect.Height -= e.Y - my;
                }
                else if (index >= 4 && index <= 6) // Bottom row
                {
                    tempRect.Height += e.Y - my;
                }

                if (index >= 2 && index <= 4) // Right row
                {
                    tempRect.Width += e.X - mx;
                }
                else if (index >= 6 || index == 0) // Left row
                {
                    tempRect.X += e.X - mx;
                    tempRect.Width -= e.X - mx;
                }

                areaManager.CurrentArea = CaptureHelpers.FixRectangle(tempRect);
                Update(tempRect);
            }
        }

        public void Show()
        {
            Update();

            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void Update()
        {
            Update(areaManager.CurrentArea);
        }

        public void Update(Rectangle rect)
        {
            int pos = nodes[0].Width / 2;

            int xStart = rect.X - pos;
            int xMid = rect.X + rect.Width / 2 - pos;
            int xEnd = rect.X + rect.Width - 1 - pos;

            int yStart = rect.Y - pos;
            int yMid = rect.Y + rect.Height / 2 - pos;
            int yEnd = rect.Y + rect.Height - 1 - pos;

            nodes[(int)NodePosition.TopLeft].Location = new Point(xStart, yStart);
            nodes[(int)NodePosition.Top].Location = new Point(xMid, yStart);
            nodes[(int)NodePosition.TopRight].Location = new Point(xEnd, yStart);
            nodes[(int)NodePosition.Right].Location = new Point(xEnd, yMid);
            nodes[(int)NodePosition.BottomRight].Location = new Point(xEnd, yEnd);
            nodes[(int)NodePosition.Bottom].Location = new Point(xMid, yEnd);
            nodes[(int)NodePosition.BottomLeft].Location = new Point(xStart, yEnd);
            nodes[(int)NodePosition.Left].Location = new Point(xStart, yMid);

            if ((nodes[0].Left < nodes[4].Left && nodes[0].Top < nodes[4].Top) ||
                   nodes[0].Left > nodes[4].Left && nodes[0].Top > nodes[4].Top)
            {
                nodes[0].Cursor = Cursors.SizeNWSE;
                nodes[2].Cursor = Cursors.SizeNESW;
                nodes[4].Cursor = Cursors.SizeNWSE;
                nodes[6].Cursor = Cursors.SizeNESW;
            }
            else if ((nodes[0].Left > nodes[4].Left && nodes[0].Top < nodes[4].Top) ||
                nodes[0].Left < nodes[4].Left && nodes[0].Top > nodes[4].Top)
            {
                nodes[0].Cursor = Cursors.SizeNESW;
                nodes[2].Cursor = Cursors.SizeNWSE;
                nodes[4].Cursor = Cursors.SizeNESW;
                nodes[6].Cursor = Cursors.SizeNWSE;
            }
            else if (nodes[0].Left == nodes[4].Left)
            {
                nodes[0].Cursor = Cursors.SizeNS;
                nodes[4].Cursor = Cursors.SizeNS;
            }
            else if (nodes[0].Top == nodes[4].Top)
            {
                nodes[0].Cursor = Cursors.SizeWE;
                nodes[4].Cursor = Cursors.SizeWE;
            }
        }

        public void MoveCurrentArea(int x, int y)
        {
            //CurrentArea = new Rectangle(new Point(CurrentArea.X + x, CurrentArea.Y + y), CurrentArea.Size);
        }

        public void ResizeCurrentArea(int x, int y, bool isBottomRightMoving)
        {
            /*if (isBottomRightMoving)
            {
                CurrentArea = new Rectangle(CurrentArea.Left, CurrentArea.Top, CurrentArea.Width + x, CurrentArea.Height + y);
            }
            else
            {
                CurrentArea = new Rectangle(CurrentArea.Left + x, CurrentArea.Top + y, CurrentArea.Width - x, CurrentArea.Height - y);
            }*/
        }
    }
}