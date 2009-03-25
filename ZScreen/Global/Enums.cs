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
using System.Linq;
using System.Text;
using System.ComponentModel;

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

    public enum WatermarkPositionType
    {
        [Description("Top Left")]
        TOP_LEFT,
        [Description("Top Right")]
        TOP_RIGHT,
        [Description("Bottom Right")]
        BOTTOM_RIGHT,
        [Description("Bottom Left")]
        BOTTOM_LEFT
    }

    public static class WatermarkPositionTypeExtensions
    {
        public static string ToDescriptionString(this WatermarkPositionType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    public static class JobsTypeExtensions
    {
        public static string ToDescriptionString(this ZSS.Tasks.MainAppTask.Jobs val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}