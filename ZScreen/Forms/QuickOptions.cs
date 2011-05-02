#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
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

using System;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib;

namespace ZScreenLib
{
    public partial class QuickOptions : Form
    {
        public event EventHandler ApplySettings;

        public QuickOptions()
        {
            InitializeComponent();

            foreach (ImageUploaderType sdt in Enum.GetValues(typeof(ImageUploaderType)))
            {
                lbDest.Items.Add(sdt.GetDescription());
            }
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                lbClipboardMode.Items.Add(cui.GetDescription());
            }
        }

        private void QuickOptions_Shown(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            NativeMethods.ActivateWindow(this.Handle);
            lbDest.Focus();
            lbDest.SelectedIndex = Engine.conf.MyImageUploader;
            lbClipboardMode.SelectedIndex = Engine.conf.MyClipboardUriMode;
        }

        private void lbDest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                ReturnResult();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void lbDest_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReturnResult();
        }

        private void ReturnResult()
        {
            ApplyOptions();
            this.Close();
        }

        private void ApplyOptions()
        {
            if (lbDest.SelectedIndex > -1)
                Engine.conf.MyImageUploader = lbDest.SelectedIndex;
            if (lbClipboardMode.SelectedIndex > -1)
                Engine.conf.MyClipboardUriMode = lbClipboardMode.SelectedIndex;
            ApplySettings(this, EventArgs.Empty);
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
            this.Close();
        }
    }
}