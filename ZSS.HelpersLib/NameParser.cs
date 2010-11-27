#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2010 ZScreen Developers

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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HelpersLib
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
        [Description("ZScreen version")]
        ver,
        [Description("Product information")]
        app,
        [Description("New line")]
        n
    }

    public enum NameParserType
    {
        EntireScreen,
        ActiveWindow,
        Watermark,
        SaveFolder,
        Text
    }

    public class NameParserInfo : IDisposable
    {
        public string Pattern { get; set; }

        public NameParserType Type { get; set; }
        public int AutoIncrement { get; set; }
        public string ProductName { get; set; }
        public bool IsFolderPath { get; set; }
        public bool IsPreview { get; set; }
        public string Host { get; set; }
        public Image Picture { get; set; }
        public DateTime CustomDate { get; set; }
        public int MaxNameLength { get; set; }

        public NameParserInfo(NameParserType nameType, string text)
        {
            ProductName = Application.ProductName;
            Type = nameType;
            Pattern = text;
        }

        public void Dispose()
        {
            if (Picture != null)
            {
                Picture.Dispose();
            }
        }
    }

    public static class NameParser
    {
        public const string Prefix = "%";

        public static string Convert(string pattern)
        {
            return Convert(new NameParserInfo(NameParserType.Text, pattern));
        }

        public static string Convert(NameParserInfo nameParser)
        {
            if (string.IsNullOrEmpty(nameParser.Pattern)) return string.Empty;

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

            #endregion width, height

            #region t

            if (nameParser.Type == NameParserType.ActiveWindow || nameParser.Type == NameParserType.Watermark)
            {
                string activeWindow = HelpersNativeMethods.GetWindowLabel();
                if (string.IsNullOrEmpty(activeWindow))
                {
                    activeWindow = Application.ProductName;
                }
                sb = sb.Replace(ToString(ReplacementVariables.t), activeWindow);
            }
            else
            {
                sb = sb.Replace(ToString(ReplacementVariables.t), string.Empty);
            }

            #endregion t

            #region FTP

            if (!string.IsNullOrEmpty(nameParser.Host))
            {
                sb = sb.Replace("%host", nameParser.Host);
            }

            #endregion FTP

            #region y, mo, mon, mon2, d

            DateTime dt = FastDateTime.Now;

            if (nameParser.CustomDate != DateTime.MinValue)
            {
                dt = nameParser.CustomDate;
            }

            sb = sb.Replace(ToString(ReplacementVariables.mon2), CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace(ToString(ReplacementVariables.mon), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace(ToString(ReplacementVariables.y), dt.Year.ToString())
                .Replace(ToString(ReplacementVariables.mo), AddZeroes(dt.Month))
                .Replace(ToString(ReplacementVariables.d), AddZeroes(dt.Day));

            #endregion y, mo, mon, mon2, d

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
                        nameParser.AutoIncrement++;
                    }

                    sb = sb.Replace(ToString(ReplacementVariables.i), AddZeroes(nameParser.AutoIncrement, 4));
                }
            }

            #endregion h, mi, s, w, w2, pm, i

            sb = sb.Replace(ToString(ReplacementVariables.ver), Application.ProductVersion);
            sb = sb.Replace(ToString(ReplacementVariables.app), nameParser.ProductName);

            if (nameParser.Type == NameParserType.Watermark)
            {
                sb = sb.Replace(ToString(ReplacementVariables.n), "\n");
            }

            if (nameParser.Type != NameParserType.Watermark)
            {
                sb = Normalize(new NormalizeOptions(sb)
                {
                    ConvertSpace = nameParser.Type != NameParserType.SaveFolder,
                    IsFolderPath = nameParser.IsFolderPath
                });
            }

            string result = sb.ToString();

            if (nameParser.MaxNameLength > 0 && (nameParser.Type == NameParserType.ActiveWindow ||
                nameParser.Type == NameParserType.EntireScreen) && result.Length > nameParser.MaxNameLength)
            {
                result = result.Substring(0, nameParser.MaxNameLength);
            }

            return result;
        }

        private static string AddZeroes(int number)
        {
            return AddZeroes(number, 2);
        }

        private static string AddZeroes(int number, int digits)
        {
            return number.ToString("d" + digits);
        }

        public class NormalizeOptions
        {
            public StringBuilder MyStringBuilder { get; private set; }
            public bool IsFolderPath { get; set; }
            public bool ConvertSpace { get; set; }

            public NormalizeOptions(StringBuilder sb)
            {
                this.MyStringBuilder = sb;
            }
        }

        public static bool IsCharValid(char c)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890()-._!";

            foreach (char c2 in chars)
            {
                if (c == c2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///    Normalize the entire thing, allow only characters and digits,
        ///    spaces become underscores, prevents possible problems
        /// </summary>
        public static StringBuilder Normalize(NormalizeOptions options)
        {
            string fName = options.MyStringBuilder.ToString();

            StringBuilder sbName = new StringBuilder();

            foreach (char c in fName)
            {
                // @ is for HttpHomePath we use in FTP Account
                if (IsCharValid(c) || options.IsFolderPath && (c == Path.DirectorySeparatorChar || c == '/' || c == '@'))
                {
                    sbName.Append(c);
                }
            }

            fName = sbName.ToString();

            while (fName.StartsWith("."))
            {
                fName = fName.Remove(0, 1);
            }

            while (fName.IndexOf("__") != -1)
            {
                fName = fName.Replace("__", "_");
            }

            return new StringBuilder(fName);
        }

        private static string ToString(ReplacementVariables replacement)
        {
            return Prefix + replacement.ToString();
        }
    }
}