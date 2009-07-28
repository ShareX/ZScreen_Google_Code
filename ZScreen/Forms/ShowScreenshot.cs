#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZSS.Forms
{
    public partial class ShowScreenshot : Form
    {
        public ShowScreenshot()
        {
            this.BackColor = Color.Black;
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            this.Bounds = GraphicsMgr.GetScreenBounds();
        }

        private void ShowScreenshot_Load(object sender, EventArgs e)
        {
            if ((this.Bounds.Width > this.BackgroundImage.Width) && (this.Bounds.Height > this.BackgroundImage.Height))
            {
                this.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                this.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void ShowScreenshot_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ShowScreenshot_Shown(object sender, EventArgs e)
        {
            User32.SetForegroundWindow(this.Handle);
            User32.SetActiveWindow(this.Handle);
        }

        private void ShowScreenshot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}