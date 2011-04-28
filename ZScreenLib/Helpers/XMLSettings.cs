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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using GradientTester;
using GraphicsMgrLib;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using ZSS.IndexersLib;

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
        public static string XMLFileName = string.Format("{0}-{1}-Settings.xml", Application.ProductName, Application.ProductVersion);

        //~~~~~~~~~~~~~~~~~~~~~
        //  Main
        //~~~~~~~~~~~~~~~~~~~~~

        public ImageUploaderType ImageUploaderType = ImageUploaderType.CLIPBOARD;
        public FileUploaderType FileUploaderType = FileUploaderType.RapidShare;
        public TextUploaderType TextUploaderType = TextUploaderType.PASTEBIN;
        public UrlShortenerType URLShortenerType = UrlShortenerType.BITLY;
        public ClipboardUriType ClipboardUriMode = ClipboardUriType.FULL;
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

        public AccountType SendSpaceAccountType = AccountType.Anonymous;
        public string SendSpaceUserName = string.Empty;
        public string SendSpacePassword = string.Empty;

        // Dropbox

        public string DropboxUserToken = string.Empty;
        public string DropboxUserSecret = string.Empty;
        public string DropboxUploadPath = "Public";
        public string DropboxEmail = string.Empty;
        public string DropboxName = string.Empty;
        public string DropboxUserID = string.Empty;

        // ImageShack

        public string ImageShackRegistrationCode = string.Empty;
        public string ImageShackUserName = string.Empty;
        public bool ImageShackShowImagesInPublic = false;

        // TinyPic

        public string TinyPicShuk = string.Empty;
        public string TinyPicUserName = string.Empty;
        public string TinyPicPassword = string.Empty;
        public bool RememberTinyPicUserPass = false;
        public bool TinyPicSizeCheck = true;

        // Imgur

        public AccountType ImgurAccountType = AccountType.Anonymous;
        public OAuthInfo ImgurOAuthInfo = null;

        // Twitter

        public int TwitterAcctSelected = 0;
        public List<OAuthInfo> TwitterOAuthInfoList = new List<OAuthInfo>();
        public TwitterClientSettings TwitterClientConfig = new TwitterClientSettings();
        public string TwitterUsername = string.Empty;
        public string TwitterPassword = string.Empty;
        public bool TwitterEnabled = false;

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
        public const Keys DefaultHotkeyFreehandCropShot = Keys.Control | Keys.PageDown;
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
        public Keys HotkeyFreehandCropShot = DefaultHotkeyFreehandCropShot;
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

        public RegionStyles CropRegionStyles = RegionStyles.REGION_BRIGHTNESS;
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
        [Category("Screenshots / Crop Shot"), DefaultValue(false), Description("Make the corners rounded")]
        public bool CropShotRoundedCorners { get; set; }
        [Category("Screenshots / Crop Shot"), DefaultValue(false), Description("Add a shadow (if the screenshot is big enough)")]
        public bool CropShotShadow { get; set; }

        // Selected Window

        public RegionStyles SelectedWindowRegionStyles = RegionStyles.REGION_BRIGHTNESS;
        public bool SelectedWindowRectangleInfo = true;
        public bool SelectedWindowRuler = true;
        public string SelectedWindowBorderColor = SerializeColor(Color.FromArgb(255, 0, 255));
        public decimal SelectedWindowBorderSize = 2;
        public bool SelectedWindowDynamicBorderColor = true;
        public decimal SelectedWindowRegionInterval = 75;
        public decimal SelectedWindowRegionStep = 5;
        public decimal SelectedWindowHueRange = 50;
        public bool SelectedWindowCaptureObjects = true;
        [Category("Screenshots / Selected Window"), DefaultValue(false), Description("Make the corners rounded")]
        public bool SelectedWindowRoundedCorners { get; set; }
        [Category("Screenshots / Selected Window"), DefaultValue(false), Description("Add a shadow (if the screenshot is big enough)")]
        public bool SelectedWindowShadow { get; set; }

        // Active Window

        public bool ActiveWindowClearBackground = true;
        public bool ActiveWindowCleanTransparentCorners = true;
        public bool ActiveWindowIncludeShadows = true;
        public bool ActiveWindowShowCheckers = false;
        public bool ActiveWindowTryCaptureChildren = false;
        public bool ActiveWindowPreferDWM = true;
        public bool ActiveWindowGDIFreezeWindow = false;

        // Freehand Crop Shot

        public bool FreehandCropShowHelpText = true;
        public bool FreehandCropAutoUpload = false;
        public bool FreehandCropAutoClose = false;
        public bool FreehandCropShowRectangleBorder = false;

        // Interaction

        public bool CopyClipboardAfterTask = true;
        public bool CompleteSound = true;
        public bool ShowBalloonTip = true;
        public bool BalloonTipOpenLink = true;
        public bool ShowUploadDuration = true;
        public decimal FlashTrayCount = 1;
        public bool CaptureEntireScreenOnError = false;
        public bool CloseDropBox = false;
        public Point LastDropBoxPosition = Point.Empty;
        public bool CloseQuickActions = false;

        // Naming Conventions

        public string ActiveWindowPattern = "%t-%y-%mo-%d_%h.%mi.%s";
        public string EntireScreenPattern = "Screenshot-%y-%mo-%d_%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        public int MaxNameLength = 100;
        [Category("Options / Naming Conventions"), DefaultValue(false), Description("Prompt to save the image in a different location")]
        public bool ShowSaveFileDialogImages { get; set; }

        // Image Settings

        public ImageFileFormatType ImageFileFormat = ImageFileFormatType.Png;
        public decimal JpgQuality = 90;
        public GIFQuality GIFQuality = GIFQuality.Bit8;
        public decimal SwitchAfter = 512;
        public ImageFileFormatType ImageFormatSwitch = ImageFileFormatType.Jpg;
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

        //~~~~~~~~~~~~~~~~~~~~~
        //  Editors
        //~~~~~~~~~~~~~~~~~~~~~

        public List<Software> ImageEditors = new List<Software>();
        public Software ImageEditor = null;
        public Software TextEditorActive;
        public List<Software> TextEditors = new List<Software>();
        public bool ImageEditorsEnabled = false;
        public bool TextEditorEnabled = false;
        public bool ImageEditorAutoSave = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  FTP
        //~~~~~~~~~~~~~~~~~~~~~

        public List<FTPAccount> FTPAccountList = new List<FTPAccount>();
        public int FtpImages = 0;
        public int FtpText = 0;
        public int FtpFiles = 0;
        public int FTPThumbnailWidth = 150;
        public bool FTPThumbnailCheckSize = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Localhost
        //~~~~~~~~~~~~~~~~~~~~~

        public List<LocalhostAccount> LocalhostAccountList = new List<LocalhostAccount>();
        public int LocalhostSelected = 0;

        //~~~~~~~~~~~~~~~~~~~~~
        //  DekiWiki
        //~~~~~~~~~~~~~~~~~~~~~

        public List<DekiWikiAccount> DekiWikiAccountList = new List<DekiWikiAccount>();
        public int DekiWikiSelected = 0;
        public bool DekiWikiForcePath = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  MediaWiki
        //~~~~~~~~~~~~~~~~~~~~~

        public List<MediaWikiAccount> MediaWikiAccountList = new List<MediaWikiAccount>();
        public int MediaWikiAccountSelected = 0;

        //~~~~~~~~~~~~~~~~~~~~~
        //  HTTP
        //~~~~~~~~~~~~~~~~~~~~~

        // Image Uploaders

        public bool AutoSwitchFileUploader = true;
        public decimal ErrorRetryCount = 2;
        public bool ImageUploadRetryOnFail = true;
        public bool ImageUploadRandomRetryOnFail = false;
        public bool AddFailedScreenshot = true;
        public bool ImageUploadRetryOnTimeout = false;
        public decimal UploadDurationLimit = 15000;

        // Indexer

        public IndexerConfig IndexerConfig = new IndexerConfig();

        // Custom Uploaders

        public List<CustomUploaderInfo> CustomUploadersList = null;
        public int CustomUploaderSelected = 0;

        // Web Page Upload

        public bool WebPageUseCustomSize = true;
        public int WebPageWidth = 1024;
        public int WebPageHeight = 768;
        public bool WebPageAutoUpload = true;

        // Language Translator

        public string GoogleSourceLanguage = "auto";
        public string GoogleTargetLanguage = "en";
        public string GoogleTargetLanguage2 = "?";
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

        public bool ShowMainWindow = false;
        public bool ShowInTaskbar = true;
        public bool ShowHelpBalloonTips = true;

        [Category("Options / General"), DefaultValue(true), Description("Remember Main Window size and position.")]
        public bool SaveFormSizePosition { get; set; }
        [Category("Options / General"), DefaultValue(false), Description("Lock Main Window size to the minimum possible size and disable resizing.")]
        public bool LockFormSize { get; set; }
        public bool AutoSaveSettings = false;
        public WindowButtonAction CloseButtonAction = WindowButtonAction.MinimizeToTray;
        public WindowButtonAction MinimizeButtonAction = WindowButtonAction.MinimizeToTaskbar;

        // General - Monitor Clipboard

        public bool MonitorImages = false;
        public bool MonitorText = false;
        public bool MonitorFiles = false;
        public bool MonitorUrls = false;

        // General - Check Updates

        public bool CheckUpdates = true;
        public bool CheckUpdatesBeta = false;

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

        // TODO: Need to remove this now?
        [Category("Destinations / FTP Server"), DefaultValue(true), Description("Use the active FTP Server instead of the active Text Uploader for uploading Text. Prerequisite: PreferFileUploaderForText")]
        public bool PreferFtpServerForIndex { get; set; }

        // Destinations / FTP

        [Category("Destinations / FTP Server"), DefaultValue(true), Description("Periodically backup FTP settings.")]
        public bool BackupFTPSettings { get; set; }

        // Options / Actions Toolbar

        [Category("Options / Actions Toolbar"), DefaultValue(false), Description("Open Actions Toolbar on startup.")]
        public bool ActionsToolbarMode { get; set; }
        [Category("Options / Actions Toolbar"), Description("Action Toolbar Location.")]
        public Point ActionToolbarLocation { get; set; }

        // Options / General

        [Category("Options / General"), DefaultValue(false), Description("Prefer System Folders for all the data created by ZScreen")]
        public bool PreferSystemFolders { get; set; }
        [Category("Options / General"), DefaultValue(false), Description("Show Clipboard Mode Chooser after upload is complete")]
        public bool ShowClipboardModeChooser { get; set; }
        [Category("Options / Interaction"), DefaultValue(true), Description("Showing upload progress percentage in tray icon")]
        public bool ShowTrayUploadProgress { get; set; }
        [Category("Options / General"), DefaultValue(true), Description("Write debug information into a log file.")]
        public bool WriteDebugFile { get; set; }
        [Category("Options / General"), DefaultValue(false), Description("Enables keyboard hook timer which reactivating keyboard hook every 5 seconds.")]
        public bool EnableKeyboardHookTimer { get; set; }

        // Options / History Settings

        [Category("Options / History Settings"), DefaultValue(false), Description("Prefer browser view to navigate uploaded images.")]
        public bool PreferBrowserForImages { get; set; }
        [Category("Options / History Settings"), DefaultValue(false), Description("Prefer browser view to navigate uploaded text.")]
        public bool PreferBrowserForText { get; set; }

        // Options / Interaction

        [Category("Options / General"), DefaultValue(true),
        Description("If you use Clipboard Upload and the clipboard contains a URL then the URL will be shortened instead of performing a text upload.")]
        public bool ShortenUrlUsingClipboardUpload { get; set; }
        [Category("Options / General"), DefaultValue(80),
        Description("ShortenUrlAfterUpload will only be activated if the length of a URL exceeds this value. To always shorten a URL set this value to 0.")]
        public int ShortenUrlAfterUploadAfter { get; set; }
        [Category("Options / General"), DefaultValue(false), Description("Optionally shorten the URL after completing a task.")]
        public bool ShortenUrlAfterUpload { get; set; }
        [Category("Options / General"), DefaultValue(true), Description("Always overwrite the clipboard with the screenshot image or url.")]
        public bool ClipboardOverwrite { get; set; }
        [Category("Options / General"), DefaultValue(false), Description("Do not store any data in the hard disk.")]
        public bool MemoryMode { get; set; }

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

        //XmlSerializer can't handle Colors so we do it
        [XmlElement("BorderEffectColor"), BrowsableAttribute(false)]
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

        //XmlSerializer can't handle Colors so we do it
        [XmlElement("ClipboardBackgroundColor"), BrowsableAttribute(false)]
        public string pseudo_ClipboardBackgroundColor
        {
            get
            {
                return SerializeColor(this.ClipboardBackgroundColor);
            }
            set
            {
                this.ClipboardBackgroundColor = DeserializeColor(value);
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
        [Category("Screenshots / General"), DefaultValue(false), Description("Use crop beta.")]
        public bool UseCropBeta { get; set; }
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

        #endregion Serialization Helpers

        #region I/O Methods

        public void Write()
        {
            new Thread(SaveThread).Start(Engine.mAppSettings.GetSettingsFilePath());
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

                XmlSerializer xs = new XmlSerializer(typeof(XMLSettings));
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
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
            string settingsFile = Engine.Portable ? Engine.GetLatestSettingsFile(Engine.SettingsDir) : Engine.mAppSettings.GetSettingsFilePath();
            if (!File.Exists(settingsFile))
            {
                if (File.Exists(Engine.mAppSettings.XMLSettingsFile))
                {
                    // Step 2 - Attempt to read previous Application Version specific Settings file
                    settingsFile = Engine.mAppSettings.XMLSettingsFile;
                }
                else
                {
                    // Step 3 - Attempt to read conventional Settings file
                    settingsFile = Engine.XMLSettingsFile;
                }
            }

            if (File.Exists(settingsFile) && settingsFile != Engine.mAppSettings.GetSettingsFilePath())
            {
                // Update AppSettings.xml
                File.Copy(settingsFile, Engine.mAppSettings.GetSettingsFilePath());
            }

            Engine.mAppSettings.XMLSettingsFile = Engine.mAppSettings.GetSettingsFilePath();
            FileSystem.AppendDebug("Reading " + Engine.mAppSettings.XMLSettingsFile);
            return Read(Engine.mAppSettings.XMLSettingsFile);
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
                        XmlSerializer xs = new XmlSerializer(typeof(XMLSettings));
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            return xs.Deserialize(fs) as XMLSettings;
                        }
                    }
                    catch (Exception ex)
                    {
                        // We dont need a MessageBox when we rename enumerations
                        // Renaming enums tend to break parts of serialization
                        FileSystem.AppendDebug("Error while reading settings", ex);
                        using (OpenFileDialog dlg = new OpenFileDialog { Filter = Engine.FILTER_SETTINGS })
                        {
                            dlg.Title = string.Format("{0} Load Settings from Backup...", ex.Message);
                            dlg.InitialDirectory = Engine.mAppSettings.RootDir;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                return XMLSettings.Read(dlg.FileName);
                            }
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

        #endregion I/O Methods

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

        #endregion Other methods
    }
}