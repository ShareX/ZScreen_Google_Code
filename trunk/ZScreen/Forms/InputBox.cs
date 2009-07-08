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
using System.Windows.Forms;

namespace ZSS.Forms
{
    public partial class InputBox : Form
    {
        public string Title { get; set; }
        public string InputText { get; set; }

        public InputBox()
        {
            InitializeComponent();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.Text = this.Title;
            this.txtInputText.Text = this.InputText;
        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            User32.ActivateWindow(this.Handle);
            txtInputText.Focus();
            int start = this.InputText.IndexOf("<");
            int end = this.InputText.IndexOf(">");
            if (start != -1 && end != -1)
            {
                txtInputText.SelectionStart = start;
            }
            else
            {
                txtInputText.SelectionLength = txtInputText.Text.Length;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInputText.Text))
            {
                this.InputText = txtInputText.Text;
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }
    }
}