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
    public partial class QuickOptions : Form
    {
        private bool isClosing = false;

        public QuickOptions()
        {
            InitializeComponent();

            foreach (ImageDestType sdt in Enum.GetValues(typeof(ImageDestType)))
            {
                lbDest.Items.Add(sdt.ToDescriptionString());
            }
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                lbClipboardMode.Items.Add(cui.ToDescriptionString());
            }
        }

        private void QuickOptions_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(Cursor.Position.X - (this.Width / 2), Cursor.Position.Y - (this.Height / 2));
            /*
            foreach (ScreenshotDestType sdt in Enum.GetValues(typeof(ScreenshotDestType)))
            {
                lbDest.Items.Add(sdt.ToDescriptionString());                
            }
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                lbClipboardMode.Items.Add(cui.ToDescriptionString());
            }
            */
        }

        public QuickOptionsPacket Result { get; private set; }

        private void lbDest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                ReturnResult();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void lbDest_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReturnResult();
        }

        private void ReturnResult()
        {
            if (lbDest.SelectedIndex > -1)
            {

                ApplyOptions();
                this.DialogResult = DialogResult.OK;
                isClosing = true;
                this.Close();
            }
        }

        private void ApplyOptions()
        {
            QuickOptionsPacket r = new QuickOptionsPacket();
            r.Destination = (ImageDestType)lbDest.SelectedIndex;
            r.ClipboardMode = (ClipboardUriType)lbClipboardMode.SelectedIndex;
            this.Result = r;

            Program.conf.ScreenshotDestMode = (ImageDestType)lbDest.SelectedIndex;
            Program.conf.ClipboardUriMode = (ClipboardUriType)lbClipboardMode.SelectedIndex;
        }

        // I like it without closing please -- McoreD
        //private void Destinations_Deactivate(object sender, EventArgs e)
        //{
        //    CloseWindow();
        //}

        private void CloseWindow()
        {
         if (isClosing) return;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void QuickOptions_Shown(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            User32.SetForegroundWindow(this.Handle.ToInt32());
            User32.SetActiveWindow(this.Handle.ToInt32());
            lbDest.Focus();
            lbDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
            lbClipboardMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ReturnResult();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyOptions();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }
    }
}