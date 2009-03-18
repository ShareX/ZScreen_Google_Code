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

namespace ZSS.Helpers
{
    public static class NameParser
    {
        public static string[] replacementVars = { "%t", "%mo", "%d", "%y", "%h", "%mi", "%s", "%i", "%pm" };
        public static string[] replacementDescriptions = { "Title of active window", "Gets the current month", "Gets the current day",
                                                             "Gets the current year", "Gets the current hour", "Gets the current minute",
                                                             "Gets the current second", "Auto increment", "Gets AM/PM" };
        public static string prefix = "%";

        public enum NameType
        {
            EntireScreen,
            ActiveWindow,
            Watermark
        }

        public static string Convert(NameType nameType)
        {
            switch (nameType)
            {
                case NameType.ActiveWindow:
                    return Convert(Program.conf.activeWindow, nameType);
                case NameType.EntireScreen:
                    return Convert(Program.conf.entireScreen, nameType);
                case NameType.Watermark:
                    return Convert(Program.conf.WatermarkText, nameType);
            }
            return "";
        }

        public static string Convert(string pattern, NameType nameType)
        {
            StringBuilder sb = new StringBuilder(pattern);

            //auto-increment
            if (sb.ToString().Contains("%i"))
            {
                if (Program.conf.awincrement != 0)
                    Program.conf.awincrement += 1;
                else
                    Program.conf.awincrement = 1;
            }

            DateTime dt = DateTime.Now;

            if (sb.ToString().Contains(replacementVars[8]))
            {
                sb = sb.Replace(replacementVars[4], dt.Hour == 0 ? "12" :
                    ((dt.Hour > 12 ? AddZeroes(dt.Hour - 12) : AddZeroes(dt.Hour))));
                //!Properties.Resources.PMvisible ? dt.Hour.ToString()
            }
            else
            {
                sb = sb.Replace(replacementVars[4], AddZeroes(dt.Hour));
            }

            if (nameType == NameType.ActiveWindow)
            {
                string activeWindow = User32.GetWindowLabel();
                if (string.IsNullOrEmpty(activeWindow))
                {
                    activeWindow = "ZScreen";
                }
                sb = sb.Replace(replacementVars[0], activeWindow);
            }
            else
            {
                sb = sb.Replace(replacementVars[0], "");
            }

            sb = sb.Replace(replacementVars[1], AddZeroes(dt.Month))
                .Replace(replacementVars[2], AddZeroes(dt.Day))
                .Replace(replacementVars[3], dt.Year.ToString())
                .Replace(replacementVars[5], AddZeroes(dt.Minute))
                .Replace(replacementVars[6], AddZeroes(dt.Second))
                .Replace(replacementVars[7], AddZeroes(Program.conf.awincrement, 4))
                .Replace(replacementVars[8], (dt.Hour >= 12 ? "PM" : "AM"));

            if (nameType == NameType.Watermark)
            {
                sb = sb.Replace("\\n", "\n");
            }

            //normalize the entire thing, allow only characters and digits
            //spaces become underscores, prevents possible problems
            if (nameType != NameType.Watermark) sb = Normalize(sb);

            return sb.ToString();
        }

        public static string AddZeroes(int number)
        {
            return AddZeroes(number, 2);
        }

        public static string AddZeroes(int number, int digits)
        {
            return number.ToString("d" + digits);
        }

        public static StringBuilder Normalize(StringBuilder sb)
        {
            StringBuilder temp = new StringBuilder("");

            foreach (char c in sb.ToString())
            {
                if (char.IsLetterOrDigit(c) || c == '.' || c == '-' || c == '_')
                    temp.Append(c);
                if (c == ' ')
                    temp.Append('_');
            }

            return temp;
        }
    }
}