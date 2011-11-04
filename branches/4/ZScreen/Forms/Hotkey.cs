#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

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

//using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Text;
using System.Windows.Forms;
using ZSS.Properties;
using System;

namespace ZSS
{
    public partial class Hotkey : UserControl
    {
        private Object[] modKeys = { Keys.Control.ToString(), Keys.Alt.ToString(), Keys.Shift.ToString() };

        public Hotkey()
        {
            InitializeComponent();

            foreach (string k in Enum.GetNames(typeof(Keys)))
            {
                if(Array.IndexOf(modKeys, k) < 0) //if not a modkey
                    cboKey.Items.Add(k);
            }

            cboMod1.Items.AddRange(modKeys);
            cboMod2.Items.AddRange(modKeys);
        }

        public void validateHK()
        {
            //Turn off duplicate mods
            if (cbMod1.Checked && cbMod2.Checked && cboMod1.SelectedItem == cboMod2.SelectedItem)
            {
                cbMod2.Checked = false;
            }

            //Turn off blank mods
            if (cbMod1.Checked && cboMod1.SelectedItem == null)
                cbMod1.Checked = false;
            if (cbMod2.Checked && cboMod2.SelectedItem == null)
                cbMod2.Checked = false;
        }

        public HKcombo getHotkey()
        {
            List<Keys> mods = new List<Keys>(2);
            Keys key;

            if (cbMod1.Checked && cboMod1.SelectedItem != null)
                mods.Add((Keys)Enum.Parse(typeof(Keys), (string)cboMod1.SelectedItem));
            if (cbMod2.Checked && cboMod2.SelectedItem != null)
                mods.Add((Keys)Enum.Parse(typeof(Keys), (string)cboMod2.SelectedItem));

            key = (Keys)Enum.Parse(typeof(Keys), (string)cboKey.SelectedItem);

            if (mods.Count > 0)
                return new HKcombo(mods.ToArray(), key);
            else
                return new HKcombo(null, key);
        }

        public void updateHotkey(HKcombo hkc)
        {
            if (hkc.Mods == null) //0 mods
            {
                cbMod1.Checked = false;
                cbMod2.Checked = false;
            }
            else // if(hkc.Mods.Length > 0)
            {
                if (hkc.Mods.Length == 1)
                {
                    cbMod1.Checked = true;
                    cboMod1.SelectedItem = hkc.Mods[0].ToString();

                    cbMod2.Checked = false;
                }
                else if (hkc.Mods.Length == 2)
                {
                    cbMod1.Checked = true;
                    cboMod1.SelectedItem = hkc.Mods[0].ToString();

                    cbMod2.Checked = true;
                    cboMod2.SelectedItem = hkc.Mods[1].ToString();
                }
            }

            cboKey.SelectedItem = hkc.Key.ToString();
        }

        private void cbMod1_CheckedChanged(object sender, EventArgs e)
        {
            cboMod1.Enabled = cbMod1.Checked;
        }

        private void cbMod2_CheckedChanged(object sender, EventArgs e)
        {
            cboMod2.Enabled = cbMod2.Checked;
        }
    }
}
