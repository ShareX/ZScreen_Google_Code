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
        //*********************
        //* Folder Settings
        //*********************
        public string ImagesDir { get; set; }
        public string TextDir { get; set; }
        public string SettingsDir { get; set; }
        public string CacheDir { get; set; }
        public string TempDir { get; set; }

        //*********************
        //* Misc Settings
        //*********************
        public bool RunOnce = false;

        //*********************
        //* Main Settings
        //*********************
        public bool EnableThumbnail { get; set; }
        private ImageDestType mScreenshotDest = ImageDestType.IMAGESHACK;
        public ImageDestType ScreenshotDestMode { get { return mScreenshotDest; } set { mScreenshotDest = value; } }
        public decimal ScreenshotDelay = 0;
        public bool RegionRectangleInfo = true;
        public bool RegionHotkeyInfo = true;
        public bool ActiveHelp = true;
        public bool GTActiveHelp = false;
        public int CropRegionStyle = 1;
        public string CropBorderColor = SerializeColor(Color.Red);
        public decimal CropBorderSize = 1;
        public bool CompleteSound = false;
        public bool ShowCursor = false;
        public string HelpToLanguage = "en";
        public int SelectedWindowRegionStyle = 2;
        public bool SelectedWindowFront = false;
        public bool SelectedWindowRectangleInfo = true;
        public string SelectedWindowBorderColor = SerializeColor(Color.Red);
        public decimal SelectedWindowBorderSize = 2;
        public bool ShowUploadDuration = false;

        //*********************
        //* Hotkey Settings
        //*********************
        public HKcombo HKActiveWindow = new HKcombo(Keys.Alt, Keys.PrintScreen);
        public HKcombo HKSelectedWindow = new HKcombo(Keys.Shift, Keys.PrintScreen);
        public HKcombo HKCropShot = new HKcombo(Keys.Control, Keys.PrintScreen);
        public HKcombo HKLastCropShot = new HKcombo(Keys.None);
        public HKcombo HKEntireScreen = new HKcombo(Keys.PrintScreen);
        public HKcombo HKClipboardUpload = new HKcombo(Keys.None);
        public HKcombo HKDropWindow = new HKcombo(Keys.None);
        public HKcombo HKQuickOptions = new HKcombo(Keys.None);
        public HKcombo HKLanguageTranslator = new HKcombo(Keys.None);
        public HKcombo HKScreenColorPicker = new HKcombo(Keys.None);

        //*********************
        //* FTP Settings
        //*********************
        public List<FTPAccount> FTPAccountList = new List<FTPAccount>();
        public int FTPselected = 0;

        //*********************
        //* HTTP Settings
        //*********************
        public string ImageShackRegistrationCode = null;
        public string TinyPicShuk = null;
        public decimal ErrorRetryCount = 3;
        public ZSS.ImageUploader.UploadMode UploadMode = ZSS.ImageUploader.UploadMode.API;
        public bool ImageUploadRetry = true;
        public bool AutoSwitchFTP = true;
        public string FromLanguage = "auto";
        public string ToLanguage = "en";
        public bool ClipboardTranslate = false;
        public bool AddFailedScreenshot = false;

        //*********************
        //* Image Software Settings
        //*********************   
        public ImageSoftware ImageSoftwareActive { get; set; }
        public List<ZSS.ImageSoftware> ImageSoftwareList = new List<ImageSoftware>();
        public bool ISenabled = false;
        private ClipboardUriType mClipboardTextMode = ClipboardUriType.FULL;
        public ClipboardUriType ClipboardUriMode { get { return mClipboardTextMode; } set { mClipboardTextMode = value; } }

        //*********************
        //* File Settings
        //*********************
        public int awincrement = 0;
        public int esincrement = 0;
        public int csincrement = 0;
        public bool DeleteLocal = false;
        public bool ManualNaming = false;
        public string entireScreen = "SS-%y.%mo.%d-%h.%mi.%s";
        public string activeWindow = "%t-%y.%mo.%d-%h.%mi.%s";
        public int FileFormat = 0;
        public long ImageQuality = 90L;
        public int SwitchAfter = 350;
        public int SwitchFormat = 1;
        public bool ShowWatermark = false;
        public string WatermarkText = "%h:%mi";
        public XmlFont WatermarkFont = SerializeFont(new Font("Arial", 8));
        public string WatermarkFontColor = SerializeColor(Color.White);
        public decimal WatermarkFontTrans = 255;
        public decimal WatermarkOffset = 5;
        public decimal WatermarkBackTrans = 225;
        public string WatermarkGradient1 = SerializeColor(Color.FromArgb(85, 85, 85));
        public string WatermarkGradient2 = SerializeColor(Color.Black);
        public string WatermarkBorderColor = SerializeColor(Color.Black);
        public WatermarkPositionType WatermarkPositionMode = WatermarkPositionType.BOTTOM_RIGHT;
        public decimal WatermarkCornerRadius = 4;
        public string WatermarkGradientType = "Vertical";
        public Size CropGridSize = new Size(100, 100);
        public bool CropGridToggle = false;
        public bool CropShowGrids = false;

        //*********************
        //* Advanced Settings
        //*********************
        public decimal ScreenshotCacheSize = 50;
        public decimal FlashTrayCount = 1;
        public bool ShowBalloonTip = true;
        public bool CheckUpdates = true;
        public bool OpenMainWindow = false;
        public bool ShowInTaskbar = true;
        public bool BalloonTipOpenLink = false;
        public bool CaptureEntireScreenOnError = false;
        public bool CheckExperimental = false;
        public ZSS.UpdateCheckerLib.UpdateCheckType UpdateCheckType = ZSS.UpdateCheckerLib.UpdateCheckType.SETUP;

        //*********************
        //* Custom Uploaders Settings
        //*********************
        public List<ImageHostingService> ImageUploadersList = null;
        public int ImageUploaderSelected = 0;

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