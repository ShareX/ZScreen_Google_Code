#region License Information (GPL v2)
/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2010 ZScreen Developers

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
using System.IO;
using System.Windows.Forms;

namespace ZUploader
{
    static class Program
    {
        public static Settings Settings;

        public static string SettingsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.ProductName);
        public static string SettingsFileName = "Settings.xml";
        public static string SettingsFilePath
        {
            get
            {
                return Path.Combine(SettingsDir, SettingsFileName);
            }
        }

        public const string URL_WEBSITE = "http://code.google.com/p/zscreen";
        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";

        public const string ImageShackKey = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TinyPicID = "e2aabb8d555322fa";
        public const string TinyPicKey = "00a68ed73ddd54da52dc2d5803fa35ee";
        public const string ImgurKey = "63499468bcc5d2d6aee1439e50b4e61c";

        [STAThread]
        static void Main()
        {
            Settings = Settings.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            Settings.Save();
        }
    }
}