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
using UploadersLib;
using UploadersLib.HelperClasses;

namespace ZScreenLib
{
    public static class Engine
    {
        public static IntPtr zHandle = IntPtr.Zero;

        #region Windows 7 Taskbar

        public static readonly string appId = Application.ProductName;  // need for Windows 7 Taskbar
        private static readonly string progId = Application.ProductName; // need for Windows 7 Taskbar
        public static string[] zImageFileTypes = { "png", "jpg", "gif", "bmp", "tif" };
        public static string[] zTextFileTypes = { "txt", "log" };
        public static string[] zWebpageFileTypes = { "html", "htm" };

        private static TaskDialog td = null;
        public static JumpList zJumpList;
        public static TaskbarManager zWindowsTaskbar;

        #endregion Windows 7 Taskbar

        public const string ZScreenCLI = "ZScreenCLI.exe";
        public static Logger MyLogger { get; private set; }
        public static Stopwatch StartTimer { get; private set; }
        public static bool MultipleInstance { get; private set; }

        private static readonly string PortableRootFolder = Application.ProductName; // using relative paths
        public static readonly string DefaultRootAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ZApps");
        public static string RootAppFolder = DefaultRootAppFolder;
        public static bool Portable = Directory.Exists(Path.Combine(Application.StartupPath, PortableRootFolder));

        private static string ApplicationName = Application.ProductName;
        internal static readonly string SettingsFileName = ApplicationName + string.Format("-{0}-Settings.xml", Application.ProductVersion);
        private static readonly string HistoryFileName = "UploadersHistory.xml";
        private static readonly string UploadersConfigFileName = "UploadersConfig.xml";
        private static readonly string LogFileName = ApplicationName + "Log-{0}.txt";
        private static readonly string PluginsFolderName = ApplicationName + "Plugins";
        private static readonly string GoogleTranslateConfigFileName = "GoogleTranslateConfig.xml";

