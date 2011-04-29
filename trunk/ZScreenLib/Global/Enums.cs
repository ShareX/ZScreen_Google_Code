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

using System.ComponentModel;

namespace ZScreenLib
{
    public enum Times
    {
        Milliseconds, Seconds, Minutes, Hours
    }

    /// <summary>
    /// Top level Capture Types for ZScreen. Pictures: Images that user created, Screenshots: Images that ZScreen created
    /// </summary>
    public enum JobLevel1
    {
        Binary, // important to have this listed first as this must be the default unless otherwise specified
        Images,
        Text
    }

    public enum CaptureType
    {
        CROP,
        SELECTED_WINDOW
    }

    public enum RegionStyles
    {
        [Description("No Transparency")]
        NO_TRANSPARENCY,
        [Description("Region Transparent")]
        REGION_TRANSPARENT,
        [Description("Region Brightness")]
        REGION_BRIGHTNESS,
        [Description("Background Region Transparent")]
        BACKGROUND_REGION_TRANSPARENT,
        [Description("Background Region Brightness")]
        BACKGROUND_REGION_BRIGHTNESS,
        [Description("Background Region Grayscale")]
        BACKGROUND_REGION_GRAYSCALE
    }

    public enum HistoryListFormat
    {
        [Description("FileName")]
        NAME,
        [Description("Time - FileName")]
        TIME_NAME,
        [Description("Date - Time - FileName")]
        DATE_TIME_NAME,
        [Description("Date - FileName")]
        DATE_NAME
    }

    public enum WatermarkType
    {
        [Description("None")]
        NONE,
        [Description("Text")]
        TEXT,
        [Description("Image")]
        IMAGE
    }

    public enum WatermarkPositionType
    {
        [Description("Top - Left")]
        TOP_LEFT,
        [Description("Top - Center")]
        TOP,
        [Description("Top - Right")]
        TOP_RIGHT,
        [Description("Center - Left")]
        LEFT,
        [Description("Centered")]
        CENTER,
        [Description("Center - Right")]
        RIGHT,
        [Description("Bottom - Left")]
        BOTTOM_LEFT,
        [Description("Bottom - Center")]
        BOTTOM,
        [Description("Bottom - Right")]
        BOTTOM_RIGHT
    }

    public enum AutoScreenshotterJobs
    {
        [Description("Entire Screen")]
        TAKE_SCREENSHOT_SCREEN,
        [Description("Active Window")]
        TAKE_SCREENSHOT_WINDOW_ACTIVE,
        [Description("Last Crop Shot")]
        TAKE_SCREENSHOT_LAST_CROPPED
    }

    public enum UploadTextType
    {
        UploadText,
        UploadTextFromClipboard,
        UploadTextFromFile
    }

    public enum ImageSizeType
    {
        DEFAULT, FIXED, RATIO
    }

    public enum WindowButtonAction
    {
        [Description("Minimize to Tray")]
        MinimizeToTray,
        [Description("Minimize to Taskbar")]
        MinimizeToTaskbar,
        [Description("Exit Application")]
        CloseApplication,
        [Description("Do Nothing")]
        Nothing
    }
}