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
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;

namespace ZSS
{
    public enum ReplacementVariables
    {
        [Description("Title of active window")]
        t,
        [Description("Current year")]
        y,
        [Description("Current month")]
        mo,
        [Description("Current month name (Local language)")]
        mon,
        [Description("Current month name (English)")]
        mon2,
        [Description("Current day")]
        d,
        [Description("Current hour")]
        h,
        [Description("Current minute")]
        mi,
        [Description("Current second")]
        s,
        [Description("Current week name (Local language)")]
        w,
        [Description("Current week name (English)")]
        w2,
        [Description("Auto increment")]
        i,
        [Description("Gets AM/PM")]
        pm,
        [Description("Gets image width")]
        width,
        [Description("Gets image height")]
        height,
        [Description("New line")]
        n
    }

    public enum NameParserType
    {
        EntireScreen,
        ActiveWindow,
        Watermark,
        SaveFolder
    }

    public class NameParserInfo
    {
        public string Pattern;

        private NameParserType type;
        public NameParserType Type
        {
            get { return type; }
            set
            {
                type = value;
                switch (type)
                {
                    case NameParserType.ActiveWindow:
                        Pattern = Program.conf.ActiveWindowPattern;
                        break;
                    case NameParserType.EntireScreen:
                        Pattern = Program.conf.EntireScreenPattern;
                        break;
                    case NameParserType.Watermark:
                        Pattern = Program.conf.WatermarkText;
                        break;
                    case NameParserType.SaveFolder:
                        Pattern = Program.conf.SaveFolderPattern;
                        break;
                }
            }
        }

        public bool IsPreview;
        public Image Picture;
        public DateTime CustomDate;

        public NameParserInfo(string pattern)
        {
            Pattern = pattern;
        }

        public NameParserInfo(NameParserType nameType)
        {
            Type = nameType;
        }
    }

    public static class NameParser
    {
        public const string Prefix = "%";

        public static string Convert(NameParserType nameParserType)
        {
            return Convert(new NameParserInfo(nameParserType));
        }

        public static string Convert(NameParserInfo nameParser)
        {
            if (string.IsNullOrEmpty(nameParser.Pattern)) return "";

            StringBuilder sb = new StringBuilder(nameParser.Pattern);

            #region width, height

            string width = "", height = "";

            if (nameParser.Picture != null)
            {
                width = nameParser.Picture.Width.ToString();
                height = nameParser.Picture.Height.ToString();
            }

            sb = sb.Replace(ToString(ReplacementVariables.width), width);
            sb = sb.Replace(ToString(ReplacementVariables.height), height);

            #endregion

            #region t

            if (nameParser.Type == NameParserType.ActiveWindow || nameParser.Type == NameParserType.Watermark)
            {
                string activeWindow = User32.GetWindowLabel();
                if (string.IsNullOrEmpty(activeWindow))
                {
                    activeWindow = "ZScreen";
                }
                sb = sb.Replace(ToString(ReplacementVariables.t), activeWindow);
            }
            else
            {
                sb = sb.Replace(ToString(ReplacementVariables.t), "");
            }

            #endregion

            #region y, mo, mon, mon2, d

            DateTime dt = DateTime.Now;

            if (nameParser.CustomDate != DateTime.MinValue)
            {
                dt = nameParser.CustomDate;
            }

            sb = sb.Replace(ToString(ReplacementVariables.y), dt.Year.ToString())
                .Replace(ToString(ReplacementVariables.mon2), CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace(ToString(ReplacementVariables.mon), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace(ToString(ReplacementVariables.mo), AddZeroes(dt.Month))
                .Replace(ToString(ReplacementVariables.d), AddZeroes(dt.Day));

            #endregion

            #region h, mi, s, w, w2, pm, i

            if (nameParser.Type != NameParserType.SaveFolder)
            {
                if (sb.ToString().Contains(ToString(ReplacementVariables.pm)))
                {
                    sb = sb.Replace(ToString(ReplacementVariables.h), dt.Hour == 0 ? "12" :
                        ((dt.Hour > 12 ? AddZeroes(dt.Hour - 12) : AddZeroes(dt.Hour))));
                }
                else
                {
                    sb = sb.Replace(ToString(ReplacementVariables.h), AddZeroes(dt.Hour));
                }

                sb = sb.Replace(ToString(ReplacementVariables.mi), AddZeroes(dt.Minute))
                    .Replace(ToString(ReplacementVariables.s), AddZeroes(dt.Second))
                    .Replace(ToString(ReplacementVariables.w2), CultureInfo.InvariantCulture.DateTimeFormat.GetDayName(dt.DayOfWeek))
                    .Replace(ToString(ReplacementVariables.w), CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dt.DayOfWeek))
                    .Replace(ToString(ReplacementVariables.pm), (dt.Hour >= 12 ? "PM" : "AM"));

                if (nameParser.Type != NameParserType.SaveFolder)
                {
                    if (!nameParser.IsPreview && sb.ToString().Contains("%i"))
                    {
                        Program.conf.AutoIncrement++;
                    }

                    sb = sb.Replace(ToString(ReplacementVariables.i), AddZeroes(Program.conf.AutoIncrement, 4));
                }
            }

            #endregion

            if (nameParser.Type == NameParserType.Watermark)
            {
                sb = sb.Replace(ToString(ReplacementVariables.n), "\n");
            }

            if (nameParser.Type != NameParserType.Watermark) sb = Normalize(sb);

            return sb.ToString();
        }

        private static string AddZeroes(int number)
        {
            return AddZeroes(number, 2);
        }

        private static string AddZeroes(int number, int digits)
        {
            return number.ToString("d" + digits);
        }

        /// <summary>
        ///    Normalize the entire thing, allow only characters and digits,
        ///    spaces become underscores, prevents possible problems
        /// </summary>
        public static StringBuilder Normalize(StringBuilder sb)
        {
            StringBuilder temp = new StringBuilder("");

            foreach (char c in sb.ToString())
            {
                if (char.IsLetterOrDigit(c) || c == '.' || c == '-' || c == '_') temp.Append(c);
                if (c == ' ') temp.Append('_');
            }

            return temp;
        }

        private static string ToString(ReplacementVariables replacement)
        {
            return Prefix + replacement.ToString();
        }
    }
}