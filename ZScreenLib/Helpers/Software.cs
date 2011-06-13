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

using System.ComponentModel;
using System.Diagnostics;
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
        [Category("Software"), DefaultValue(true), Description("Toggle the behaviour of launching this application for images copied from Explorer or copied to clipboard.")]
        public bool TriggerForImages { get; set; }
        [Category("Software"), DefaultValue(true), Description("Toggle the behaviour of launching this application for screenshots captured using hotkeys such as Print Screen.")]
        public bool TriggerForScreenshots { get; set; }
        [Category("Software"), DefaultValue(false), Description("Toggle the behaviour of launching this application for text.")]
        public bool TriggerForText { get; set; }
        [Category("Software"), DefaultValue(false), Description("Toggle the behaviour of launching this application for files.")]
        public bool TriggerForFiles { get; set; }

        /// <summary>
        /// Built-in software are protected from being deleted
        /// </summary>
        public bool Protected;

        public Software()
        {
            ApplyDefaultValues(this);
        }

        public Software(string name, string path, bool isProtected = false, bool enabled = false)
            : this()
        {
            Name = name;
            Path = path;
            Protected = isProtected;
            Enabled = enabled;
        }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public static Software GetByName(string name)
        {
            foreach (Software software in Engine.conf.ActionsList)
            {
                if (software.Name == name) return software;
            }

            return null;
        }

        public static bool Exist(string name)
        {
            foreach (Software software in Engine.conf.ActionsList)
            {
                if (software.Name == name) return true;
            }

            return false;
        }

        public static bool Remove(string name)
        {
            if (Exist(name))
            {
                foreach (Software software in Engine.conf.ActionsList)
                {
                    if (software.Name == name)
                    {
                        Engine.conf.ActionsList.Remove(software);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Method to open a file using the Software
        /// </summary>
        /// <param name="fp">File path to be opened</param>
        public void OpenFile(string fp)
        {
            RunWithArgs(string.Format("\"{0}\"", fp));
        }

        /// <summary>
        /// Method to run the Software with Arguments
        /// </summary>
        /// <param name="args">Arguments to be passed to the sofware</param>
        private void RunWithArgs(string fp)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Path);
            if (string.IsNullOrEmpty(Args))
            {
                psi.Arguments = fp;
            }
            else
            {
                psi.Arguments = Args.Replace(SyntaxParser.FilePath, fp);
                Engine.MyLogger.WriteLine(string.Format("Running {0} with Arguments: {1}", Path, psi.Arguments));
            }
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
        }
    }
}