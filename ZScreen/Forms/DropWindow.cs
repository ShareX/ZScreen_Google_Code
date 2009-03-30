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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZSS.Forms
{
    /// <summary>
    /// Drop Window for Drag 'n' Drop
    /// </summary>
    public partial class DropWindow : Form
    {
        public event StringsEventHandler Result;

        public string[] FilePaths { get; set; }

        public DropWindow()
        {
            InitializeComponent();
        }

        private void DropWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void DropWindow_DragDrop(object sender, DragEventArgs e)
        {
            this.FilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            Result(this, FilePaths);
            if (Program.conf.CloseDropBox) this.Close();
        }

        private void DropWindow_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void DropWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.conf.LastDropBoxPosition = this.Location;
        }

        private void DropWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32.ReleaseCapture();
                User32.SendMessage(Handle, User32.WM_NCLBUTTONDOWN, User32.HT_CAPTION, 0);
            }
            else
            {
                this.Close();
            }
        }
    }
}