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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using ZSS;
using ZSS.IndexersLib;
using System.Drawing.Drawing2D;
using GradientTester;

namespace ZScreenLib
{
    [XmlRoot("Settings")]
    public class XMLSettings
    {
        #region Settings

        //~~~~~~~~~~~~~~~~~~~~~
        //  Misc Settings
        //~~~~~~~~~~~~~~~~~~~~~

        public bool RunOnce = false;
        public FormWindowState WindowState = FormWindowState.Normal;
        public Size WindowSize = Size.Empty;
        public Point WindowLocation = Point.Empty;
        public bool Windows7TaskbarIntegration = false;
        public static string XMLFileName = string.Format("{0}-{1}-Settings.xml", Application.ProductName, Application.ProductVersion);

        //~~~~~~~~~~~~~~~~~~~~~
        //  Main
        //~~~~~~~~~~~~~~~~~~~~~

        public ImageDestType ImageUploaderType = ImageDestType.CLIPBOARD;
        public ClipboardUriType ClipboardUriMode = ClipboardUriType.FULL;
        public TextDestType TextUploaderType = TextDestType.PASTE2;
        public FileUploaderType FileDestMode = FileUploaderType.FTP;
        [Category("Destinations / General"), DefaultValue(false), Description("Use the active File Uploader instead of the active Image Uploader for uploading Images")]
        public bool PreferFileUploaderForImages { get; set; }
        [Category("Destinations / General"), DefaultValue(false), Description("Use the active File Uploader instead of the active Text Uploader for uploading Text")]
        public bool PreferFileUploaderForText { get; set; }
        [Category("Destinations / FTP Server"), DefaultValue(true), Description("Use the active FTP Server instead of the active Text Uploader for uploading Text. Prerequisite: PreferFileUploaderForText")]
        public bool PreferFtpServerForIndex { get; set; }
        public long ScreenshotDelayTime = 0;
        public Times ScreenshotDelayTimes = Times.Seconds;
        public bool ManualNaming = false;
        public bool ShowCursor = false;
        public bool ShowWatermark = false;
        public bool CropGridToggle = false;
        public Size CropGridSize = new Size(100, 100);
        public string HelpToLanguage = "en";

        //~~~~~~~~~~~~~~~~~~~~~
        //  Destinations
        //~~~~~~~~~~~~~~~~~~~~~

        // ImageBam 

        public string ImageBamApiKey = string.Empty;
        public string ImageBamSecret = string.Empty;
        public int ImageBamGalleryActive = 0;
        public bool ImageBamContentNSFW = false;
        public List<string> ImageBamGallery = new List<string>();
        public int ImageBamGallerySelected = 0;

        // Rapid Share 

        public string RapidSharePremiumUserName = string.Empty;
        public string RapidShareCollectorsID = string.Empty;
        public string RapidSharePassword = string.Empty;
        public RapidShareAcctType RapidShareAccountType = RapidShareAcctType.Free;

        // SendSpace

        public AcctType SendSpaceAccountType = AcctType.Anonymous;
        public string SendSpaceUserName = string.Empty;
        public string SendSpacePassword = string.Empty;

        // ImageShack

        public string ImageShackRegistrationCode = "";
        public string ImageShackUserName = "";
        public bool ImageShackShowImagesInPublic = false;

        // TinyPic

        public string TinyPicShuk = "";
        public string TinyPicUserName = "";
        public string TinyPicPassword = "";
        public bool RememberTinyPicUserPass = false;
        public bool TinyPicSizeCheck = true;

        // Twitter 
        public List<TwitterAuthInfo> TwitterAccountsList = new List<TwitterAuthInfo>();
        public int TwitterAcctSelected = 0;
        public bool TwitterEnabled = false;
        public TwitterClientSettings TwitterClientConfig = new TwitterClientSettings();

        // TwitPic

        public bool TwitPicShowFull = true;
        public TwitPicThumbnailType TwitPicThumbnailMode = TwitPicThumbnailType.Thumb;

        // Flickr

        public FlickrUploader.AuthInfo FlickrAuthInfo = new FlickrUploader.AuthInfo();
        public FlickrUploader.FlickrSettings FlickrSettings = new FlickrUploader.FlickrSettings();

        //~~~~~~~~~~~~~~~~~~~~~
        //  Hotkeys
        //~~~~~~~~~~~~~~~~~~~~~

