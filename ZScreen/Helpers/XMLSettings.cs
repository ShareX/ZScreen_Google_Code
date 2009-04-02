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
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing;

namespace ZSS
{
    [XmlRoot("Settings")]
    public class XMLSettings
    {
        #region Settings

        //~~~~~~~~~~~~~~~~~~~~~
        //  Misc Settings
        //~~~~~~~~~~~~~~~~~~~~~

        public bool RunOnce = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Main
        //~~~~~~~~~~~~~~~~~~~~~

        public ImageDestType ScreenshotDestMode = ImageDestType.IMAGESHACK;
        public ClipboardUriType ClipboardUriMode = ClipboardUriType.FULL;
        public decimal ScreenshotDelay = 0;
        public bool ManualNaming = false;
        public bool ShowCursor = false;
        public bool ShowWatermark = false;
        public Size CropGridSize = new Size(100, 100);
        public bool ActiveHelp = true;
        public bool GTActiveHelp = false;
        public string HelpToLanguage = "en";

        //~~~~~~~~~~~~~~~~~~~~~
        //  Hotkeys
        //~~~~~~~~~~~~~~~~~~~~~

        public HKcombo HKActiveWindow = new HKcombo(Keys.Alt, Keys.PrintScreen);
        public HKcombo HKSelectedWindow = new HKcombo(Keys.Shift, Keys.PrintScreen);
        public HKcombo HKCropShot = new HKcombo(Keys.Control, Keys.PrintScreen);
        public HKcombo HKLastCropShot = new HKcombo(Keys.None);
        public HKcombo HKEntireScreen = new HKcombo(Keys.PrintScreen);
        public HKcombo HKClipboardUpload = new HKcombo(Keys.None);
        public HKcombo HKDropWindow = new HKcombo(Keys.None);
        public HKcombo HKActionsToolbar = new HKcombo(Keys.None);
        public HKcombo HKQuickOptions = new HKcombo(Keys.None);
        public HKcombo HKLanguageTranslator = new HKcombo(Keys.None);
        public HKcombo HKScreenColorPicker = new HKcombo(Keys.None);

        //~~~~~~~~~~~~~~~~~~~~~
        //  Capture
        //~~~~~~~~~~~~~~~~~~~~~

        // Crop Shot
        public int CropRegionStyle = 1;
        public bool CropRegionHotkeyInfo = true;
        public string CropCrosshairColor = SerializeColor(Color.Red);
        public bool CropDynamicCrosshair = true;
        public int CropInterval = 25;
        public int CropStep = 1;
        public int CrosshairLineCount = 2;
        public int CrosshairLineSize = 25;
        public bool CropShowRuler = false;
        public string CropBorderColor = SerializeColor(Color.Black);
        public bool CropShowBigCross = true;
        public decimal CropBorderSize = 1;
        public bool CropShowGrids = false;
        public bool CropRegionRectangleInfo = true;
        public bool CropGridToggle = false;

        // Selected Window
        public int SelectedWindowRegionStyle = 2;
        public bool SelectedWindowFront = false;
        public bool SelectedWindowRectangleInfo = true;
        public bool SelectedWindowRuler = false;
        public string SelectedWindowBorderColor = SerializeColor(Color.Red);
        public decimal SelectedWindowBorderSize = 2;

        // Interaction
        public decimal FlashTrayCount = 1;
        public bool CaptureEntireScreenOnError = false;
        public bool ShowBalloonTip = true;
        public bool BalloonTipOpenLink = false;
        public bool ShowUploadDuration = false;
        public bool CompleteSound = false;
        public bool CloseDropBox = false;
        public Point LastDropBoxPosition = Point.Empty;
        public bool CloseQuickActions = false;

        // Naming Conventions
        public string activeWindow = "%t-%y.%mo.%d-%h.%mi.%s";
        public string entireScreen = "SS-%y.%mo.%d-%h.%mi.%s";
        public int AutoIncrement = 0;

