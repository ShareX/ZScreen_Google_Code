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

#region Source code: Greenshot (GPL)
/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/
#endregion

using System;
using Greenshot.Configuration;
using System.Collections;

namespace Greenshot.Helpers
{
	public class FilenameHelper
	{
		private FilenameHelper()
		{
		}
		
		// a map of all placeholders and their respective regexes
		public static Hashtable Placeholders = new Hashtable();
		static FilenameHelper() {
			Placeholders.Add("%YYYY%",@"\d{4}"); // year
			Placeholders.Add("%MM%",@"\d{2}"); // month
			Placeholders.Add("%DD%",@"\d{2}"); // day
			Placeholders.Add("%hh%",@"\d{2}"); // hour
			Placeholders.Add("%mm%",@"\d{2}"); // minute
			Placeholders.Add("%ss%",@"\d{2}"); // second
			Placeholders.Add("%NUM%",@"\d{6}"); // second
		}
		
		public static string GetFilenameWithoutExtensionFromPattern(string pattern) {
			return FillPattern(pattern);
		}

		public static string GetFilenameFromPattern(string pattern, string imageFormat) {
			string ext = imageFormat.ToLower();
			if(ext.Equals("jpeg")) ext = "jpg";
			return FillPattern(pattern) + "." + ext;
		}
		
		private static string FillPattern(string pattern) {
			DateTime d = DateTime.Now;
			pattern = pattern.Replace("%YYYY%",d.Year.ToString());
			pattern = pattern.Replace("%MM%", zeroPad(d.Month.ToString(), 2));
			pattern = pattern.Replace("%DD%", zeroPad(d.Day.ToString(), 2));
			pattern = pattern.Replace("%hh%", zeroPad(d.Hour.ToString(), 2));
			pattern = pattern.Replace("%mm%", zeroPad(d.Minute.ToString(), 2));
			pattern = pattern.Replace("%ss%", zeroPad(d.Second.ToString(), 2));
			if(pattern.Contains("%NUM%")) {
			   	AppConfig conf = AppConfig.GetInstance();
			   	int num = conf.Output_File_IncrementingNumber++;
			   	conf.Save();
			   	pattern = pattern.Replace("%NUM%", zeroPad(num.ToString(), 6));
            }
			return pattern;
		}
		
		private static string zeroPad(string input, int chars) {
			while(input.Length < chars) input = "0" + input;
			return input;
		}
	}
}
