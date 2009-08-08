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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack;
using ZSS.TextUploadersLib;
using ZSS.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace ZScreenLib
{
    public static class Program
    {
        internal static readonly string LocalAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName);
        public static AppSettings appSettings = AppSettings.Read();

        private static readonly string XMLFileName = "Settings.xml";
        private static readonly string HistoryFileName = "History.xml";
        private static readonly string OldXMLFilePath = Path.Combine(LocalAppDataFolder, XMLFileName);
        private static readonly string OldXMLPortableFile = Path.Combine(Application.StartupPath, XMLFileName);
        private static readonly string PortableRootFolder = Application.ProductName; //Path.Combine(Application.StartupPath, Application.ProductName);

        public static string RootAppFolder { get; set; }
        public static string RootImagesDir { get; private set; }

        public static string CacheDir { get; set; }
        public static string FilesDir { get; set; }
        public static string ImagesDir
        {
            get
            {
                if (conf != null && conf.UseCustomImagesDir && !String.IsNullOrEmpty(conf.CustomImagesDir))
                {
                    return Program.ImagesDir = conf.CustomImagesDir;
                }
                else
                {
                    return GetDefaultImagesDir();
                }
            }
            set { ; }
        }
        public static string LogsDir { get; set; }
        public static string SettingsDir { get; set; }
        public static readonly string TempDir = Path.Combine(LocalAppDataFolder, "Temp");
        public static string TextDir { get; set; }

        private static string[] AppDirs;

        internal static string DefaultXMLFilePath;
        private static string XMLPortableFile;

        public static string DefaultRootAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.ProductName);

        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";
        public const string URL_PROJECTPAGE = "http://code.google.com/p/zscreen";
        public const string URL_HELP = "http://code.google.com/p/zscreen/wiki/Tutorials";
        public const string URL_WEBSITE = "http://brandonz.net/projects/zscreen";

        public const string IMAGESHACK_KEY = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TINYPIC_ID = "e2aabb8d555322fa";
        public const string TINYPIC_KEY = "00a68ed73ddd54da52dc2d5803fa35ee";

        public static readonly string appId = Application.ProductName;  // need for Windows 7 Taskbar
        private static readonly string progId = Application.ProductName; // need for Windows 7 Taskbar
        public const string ZSCREEN_IMAGE_EDITOR = "Image Editor";
        public const string DISABLED_IMAGE_EDITOR = "Disabled";

        public static string[] zImageFileTypes = { "png", "jpg", "gif", "bmp", "tif", "ico" };
        public static string[] zTextFileTypes = { "txt", "log" };
        public static string[] zWebpageFileTypes = { "html", "htm" };

        public static McoreSystem.AppInfo mAppInfo = new McoreSystem.AppInfo(Application.ProductName,
            Application.ProductVersion, McoreSystem.AppInfo.SoftwareCycle.Beta, false);
        public static bool MultipleInstance;
        private static string mProductName = Application.ProductName;

        public static Microsoft.WindowsAPICodePack.Taskbar.JumpList zJumpList;
        public static TaskbarManager zWindowsTaskbar;

        public static void SetRootFolder(string dp)
        {
            appSettings.RootDir = dp;
            RootAppFolder = dp;
        }

        private static string GetDefaultImagesDir()
        {
            string saveFolderPath = string.Empty;
            if (Program.conf != null)
            {
                saveFolderPath = NameParser.Convert(NameParserType.SaveFolder);
            }
            return Path.Combine(RootImagesDir, saveFolderPath);
        }

        /// <summary>
        /// Function to update Default Folder Paths based on Root folder
        /// </summary>
        public static void InitializeDefaultFolderPaths()
        {
            CacheDir = Path.Combine(RootAppFolder, "Cache");
            FilesDir = Path.Combine(RootAppFolder, "Files");

            //if (conf != null && Directory.Exists(conf.CustomImagesDir))
            //{
            //    ImagesDir = conf.CustomImagesDir;
            //}
            //else
            //{
            //    ImagesDir = GetDefaultImagesDir();             
            //}

            LogsDir = Path.Combine(RootAppFolder, "Logs");
            SettingsDir = Path.Combine(RootAppFolder, "Settings");
            TextDir = Path.Combine(RootAppFolder, "Text");

            AppDirs = new[] { CacheDir, FilesDir, RootImagesDir, LogsDir, SettingsDir, TempDir, TextDir };

            foreach (string dp in AppDirs)
            {
                if (!string.IsNullOrEmpty(dp) && !Directory.Exists(dp))
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
                ZSS.IndexersLib.IndexerAdapter.CopyDefaultCss(SettingsDir);
                conf.IndexerConfig.CssFilePath = cssIndexer;
            }
        }

        /// <summary>
        /// Check for file registration if running Windows 7 for Taskbar support
        /// </summary>
        public static void CheckFileRegistration()
        {
            bool registered = false;

            try
            {
                RegistryKey openWithKey = Registry.ClassesRoot.OpenSubKey(Path.Combine(".png", "OpenWithProgIds"));
                string value = openWithKey.GetValue(progId, null) as string;

                if (value == null)
                    registered = false;
                else
                    registered = true;
            }
            finally
            {
                // Let the user know
                if (!registered)
                {
                    TaskDialog td = new TaskDialog();

                    td.Text = "File type is not registered";
                    td.InstructionText = "ZScreen needs to register image files as associated files to properly execute the Taskbar related features.";
                    td.Icon = TaskDialogStandardIcon.Information;
                    td.Cancelable = true;

                    TaskDialogCommandLink button1 = new TaskDialogCommandLink("registerButton", "Register file type for this application",
                        "Register image/text files with this application to run ZScreen correctly.");

                    // Show UAC sheild as this task requires elevation
                    button1.ShowElevationIcon = true;

                    td.StandardButtons = TaskDialogStandardButtons.None;
                    td.Controls.Add(button1);

                    TaskDialogResult tdr = td.Show();

                    RegistrationHelper.RegisterFileAssociations(
                               progId,
                               false,
                               appId,
                               Application.ExecutablePath + " /doc %1",
                               GetExtensionsToRegister());
                }
            }
        }

        private static string GetExtensionsToRegister()
        {
            StringBuilder sbExt = new StringBuilder();
            foreach (string ext in zImageFileTypes)
            {
                sbExt.Append(".");
                sbExt.Append(ext);
                sbExt.Append(" ");
            }
            foreach (string ext in zTextFileTypes)
            {
                sbExt.Append(".");
                sbExt.Append(ext);
                sbExt.Append(" ");
            }
            foreach (string ext in zWebpageFileTypes)
            {
                sbExt.Append(".");
                sbExt.Append(ext);
                sbExt.Append(" ");
            }
            return sbExt.ToString().Trim();
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
        public static bool CLImode { get; set; }

        public static KeyboardHook ZScreenKeyboardHook = new KeyboardHook();

        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            CLImode = args.Length > 1;

            if (args.Length > 2 && args[1] == "/doc")
            {
                string filePath = string.Join(" ", args, 2, args.Length - 2);
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
            }
            else
            {
                RunZScreen();
            }
        }

        private static void RunZScreen()
        {
            FileSystem.AppendDebug("Operating System: " + Environment.OSVersion.VersionString);
            FileSystem.AppendDebug("Product Version: " + mAppInfo.GetApplicationTitleFull());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigWizard cw = null;
            if (string.IsNullOrEmpty(appSettings.RootDir))
            {
                cw = new ConfigWizard(DefaultRootAppFolder);
                cw.ShowDialog();
                appSettings.RootDir = cw.RootFolder;
                appSettings.Save();
            }

            if (Directory.Exists(PortableRootFolder))
            {
                RootAppFolder = PortableRootFolder;
                mProductName += " Portable";
                mAppInfo.AppName = mProductName;
            }
            else
            {
                RootAppFolder = appSettings.RootDir;
            }

            RootImagesDir = Path.Combine(RootAppFolder, "Images"); // after RootAppFolder is set, now set RootImagesDir
            InitializeDefaultFolderPaths(); // happens before XMLSettings is readed
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