        // Watermark
        public WatermarkPositionType WatermarkPositionMode = WatermarkPositionType.BOTTOM_RIGHT;
        public decimal WatermarkOffset = 5;
        public string WatermarkText = "%h:%mi";
        public XmlFont WatermarkFont = SerializeFont(new Font("Arial", 8));
        public string WatermarkFontColor = SerializeColor(Color.White);
        public decimal WatermarkFontTrans = 255;
        public decimal WatermarkCornerRadius = 4;
        public string WatermarkGradient1 = SerializeColor(Color.FromArgb(85, 85, 85));
        public string WatermarkGradient2 = SerializeColor(Color.Black);
        public string WatermarkBorderColor = SerializeColor(Color.Black);
        public decimal WatermarkBackTrans = 225;
        public System.Drawing.Drawing2D.LinearGradientMode WatermarkGradientType = System.Drawing.Drawing2D.LinearGradientMode.Vertical;

        // Quality
        public int FileFormat = 0;
        public int ImageQuality = 90;
        public int SwitchAfter = 350;
        public int SwitchFormat = 1;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Editors
        //~~~~~~~~~~~~~~~~~~~~~

        public List<Software> ImageSoftwareList = new List<Software>();
        public Software ImageSoftwareActive = new Software();
        public bool ImageSoftwareEnabled = false;
        public Software TextEditorActive = new Software();
        public List<Software> TextEditors = new List<Software>();
        public bool TextEditorEnabled = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  FTP
        //~~~~~~~~~~~~~~~~~~~~~

        public List<FTPAccount> FTPAccountList = new List<FTPAccount>();
        public int FTPselected = -1;
        public bool FTPCreateThumbnail = false;
        public bool AutoSwitchFTP = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  HTTP
        //~~~~~~~~~~~~~~~~~~~~~

        // Image Uploaders
        public UploadMode UploadMode = UploadMode.API;
        public decimal ErrorRetryCount = 3;
        public bool ImageUploadRetry = true;
        public bool AddFailedScreenshot = false;
        public string ImageShackRegistrationCode { get; set; }
        public string TinyPicShuk { get; set; }
        public string TinyPicUserName { get; set; }
        public string TinyPicPassword { get; set; }
        public bool RememberTinyPicUserPass { get; set; }

        // Custom Image Uploaders
        public List<ImageHostingService> ImageUploadersList = null;
        public int ImageUploaderSelected = 0;

        // Language Translator
        public string FromLanguage = "auto";
        public string ToLanguage = "en";
        public bool ClipboardTranslate = false;

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

        // General
        public bool OpenMainWindow = false;
        public bool ShowInTaskbar = true;
        public bool CheckUpdates = true;
        public ZSS.UpdateCheckerLib.UpdateCheckType UpdateCheckType = ZSS.UpdateCheckerLib.UpdateCheckType.SETUP;
        public bool CheckExperimental = false;

        // Paths
        public string ImagesDir = "";
        public bool DeleteLocal = false;
        public string CacheDir = "";
        public decimal ScreenshotCacheSize = 50;
        public string SettingsDir = "";
        public string TextDir = "";
        public string TempDir = "";

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
            else
            {
                return string.Format("{0}:{1}:{2}:{3}:{4}", ColorFormat.ARGBColor, color.A, color.R, color.G, color.B);
            }
        }

        public static Color DeserializeColor(string color)
        {
            if (!color.Contains(":")) //For old method
            {
                return Color.Black;
            }

            byte a, r, g, b;

            string[] pieces = color.Split(new char[] { ':' });

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

        public void Save()
        {
            Save(Program.XMLSettingsFile);
        }

        public void Save(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                XmlSerializer xs = new XmlSerializer(typeof(XMLSettings));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public static XMLSettings Read()
        {
            return Read(Program.XMLSettingsFile);
        }

        public static XMLSettings Read(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(XMLSettings));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return (XMLSettings)xs.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    // We dont need a MessageBox when we rename enumerations
                    // Renaming enums tend to break parts of serialization
                    Console.WriteLine(ex.ToString());
                }
            }

            return new XMLSettings();
        }

        #endregion

    }
}