        public const Keys DefaultHotkeyEntireScreen = Keys.PrintScreen;
        public const Keys DefaultHotkeyActiveWindow = Keys.Alt | Keys.PrintScreen;
        public const Keys DefaultHotkeyCropShot = Keys.Control | Keys.PrintScreen;
        public const Keys DefaultHotkeySelectedWindow = Keys.Shift | Keys.PrintScreen;
        public const Keys DefaultHotkeyClipboardUpload = Keys.Control | Keys.F6;
        public const Keys DefaultHotkeyLastCropShot = Keys.None;
        public const Keys DefaultHotkeyAutoCapture = Keys.None;
        public const Keys DefaultHotkeyDropWindow = Keys.None;
        public const Keys DefaultHotkeyActionsToolbar = Keys.None;
        public const Keys DefaultHotkeyQuickOptions = Keys.None;
        public const Keys DefaultHotkeyLanguageTranslator = Keys.None;
        public const Keys DefaultHotkeyScreenColorPicker = Keys.None;
        public const Keys DefaultHotkeyTwitterClient = Keys.None;

        public Keys HotkeyEntireScreen = DefaultHotkeyEntireScreen;
        public Keys HotkeyActiveWindow = DefaultHotkeyActiveWindow;
        public Keys HotkeyCropShot = DefaultHotkeyCropShot;
        public Keys HotkeySelectedWindow = DefaultHotkeySelectedWindow;
        public Keys HotkeyClipboardUpload = DefaultHotkeyClipboardUpload;
        public Keys HotkeyLastCropShot = DefaultHotkeyLastCropShot;
        public Keys HotkeyAutoCapture = DefaultHotkeyAutoCapture;
        public Keys HotkeyDropWindow = DefaultHotkeyDropWindow;
        public Keys HotkeyActionsToolbar = DefaultHotkeyActionsToolbar;
        public Keys HotkeyQuickOptions = DefaultHotkeyQuickOptions;
        public Keys HotkeyLanguageTranslator = DefaultHotkeyLanguageTranslator;
        public Keys HotkeyScreenColorPicker = DefaultHotkeyScreenColorPicker;
        public Keys HotkeyTwitterClient = DefaultHotkeyTwitterClient;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Capture
        //~~~~~~~~~~~~~~~~~~~~~

        // Crop Shot

        public RegionStyles CropRegionStyles = RegionStyles.BACKGROUND_REGION_BRIGHTNESS;
        public bool CropRegionRectangleInfo = true;
        public bool CropRegionHotkeyInfo = true;

        public bool CropDynamicCrosshair = true;
        public int CropInterval = 25;
        public int CropStep = 1;
        public int CrosshairLineCount = 2;
        public int CrosshairLineSize = 25;
        public string CropCrosshairColor = SerializeColor(Color.Black);
        public bool CropShowBigCross = true;
        public bool CropShowMagnifyingGlass = true;

        public bool CropShowRuler = true;
        public bool CropDynamicBorderColor = true;
        public decimal CropRegionInterval = 75;
        public decimal CropRegionStep = 5;
        public decimal CropHueRange = 50;
        public string CropBorderColor = SerializeColor(Color.FromArgb(255, 0, 255));
        public decimal CropBorderSize = 1;
        public bool CropShowGrids = false;

        // Selected Window

        public RegionStyles SelectedWindowRegionStyles = RegionStyles.BACKGROUND_REGION_BRIGHTNESS;
        public bool SelectedWindowRectangleInfo = true;
        public bool SelectedWindowRuler = true;
        public string SelectedWindowBorderColor = SerializeColor(Color.FromArgb(255, 0, 255));
        public decimal SelectedWindowBorderSize = 2;
        public bool SelectedWindowDynamicBorderColor = true;
        public decimal SelectedWindowRegionInterval = 75;
        public decimal SelectedWindowRegionStep = 5;
        public decimal SelectedWindowHueRange = 50;
        public bool SelectedWindowCaptureObjects = true;

        // Interaction

        public decimal FlashTrayCount = 1;
        public bool CaptureEntireScreenOnError = false;
        public bool ShowBalloonTip = true;
        public bool BalloonTipOpenLink = true;
        public bool ShowUploadDuration = false;
        public bool CompleteSound = false;
        public bool CloseDropBox = false;
        public Point LastDropBoxPosition = Point.Empty;
        public bool CloseQuickActions = false;

        // Naming Conventions

