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
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Threading;

namespace ZSS
{
    static class Program
    {
        private static readonly string LocalAppDataFolder = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName);

        private static readonly string XMLFileName = "Settings.xml";
        private static readonly string HistoryFileName = "History.xml";
        private static readonly string OldXMLFilePath = Path.Combine(LocalAppDataFolder, XMLFileName);
        private static readonly string OldXMLPortableFile = Path.Combine(Application.StartupPath, XMLFileName);
        private static readonly string PortableRootFolder = Path.Combine(Application.StartupPath, Application.ProductName);

        private static string DefaultSettingsFolder;
        private static string DefaultImagesFolder;
        private static string DefaultTempFolder;
        private static string DefaultTextFolder;
        private static string DefaultCacheFolder;
        private static string DefaultXMLFilePath;
        private static string XMLPortableFile;

        private static string DefaultRootAppFolder = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.Personal), Application.ProductName);

        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";
        public const string URL_PROJECTPAGE = "http://code.google.com/p/zscreen/";
        public const string URL_WEBSITE = "http://brandonz.net/projects/zscreen/";

        public const string IMAGESHACK_KEY = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TINYPIC_ID = "e2aabb8d555322fa";
        public const string TINYPIC_KEY = "00a68ed73ddd54da52dc2d5803fa35ee";

        public static string[] mFileTypes = { "png", "jpg", "gif", "bmp", "tif", "emf", "wmf", "ico" };

        public static McoreSystem.AppInfo mAppInfo = new McoreSystem.AppInfo(Application.ProductName,
            Application.ProductVersion, McoreSystem.AppInfo.SoftwareCycle.Beta, false);
        public static bool MultipleInstance = false;

        /// <summary>
        /// Root Folder of Images, Text, Settings, Cache. 
        /// </summary>
        public static string RootFolder
        {
            get
            {
                if (Directory.Exists(PortableRootFolder))
                    return PortableRootFolder;
                else
                {
                    return DefaultRootAppFolder;
                }
            }
            set
            {
                DefaultRootAppFolder = value;
            }
        }

        public static string XMLSettingsFile
        {
            get
            {
                DefaultSettingsFolder = Path.Combine(RootFolder, "Settings");
                DefaultImagesFolder = Path.Combine(RootFolder, "Images");
                DefaultTextFolder = Path.Combine(RootFolder, "Text");
                DefaultTempFolder = Path.Combine(RootFolder, "Temp");
                DefaultCacheFolder = Path.Combine(RootFolder, "Cache");

                DefaultXMLFilePath = Path.Combine(DefaultSettingsFolder, XMLFileName);
                XMLPortableFile = Path.Combine(DefaultSettingsFolder, XMLFileName);

                if (!Directory.Exists(DefaultSettingsFolder))
                {
                    Directory.CreateDirectory(DefaultSettingsFolder);
                }

                if (File.Exists(OldXMLPortableFile))
                {
                    if (!File.Exists(XMLPortableFile))
                    {
                        File.Move(OldXMLPortableFile, XMLPortableFile);
                    }
                    return XMLPortableFile;                               // Portable
                }
                else if (File.Exists(OldXMLFilePath))
                {
                    if (!File.Exists(DefaultXMLFilePath))
                    {
                        File.Move(OldXMLFilePath, DefaultXMLFilePath);
                    }
                    return DefaultXMLFilePath;                            // v1.x
                }
                else
                {
                    return DefaultXMLFilePath;                            // v2.x
                }
            }
        }

        public static string HistoryFile
        {
            get
            {
                return Path.Combine(DefaultSettingsFolder, HistoryFileName);
            }
        }

        public static XMLSettings conf = XMLSettings.Read();

        public const string FILTER_ACCOUNTS = "ZScreen FTP Accounts(*.zfa)|*.zfa";
        public const string FILTER_IMAGE_HOSTING_SERVICES = "ZScreen Image Uploaders(*.zihs)|*.zihs";
        public const string FILTER_SETTINGS = "ZScreen XML Settings(*.xml)|*.xml";

        public static Rectangle LastRegion = Rectangle.Empty;
        public static Rectangle LastCapture = Rectangle.Empty;

        private static ZScreen ZScreenWindow;

        private static Mutex mAppMutex = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool bGrantedOwnership;
            try
            {
                mAppMutex = new Mutex(true, @"Global\0167D1A0-6054-42f5-BA2A-243648899A6B", out bGrantedOwnership);
            }
            catch (UnauthorizedAccessException)
            {
                bGrantedOwnership = false;
            }

            if (!bGrantedOwnership)
            {
                MultipleInstance = true;
                mAppInfo.AppName = Application.ProductName + "*";
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ZScreenWindow = new ZScreen();

            User32.m_Proc = ZScreenWindow.ScreenshotUsingHotkeys;

            ZScreenWindow.m_hID = User32.setHook();

            Application.Run(ZScreenWindow);

            User32.UnhookWindowsHookEx(ZScreenWindow.m_hID);
        }

        public static void ConfigureDirs()
        {
            // Settings         
            if (string.IsNullOrEmpty(Program.conf.SettingsDir))
            {
                conf.SettingsDir = DefaultSettingsFolder;
            }
            if (!Directory.Exists(conf.SettingsDir))
            {
                Directory.CreateDirectory(DefaultSettingsFolder);
            }
            // Images
            if (string.IsNullOrEmpty(conf.ImagesDir))
            {
                conf.ImagesDir = DefaultImagesFolder;
            }
            if (!Directory.Exists(conf.ImagesDir))
            {
                Directory.CreateDirectory(DefaultImagesFolder);
            }
            // Text
            if (string.IsNullOrEmpty(conf.TextDir))
            {
                conf.TextDir = DefaultTextFolder;
            }
            if (!Directory.Exists(conf.TextDir))
            {
                Directory.CreateDirectory(DefaultTextFolder);
            }
            // Cache
            if (string.IsNullOrEmpty(Program.conf.CacheDir))
            {
                conf.CacheDir = DefaultCacheFolder;
            }
            if (!Directory.Exists(Program.conf.CacheDir))
            {
                Directory.CreateDirectory(Program.conf.CacheDir);
            }
            // Temp
            if (string.IsNullOrEmpty(Program.conf.TempDir))
            {
                conf.TempDir = DefaultTempFolder;
            }
            if (!Directory.Exists(Program.conf.TempDir))
            {
                Directory.CreateDirectory(Program.conf.TempDir);
            }
        }
    }
}