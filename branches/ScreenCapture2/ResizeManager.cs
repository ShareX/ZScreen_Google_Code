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

                for (int i = 0; i < resizers.Length; i++)
                {
                    resizers[i].Visible = visible;
                }
            }
        }

        public int MaxMoveSpeed { get; set; }
        public int MinMoveSpeed { get; set; }
        public bool IsBottomRightResizing { get; set; }

        private Surface surface;
        private AreaManager areaManager;
        private Label[] resizers = new Label[8];
        private int mx, my;
        private Rectangle tempRect;

        public ResizeManager(Surface surface, AreaManager areaManager)
        {
            this.surface = surface;
            this.areaManager = areaManager;

            surface.KeyDown += new KeyEventHandler(surface_KeyDown);

            for (int i = 0; i < resizers.Length; i++)
            {
                resizers[i] = new Label();
                resizers[i].Tag = i;
                resizers[i].Width = resizers[i].Height = 10;
                resizers[i].BackColor = Color.White;
                resizers[i].BorderStyle = BorderStyle.FixedSingle;
                resizers[i].MouseDown += new MouseEventHandler(ResizeManager_MouseDown);
                resizers[i].MouseUp += new MouseEventHandler(ResizeManager_MouseUp);
                resizers[i].MouseMove += new MouseEventHandler(ResizeManager_MouseMove);
                resizers[i].Visible = false;
                surface.Controls.Add(resizers[i]);
            }

            resizers[0].Cursor = Cursors.SizeNWSE;
            resizers[1].Cursor = Cursors.SizeNS;
            resizers[2].Cursor = Cursors.SizeNESW;
            resizers[3].Cursor = Cursors.SizeWE;
            resizers[4].Cursor = Cursors.SizeNWSE;
            resizers[5].Cursor = Cursors.SizeNS;
            resizers[6].Cursor = Cursors.SizeNESW;
            resizers[7].Cursor = Cursors.SizeWE;
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
            int pos = resizers[0].Width / 2;

            int[] xChoords = new int[] { rect.Left - pos, rect.Left + rect.Width / 2 - pos, rect.Left + rect.Width - pos };
            int[] yChoords = new int[] { rect.Top - pos, rect.Top + rect.Height / 2 - pos, rect.Top + rect.Height - pos };

            resizers[0].Left = xChoords[0]; resizers[0].Top = yChoords[0];
            resizers[1].Left = xChoords[1]; resizers[1].Top = yChoords[0];
            resizers[2].Left = xChoords[2]; resizers[2].Top = yChoords[0];
            resizers[3].Left = xChoords[2]; resizers[3].Top = yChoords[1];
            resizers[4].Left = xChoords[2]; resizers[4].Top = yChoords[2];
            resizers[5].Left = xChoords[1]; resizers[5].Top = yChoords[2];
            resizers[6].Left = xChoords[0]; resizers[6].Top = yChoords[2];
            resizers[7].Left = xChoords[0]; resizers[7].Top = yChoords[1];

            if ((resizers[0].Left < resizers[4].Left && resizers[0].Top < resizers[4].Top) ||
                   resizers[0].Left > resizers[4].Left && resizers[0].Top > resizers[4].Top)
            {
                resizers[0].Cursor = Cursors.SizeNWSE;
                resizers[2].Cursor = Cursors.SizeNESW;
                resizers[4].Cursor = Cursors.SizeNWSE;
                resizers[6].Cursor = Cursors.SizeNESW;
            }
            else if ((resizers[0].Left > resizers[4].Left && resizers[0].Top < resizers[4].Top) ||
                resizers[0].Left < resizers[4].Left && resizers[0].Top > resizers[4].Top)
            {
                resizers[0].Cursor = Cursors.SizeNESW;
                resizers[2].Cursor = Cursors.SizeNWSE;
                resizers[4].Cursor = Cursors.SizeNESW;
                resizers[6].Cursor = Cursors.SizeNWSE;
            }
            else if (resizers[0].Left == resizers[4].Left)
            {
                resizers[0].Cursor = Cursors.SizeNS;
                resizers[4].Cursor = Cursors.SizeNS;
            }
            else if (resizers[0].Top == resizers[4].Top)
            {
                resizers[0].Cursor = Cursors.SizeWE;
                resizers[4].Cursor = Cursors.SizeWE;
            }
        }

        public void MoveCurrentArea(int x, int y)
        {
            CurrentArea = new Rectangle(new Point(CurrentArea.X + x, CurrentArea.Y + y), CurrentArea.Size);
        }

        public void ResizeCurrentArea(int x, int y, bool isBottomRightMoving)
        {
            if (isBottomRightMoving)
            {
                CurrentArea = new Rectangle(CurrentArea.Left, CurrentArea.Top, CurrentArea.Width + x, CurrentArea.Height + y);
            }
            else
            {
                CurrentArea = new Rectangle(CurrentArea.Left + x, CurrentArea.Top + y, CurrentArea.Width - x, CurrentArea.Height - y);
            }
        }
    }
}