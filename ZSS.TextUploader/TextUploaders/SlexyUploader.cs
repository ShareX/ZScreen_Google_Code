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

using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using ZSS.TextUploadersLib.Helpers;
using System.Xml.Serialization;
using System.ComponentModel;

namespace ZSS.TextUploadersLib
{
    [Serializable]
    public sealed class SlexyUploader : TextUploader
    {
        public const string Hostname = "slexy.org";

        public override object Settings
        {
            get
            {
                return HostSettings;
            }
            set
            {
                HostSettings = (SlexySettings)value;
            }
        }

        public SlexySettings HostSettings = new SlexySettings();

        public SlexyUploader()
        {
            HostSettings.URL = "http://slexy.org/index.php/submit";
        }

        public override string ToString()
        {
            return HostSettings.Name;
        }

        public override string TesterString
        {
            get { return "Testing " + Hostname; }
        }

        public override string UploadText(TextInfo text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("raw_paste", HttpUtility.UrlEncode(text.LocalString));
                arguments.Add("author", HostSettings.Author);
                arguments.Add("comment", "");
                arguments.Add("desc", HostSettings.Description);
                arguments.Add("expire", HostSettings.Expiration);
                arguments.Add("language", HostSettings.TextFormat);
                arguments.Add("linenumbers", HostSettings.LineNumbers ? "1" : "0");
                arguments.Add("permissions", HostSettings.Visibility == Privacy.Private ? "1" : "0");
                arguments.Add("submit", "Submit Paste");
                arguments.Add("tabbing", "true");
                arguments.Add("tabtype", "real");

                return GetResponse(HostSettings.URL, arguments);
            }

            return "";
        }

        [Serializable]
        public class SlexySettings : TextUploaderSettings 
        {
            public override string Name { get; set; }
            public override string URL { get; set; }
            /// <summary>language</summary>
            public string TextFormat { get; set; }
            /// <summary>author</summary>
            public string Author { get; set; }
            /// <summary>permissions</summary>
            public Privacy Visibility { get; set; }
            /// <summary>desc</summary>
            public string Description { get; set; }
            /// <summary>linenumbers</summary>
            public bool LineNumbers { get; set; }
            /// <summary>expire</summary>
            [Description("Expiration time with seconds. Example: 0 = Forever, 60 = 1 minutes, 3600 = 1 hour, 2592000 = 1 month")]
            public string Expiration { get; set; }

            public SlexySettings()
            {
                Name = Hostname;
                TextFormat = "text";
                Author = "";
                Visibility = Privacy.Private;
                Description = "";
                LineNumbers = true;
                Expiration = "2592000";
            }
        }
    }
}