        public string ActiveWindowPattern = "%t-%y.%mo.%d-%h.%mi.%s";
        public string EntireScreenPattern = "SS-%y.%mo.%d-%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        public int MaxNameLength = 100;

        // Image Settings

        public int FileFormat = 0;
        public decimal ImageQuality = 90;
        public decimal SwitchAfter = 512;
        public int SwitchFormat = 1;

        public ImageSizeType ImageSizeType = ImageSizeType.DEFAULT;
        public int ImageSizeFixedWidth = 500;
        public int ImageSizeFixedHeight = 500;
        public float ImageSizeRatioPercentage = 50.0f;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Watermark
        //~~~~~~~~~~~~~~~~~~~~~

        public WatermarkType WatermarkMode = WatermarkType.NONE;
        public WatermarkPositionType WatermarkPositionMode = WatermarkPositionType.BOTTOM_RIGHT;
        public decimal WatermarkOffset = 5;
        public bool WatermarkAddReflection = false;
        public bool WatermarkAutoHide = true;

        public string WatermarkText = "%h:%mi";
        public XmlFont WatermarkFont = new XmlFont();
        public string WatermarkFontColor = SerializeColor(Color.White);
        public decimal WatermarkFontTrans = 255;
        public decimal WatermarkCornerRadius = 4;
        public string WatermarkGradient1 = SerializeColor(Color.FromArgb(85, 85, 85));
        public string WatermarkGradient2 = SerializeColor(Color.Black);
        public string WatermarkBorderColor = SerializeColor(Color.Black);
        public decimal WatermarkBackTrans = 225;
        public LinearGradientMode WatermarkGradientType = LinearGradientMode.Vertical;
        public bool WatermarkUseCustomGradient = false;
        public GradientMakerSettings GradientMakerOptions = new GradientMakerSettings();

        public string WatermarkImageLocation = "";
        public bool WatermarkUseBorder = false;
        public decimal WatermarkImageScale = 100;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //  Text Uploaders & URL Shorteners
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public List<TextUploader> TextUploadersList = new List<TextUploader>();
        public int TextUploaderSelected = 0;

        public List<TextUploader> UrlShortenersList = new List<TextUploader>();
        public int UrlShortenerSelected = 0;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Editors
        //~~~~~~~~~~~~~~~~~~~~~

        public List<Software> ImageEditors = new List<Software>();
        public Software ImageEditor = null;
        public Software TextEditorActive;
        public List<Software> TextEditors = new List<Software>();
        public bool TextEditorEnabled = false;
        public bool ImageEditorAutoSave = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  FTP
        //~~~~~~~~~~~~~~~~~~~~~

        public List<FTPAccount> FTPAccountList = new List<FTPAccount>();
        public int FTPSelected = 0;
        public bool FTPCreateThumbnail = false;
        public int FTPThumbnailWidth = 150;
        public int FTPThumbnailHeight = 125;
        public bool FTPThumbnailCheckSize = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  DekiWiki
        //~~~~~~~~~~~~~~~~~~~~~

        public List<DekiWikiAccount> DekiWikiAccountList = new List<DekiWikiAccount>();
        public int DekiWikiSelected = 0;
        public bool DekiWikiForcePath = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  HTTP
        //~~~~~~~~~~~~~~~~~~~~~

        // Image Uploaders

        public UploadMode UploadMode = UploadMode.API;
        public bool AutoSwitchFileUploader = true;
        public decimal ErrorRetryCount = 2;
        public bool ImageUploadRetryOnFail = true;
        public bool ImageUploadRandomRetryOnFail = true;
        public bool AddFailedScreenshot = true;
        public bool ImageUploadRetryOnTimeout = false;
        public decimal UploadDurationLimit = 15000;

        // Indexer

        public IndexerConfig IndexerConfig = new IndexerConfig();

        // Custom Image Uploaders

        public List<ImageHostingService> ImageUploadersList = null;
        public int ImageUploaderSelected = 0;

        // Web Page Upload

        public bool WebPageUseCustomSize = true;
        public int WebPageWidth = 1024;
        public int WebPageHeight = 768;
        public bool WebPageAutoUpload = true;

        // Language Translator

        public string FromLanguage = "auto";
        public string ToLanguage = "en";
        public string ToLanguage2 = "?";
        public bool ClipboardTranslate = false;
        public bool AutoTranslate = false;
        public int AutoTranslateLength = 20;

