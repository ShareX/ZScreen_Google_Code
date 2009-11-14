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

using System.Diagnostics;
using System.ComponentModel;
using System.Drawing.Design;

namespace ZScreenLib
{
    public class Software
    {
        [Category("Software"), DefaultValue(""), Description("Descriptive Name of the Software")]
        public string Name { get; set; }
        [EditorAttribute(typeof(ExeFileNameEditor), typeof(UITypeEditor))]
        [Category("Software"), DefaultValue(""), Description("Location of the Software")]
        public string Path { get; set; }
        [Category("Software"), DefaultValue(SyntaxParser.FilePath), Description("Arguments passed to the application. Use " + SyntaxParser.FilePath + " syntax to specify the file path of the image that is going to be processed.")]
        public string Args { get; set; }
        [Browsable(false)]
        public bool Enabled { get; set; }
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

        public Software(string sName, string sPath, bool bProtected, bool bEnabled)
            : this(sName, sPath, bProtected)
        {
            this.Enabled = bEnabled;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static bool Exist(string sName)
        {
            foreach (Software software in Engine.conf.ImageEditors)
            {
                if (software.Name == sName) return true;
            }
            return false;
        }

        public static bool Remove(string sName)
        {
            if (Exist(sName))
            {
                foreach (Software software in Engine.conf.ImageEditors)
                {
                    if (software.Name == sName)
                    {
                        Engine.conf.ImageEditors.Remove(software);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Method to run the Software with Arguments
        /// </summary>
        /// <param name="args">Arguments to be passed to the sofware</param>
        private void RunWithArgs(string fp)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(this.Path);
            if (string.IsNullOrEmpty(this.Args))
            {
                psi.Arguments = fp;
            }
            else
            {
                psi.Arguments = this.Args.Replace(SyntaxParser.FilePath, fp);
                FileSystem.AppendDebug(string.Format("Running {0} with Arguments: {1}", Path, psi.Arguments));
            }
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
        }

        /// <summary>
        /// Method to open a file using the Software
        /// </summary>
        /// <param name="fp">File path to be opened</param>
        public void OpenFile(string fp)
        {
            RunWithArgs(string.Format("{0}{1}{0}", "\"", fp));
        }
    }
}