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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZScreenLib.UserControls
{
    public partial class AccountsControl : UserControl
    {
        public AccountsControl()
        {
            InitializeComponent();
        }

        public virtual bool RemoveItem(int sel)
        {
            if (sel != -1)
            {
                this.AccountsList.Items.RemoveAt(sel);
                if (this.AccountsList.Items.Count > 0)
                {
                    this.AccountsList.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
                return true;
            }
            return false;
        }

        private void SettingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.AccountsList.SelectedIndex > -1)
            {
                this.AccountsList.Items[this.AccountsList.SelectedIndex] = SettingsGrid.SelectedObject;
            }
        }
    }
}