        //~~~~~~~~~~~~~~~~~~~~~
        //  History
        //~~~~~~~~~~~~~~~~~~~~~

        // History Settings

        public HistoryListFormat HistoryListFormat = HistoryListFormat.NAME;
        public int HistoryMaxNumber = 50;
        public bool HistorySave = true;
        public bool HistoryShowTooltips = true;
        public bool HistoryAddSpace = false;
        public bool HistoryReverseList = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Options
        //~~~~~~~~~~~~~~~~~~~~~

        // General - Program

        public bool OpenMainWindow = true;
        public bool ShowInTaskbar = true;
        public bool ShowHelpBalloonTips = true;
        public bool SaveFormSizePosition = true;
        public bool LockFormSize = false;
        public bool AutoSaveSettings = true;

        // General - Check Updates

        public bool CheckUpdates = true;
        public bool CheckUpdatesBeta = false;

        // Proxy Settings

        public List<ProxyInfo> ProxyList = new List<ProxyInfo>();
        public int ProxySelected = 0;
        public ProxyInfo ProxyActive = null;
        public bool ProxyEnabled = false;

        // Paths

        public bool DeleteLocal = false;
        public decimal ScreenshotCacheSize = 50;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Auto Capture
        //~~~~~~~~~~~~~~~~~~~~~

        public AutoScreenshotterJobs AutoCaptureScreenshotTypes = AutoScreenshotterJobs.TAKE_SCREENSHOT_SCREEN;
        public long AutoCaptureDelayTime = 10000;
        public Times AutoCaptureDelayTimes = Times.Seconds;
        public bool AutoCaptureAutoMinimize = false;
        public bool AutoCaptureWaitUploads = true;

        #region Properties for PropertyGrid

