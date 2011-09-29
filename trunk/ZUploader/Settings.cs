#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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

using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using ScreenCapture;
using UploadersLib.HelperClasses;

namespace ZUploader
{
    public class Settings
    {
        // Main Form

        public int SelectedImageUploaderDestination = 0;
        public int SelectedTextUploaderDestination = 0;
        public int SelectedFileUploaderDestination = 0;
        public int SelectedURLShortenerDestination = 0;
        public ScreenshotDestination CaptureOutput = ScreenshotDestination.Upload;
        public bool ShowClipboardContentViewer = true;

        #region Settings Form

        // General
        public bool ClipboardAutoCopy = true;
        public bool AutoPlaySound = true;
        public bool URLShortenAfterUpload = false;
        public bool ShowTray = false;

        // Hotkeys
        public HotkeySetting HotkeyClipboardUpload = new HotkeySetting(Keys.Control | Keys.PageUp);
        public HotkeySetting HotkeyFileUpload = new HotkeySetting(Keys.Shift | Keys.PageUp);
        public HotkeySetting HotkeyPrintScreen = new HotkeySetting(Keys.PrintScreen);
        public HotkeySetting HotkeyActiveWindow = new HotkeySetting(Keys.Alt | Keys.PrintScreen);
        public HotkeySetting HotkeyRectangleRegion = new HotkeySetting(Keys.Control | Keys.PrintScreen);
        public HotkeySetting HotkeyRoundedRectangleRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.R);
        public HotkeySetting HotkeyEllipseRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.E);
        public HotkeySetting HotkeyTriangleRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.T);
        public HotkeySetting HotkeyDiamondRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.D);
        public HotkeySetting HotkeyPolygonRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.P);
        public HotkeySetting HotkeyFreeHandRegion = new HotkeySetting(Keys.Shift | Keys.PrintScreen);

        // Upload
        public bool UseCustomUploadersConfigPath = false;
        public string CustomUploadersConfigPath = string.Empty;
        public int UploadLimit = 5;
        public int BufferSizePower = 3;

        // Image
        public EImageFormat ImageFormat = EImageFormat.PNG;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        // Clipboard upload
        // Test: %y %mo %mon %mon2 %d %h %mi %s %ms %w %w2 %pm %rn %ra %width %height %app %ver
        public string NameFormatPattern = "%y-%mo-%d_%h-%mi-%s";

        // Capture
        public SurfaceOptions SurfaceOptions = new SurfaceOptions();

        // History
        public bool SaveHistory = true;
        public bool UseCustomHistoryPath = false;
        public string CustomHistoryPath = string.Empty;
        public int HistoryMaxItemCount = -1;

        // Proxy
        public ProxyInfo ProxySettings = new ProxyInfo();

        #endregion Settings Form

        #region I/O Methods

        public bool Save()
        {
            return SettingsHelper.Save(this, Program.SettingsFilePath, SerializationType.Xml);
        }

        public static Settings Load()
        {
            return SettingsHelper.Load<Settings>(Program.SettingsFilePath, SerializationType.Xml);
        }

        #endregion I/O Methods
    }
}