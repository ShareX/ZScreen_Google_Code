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
using System.Diagnostics;
using System.Threading;

namespace HelpersLib
{
    public static class StaticHelper
    {
        public const string FILTER_XML_FILES = "XML Files(*.xml)|*.xml";
        public const string FILTER_EXE_FILES = "All Files(*.exe)|*.exe";

        public static Logger MyLogger { private get; set; }

        public static void WriteLine(string message)
        {
            if (MyLogger != null)
            {
                MyLogger.WriteLine(message);
            }
            else
            {
                Debug.WriteLine(message);
            }
        }

        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public static void WriteException(Exception e, string message = "Exception")
        {
            if (MyLogger != null)
            {
                MyLogger.WriteException(e, message);
            }
            else
            {
                Debug.WriteLine(e);
            }
        }

        public static void LoadBrowser(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                ThreadPool.QueueUserWorkItem(x => Process.Start(url));
            }
        }

        public static void ShowDirectory(string dir)
        {
            Process.Start("explorer.exe", dir);
        }

        public static bool IsWindows8()
        {
            return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}