        public XMLSettings()
        {
            ApplyDefaultValues(this);
            try
            {
                this.WatermarkFont = new XmlFont(new Font("Arial", 8));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Destinations / FTP

        [Category("Destinations / FTP Server"), DefaultValue(true), Description("Periodically backup FTP settings.")]
        public bool BackupFTPSettings { get; set; }

        // Options / Actions Toolbar

        [Category("Options / Actions Toolbar"), DefaultValue(false), Description("Open Actions Toolbar on startup.")]
        public bool ActionsToolbarMode { get; set; }
        [Category("Options / Actions Toolbar"), Description("Action Toolbar Location.")]
        public Point ActionToolbarLocation { get; set; }

        // Options / General

        [Category("Options / General"), DefaultValue(false), Description("Show Clipboard Mode Chooser after upload is complete")]
        public bool ShowClipboardModeChooser { get; set; }
        [Category("Options / General"), DefaultValue(true), Description("Showing upload progress percentage in tray icon")]
        public bool ShowTrayUploadProgress { get; set; }
        [Category("Options / General"), DefaultValue(true), Description("Write debug information into a log file.")]
        public bool WriteDebugFile { get; set; }
        [Category("Options / General"), DefaultValue(false), Description("When holding shift when finishing cropping show advanced options")]
        public bool ShowAdvancedOptionsAfterCrop { get; set; }

        [Category("Options / Windows 7 Taskbar"), DefaultValue(false), Description("Determine whether Windows 7 Taskbar was refreshed once")]
        public bool UserTasksAdded { get; set; }

        // Options / History Settings

        [Category("Options / History Settings"), DefaultValue(false), Description("Prefer browser view to navigate uploaded images.")]
        public bool PreferBrowserForImages { get; set; }
        [Category("Options / History Settings"), DefaultValue(false), Description("Prefer browser view to navigate uploaded text.")]
        public bool PreferBrowserForText { get; set; }

        // Options / Interaction

        [Category("Options / Interaction"), DefaultValue(true),
        Description("If you use Clipboard Upload and the clipboard contains a URL then the URL will be shortened instead of performing a text upload.")]
        public bool AutoShortenURL { get; set; }
        [Category("Options / Interaction"), DefaultValue(100),
        Description("URL Shortening will only be activated if the length of a URL exceeds this value. To always shorten a URL set this value to 0.")]
        public int LimitLongURL { get; set; }
        [Category("Options / Interaction"), DefaultValue(false), Description("Optionally shorten the URL after completing a task.")]
        public bool MakeTinyURL { get; set; }
        [Category("Options / Interaction"), DefaultValue(false), Description("Minimize ZScreen to taskbar on close.")]
        public bool MinimizeOnClose { get; set; }

        // Options / Paths

        [Category("Options / Paths"), DefaultValue(true), Description("Periodically backup application settings.")]
        public bool BackupApplicationSettings { get; set; }
        [Category("Options / Paths"), Description("Custom Images directory where screenshots and pictures will be stored locally.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string CustomImagesDir { get; set; }
        [Category("Options / Paths"), DefaultValue(false), Description("Use Custom Images directory.")]
        public bool UseCustomImagesDir { get; set; }

        // Options / Watch Folder

        [Category("Options / Watch Folder"), DefaultValue(false), Description("Automatically upload files saved in to this folder.")]
        public bool FolderMonitoring { get; set; }
        [Category("Options / Watch Folder"), Description("Folder monitor path where files automatically get uploaded.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string FolderMonitorPath { get; set; }

        // Screenshots / Bevel

        [Category("Screenshots / Bevel"), DefaultValue(false), Description("Add bevel effect to screenshots.")]
        public bool BevelEffect { get; set; }
        [Category("Screenshots / Bevel"), DefaultValue(15), Description("Bevel effect size.")]
        public int BevelEffectOffset { get; set; }
        [Category("Screenshots / Bevel"), DefaultValue(FilterType.Brightness), Description("Bevel effect filter type.")]
        public FilterType BevelFilterType { get; set; }


        //Screenshots / Border

        [Category("Screenshots / Border"), DefaultValue(false), Description("Add border to screenshots.")]
        public bool BorderEffect { get; set; }
        [Category("Screenshots / Border"), DefaultValue(1), Description("Border size in px.")]
        public int BorderEffectSize { get; set; }

        [XmlIgnore(), Category("Screenshots / Border"), Description("Border Color.")]
        public Color BorderEffectColor { get; set; }

        //XmlSerializer can't handle Colors so whe do it
        //TODO hide this in property grid
        [XmlElement("BorderEffectColor")]
        public string pseudo_BorderEffectColor
        {
            get
            {
                return SerializeColor(this.BorderEffectColor);
            }
            set
            {
                this.BorderEffectColor = DeserializeColor(value);
            }
        }

        // Screenshots / General

        [Category("Screenshots / General"), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }
        [Category("Screenshots / General"), DefaultValue(-10), Description("Region style setting. Must be between these values: -100, 100")]
        public int BackgroundRegionBrightnessValue { get; set; }
        [Category("Screenshots / General"), DefaultValue(100), Description("Region style setting. Must be between these values: 0, 255")]
        public int BackgroundRegionTransparentValue { get; set; }
        [Category("Screenshots / General"), DefaultValue(false), Description("Copy image to clipboard until URL is retrieved.")]
        public bool CopyImageUntilURL { get; set; }
        [Category("Screenshots / General"), DefaultValue(false), Description("Show Confirmation for Entire Screen or Active Window.")]
        public bool PromptForUpload { get; set; }
        [Category("Screenshots / General"), DefaultValue(15), Description("Region style setting. Must be between these values: -100, 100")]
        public int RegionBrightnessValue { get; set; }
        [Category("Screenshots / General"), DefaultValue(75), Description("Region style setting. Must be between these values: 0, 255")]
        public int RegionTransparentValue { get; set; }
        [Category("Screenshots / General"), DefaultValue(false), Description("Don't display the crosshair and use the cross mouse cursor insted.")]
        public bool UseHardwareCursor { get; set; }

        // Screenshots / Reflection

        [Category("Screenshots / Reflection"), DefaultValue(false), Description("Draw reflection bottom of screenshots.")]
        public bool DrawReflection { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(0), Description("Reflection position will be start: Screenshot height + Offset")]
        public int ReflectionOffset { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(20), Description("Reflection height size relative to screenshot height.")]
        public int ReflectionPercentage { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(true), Description("Adding skew to reflection from bottom left to bottom right.")]
        public bool ReflectionSkew { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(25), Description("How much pixel skew left to right.")]
        public int ReflectionSkewSize { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(255), Description("Reflection transparency start from this value to 0.")]
        public int ReflectionTransparency { get; set; }

        #endregion

        #endregion

        #region Serialization Helpers

        public enum ColorFormat
        {
            NamedColor,
            ARGBColor
        }

        public static string SerializeColor(Color color)
        {
            if (color.IsNamedColor)
            {
                return string.Format("{0}:{1}", ColorFormat.NamedColor, color.Name);
            }
            return string.Format("{0}:{1}:{2}:{3}:{4}", ColorFormat.ARGBColor, color.A, color.R, color.G, color.B);
        }

        public static Color DeserializeColor(string color)
        {
            if (!color.Contains(":")) //For old method
            {
                return Color.Black;
            }

            byte a, r, g, b;

            string[] pieces = color.Split(new[] { ':' });

            ColorFormat colorType = (ColorFormat)Enum.Parse(typeof(ColorFormat), pieces[0], true);

            switch (colorType)
            {
                case ColorFormat.NamedColor:
                    return Color.FromName(pieces[1]);

                case ColorFormat.ARGBColor:
                    a = byte.Parse(pieces[1]);
                    r = byte.Parse(pieces[2]);
                    g = byte.Parse(pieces[3]);
                    b = byte.Parse(pieces[4]);

                    return Color.FromArgb(a, r, g, b);
            }

            return Color.Empty;
        }

        public static XmlFont SerializeFont(Font font)
        {
            return new XmlFont(font);
        }

        public static Font DeserializeFont(XmlFont font)
        {
            return font.ToFont();
        }

        public struct XmlFont
        {
            public string FontFamily;
            public GraphicsUnit GraphicsUnit;
            public float Size;
            public FontStyle Style;

            public XmlFont(Font f)
            {
                FontFamily = f.FontFamily.Name;
                GraphicsUnit = f.Unit;
                Size = f.Size;
                Style = f.Style;
            }

            public Font ToFont()
            {
                return new Font(FontFamily, Size, Style, GraphicsUnit);
            }
        }

        #endregion

        #region I/O Methods

        public void Write()
        {
            new Thread(SaveThread).Start(Engine.appSettings.GetSettingsFilePath());
        }

        public void SaveThread(object filePath)
        {
            lock (this)
            {
                Write((string)filePath);
            }
        }

        public void Write(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                XmlSerializer xs = new XmlSerializer(typeof(XMLSettings), TextUploader.Types.ToArray());
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Error while writing settings", ex);
                MessageBox.Show(ex.Message);
            }
        }

        public static XMLSettings Read()
        {
            string settingsFile = Engine.appSettings.GetSettingsFilePath();
            if (!File.Exists(settingsFile))
            {
                if (File.Exists(Engine.appSettings.XMLSettingsFile))
                {
                    // Step 2 - Attempt to read previous Application Version specific Settings file
                    settingsFile = Engine.appSettings.XMLSettingsFile;
                }
                else
                {
                    // Step 3 - Attempt to read conventional Settings file
                    settingsFile = Engine.XMLSettingsFile;
                }
            }

            if (File.Exists(settingsFile) && settingsFile != Engine.appSettings.GetSettingsFilePath())
            {
                // Update AppSettings.xml
                File.Copy(settingsFile, Engine.appSettings.GetSettingsFilePath());
            }

            Engine.appSettings.XMLSettingsFile = Engine.appSettings.GetSettingsFilePath();
            return Read(Engine.appSettings.XMLSettingsFile);
        }

        public static XMLSettings Read(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string settingsDir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(settingsDir))
                {
                    Directory.CreateDirectory(settingsDir);
                }

                if (File.Exists(filePath))
                {
                    try
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(XMLSettings), TextUploader.Types.ToArray());
                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            return xs.Deserialize(fs) as XMLSettings;
                        }
                    }
                    catch (Exception ex)
                    {
                        // We dont need a MessageBox when we rename enumerations
                        // Renaming enums tend to break parts of serialization
                        FileSystem.AppendDebug("Error while reading settings", ex);
                        OpenFileDialog dlg = new OpenFileDialog { Filter = Engine.FILTER_SETTINGS };
                        dlg.Title = string.Format("{0} Load Settings from Backup...", ex.Message);
                        dlg.InitialDirectory = Engine.appSettings.RootDir;
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            return XMLSettings.Read(dlg.FileName);
                        }
                    }
                }
            }

            return new XMLSettings();
        }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        #endregion

        #region Other methods

        public object GetFieldValue(string name)
        {
            FieldInfo fieldInfo = this.GetType().GetField(name);
            if (fieldInfo != null) return fieldInfo.GetValue(this);
            return null;
        }

        public bool SetFieldValue(string name, object value)
        {
            FieldInfo fieldInfo = this.GetType().GetField(name);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(this, value);
                return true;
            }
            return false;
        }

        #endregion
    }
}