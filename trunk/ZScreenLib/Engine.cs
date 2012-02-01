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
using System.IO;
using System.Linq;
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
        public static Logger EngineLogger = new Logger();

        public static Stopwatch StartTimer { get; private set; }

        public static bool IsTakingScreenShot { get; set; }

        public static bool IsClipboardUploading { get; set; }

        public static string zPreviousSetClipboardText = Clipboard.GetText();

        private static readonly string ApplicationName = Application.ProductName;
        public static readonly string PortableRootFolder = ApplicationName; // using relative paths
        public static readonly bool IsPortable = Directory.Exists(Path.Combine(Application.StartupPath, PortableRootFolder));

        public static bool IsMultiInstance { get; set; }

        public static readonly string DocsAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ApplicationName);

        #region 100 Properties - File Names

        public static readonly string SettingsFileName = ApplicationName + string.Format("-{0}-Settings.xml", Application.ProductVersion);
        public static readonly string HistoryFileName = "UploadersHistory.xml";
        public static readonly string LogFileName = ApplicationName + "Log-{0}.txt";
        public static readonly string PluginsFolderName = ApplicationName + "Plugins";
        public const string GoogleTranslateConfigFileName = "GoogleTranslateConfig.xml";
        public const string UploadersConfigFileName = "UploadersConfig.xml";
        public const string WorkflowConfigFileName = "WorkflowConfig.xml";
        public const string OptionsFileName = "ZScreenOptions.xml";

        #endregion 100 Properties - File Names

        #region 101 Properties - Folder Paths

        internal static readonly string zRoamingAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
        internal static readonly string zLocalAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationName);
        internal static readonly string zCacheDir = Path.Combine(zLocalAppDataFolder, "Cache");
        internal static readonly string zFilesDir = Path.Combine(DocsAppFolder, "Files");
        internal static readonly string zLogsDir = Path.Combine(zLocalAppDataFolder, "Logs");
        internal static readonly string zPicturesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ApplicationName);
        internal static readonly string zSettingsDir = Path.Combine(zRoamingAppDataFolder, "Settings");
        internal static readonly string zTextDir = Path.Combine(DocsAppFolder, "Text");
        internal static readonly string zTempDir = Path.Combine(zLocalAppDataFolder, "Temp");

        #endregion 101 Properties - Folder Paths

        #region 102 Properties - Config Objects

        public static AppSettings ConfigApp = AppSettings.Read();

        public static XMLSettings ConfigUI = new XMLSettings(); // create object for Config Wizard

        public static ZScreenOptions ConfigOptions = new ZScreenOptions();

        public static Workflow ConfigWorkflow { get; set; }

        public static UploadersConfig ConfigUploaders = new UploadersConfig(); // create object for Config Wizard

        public static GoogleTranslatorConfig ConfigGT { get; set; }

        #endregion 102 Properties - Config Objects

        public static string RootAppFolder = ConfigApp.PreferSystemFolders ? zRoamingAppDataFolder : DocsAppFolder;

        static Engine()
        {
            StaticHelper.MyLogger = EngineLogger;
        }

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
                if (ConfigUI != null && ConfigApp.UseHistoryCustomPath && !string.IsNullOrEmpty(ConfigApp.HistoryCustomPath))
                {
                    return ConfigApp.HistoryCustomPath;
                }
                else
                {
                    return Path.Combine(RootAppFolder, HistoryFileName);
                }
            }
        }

        public static string WorkflowConfigPath
        {
            get
            {
                if (ConfigApp != null && ConfigApp.UseWorkflowConfigCustomPath && !string.IsNullOrEmpty(ConfigApp.WorkflowConfigCustomPath))
                {
                    return ConfigApp.WorkflowConfigCustomPath;
                }
                else
                {
                    return Path.Combine(SettingsDir, WorkflowConfigFileName);
                }
            }
        }

        public static string UploadersConfigPath
        {
            get
            {
                if (ConfigApp != null && ConfigApp.UseUploadersConfigCustomPath && !string.IsNullOrEmpty(ConfigApp.UploadersConfigCustomPath))
                {
                    return ConfigApp.UploadersConfigCustomPath;
                }
                else
                {
                    return Path.Combine(SettingsDir, UploadersConfigFileName);
                }
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

        public static string OptionsFilePath
        {
            get
            {
                return Path.Combine(SettingsDir, OptionsFileName);
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
                if (ConfigUI != null && ConfigUI.UseCustomImagesDir && !String.IsNullOrEmpty(ConfigUI.CustomImagesDir))
                {
                    return ConfigUI.CustomImagesDir;
                }
                else
                {
                    string imagesDir = RootImagesDir;
                    string saveFolderPath = string.Empty;
                    if (Engine.ConfigWorkflow != null)
                    {
                        saveFolderPath = new NameParser(NameParserType.SaveFolder).Convert(Engine.ConfigWorkflow.SaveFolderPattern);
                        if (!IsPortable && Engine.ConfigApp.PreferSystemFolders)
                        {
                            imagesDir = zPicturesDir;
                        }
                    }
                    return Path.Combine(imagesDir, saveFolderPath);
                }
            }
        }

        private static string[] AppDirs;

        public const string zImageAnnotator = "Image Annotator";
        public const string zImageEffects = "Image Effects";

        private static bool RunConfig = false;

        public const string EXT_FTP_ACCOUNTS = "zfa";
        public const string FILTER_IMAGE_HOSTING_SERVICES = "ZScreen Image Uploaders(*.zihs)|*.zihs";

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

        public static Mutex mAppMutex;

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

            StaticHelper.WriteLine("");
            StaticHelper.WriteLine(string.Format("{0} r{1} started", GetProductName(), Adapter.AppRevision));
            StaticHelper.WriteLine("Operating system: " + Environment.OSVersion.VersionString);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            DialogResult startEngine = DialogResult.OK;

            if (IsPortable)
            {
                ConfigApp.PreferSystemFolders = false;
                RootAppFolder = PortableRootFolder;
            }
            else
            {
                if (options.ShowConfigWizard && !File.Exists(AppSettings.AppSettingsFile))
                {
                    if (ConfigWorkflow == null)
                    {
                        ConfigWorkflow = Workflow.Read(Engine.WorkflowConfigPath);
                    }
                    ConfigWizard cw = new ConfigWizard(DocsAppFolder);
                    startEngine = cw.ShowDialog();
                    if (startEngine == DialogResult.OK)
                    {
                        if (!cw.PreferSystemFolders)
                        {
                            Engine.ConfigApp.RootDir = cw.RootFolder;
                        }
                        Engine.ConfigApp.PreferSystemFolders = cw.PreferSystemFolders;
                        Engine.ConfigApp.AppOutputs = cw.cwOutputs.Cast<int>().ToList();
                        Engine.ConfigApp.ClipboardContent = cw.cwClipboardContent.Cast<int>().ToList();
                        Engine.ConfigApp.ImageUploaders = cw.cwImageUploaders;
                        Engine.ConfigApp.FileUploaders = cw.cwFileUploaders;
                        Engine.ConfigApp.TextUploaders = cw.cwTextUploaders;
                        Engine.ConfigApp.LinkUploaders = cw.cwLinkUploaders;

                        ConfigWorkflow.Write(WorkflowConfigPath); // DestSelector in ConfigWizard automatically initializes MyUploadersConfig if null so no errors

                        RunConfig = true;
                    }
                }
            }

            ConfigApp.Write();

            if (!ConfigApp.PreferSystemFolders && Directory.Exists(Engine.ConfigApp.RootDir))
            {
                RootAppFolder = Engine.ConfigApp.RootDir;
            }
            else if (!IsPortable) // issue 613
            {
                RootAppFolder = DocsAppFolder;
            }

            if (startEngine == DialogResult.OK)
            {
                StaticHelper.WriteLine("Core file: " + AppSettings.AppSettingsFile);
                if (!ConfigApp.PreferSystemFolders)
                {
                    StaticHelper.WriteLine(string.Format("Root Folder: {0}", RootAppFolder));
                }
                StaticHelper.WriteLine("Initializing Default folder paths...");
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
                    IsMultiInstance = true;
                }
            }

            return startEngine == DialogResult.OK;
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnError((Exception)e.ExceptionObject);
        }

        private static void OnError(Exception e)
        {
            new ErrorForm(Application.ProductName, e, Engine.EngineLogger, LogFilePath, ZLinks.URL_ISSUES).ShowDialog();
        }

        public static void InitializeDefaultFolderPaths(bool dirCreation = true)
        {
            if (ConfigApp.PreferSystemFolders)
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
                Engine.ConfigApp.XMLSettingsPath = SettingsFilePath;
            }
        }

        public static void InitializeFiles()
        {
            string cssIndexer = Path.Combine(SettingsDir, ZSS.IndexersLib.IndexerConfig.DefaultCssFileName);
            if (!File.Exists(cssIndexer))
            {
                ZSS.IndexersLib.IndexerAdapter.CopyDefaultCss(SettingsDir);
                ConfigOptions.IndexerConfig.CssFilePath = cssIndexer;
            }
        }

        public static void TurnOff()
        {
            ConfigApp.UploadersConfigPath = Engine.UploadersConfigPath;
            ConfigApp.WorkflowConfigPath = Engine.WorkflowConfigPath;
            ConfigApp.Write();
            StaticHelper.WriteLine("ZScreen closing");

            if (Engine.ConfigUI != null && Engine.ConfigUI.WriteDebugFile)
            {
                string path = Engine.LogFilePath;
                StaticHelper.WriteLine("Writing debug file: " + path);
                Engine.EngineLogger.SaveLog(path);
            }
        }

        #endregion Engine Turn On/Off

        #region Settings Load/Save Methods

        public static void WriteSettingsAsync()
        {
            WriteSettings(true);
        }

        public static void WriteSettings(bool isAsync = false)
        {
            StaticHelper.WriteLine("WriteSettings is async: " + isAsync);

            Thread settingsOptions = new Thread(() =>
            {
                if (Engine.ConfigOptions != null)
                {
                    Engine.ConfigOptions.Write(OptionsFilePath);
                }
            });
            settingsOptions.Name = "SettingsOptions";

            Thread settingsThread = new Thread(() =>
            {
                if (Engine.ConfigUI != null)
                {
                    Engine.ConfigUI.Write();
                }
            });
            settingsThread.Name = "SettingsThread";

            Thread workflowConfigThread = new Thread(() =>
            {
                if (Engine.ConfigWorkflow != null)
                {
                    Engine.ConfigWorkflow.Write(WorkflowConfigPath);
                }
            });
            workflowConfigThread.Name = "WorkFlowConfigThread";

            Thread uploadersConfigThread = new Thread(() =>
            {
                if (Engine.ConfigUploaders != null)
                {
                    Engine.ConfigUploaders.Save(UploadersConfigPath);
                }
            });
            uploadersConfigThread.Name = "UploadersConfigThread";

            Thread googleTranslateThread = new Thread(() =>
            {
                if (Engine.ConfigGT != null)
                {
                    Engine.ConfigGT.Write(GoogleTranslateConfigPath);
                }
            });
            googleTranslateThread.Name = "GoogleTranslateThread";

            settingsOptions.Start();
            settingsThread.Start();
            googleTranslateThread.Start();
            workflowConfigThread.Start();
            uploadersConfigThread.Start();

            if (!isAsync)
            {
                settingsOptions.Join();
                settingsThread.Join();
                googleTranslateThread.Join();
                workflowConfigThread.Join();
                uploadersConfigThread.Join();
            }
        }

        public static void LoadSettings(string fp = null)
        {
            LoggerTimer timer = EngineLogger.StartTimer("LoadSettings started");

            Thread threadOptions = new Thread(() =>
            {
                Engine.ConfigOptions = ZScreenOptions.Read(OptionsFilePath);
            });

            Thread threadMainFormSettings = new Thread(() =>
            {
                if (string.IsNullOrEmpty(fp))
                {
                    Engine.ConfigUI = XMLSettings.Read();
                }
                else
                {
                    Engine.ConfigUI = XMLSettings.Read(fp);
                }
            });

            Thread threadWorkflow = new Thread(() =>
            {
                Engine.ConfigWorkflow = Workflow.Read(WorkflowConfigPath);
            });

            Thread threadUploadersConfig = new Thread(() =>
            {
                Engine.ConfigUploaders = UploadersConfig.Load(UploadersConfigPath);
            });

            Thread threadGt = new Thread(() =>
            {
                Engine.ConfigGT = GoogleTranslatorConfig.Read(GoogleTranslateConfigPath);
            });

            threadOptions.Start();
            threadMainFormSettings.Start();
            threadGt.Start();
            threadWorkflow.Start();
            threadUploadersConfig.Start();

            threadMainFormSettings.Join();
            threadWorkflow.Join();
            threadUploadersConfig.Join();

            timer.WriteLineTime("LoadSettings finished");

            Engine.InitializeFiles();

            // Use Configuration Wizard Settings if applied
            if (RunConfig)
            {
                Engine.ConfigUI.ConfOutputs = Engine.ConfigApp.AppOutputs;
                Engine.ConfigUI.ConfClipboardContent = Engine.ConfigApp.ClipboardContent;
                Engine.ConfigUI.MyImageUploaders = Engine.ConfigApp.ImageUploaders;
                Engine.ConfigUI.MyTextUploaders = Engine.ConfigApp.TextUploaders;
                Engine.ConfigUI.MyFileUploaders = Engine.ConfigApp.FileUploaders;
                Engine.ConfigUI.MyURLShorteners = Engine.ConfigApp.LinkUploaders;
            }

            // Portable then we don't need PreferSystemFolders to be true
            if (IsPortable)
            {
                Engine.ConfigApp.PreferSystemFolders = false;
            }
        }

        public static void LoadSettingsLatest()
        {
            LoadSettings(GetLatestSettingsFile());
        }

        public static string GetLatestSettingsFile()
        {
            return GetPreviousSettingsFile(Path.GetDirectoryName(Engine.ConfigApp.XMLSettingsPath));
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
            string title = ApplicationName;
            if (IsMultiInstance) title += "*";
            title += " " + Application.ProductVersion;
            if (ConfigUI != null && ConfigUI.ReleaseChannel == ZSS.UpdateCheckerLib.ReleaseChannelType.Dev) title += " r" + Adapter.AppRevision;
            if (IsPortable) title += " Portable";
            return title;
        }

        public static void SetRootFolder(string dp)
        {
            ConfigApp.RootDir = dp;
            RootAppFolder = dp;
        }

        #endregion Helper Methods

        #region Windows 7 Taskbar Methods

        /// <summary>
        /// Method to return if Windows Vista or Windows 7 or above
        /// </summary>
        /// <returns></returns>
        public static bool HasVista
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
                return HasVista && Environment.OSVersion.Version.Minor >= 1;
            }
        }

        public static bool HasWindows8
        {
            get
            {
                return HasVista && Environment.OSVersion.Version.Minor >= 2;
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
                RegistryKey appCommand = Registry.ClassesRoot.OpenSubKey(Path.Combine(Application.ProductName, @"shell\Open\Command"));
                if (appCommand != null)
                {
                    string value = appCommand.GetValue("", null) as string;

                    if (string.IsNullOrEmpty(value)) // !value.Contains(Application.ExecutablePath) is quite annoying
                        registered = false;
                    else
                        registered = true;
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex);
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

                    TaskDialogCommandLink tdclRegister = new TaskDialogCommandLink("registerButton", "Register file type for this application",
                        "Register image/text files with this application to run ZScreen correctly.");
                    tdclRegister.Click += new EventHandler(btnRegisterWin7_Click);
                    // Show UAC shield as this task requires elevation
                    tdclRegister.UseElevationIcon = true;

                    td.Controls.Add(tdclRegister);

                    TaskDialogResult tdr = td.Show();
                }
            }
        }

        private static void btnRegisterWin7_Click(object sender, EventArgs e)
        {
            RegistrationHelper.RegisterFileAssociations(progId, false, appId, Application.ExecutablePath + " /doc %1", GetExtensionsToRegister());
            td.Close();
        }

        #endregion Windows 7 Taskbar Methods

        public static void SetImageFormat(ref ImageFileFormat ziff, EImageFormat imgFormat)
        {
            switch (imgFormat)
            {
                case EImageFormat.BMP:
                    ziff = new ImageFileFormatBmp();
                    break;
                case EImageFormat.GIF:
                    ziff = new ImageFileFormatGif();
                    break;
                case EImageFormat.JPEG:
                    ziff = new ImageFileFormatJpg();
                    break;
                case EImageFormat.PNG:
                    ziff = new ImageFileFormatPng();
                    break;
                case EImageFormat.TIFF:
                    ziff = new ImageFileFormatTif();
                    break;
            }
        }
    }
}