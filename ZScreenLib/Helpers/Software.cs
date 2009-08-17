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

namespace ZScreenLib
{
    public class Software
    {
        public string Name { get; set; }
        public string Path { get; set; }

        /// <summary>
        /// Built-in software are protected from being deleted
        /// </summary>
        public bool Protected;

        public Software() { }

        public Software(string sName, string sPath)
        {
            this.Name = sName;
            this.Path = sPath;
        }

        public Software(string sName, string sPath, bool bProtected)
            : this(sName, sPath)
        {
            this.Protected = bProtected;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static bool Exist(string sName)
        {
            foreach (Software software in Program.conf.ImageEditors)
            {
                if (software.Name == sName) return true;
            }
            return false;
        }

        public static bool Remove(string sName)
        {
            if (Exist(sName))
            {
                foreach (Software software in Program.conf.ImageEditors)
                {
                    if (software.Name == sName)
                    {
                        Program.conf.ImageEditors.Remove(software);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}