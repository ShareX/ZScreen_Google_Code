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
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Threading;
using ZSS.Properties;
using ZSS.Forms;
using ZSS.Helpers;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.URLShorteners;
using Microsoft.WindowsAPICodePack;
using Microsoft.WindowsAPICodePack.Shell.Taskbar;
using Microsoft.WindowsAPICodePack.Shell;

namespace ZSS
{
    static class Program
    {
        private static readonly string LocalAppDataFolder = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), Application.ProductName);

        private static readonly string XMLFileName = "Settings.xml";
        private static readonly string HistoryFileName = "History.xml";
        private static readonly string OldXMLFilePath = Path.Combine(LocalAppDataFolder, XMLFileName);
        private static readonly string OldXMLPortableFile = Path.Combine(Application.StartupPath, XMLFileName);
        private static readonly string PortableRootFolder = Application.ProductName; //Path.Combine(Application.StartupPath, Application.ProductName);

        public static string RootAppFolder { get; set; }

        public static string CacheDir { get; set; }
        public static string FilesDir { get; set; }
        public static string ImagesDir
        {
            get
            {
                if (conf != null && !String.IsNullOrEmpty(conf.ImagesDir))
                {
                    return Program.ImagesDir = conf.ImagesDir;
                }
                else
                {
                    return Path.Combine(RootAppFolder, "Images");
                }
            }
            set
            {
                ;
            }
        }
        public static string LogsDir { get; set; }
        public static string SettingsDir { get; set; }
        public static string TempDir { get; set; }
        public static string TextDir { get; set; }

        private static string[] AppDirs;

        internal static string DefaultXMLFilePath;
        private static string XMLPortableFile;

        private static string DefaultRootAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.ProductName);

        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";
        public const string URL_PROJECTPAGE = "http://code.google.com/p/zscreen";
        public const string URL_HELP = "http://code.google.com/p/zscreen/wiki/Tutorials";
        public const string URL_WEBSITE = "http://brandonz.net/projects/zscreen";

        public const string IMAGESHACK_KEY = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TINYPIC_ID = "e2aabb8d555322fa";
        public const string TINYPIC_KEY = "00a68ed73ddd54da52dc2d5803fa35ee";

        public const string ZSCREEN_IMAGE_EDITOR = "Image Editor";
        public const string DISABLED_IMAGE_EDITOR = "Disabled";

        public static string[] zImageFileTypes = { "png", "jpg", "gif", "bmp", "tif", "ico" };
        public static string[] zTextFileTypes = { "txt", "log" };
        public static string[] zWebpageFileTypes = { "html", "htm" };

        public static McoreSystem.AppInfo mAppInfo = new McoreSystem.AppInfo(Application.ProductName,
            Application.ProductVersion, McoreSystem.AppInfo.SoftwareCycle.Beta, false);
        public static bool MultipleInstance;
        private static string mProductName = Application.ProductName;

        public static void SetRootFolder(string dp)
        {
            Settings.Default.RootDir = dp;
            RootAppFolder = dp;
        }

        /// <summary>
        /// Function to update Default Folder Paths based on Root folder
        /// </summary>
        public static void InitializeDefaultFolderPaths()
        {
            CacheDir = Path.Combine(RootAppFolder, "Cache");
            FilesDir = Path.Combine(RootAppFolder, "Files");

            if (conf != null && Directory.Exists(conf.ImagesDir))
            {
                ImagesDir = conf.ImagesDir;
            }
            else
            {
                ImagesDir = Path.Combine(RootAppFolder, "Images");
            }

            LogsDir = Path.Combine(RootAppFolder, "Logs");
            SettingsDir = Path.Combine(RootAppFolder, "Settings");
            TextDir = Path.Combine(RootAppFolder, "Text");
            TempDir = Path.Combine(RootAppFolder, "Temp");

            AppDirs = new[] { CacheDir, FilesDir, ImagesDir, LogsDir, SettingsDir, TempDir, TextDir };

            foreach (string dp in AppDirs)
            {
                if (!Directory.Exists(dp))
                {
                    Directory.CreateDirectory(dp);
                }
            }

            DefaultXMLFilePath = Path.Combine(SettingsDir, XMLFileName);
            XMLPortableFile = Path.Combine(SettingsDir, XMLFileName);
        }

        public static void InitializeFiles()
        {
            string cssIndexer = Path.Combine(SettingsDir, ZSS.IndexersLib.IndexerConfig.DefaultCssFileName);
            if (!File.Exists(cssIndexer))
            {
                IndexersLib.IndexerAdapter.CopyDefaultCss(SettingsDir);
                conf.IndexerConfig.CssFilePath = cssIndexer;
            }
        }

        public static void ZScreen_Windows7onlyTasks()
        {
            if (CoreHelpers.RunningOnWin7)
            {
                JumpList jumpList = Taskbar.JumpList;
                //jumpList.UserTasks.Add(new JumpListLink
                //{
                //    Title = "Crop Shot",
                //    Arguments = "crop_shot",
                //    Path = Application.ExecutablePath,
                //    IconReference = new IconReference(Application.ExecutablePath, 0)
                //});
                CustomCategory paths = new CustomCategory("Paths");
                paths.JumpListItems.Add(new JumpListLink
                {
                    Title = "Images",
                    IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0),
                    Path = Program.ImagesDir
                });
                paths.JumpListItems.Add(new JumpListLink
                {
                    Title = "Settings",
                    IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0),
                    Path = Program.SettingsDir
                });
                jumpList.CustomCategories.Add(paths);
                Taskbar.JumpList.RefreshTaskbarList();
            }
        }

        public static string XMLSettingsFile
        {
            get
            {
                if (!Directory.Exists(SettingsDir))
                {
                    Directory.CreateDirectory(SettingsDir);
                }

                if (File.Exists(OldXMLPortableFile))
                {
                    if (!File.Exists(XMLPortableFile))
                    {
                        File.Move(OldXMLPortableFile, XMLPortableFile);
                    }
                    return XMLPortableFile;                               // Portable
                }

                if (File.Exists(OldXMLFilePath))
                {
                    if (!File.Exists(DefaultXMLFilePath))
                    {
                        File.Move(OldXMLFilePath, DefaultXMLFilePath);
                    }
                    return DefaultXMLFilePath;                            // v1.x
                }

                return DefaultXMLFilePath;                                // v2.x
            }
        }

        public static string HistoryFile
        {
            get
            {
                return Path.Combine(SettingsDir, HistoryFileName);
            }
        }

        public static XMLSettings conf;
        internal static GoogleTranslate mGTranslator;

        public const string EXT_FTP_ACCOUNTS = "zfa";
        public static readonly string FILTER_ACCOUNTS = string.Format("ZScreen FTP Accounts(*.{0})|*.{0}", EXT_FTP_ACCOUNTS);
        public const string FILTER_IMAGE_HOSTING_SERVICES = "ZScreen Image Uploaders(*.zihs)|*.zihs";
        public const string FILTER_SETTINGS = "ZScreen XML Settings(*.xml)|*.xml";

        public static Rectangle LastRegion = Rectangle.Empty;
        public static Rectangle LastCapture = Rectangle.Empty;

        public static WorkerPrimary Worker;
        public static WorkerSecondary Worker2;

        public static Mutex mAppMutex;

        public static KeyboardHook ZScreenKeyboardHook = new KeyboardHook();

        [STAThread]
        static void Main()
        {
            FileSystem.AppendDebug("Operating System: " + Environment.OSVersion.VersionString);
            FileSystem.AppendDebug("Product Version: " + mAppInfo.GetApplicationTitleFull());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigWizard cw = null;
            if (String.IsNullOrEmpty(Settings.Default.RootDir))
            {
                /* 
                 * Everytime SVN Revision is updated in AssemblyVersion, Default settings reset
                 * So we need to Upgrade settings
                 */
                Settings.Default.Upgrade();
                if (String.IsNullOrEmpty(Settings.Default.RootDir))
                {
                    // If RootDir is still empty that means it is a new installation
                    cw = new ConfigWizard(DefaultRootAppFolder);
                    cw.ShowDialog();
                    Settings.Default.RootDir = cw.RootFolder;
                }
            }

            if (Directory.Exists(PortableRootFolder))
            {
                RootAppFolder = PortableRootFolder;
                mProductName += " Portable";
                mAppInfo.AppName = mProductName;
            }
            else
            {
                RootAppFolder = Settings.Default.RootDir;
            }

            InitializeDefaultFolderPaths();
            conf = XMLSettings.Read();
            InitializeFiles();

            FileSystem.AppendDebug(string.Format("Root Folder: {0}", Program.RootAppFolder));

            // Use Configuration Wizard Settings if applied
            if (cw != null)
            {
                conf.ScreenshotDestMode = cw.ImageDestinationType;
            }

            bool bGrantedOwnership;
            try
            {
                Guid assemblyGuid = Guid.Empty;
                object[] assemblyObjects = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), true);
                if (assemblyObjects.Length > 0)
                {
                    assemblyGuid = new Guid(((System.Runtime.InteropServices.GuidAttribute)assemblyObjects[0]).Value);
                }
                mAppMutex = new Mutex(true, assemblyGuid.ToString(), out bGrantedOwnership);
            }
            catch (UnauthorizedAccessException)
            {
                bGrantedOwnership = false;
            }

            if (!bGrantedOwnership)
            {
                MultipleInstance = true;
                mProductName += "*";
                mAppInfo.AppName = mProductName;
            }

            try
            {
                Application.Run(new ZScreen());
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex);
            }
            finally
            {
                FileSystem.WriteDebugFile();
                ZScreenKeyboardHook.Dispose();
            }
        }
    }
}