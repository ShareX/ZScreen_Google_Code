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
using System.ComponentModel;
using System.Reflection;

namespace ZSS
{
    /// <summary>
    /// Top level Capture Types for ZScreen. Pictures: Images that user created, Screenshots: Images that ZScreen created
    /// </summary>
    public enum JobCategoryType
    {
        PICTURES,
        SCREENSHOTS,
        TEXT
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
        [Description("Top Left")]
        TOP_LEFT,
        [Description("Top Right")]
        TOP_RIGHT,
        [Description("Bottom Right")]
        BOTTOM_RIGHT,
        [Description("Bottom Left")]
        BOTTOM_LEFT,
        [Description("Center")]
        CENTER,
        [Description("Left")]
        LEFT,
        [Description("Top")]
        TOP,
        [Description("Right")]
        RIGHT,
        [Description("Bottom")]
        BOTTOM
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

    public static class Enums
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static string[] GetDescriptions(this Type type)
        {
            string[] descriptions = new string[Enum.GetValues(type).Length];
            int i = 0;
            foreach (int value in Enum.GetValues(type))
            {
                descriptions[i++] = ((Enum)Enum.ToObject(type, value)).GetDescription();
            }
            return descriptions;
        }
    }
}