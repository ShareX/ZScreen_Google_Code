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
using System.Linq;
using System.Windows.Forms;
using NDesk.Options;
using System.IO;

namespace Updater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string mUrl = string.Empty;
            string mFilePath = string.Empty;
            bool mRunAs = false;

            var p = new OptionSet() 
            {
                { "l|url=", "URL of the updated setup", v => mUrl = v },
                { "fp|filepath=", "File path of the running application", v => mFilePath = v }, 
                { "runas=", "Whether or not to run as Administrator", (bool v) => mRunAs = v },
            };

            p.Parse(args);

            if (!string.IsNullOrEmpty(mUrl) && File.Exists(mFilePath))
            {
                Application.Run(new UpdaterForm(mUrl, mFilePath, mRunAs));
            }
            else
            {
                MessageBox.Show("Update did not succeed.\n\n" + String.Format("URL: {0}\nApplication Path: {1}\nCommand Line: {2}\nArguments: {3}",
                    mUrl, mFilePath, Environment.CommandLine, args.Length), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}