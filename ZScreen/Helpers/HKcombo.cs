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
using System.Text;
using System.Windows.Forms;

namespace ZSS
{
    public class HKcombo
    {
        public Keys[] Mods;
        public Keys Key;

        public HKcombo()
        {

        }

        public HKcombo(Keys key)
        {
            Mods = null;

            Key = key;
        }

        public HKcombo(Keys mod1, Keys key)
        {
            Mods = new Keys[1];
            Mods[0] = mod1;

            Key = key;
        }

        public HKcombo(Keys mod1, Keys mod2, Keys key)
        {
            Mods = new Keys[2];
            Mods[0] = mod1;
            Mods[1] = mod2;

            Key = key;
        }

        public HKcombo(Keys[] mods, Keys key)
        {
            Mods = mods;

            Key = key;
        }

        public override string ToString()
        {
            string sep = " + ";
            
            //no mods
            if (Mods == null)
                return Key.ToString();

            //one mod
            if (Mods.Length == 1)
                return Mods[0].ToString() + sep + Key.ToString();

            //two mods
            if (Mods.Length == 2)
                return Mods[0].ToString() + sep + Mods[1].ToString() + sep + Key.ToString();

            return "None";
        }
    }
}
