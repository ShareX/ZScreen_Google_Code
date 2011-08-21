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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using GradientTester;
using GraphicsMgrLib;
using HelpersLib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZSS.IndexersLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenLib
{
    [XmlRoot("Settings")]
    public class XMLSettings
    {
        #region Settings

        //~~~~~~~~~~~~~~~~~~~~~
        //  Misc Settings
        //~~~~~~~~~~~~~~~~~~~~~

        public bool FirstRun = true;
        public FormWindowState WindowState = FormWindowState.Normal;
        public Size WindowSize = Size.Empty;
        public Point WindowLocation = Point.Empty;
        public bool Windows7TaskbarIntegration = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Main
        //~~~~~~~~~~~~~~~~~~~~~

        public List<int> MyImageUploaders = new List<int>();
        public List<int> MyFileUploaders = new List<int>();
        public List<int> MyTextUploaders = new List<int>();
        public List<int> MyURLShorteners = new List<int>();
        public List<int> ConfClipboardContent = new List<int>();
        public List<int> ConfOutputs = new List<int>();
        public List<int> ConfLinkFormat = new List<int>();

        public long ScreenshotDelayTime = 0;
        public Times ScreenshotDelayTimes = Times.Seconds;
        public bool PromptForOutputs = false;
        public bool ShowCursor = false;

        [Category("Options / Clipboard Upload"), DefaultValue(true), Description("Show Clipboard Content Viewer before uploading Clipboard Content using the Main tab.")]
        public bool ShowClipboardContentViewer { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~
        //  Destinations
        //~~~~~~~~~~~~~~~~~~~~~

        // Printer

        public PrintSettings PrintSettings = new PrintSettings();

        // TinyPic

        [Category("Options / Image Uploaders"), DefaultValue(false), Description("Switch from TinyPic to ImageShack if the image dimensions are greater than 1600 pixels.")]
        public bool TinyPicSizeCheck { get; set; }

        // Twitter

        public bool TwitterEnabled = false;
        public TwitterClientSettings TwitterClientConfig = new TwitterClientSettings();

        //~~~~~~~~~~~~~~~~~~~~~
        //  Hotkeys
        //~~~~~~~~~~~~~~~~~~~~~

        public const Keys DefaultHotkeyEntireScreen = Keys.PrintScreen;
        public const Keys DefaultHotkeyActiveWindow = Keys.Alt | Keys.PrintScreen;
        public const Keys DefaultHotkeyCropShot = Keys.Control | Keys.PrintScreen;
        public const Keys DefaultHotkeySelectedWindow = Keys.Shift | Keys.PrintScreen;
        public const Keys DefaultHotkeyFreehandCropShot = Keys.Control | Keys.Shift | Keys.PrintScreen;
        public const Keys DefaultHotkeyClipboardUpload = Keys.Control | Keys.PageUp;
        public const Keys DefaultHotkeyLastCropShot = Keys.None;
        public const Keys DefaultHotkeyAutoCapture = Keys.None;
        public const Keys DefaultHotkeyDropWindow = Keys.None;
        public const Keys DefaultHotkeyLanguageTranslator = Keys.None;
        public const Keys DefaultHotkeyScreenColorPicker = Keys.None;
        public const Keys DefaultHotkeyTwitterClient = Keys.None;

        public Keys HotkeyEntireScreen = DefaultHotkeyEntireScreen;
        public Keys HotkeyActiveWindow = DefaultHotkeyActiveWindow;
        public Keys HotkeyCropShot = DefaultHotkeyCropShot;
        public Keys HotkeySelectedWindow = DefaultHotkeySelectedWindow;
        public Keys HotkeyFreehandCropShot = DefaultHotkeyFreehandCropShot;
        public Keys HotkeyClipboardUpload = DefaultHotkeyClipboardUpload;
        public Keys HotkeyLastCropShot = DefaultHotkeyLastCropShot;
        public Keys HotkeyAutoCapture = DefaultHotkeyAutoCapture;
        public Keys HotkeyDropWindow = DefaultHotkeyDropWindow;
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
        public XColor CropCrosshairArgb = Color.Black;
        public bool CropShowBigCross = true;
        public bool CropShowMagnifyingGlass = true;

        public bool CropShowRuler = true;
        public bool CropDynamicBorderColor = true;
        public decimal CropRegionInterval = 75;
        public decimal CropRegionStep = 5;
        public decimal CropHueRange = 50;
        public XColor CropBorderArgb = Color.FromArgb(255, 0, 255);
        public decimal CropBorderSize = 1;
        public bool CropShowGrids = false;

        public Rectangle LastRegion = Rectangle.Empty;
        public Rectangle LastCapture = Rectangle.Empty;

        [Category("Screenshots"), DefaultValue(false), Description("Make the corners rounded")]
        public bool ImageAddRoundedCorners { get; set; }

        [Category("Screenshots"), DefaultValue(false), Description("Add a shadow (if the screenshot is big enough)")]
        public bool ImageAddShadow { get; set; }

        public bool CropGridToggle = false;
        public Size CropGridSize = new Size(100, 100);

        // Selected Window

        public RegionStyles SelectedWindowRegionStyles = RegionStyles.REGION_BRIGHTNESS;
        public bool SelectedWindowRectangleInfo = true;
        public bool SelectedWindowRuler = true;
        public XColor SelectedWindowBorderArgb = Color.FromArgb(255, 0, 255);
        public decimal SelectedWindowBorderSize = 2;
        public bool SelectedWindowDynamicBorderColor = true;
        public decimal SelectedWindowRegionInterval = 75;
        public decimal SelectedWindowRegionStep = 5;
        public decimal SelectedWindowHueRange = 50;
        public bool SelectedWindowCaptureObjects = true;

        // Active Window

        public bool ActiveWindowClearBackground = true;
        public bool ActiveWindowCleanTransparentCorners = true;
        public bool ActiveWindowIncludeShadows = true;
        public bool ActiveWindowShowCheckers = false;
        public bool ActiveWindowTryCaptureChildren = false;
        public bool ActiveWindowPreferDWM = false;

        [Category("Screenshots / Active Window"), DefaultValue(false), Description("Freeze active window during capture. WARNING: Do not try this on a Windows process.")]
        public bool ActiveWindowGDIFreezeWindow { get; set; }

        // Freehand Crop Shot

        public bool FreehandCropShowHelpText = true;
        public bool FreehandCropAutoUpload = false;
        public bool FreehandCropAutoClose = false;
        public bool FreehandCropShowRectangleBorder = false;

        // Interaction

        public bool CompleteSound = true;
        public bool ShowBalloonTip = true;
        public bool BalloonTipOpenLink = true;
        public bool ShowUploadDuration = true;
        public decimal FlashTrayCount = 2;
        public bool CaptureEntireScreenOnError = false;
        public bool CloseDropBox = false;
        public Point LastDropBoxPosition = Point.Empty;

        // Naming Conventions

        public string ActiveWindowPattern = "%t-%y-%mo-%d_%h.%mi.%s";
        public string EntireScreenPattern = "Screenshot-%y-%mo-%d_%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        public int MaxNameLength = 100;

        [Category("Screenshots / Naming Conventions"), DefaultValue(false), Description("Prompt to save the image in a different location")]
        public bool ShowSaveFileDialogImages { get; set; }

        // Image Settings
        public EImageFormat ImageFormat = EImageFormat.PNG;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        public bool MakeJPGBackgroundWhite = true;

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

        [Category("Options / Clipboard Upload"), DefaultValue(false), Description("Do not apply watermark during Clipboard Upload")]
        public bool WatermarkExcludeClipboardUpload { get; set; }

        public string WatermarkText = "%h:%mi";
        public XFont WatermarkFont = new XFont("Arial", 8);
        public XColor WatermarkFontArgb = Color.White;
        public decimal WatermarkFontTrans = 255;
        public decimal WatermarkCornerRadius = 4;
        public XColor WatermarkGradient1Argb = Color.FromArgb(85, 85, 85);
        public XColor WatermarkGradient2Argb = Color.Black;
        public XColor WatermarkBorderArgb = Color.Black;
        public decimal WatermarkBackTrans = 225;
        public LinearGradientMode WatermarkGradientType = LinearGradientMode.Vertical;
        public bool WatermarkUseCustomGradient = false;
        public GradientMakerSettings GradientMakerOptions = new GradientMakerSettings();

        public string WatermarkImageLocation = "";
        public bool WatermarkUseBorder = false;
        public decimal WatermarkImageScale = 100;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Editors
        //~~~~~~~~~~~~~~~~~~~~~

        public List<Software> ActionsList = new List<Software>();
        public Software ImageEditor = null;
        public List<Software> TextEditors = new List<Software>();
        public Software TextEditorActive;
        public bool PerformActions = false;
        public bool TextEditorEnabled = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  HTTP
        //~~~~~~~~~~~~~~~~~~~~~

        // Image Uploaders
        [Category("Options / Image Uploaders"), DefaultValue(true), Description("Automatically switch to File Uploader if a user copies (Clipboard Upload) or drags a non-Image.")]
        public bool AutoSwitchFileUploader { get; set; }

        [Category("Options / Image Uploaders"), DefaultValue(2), Description("Number of Retries if image uploading fails.")]
        public int ErrorRetryCount { get; set; }

        [Category("Options / Image Uploaders"), DefaultValue(false), Description("Retry with between TinyPic and ImageShack if the TinyPic or ImageShack fails the first attempt.")]
        public bool ImageUploadRetryOnFail { get; set; }

        [Category("Options / Image Uploaders"), DefaultValue(false), Description("Randomly select a valid destination when instead of retrying between ImageShack and TinyPic.")]
        public bool ImageUploadRandomRetryOnFail { get; set; }

        [Category("Options / Image Uploaders"), DefaultValue(false), Description("Retry with another Image Uploader if the Image Uploader fails the first attempt.")]
        public bool ImageUploadRetryOnTimeout { get; set; }

        [Category("Options / Image Uploaders"), DefaultValue(30000), Description("Change the Image Uploader if the upload times out by this amount of milliseconds.")]
        public int UploadDurationLimit { get; set; }

        // Indexer

        public IndexerConfig IndexerConfig = new IndexerConfig();

        // Web Page Upload

        public bool WebPageUseCustomSize = true;
        public int WebPageWidth = 1024;
        public int WebPageHeight = 768;
        public bool WebPageAutoUpload = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  History
        //~~~~~~~~~~~~~~~~~~~~~

        public int HistoryMaxNumber = 100;
        public bool HistorySave = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Options
        //~~~~~~~~~~~~~~~~~~~~~

        // General - Program

        public bool ShowMainWindow = false;
        public bool ShowInTaskbar = true;
        public bool ShowHelpBalloonTips = true;

        [Category("Options / General"), DefaultValue(true), Description("Remember Main Window size and position.")]
        public bool SaveFormSizePosition { get; set; }

        [Category("Options / General"), DefaultValue(false), Description("Lock Main Window size to the minimum possible size and disable resizing.")]
        public bool LockFormSize { get; set; }

        public bool AutoSaveSettings = true;
        public WindowButtonAction WindowButtonActionClose = WindowButtonAction.MinimizeToTray;
        public WindowButtonAction WindowButtonActionMinimize = WindowButtonAction.MinimizeToTaskbar;

        // General - Monitor Clipboard

        public bool MonitorImages = false;
        public bool MonitorText = false;
        public bool MonitorFiles = false;
        public bool MonitorUrls = false;

        // General - Check Updates

        public bool CheckUpdates = true;
        public bool CheckUpdatesBeta = false;
        public ReleaseChannelType ReleaseChannel = ReleaseChannelType.Stable;

        //~~~~~~~~~~~~~~~
        // Proxy Settings
        //~~~~~~~~~~~~~~~

        public List<ProxyInfo> ProxyList = new List<ProxyInfo>();
        public int ProxySelected = 0;
        public ProxyInfo ProxyActive = null;
        public ProxyConfigType ProxyConfig = ProxyConfigType.NoProxy;

        //~~~~~~~~~
        // Paths
        //~~~~~~~~~

        public bool DeleteLocal = false;

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
        }

        // Destinations / FTP

        [Category("Destinations / FTP"), DefaultValue(true), Description("Periodically backup FTP settings.")]
        public bool BackupFTPSettings { get; set; }

        [Category("Destinations / FTP"), DefaultValue(50), Description("Screenshots cache size in MiB for the FTP Client.")]
        public int ScreenshotCacheSize { get; set; }

        [Category("Destinations / FTP"), DefaultValue(false), Description("Allows you to choose the FTP account before uploading.")]
        public bool ShowFTPSettingsBeforeUploading { get; set; }

        // Options / General

        [Category("Options / General"), DefaultValue(false), Description("Show Clipboard Mode Chooser after upload is complete")]
        public bool ShowUploadResultsWindow { get; set; }

        [Category("Options / Interaction"), DefaultValue(true), Description("Showing upload progress percentage in tray icon")]
        public bool ShowTrayUploadProgress { get; set; }

        [Category("Options / General"), DefaultValue(true), Description("Write debug information into a log file.")]
        public bool WriteDebugFile { get; set; }

        [Category("Options / General"), DefaultValue(false), Description("Use SetProcessWorkingSetSize when ZScreen window is closed (minimized to tray) or idle.")]
        public bool EnableAutoMemoryTrim { get; set; }

        [Category("Options / General"), DefaultValue(false), Description("Enables keyboard hook timer which reactivating keyboard hook every 5 seconds.")]
        public bool EnableKeyboardHookTimer { get; set; }

        // Options / Interaction

        [Category("Options / URL Shorteners"), DefaultValue(true),
        Description("If you use Clipboard Upload and the clipboard contains a URL then the URL will be shortened instead of performing a text upload.")]
        public bool ShortenUrlUsingClipboardUpload { get; set; }

        [Category("Options / URL Shorteners"), DefaultValue(80),
        Description("ShortenUrlAfterUpload will only be activated if the length of a URL exceeds this value. To always shorten a URL set this value to 0.")]
        public int ShortenUrlAfterUploadAfter { get; set; }

        [Category("Options / URL Shorteners"), DefaultValue(false), Description("Optionally shorten the URL after completing a task.")]
        public bool ShortenUrlAfterUpload { get; set; }

        [Category("Options / Clipboard"), DefaultValue(true), Description("Always overwrite the clipboard with the screenshot image or url.")]
        public bool ClipboardOverwrite { get; set; }

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

        [XmlIgnore(), Category("Screenshots / Clipboard"), Description("Background color of images captured to clipboard.")]
        public Color ClipboardBackgroundColor { get; set; }

        [XmlElement("BorderEffectColor"), BrowsableAttribute(false)]
        public XColor BorderEffectArgb
        {
            get
            {
                return BorderEffectColor;
            }
            set
            {
                BorderEffectColor = value;
            }
        }

        [XmlElement("ClipboardBackgroundColor"), BrowsableAttribute(false)]
        public XColor ClipboardBackgroundArgb
        {
            get
            {
                return ClipboardBackgroundColor;
            }
            set
            {
                ClipboardBackgroundColor = value;
            }
        }

        // Screenshots / General

        [Category("Screenshots / General"), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }

        [Category("Screenshots / General"), DefaultValue(-10), Description("Region style setting. Must be between these values: -100, 100")]
        public int BackgroundRegionBrightnessValue { get; set; }

        [Category("Screenshots / General"), DefaultValue(100), Description("Region style setting. Must be between these values: 0, 255")]
        public int BackgroundRegionTransparentValue { get; set; }

        [Category("Screenshots / General"), DefaultValue(false), Description("Show output to the user as soon as at least one output is ready e.g. copy image to clipboard until URL is retrieved.")]
        public bool ShowOutputsAsap { get; set; }

        [Category("Screenshots / General"), DefaultValue(false), Description("Show Confirmation for Entire Screen or Active Window.")]
        public bool PromptForUpload { get; set; }

        [Category("Screenshots / General"), DefaultValue(15), Description("Region style setting. Must be between these values: -100, 100")]
        public int RegionBrightnessValue { get; set; }

        [Category("Screenshots / General"), DefaultValue(75), Description("Region style setting. Must be between these values: 0, 255")]
        public int RegionTransparentValue { get; set; }

        [Category("Screenshots / General"), DefaultValue(CropEngineType.Cropv1), Description("Choose the method of Crop")]
        public CropEngineType CropEngineMode { get; set; }

        [Category("Screenshots / General"), DefaultValue(false), Description("Don't display the crosshair and use the cross mouse cursor instead.")]
        public bool UseHardwareCursor { get; set; }

        // Screenshots / Reflection

        [Category("Screenshots / Reflection"), DefaultValue(false), Description("Draw reflection bottom of screenshots.")]
        public bool DrawReflection { get; set; }

        [Category("Screenshots / Reflection"), DefaultValue(0), Description("Reflection position will start: Screenshot height + Offset")]
        public int ReflectionOffset { get; set; }

        [Category("Screenshots / Reflection"), DefaultValue(20), Description("Reflection height size relative to screenshot height.")]
        public int ReflectionPercentage { get; set; }

        [Category("Screenshots / Reflection"), DefaultValue(true), Description("Adding skew to reflection from bottom left to bottom right.")]
        public bool ReflectionSkew { get; set; }

        [Category("Screenshots / Reflection"), DefaultValue(25), Description("How much pixel skew left to right.")]
        public int ReflectionSkewSize { get; set; }

        [Category("Screenshots / Reflection"), DefaultValue(255), Description("Reflection transparency start from this value to 0.")]
        public int ReflectionTransparency { get; set; }

        #endregion Properties for PropertyGrid

        #endregion Settings

        #region I/O Methods

        public bool Write()
        {
            return Write(Engine.SettingsFilePath);
        }

        public bool Write(string filePath)
        {
            return SettingsHelper.Save(this, filePath, SerializationType.Xml);
        }

        public static XMLSettings Read()
        {
            string settingsFile = Engine.IsPortable ? Engine.GetPreviousSettingsFile(Engine.SettingsDir) : Engine.SettingsFilePath;
            if (!File.Exists(settingsFile))
            {
                if (File.Exists(Engine.AppConf.XMLSettingsPath))
                {
                    // Step 2 - Attempt to read previous Application Version specific Settings file
                    settingsFile = Engine.AppConf.XMLSettingsPath;
                }
                else
                {
                    // Step 3 - Attempt to read conventional Settings file
                    settingsFile = Engine.SettingsFilePath;
                }
                Engine.MyLogger.WriteLine("Using " + settingsFile);
            }

            if (File.Exists(settingsFile) && settingsFile != Engine.SettingsFilePath)
            {
                File.Copy(settingsFile, Engine.SettingsFilePath);                 // Update AppSettings.xml
            }

            Engine.AppConf.XMLSettingsPath = Engine.SettingsFilePath;

            return Read(Engine.AppConf.XMLSettingsPath);
        }

        public static XMLSettings Read(string filePath)
        {
            return SettingsHelper.Load<XMLSettings>(filePath, SerializationType.Xml);
        }

        #endregion I/O Methods

        #region Other methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

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

        #endregion Other methods
    }
}