        internal static readonly string zRoamingAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
        internal static readonly string zLocalAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationName);
        internal static readonly string zCacheDir = Path.Combine(zLocalAppDataFolder, "Cache");
        internal static readonly string zFilesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Path.Combine(ApplicationName, "Files"));
        internal static readonly string zLogsDir = Path.Combine(zLocalAppDataFolder, "Logs");
        internal static readonly string zPicturesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ApplicationName);
        internal static readonly string zSettingsDir = Path.Combine(zRoamingAppDataFolder, "Settings");
        internal static readonly string zTextDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Path.Combine(ApplicationName, "Text"));
        internal static readonly string zTempDir = Path.Combine(zLocalAppDataFolder, "Temp");

        public static AppSettings mAppSettings = AppSettings.Read();

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
            }
            set { ; }
        }

        private static string[] AppDirs;

        public const string zImageAnnotator = "Image Annotator";
        public static ImageFileFormat zImageFileFormat = new ImageFileFormatPng();
        public static ImageFileFormat zImageFileFormatSwitch = new ImageFileFormatJpg();

        public static ClipboardHook zClipboardHook = null;
        public static string zClipboardText = string.Empty;

        private static bool RunConfig = false;

        public static XMLSettings conf;
        public static UploadersConfig MyUploadersConfig;
        public static GoogleTranslatorConfig MyGTConfig;

        public const string EXT_FTP_ACCOUNTS = "zfa";
        public const string FILTER_IMAGE_HOSTING_SERVICES = "ZScreen Image Uploaders(*.zihs)|*.zihs";
        public const string FILTER_SETTINGS = "ZScreen XML Settings(*.xml)|*.xml";

        public static Rectangle LastRegion = Rectangle.Empty;
        public static Rectangle LastCapture = Rectangle.Empty;

        public static Mutex mAppMutex;

        public static KeyboardHook ZScreenKeyboardHook;

        public class EngineOptions
        {
            public bool KeyboardHook { get; set; }
            public bool ShowConfigWizard { get; set; }
        }

        #region Engine Turn On/Off

        public static void TurnOn()
        {
            TurnOn(new EngineOptions());
        }

        public static bool TurnOn(EngineOptions options)
        {
            StartTimer = Stopwatch.StartNew();

            MyLogger = new Logger();
            StaticHelper.MyLogger = MyLogger;
            MyLogger.WriteLine(string.Format("{0} rev {1} started", GetProductName(), Adapter.AppRevision));
            MyLogger.WriteLine("Operating system: " + Environment.OSVersion.VersionString);

            DialogResult startEngine = DialogResult.OK;

            if (Directory.Exists(Path.Combine(Application.StartupPath, PortableRootFolder)))
            {
                mAppSettings.PreferSystemFolders = false;
                RootAppFolder = PortableRootFolder;
                ApplicationName += " Portable";
            }
            else
            {
                if (options.ShowConfigWizard && string.IsNullOrEmpty(Engine.mAppSettings.XMLSettingsFile))
                {
                    ConfigWizard cw = new ConfigWizard(DefaultRootAppFolder);
                    startEngine = cw.ShowDialog();
                    if (startEngine == DialogResult.OK)
                    {
                        if (!cw.PreferSystemFolders)
                        {
                            Engine.mAppSettings.RootDir = cw.RootFolder;
                        }
                        Engine.mAppSettings.PreferSystemFolders = cw.PreferSystemFolders;
                        Engine.mAppSettings.ImageUploader = (int)cw.ImageDestinationType;
                        Engine.mAppSettings.FileUploader = (int)cw.FileUploaderType;
                        Engine.mAppSettings.TextUploader = (int)cw.MyTextUploaderType;
                        Engine.mAppSettings.UrlShortener = (int)cw.MyUrlShortenerType;

                        MyUploadersConfig.Save(UploaderConfigPath); // DestSelector in ConfigWizard automatically initializes MyUploadersConfig if null so no errors
                        mAppSettings.Write();

                        if (!cw.PreferSystemFolders && Directory.Exists(Engine.mAppSettings.RootDir))
                        {
                            RootAppFolder = Engine.mAppSettings.RootDir;
                        }
                        else
                        {
                            RootAppFolder = DefaultRootAppFolder;
                        }

                        RunConfig = true;
                    }
                }
            }

            if (startEngine == DialogResult.OK)
            {
                Engine.MyLogger.WriteLine("Config file: " + AppSettings.AppSettingsFile);
                if (!mAppSettings.PreferSystemFolders)
                {
                    Engine.MyLogger.WriteLine(string.Format("Root Folder: {0}", RootAppFolder));
                }
                Engine.MyLogger.WriteLine("Initializing Default folder paths...");
                Engine.InitializeDefaultFolderPaths(); // happens before XMLSettings is readed

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
                    ApplicationName += "*";
                }
            }

            return startEngine == DialogResult.OK;
        }

        public static void InitializeDefaultFolderPaths(bool dirCreation = true)
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

            if (dirCreation)
            {
                AppDirs = new[] { CacheDir, FilesDir, RootImagesDir, LogsDir, SettingsDir, TempDir, TextDir };

                foreach (string dp in AppDirs)
                {
                    if (!string.IsNullOrEmpty(dp) && !Directory.Exists(dp))
                    {
                        Directory.CreateDirectory(dp);
                    }
                }
            }

            if (File.Exists(Engine.SettingsFilePath))
            {
                Engine.mAppSettings.XMLSettingsFile = SettingsFilePath;
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

        public static void TurnOff()
        {
            if (!Portable)
            {
                mAppSettings.Write(); // DONT UPDATE FOR PORTABLE MODE
            }
            if (null != ZScreenKeyboardHook)
            {
                ZScreenKeyboardHook.Dispose();
                Engine.MyLogger.WriteLine("Keyboard Hook terminated");
            }

            WriteSettings(false);

            FileSystem.WriteDebugFile();
        }

        #endregion Engine Turn On/Off

        #region Settings Load/Save Methods

        public static void WriteSettings(bool isAsync = true)
        {
            Thread settingsThread = new Thread(() =>
            {
                if (Engine.conf != null)
                {
                    Engine.conf.Write();
                }
            });

            Thread uploadersConfigThread = new Thread(() =>
            {
                if (Engine.MyUploadersConfig != null)
                {
                    Engine.MyUploadersConfig.Save(UploaderConfigPath);
                }
            });

            Thread googleTranslateThread = new Thread(() =>
            {
                if (Engine.MyGTConfig != null)
                {
                    Engine.MyGTConfig.Write(GoogleTranslateConfigPath);
                }
            });

            settingsThread.Start();
            uploadersConfigThread.Start();
            googleTranslateThread.Start();

            // TODO: When ZScreen closing make it not async

            if (!isAsync)
            {
                settingsThread.Join();
                uploadersConfigThread.Join();
                googleTranslateThread.Join();
            }
        }

        public static void LoadSettings(string fp = null)
        {
            Thread settingsThread = new Thread(() =>
            {
                if (string.IsNullOrEmpty(fp))
                {
                    Engine.conf = XMLSettings.Read();
                }
                else
                {
                    Engine.conf = XMLSettings.Read(fp);
                }
            });

            Thread uploadersConfigThread = new Thread(() =>
            {
                Engine.MyUploadersConfig = UploadersConfig.Load(UploaderConfigPath);
            });

            Thread googleTranslateThread = new Thread(() =>
            {
                Engine.MyGTConfig = GoogleTranslatorConfig.Read(GoogleTranslateConfigPath);
            });

            settingsThread.Start();
            uploadersConfigThread.Start();
            googleTranslateThread.Start();
            settingsThread.Join();
            uploadersConfigThread.Join();
            googleTranslateThread.Join();

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

        public static void LoadSettingsLatest()
        {
            LoadSettings(GetLatestSettingsFile());
        }

        public static string GetLatestSettingsFile()
        {
            return GetPreviousSettingsFile(Path.GetDirectoryName(Engine.mAppSettings.XMLSettingsFile));
        }

        public static string GetPreviousSettingsFile(string settingsDir)
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

        #endregion Settings Load/Save Methods

        #region Helper Methods

        public static string GetProductName()
        {
            return string.Format("{0} {1}", ApplicationName, Application.ProductVersion);
        }

        public static void SetRootFolder(string dp)
        {
            mAppSettings.RootDir = dp;
            RootAppFolder = dp;
        }

        #endregion Helper Methods

        #region Windows 7 Taskbar Methods

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

        #endregion Windows 7 Taskbar Methods

        #region Paths

        public static string SettingsFilePath
        {
            get
            {
                if (!Directory.Exists(SettingsDir))
                {
                    Directory.CreateDirectory(SettingsDir);
                }
                return Path.Combine(Engine.SettingsDir, Engine.SettingsFileName);
            }
        }

        public static string HistoryPath
        {
            get
            {
                return Path.Combine(SettingsDir, HistoryFileName);
            }
        }

        public static string UploaderConfigPath
        {
            get
            {
                return Path.Combine(SettingsDir, UploadersConfigFileName);
            }
        }

        public static string LogFilePath
        {
            get
            {
                DateTime now = FastDateTime.Now;
                return Path.Combine(LogsDir, string.Format(LogFileName, now.ToString("yyyy-MM")));
            }
        }

        public static string GoogleTranslateConfigPath
        {
            get
            {
                return Path.Combine(SettingsDir, GoogleTranslateConfigFileName);
            }
        }

        #endregion Paths

        #region Clipboard Methods

        public static void ClipboardHook()
        {
            if (null == zClipboardHook && IntPtr.Zero != zHandle)
            {
                zClipboardHook = new ClipboardHook(zHandle);
            }
            if (null != zClipboardHook && Adapter.ClipboardMonitor)
            {
                zClipboardHook.RegisterClipboardViewer();
                Engine.MyLogger.WriteLine("Registered Clipboard Monitor via " + new StackFrame(1).GetMethod().Name);
            }
        }

        public static void ClipboardUnhook()
        {
            if (null != zClipboardHook && Adapter.ClipboardMonitor)
            {
                zClipboardHook.UnregisterClipboardViewer();
                Engine.MyLogger.WriteLine("Unregisterd Clipboard Monitor via " + new StackFrame(1).GetMethod().Name);
            }
        }

        #endregion Clipboard Methods

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
    }
}