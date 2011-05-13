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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GraphicsMgrLib;
using HelpersLib;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersLib.HelperClasses;

namespace ZScreenLib
{
    public static class Engine
    {
        // App Info
        private static string mProductName = Application.ProductName;
        private static readonly string PortableRootFolder = Application.ProductName; // using relative paths

        public const string ZScreenCLI = "ZScreenCLI.exe";
        public static bool Portable = Directory.Exists(Path.Combine(Application.StartupPath, PortableRootFolder));
        public static bool MultipleInstance { get; private set; }

        public static IntPtr zHandle = IntPtr.Zero;

        public static McoreSystem.AppInfo mAppInfo = new McoreSystem.AppInfo(mProductName, Application.ProductVersion);

        internal static readonly string zRoamingAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), mProductName);
        internal static readonly string zLocalAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), mProductName);
        internal static readonly string zCacheDir = Path.Combine(zLocalAppDataFolder, "Cache");
        internal static readonly string zFilesDir = Path.Combine(zLocalAppDataFolder, "Files");
        internal static readonly string zLogsDir = Path.Combine(zLocalAppDataFolder, "Logs");
        internal static readonly string zPicturesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), mProductName);
        internal static readonly string zSettingsDir = Path.Combine(zRoamingAppDataFolder, "Settings");
        internal static readonly string zTextDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), mProductName);
        internal static readonly string zTempDir = Path.Combine(zLocalAppDataFolder, "Temp");

        public static AppSettings mAppSettings = AppSettings.Read();

        private static readonly string XMLFileName = XMLSettings.XMLFileName;
        private static readonly string HistoryFileName = "ZScreenHistory.xml";
        private static readonly string OldXMLFilePath = Path.Combine(zLocalAppDataFolder, XMLFileName);
        private static readonly string OldXMLPortableFile = Path.Combine(Application.StartupPath, XMLFileName);

        public static string DefaultRootAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), mProductName);
        public static string RootAppFolder = zLocalAppDataFolder;
        public static string RootImagesDir = zPicturesDir;

        public static string CacheDir = zCacheDir;
        public static string FilesDir = zFilesDir;
        public static string LogsDir = zLogsDir;
        public static string SettingsDir = zSettingsDir;
        public static string TextDir = zTextDir;
        public static string TempDir = zTempDir;
        public static string ImagesDir
        {
            get
            {
                if (conf != null && conf.UseCustomImagesDir && !String.IsNullOrEmpty(conf.CustomImagesDir))
                {
                    return Engine.ImagesDir = conf.CustomImagesDir;
                }
                else
                {
                    return GetDefaultImagesDir();
                }
            }
            set { ; }
        }

        private static string[] AppDirs;

        // URLS
        public const string URL_WEBSITE = "http://code.google.com/p/zscreen";
        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";
        public const string URL_WIKIPAGES = "http://code.google.com/p/zscreen/w/list";
        public const string URL_HELP = "http://code.google.com/p/zscreen/wiki/Tutorials";
        public const string URL_UPDATE = "http://zscreen.googlecode.com/svn/trunk/Update.xml";
        public const string URL_BERK = "http://code.google.com/u/flexy123";
        public const string URL_MIKE = "http://code.google.com/u/mcored";
        public const string URL_BRANDON = "http://code.google.com/u/rgrthat";

        // Image Uploaders
        public const string ImageShackKey = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TinyPicID = "e2aabb8d555322fa";
        public const string TinyPicKey = "00a68ed73ddd54da52dc2d5803fa35ee";
        public const string ImgurAnonymousKey = "5468eb7bde830c779b37da30c5c7ebae";
        public const string ImgurConsumerKey = "cc6a3227dc7cbe15d2754b194ae3c26504db122ab";
        public const string ImgurConsumerSecret = "edd13f72e7c9908b50c8090a8e912b73";
        public const string FlickrKey = "009382d913746758f23d0ba9906b9fde";
        public const string FlickrSecret = "7a147b763b1c7ebc";
        public const string UploadScreenshotKey = "2807828f379860848433221358";
        public const string ImageBamKey = "P9MMWVORXYCVM9LL";
        public const string ImageBamSecret = "8NHVT2W777DEHZBV54A4CIT23XJFTL1D";
        public const string TwitsnapsKey = "694aaea3bc8f28479c383cbcc094fb55";

        // File Uploaders
        public const string DropboxConsumerKey = "0te7j9ype9lrdfn";
        public const string DropboxConsumerSecret = "r5d3aptd9a0cwp9";
        public const string SendSpaceKey = "LV6OS1R0Q3";
        public const string DropIOKey = "6c65e2d2bfd858f7d0aa6509784f876483582eea";

        // Text Uploaders
        public const string PastebinKey = "4b23be71ec78bbd4fb96735320aa09ef";
        public const string PastebinCaKey = "KxTofLKQThSBZ63Gpa7hYLlMdyQlMD6u";

        // URL Shorteners
        public const string BitlyLogin = "jaex";
        public const string BitlyKey = "R_1734f57b772acb3c048eb2365075743b";
        public const string BitlyConsumerKey = "91e5a02e001ada9c1122f54d73bc442d9cc2a7ab";
        public const string BitlyConsumerSecret = "fe5f906b5f5e8114d5c266a709a9438c94e1cd3f";
        public const string GoogleURLShortenerKey = "AIzaSyCcYJvYPvS3UE0JqqsSNpjPjN1NPBmMbmE";
        public const string KlamKey = "a4e5a8de710d80db774a8264f4588ffb";
        public const string ThreelyKey = "em5893833";

        // Other Services
        public const string TwitterConsumerKey = "Jzzcm6ytcyml14sQIvqvmA";
        public const string TwitterConsumerSecret = "aJYZ9W1gJnGMgSqhRYrvoUyUc14FssVJOFAqHjriU";
        public const string GoogleTranslateKey = "AIzaSyCcYJvYPvS3UE0JqqsSNpjPjN1NPBmMbmE";
        public const string PicnikKey = "3aacd2de4563b8817de708b29b5bdd0e";

        public static readonly string appId = Application.ProductName;  // need for Windows 7 Taskbar
        private static readonly string progId = Application.ProductName; // need for Windows 7 Taskbar
        public const string ZSCREEN_IMAGE_EDITOR = "Image Editor";

        public static ImageFileFormat zImageFileFormat = new ImageFileFormatPng();
        public static ImageFileFormat zImageFileFormatSwitch = new ImageFileFormatJpg();
        public static string[] zImageFileTypes = { "png", "jpg", "gif", "bmp", "tif" };
        public static string[] zTextFileTypes = { "txt", "log" };
        public static string[] zWebpageFileTypes = { "html", "htm" };

        public static ClipboardHook zClipboardHook = null;
        public static string zClipboardText = string.Empty;

        private static TaskDialog td = null;
        public static JumpList zJumpList;
        public static TaskbarManager zWindowsTaskbar;
        private static bool RunConfig = false;

        public class EngineOptions
        {
            public bool KeyboardHook { get; set; }
            public bool ShowConfigWizard { get; set; }
        }

        public static void TurnOn()
        {
            TurnOn(new EngineOptions());
        }

        public static bool TurnOn(EngineOptions options)
        {
            FileSystem.AppendDebug("Operating System: " + Environment.OSVersion.VersionString);
            FileSystem.AppendDebug(string.Format("Product Version: {0} rev {1}", mAppInfo.GetApplicationTitle(), Adapter.AppRevision));
            DialogResult configResult = DialogResult.OK;

            if (Directory.Exists(Path.Combine(Application.StartupPath, PortableRootFolder)))
            {
                mAppSettings.PreferSystemFolders = false;
                RootAppFolder = PortableRootFolder;
                mProductName += " Portable";
                mAppInfo.AppName = mProductName;
            }
            else
            {
                if (options.ShowConfigWizard && string.IsNullOrEmpty(Engine.mAppSettings.RootDir))
                {
                    ConfigWizard cw = new ConfigWizard(DefaultRootAppFolder);
                    configResult = cw.ShowDialog();
                    Engine.mAppSettings.RootDir = cw.RootFolder;
                    Engine.mAppSettings.PreferSystemFolders = cw.PreferSystemFolders;
                    Engine.mAppSettings.ImageUploader = (int)cw.ImageDestinationType;
                    Engine.mAppSettings.FileUploader = (int)cw.FileUploaderType;
                    Engine.mAppSettings.TextUploader = (int)cw.MyTextUploaderType;
                    Engine.mAppSettings.UrlShortener = (int)cw.MyUrlShortenerType;
                    if (!Portable)
                    {
                        mAppSettings.Write(); // DONT UPDATE FOR PORTABLE MODE
                    }
                    RunConfig = true;
                }
                if (!string.IsNullOrEmpty(Engine.mAppSettings.RootDir) && Directory.Exists(Engine.mAppSettings.RootDir))
                {
                    RootAppFolder = Engine.mAppSettings.RootDir;
                }
                else
                {
                    RootAppFolder = Engine.mAppSettings.PreferSystemFolders ? zLocalAppDataFolder : DefaultRootAppFolder;
                }
            }

            if (configResult == DialogResult.OK)
            {
                FileSystem.AppendDebug("Config file: " + AppSettings.AppSettingsFile);
                FileSystem.AppendDebug(string.Format("Root Folder: {0}", mAppSettings.PreferSystemFolders ? zLocalAppDataFolder : RootAppFolder));
                FileSystem.AppendDebug("Initializing Default folder paths...");
                Engine.InitializeDefaultFolderPaths(); // happens before XMLSettings is readed
                // ZSS.Loader.Splash.AsmLoads.Enqueue("Reading " + Path.GetFileName(Program.XMLSettingsFile));

                bool bGrantedOwnership;
                try
                {
                    Guid assemblyGuid = Guid.Empty;
                    object[] assemblyObjects = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), true);
                    if (assemblyObjects.Length > 0)
                    {
                        assemblyGuid = new Guid(((System.Runtime.InteropServices.GuidAttribute)assemblyObjects[0]).Value);
                    }
                    Engine.mAppMutex = new Mutex(true, assemblyGuid.ToString(), out bGrantedOwnership);
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
            }

            return configResult == DialogResult.OK;
        }

        public static void LoadSettings()
        {
            LoadSettings(string.Empty);
        }

        public static void LoadSettingsLatest()
        {
            string fp = GetLatestSettingsFile();
            XMLSettings.XMLFileName = Path.GetFileName(fp);
            LoadSettings(fp);
        }

        public static string GetLatestSettingsFile()
        {
            return GetLatestSettingsFile(Path.GetDirectoryName(Engine.mAppSettings.XMLSettingsFile));
        }

        public static string GetLatestSettingsFile(string settingsDir)
        {
            string fp = string.Empty;
            if (!string.IsNullOrEmpty(settingsDir))
            {
                List<ImageFile> imgFiles = new List<ImageFile>();
                string[] files = Directory.GetFiles(settingsDir, Application.ProductName + "-*-Settings.xml");
                if (files.Length > 0)
                {
                    foreach (string f in files)
                    {
                        imgFiles.Add(new ImageFile(f));
                    }
                    imgFiles.Sort();
                    fp = imgFiles[imgFiles.Count - 1].LocalFilePath;
                }
            }
            return fp;
        }

        public static void LoadSettings(string fp)
        {
            if (string.IsNullOrEmpty(fp))
            {
                Engine.conf = XMLSettings.Read();
                FileSystem.AppendDebug("Finished reading " + Engine.mAppSettings.XMLSettingsFile);
            }
            else
            {
                FileSystem.AppendDebug("Reading " + fp);
                Engine.conf = XMLSettings.Read(fp);
            }

            Engine.InitializeFiles();

            // Use Configuration Wizard Settings if applied
            if (RunConfig)
            {
                Engine.conf.PreferSystemFolders = Engine.mAppSettings.PreferSystemFolders;
                Engine.conf.MyImageUploader = Engine.mAppSettings.ImageUploader;
                Engine.conf.MyFileUploader = Engine.mAppSettings.FileUploader;
                Engine.conf.MyTextUploader = Engine.mAppSettings.TextUploader;
                Engine.conf.MyURLShortener = Engine.mAppSettings.UrlShortener;
            }
            // Portable then we don't need PreferSystemFolders to be true
            if (Portable)
            {
                Engine.conf.PreferSystemFolders = false;
            }
        }

        public static void TurnOff()
        {
            if (!Portable)
            {
                mAppSettings.Write(); // DONT UPDATE FOR PORTABLE MODE
            }
            if (null != ZScreenKeyboardHook)
            {
                ZScreenKeyboardHook.Dispose();
                FileSystem.AppendDebug("Keyboard Hook terminated");
            }
            FileSystem.WriteDebugFile();
            if (Engine.conf != null)
            {
                Engine.conf.Write();
            }
        }

        public static void SetRootFolder(string dp)
        {
            mAppSettings.RootDir = dp;
            RootAppFolder = dp;
        }

        public static string GetProductName()
        {
            return mAppInfo.GetApplicationTitle(McoreSystem.AppInfo.VersionDepth.MajorMinorBuildRevision);
        }

        private static string GetDefaultImagesDir()
        {
            string imagesDir = RootImagesDir;
            string saveFolderPath = string.Empty;
            if (Engine.conf != null)
            {
                saveFolderPath = new NameParser(NameParserType.SaveFolder).Convert(Engine.conf.SaveFolderPattern);
                if (!Portable && Engine.conf.PreferSystemFolders)
                {
                    imagesDir = zPicturesDir;
                }
            }
            return Path.Combine(imagesDir, saveFolderPath);
        }

        /// <summary>
        /// Function to update Default Folder Paths based on Root folder
        /// </summary>
        public static void InitializeDefaultFolderPaths()
        {
            if (mAppSettings.PreferSystemFolders)
            {
                CacheDir = zCacheDir;
                FilesDir = zFilesDir;
                LogsDir = zLogsDir;
                SettingsDir = zSettingsDir;
                RootImagesDir = zPicturesDir;
                TextDir = zTextDir;
                TempDir = zTempDir;
            }
            else
            {
                CacheDir = Path.Combine(RootAppFolder, "Cache");
                FilesDir = Path.Combine(RootAppFolder, "Files");
                LogsDir = Path.Combine(RootAppFolder, "Logs");
                RootImagesDir = Path.Combine(RootAppFolder, "Images");
                SettingsDir = Path.Combine(RootAppFolder, "Settings");
                TextDir = Path.Combine(RootAppFolder, "Text");
                TempDir = Path.Combine(RootAppFolder, "Temp");
            }

            AppDirs = new[] { CacheDir, FilesDir, RootImagesDir, LogsDir, SettingsDir, TempDir, TextDir };

            foreach (string dp in AppDirs)
            {
                if (!string.IsNullOrEmpty(dp) && !Directory.Exists(dp))
                {
                    Directory.CreateDirectory(dp);
                }
            }

            string latestSettingsFile = Path.Combine(SettingsDir, XMLSettings.XMLFileName);
            if (File.Exists(latestSettingsFile))
            {
                Engine.mAppSettings.XMLSettingsFile = latestSettingsFile;
            }
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
                    td = new TaskDialog();
                    td.Caption = GetProductName();
                    td.Text = "File types are not registered";
                    td.InstructionText = "ZScreen needs to register image files as associated files to properly execute the Taskbar related features.";
                    td.Icon = TaskDialogStandardIcon.Information;
                    td.Cancelable = true;

                    TaskDialogCommandLink button1 = new TaskDialogCommandLink("registerButton", "Register file type for this application",
                        "Register image/text files with this application to run ZScreen correctly.");
                    button1.Click += new EventHandler(button1_Click);
                    // Show UAC shield as this task requires elevation
                    button1.UseElevationIcon = true;

                    td.Controls.Add(button1);

                    TaskDialogResult tdr = td.Show();
                }
            }
        }

        private static void button1_Click(object sender, EventArgs e)
        {
            RegistrationHelper.RegisterFileAssociations(progId, false, appId, Application.ExecutablePath + " /doc %1", GetExtensionsToRegister());
            td.Close();
        }

        /// <summary>
        /// Method to return if Windows Vista or Windows 7 or above
        /// </summary>
        /// <returns></returns>
        public static bool HasAero
        {
            get
            {
                return Environment.OSVersion.Version.Major >= 6;
            }
        }

        public static bool HasWindows7
        {
            get
            {
                return HasAero && Environment.OSVersion.Version.Minor >= 1;
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

                return Engine.mAppSettings.XMLSettingsFile;
            }
        }

        public static string HistoryPath
        {
            get
            {
                return Path.Combine(SettingsDir, HistoryFileName);
            }
        }

        public static void ClipboardHook()
        {
            if (null == zClipboardHook && IntPtr.Zero != zHandle)
            {
                zClipboardHook = new ClipboardHook(zHandle);
            }
            if (null != zClipboardHook && Adapter.ClipboardMonitor)
            {
                zClipboardHook.RegisterClipboardViewer();
                FileSystem.AppendDebug("Registered Clipboard Monitor via " + new StackFrame(1).GetMethod().Name);
            }
        }

        public static void ClipboardUnhook()
        {
            if (null != zClipboardHook && Adapter.ClipboardMonitor)
            {
                zClipboardHook.UnregisterClipboardViewer();
                FileSystem.AppendDebug("Unregisterd Clipboard Monitor via " + new StackFrame(1).GetMethod().Name);
            }
        }

        public static void SetImageFormat(ref ImageFileFormat ziff, ImageFileFormatType imgFormat)
        {
            switch (imgFormat)
            {
                case ImageFileFormatType.Bmp:
                    ziff = new ImageFileFormatBmp();
                    break;
                case ImageFileFormatType.Gif:
                    ziff = new ImageFileFormatGif();
                    break;
                case ImageFileFormatType.Jpg:
                    ziff = new ImageFileFormatJpg();
                    break;
                case ImageFileFormatType.Png:
                    ziff = new ImageFileFormatPng();
                    break;
                case ImageFileFormatType.Tif:
                    ziff = new ImageFileFormatTif();
                    break;
            }
        }

        public static XMLSettings conf;

        public const string EXT_FTP_ACCOUNTS = "zfa";
        public static readonly string FILTER_ACCOUNTS = string.Format("ZScreen FTP Accounts(*.{0})|*.{0}", EXT_FTP_ACCOUNTS);
        public const string FILTER_IMAGE_HOSTING_SERVICES = "ZScreen Image Uploaders(*.zihs)|*.zihs";
        public const string FILTER_SETTINGS = "ZScreen XML Settings(*.xml)|*.xml";

        public static Rectangle LastRegion = Rectangle.Empty;
        public static Rectangle LastCapture = Rectangle.Empty;

        public static Mutex mAppMutex;

        public static KeyboardHook ZScreenKeyboardHook;
    }
}