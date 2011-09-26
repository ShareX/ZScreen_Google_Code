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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpersLib.Hotkey
{
    public partial class HotkeySelectionControl : UserControl
    {
        public Keys Hotkey { get; set; }

        private Action hotkeyAction;

        public HotkeySelectionControl(string description, Keys key, Action hotkeyAction)
        {
            InitializeComponent();
            lblHotkeyDescription.Text = description;
            Hotkey = key;
            this.hotkeyAction = hotkeyAction;
            btnSetHotkey.Text = new KeyInfo(key).ToString();
        }

        private void btnSetHotkey_Click(object sender, EventArgs e)
        {
            using (HotkeyInputForm inputForm = new HotkeyInputForm(Hotkey))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    // Unregister old key
                    Hotkey = inputForm.SelectedKey;
                    // Register new key
                }
            }
        }
    }
}