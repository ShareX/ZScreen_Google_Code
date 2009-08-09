using System;
using System.IO;
using System.Windows.Forms;
using ZScreenLib.Helpers;
using System.Drawing;
using ZScreenLib.Forms;

namespace ZScreenLib
{
    public static class Loader
    {
        public static readonly string LocalAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName);
        public static string DefaultRootAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.ProductName);
        public static AppSettings appSettings = AppSettings.Read();

        #region "Variables/Constants"
        public const string EXT_FTP_ACCOUNTS = "zfa";
        public static readonly string FILTER_ACCOUNTS = string.Format("ZScreen FTP Accounts(*.{0})|*.{0}", EXT_FTP_ACCOUNTS);
        public const string FILTER_IMAGE_HOSTING_SERVICES = "ZScreen Image Uploaders(*.zihs)|*.zihs";
        public const string FILTER_SETTINGS = "ZScreen XML Settings(*.xml)|*.xml";

        private static readonly string XMLFileName = "Settings.xml";
        private static readonly string HistoryFileName = "History.xml";

        public static string RootAppFolder { get; set; }
        public static string RootImagesDir { get; private set; }

        private static string[] AppDirs;
        public static string CacheDir { get; set; }
        public static string FilesDir { get; set; }
        public static string ImagesDir
        {
            get
            {
                if (conf != null && conf.UseCustomImagesDir && !String.IsNullOrEmpty(conf.CustomImagesDir))
                {
                    return Loader.ImagesDir = conf.CustomImagesDir;
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

        public const string ZSCREEN_IMAGE_EDITOR = "Image Editor";
        public const string DISABLED_IMAGE_EDITOR = "Disabled";



        public const string IMAGESHACK_KEY = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TINYPIC_ID = "e2aabb8d555322fa";
        public const string TINYPIC_KEY = "00a68ed73ddd54da52dc2d5803fa35ee";

        public static string[] zImageFileTypes = { "png", "jpg", "gif", "bmp", "tif", "ico" };
        public static string[] zTextFileTypes = { "txt", "log" };
        public static string[] zWebpageFileTypes = { "html", "htm" };

        public static Rectangle LastRegion = Rectangle.Empty;
        public static Rectangle LastCapture = Rectangle.Empty;

        internal static string DefaultXMLFilePath;
        public static XMLSettings conf;

        /// <summary>
        /// First method that needs to be executed once ZScreenLib is referenced
        /// </summary>
        public static void Load()
        {
            ConfigWizard cw = null;
            if (string.IsNullOrEmpty(appSettings.RootDir))
            {
                cw = new ConfigWizard(DefaultRootAppFolder);
                cw.ShowDialog();
                appSettings.RootDir = cw.RootFolder;
                appSettings.Save();
            }
            RootAppFolder = appSettings.RootDir;
            RootImagesDir = Path.Combine(RootAppFolder, "Images"); // after RootAppFolder is set, now set RootImagesDir
            InitializeDefaultFolderPaths(); // happens before XMLSettings is readed
            conf = XMLSettings.Read();
        }

        public static string HistoryFile
        {
            get
            {
                return Path.Combine(SettingsDir, HistoryFileName);
            }
        }

        /// <summary>
        /// Function to update Default Folder Paths based on Root folder
        /// </summary>
        public static void InitializeDefaultFolderPaths()
        {
            CacheDir = Path.Combine(RootAppFolder, "Cache");
            FilesDir = Path.Combine(RootAppFolder, "Files");

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
        }

        public static string XMLSettingsFile
        {
            get
            {
                if (!Directory.Exists(SettingsDir))
                {
                    Directory.CreateDirectory(SettingsDir);
                }

                return DefaultXMLFilePath;                                // v2.x
            }
        }

        #endregion

        #region "Methods"

        private static string GetDefaultImagesDir()
        {
            string saveFolderPath = string.Empty;
            if (Loader.conf != null)
            {
                saveFolderPath = NameParser.Convert(NameParserType.SaveFolder);
            }
            return Path.Combine(RootImagesDir, saveFolderPath);
        }
        #endregion

    }
}
