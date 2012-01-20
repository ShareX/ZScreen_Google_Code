#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2012 ZScreen Developers

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
using System.Windows.Forms;

namespace Updater
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string url = string.Empty;

            if (args.Length > 0)
            {
                url = args[1];
            }

#if DEBUG
            Application.Run(new DownloaderForm(url));
#else
            if (!string.IsNullOrEmpty(url))
            {
                Application.Run(new UpdaterForm(url));
            }
            else
            {
                MessageBox.Show("Update did not succeed.\n\n" + String.Format("URL: {0}\nCommand Line: {1}\nArguments length: {2}",
                    url, Environment.CommandLine, args.Length), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
#endif
        }
    }
}