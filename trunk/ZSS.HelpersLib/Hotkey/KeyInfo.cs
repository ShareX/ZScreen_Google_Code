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

using System.Text;
using System.Windows.Forms;

namespace HelpersLib
{
    public class KeyInfo
    {
        public Keys Key { get; set; }

        public KeyInfo(Keys key)
        {
            Key = key;
        }

        public override string ToString()
        {
            string name = string.Empty;

            if ((Key & Keys.Control) == Keys.Control)
            {
                name += "Control + ";
            }

            if ((Key & Keys.Shift) == Keys.Shift)
            {
                name += "Shift + ";
            }

            if ((Key & Keys.Alt) == Keys.Alt)
            {
                name += "Alt + ";
            }

            Keys vk = Key & ~Keys.Modifiers;

            if (vk >= Keys.D0 || vk <= Keys.D9)
            {
                name += (vk - Keys.D0).ToString();
            }
            else
            {
                name += ProperString(Key);
            }

            return name;
        }

        private string ProperString(Keys key)
        {
            string name = key.ToString();

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i]))
                {
                    result.Append(" " + name[i]);
                }
                else
                {
                    result.Append(name[i]);
                }
            }

            return result.ToString();
        }
    }
}