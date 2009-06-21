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
using ZSS.TextUploader.Helpers;

namespace ZSS.TextUploader
{
    public sealed class PastebinUploader : TextUploader
    {
        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (PastebinSettings)value;
            }
        }

        public PastebinSettings HostSettings = new PastebinSettings();

        public PastebinUploader()
        {
            HostSettings.URL = CreateURL("http://pastebin.com");
        }

        public PastebinUploader(string url)
        {
            HostSettings.URL = CreateURL(url);
        }

        public override string Name
        {
            get { return "pastebin.com - " + HostSettings.URL; }
        }

        private string CreateURL(string url)
        {
            if (!url.EndsWith(".php"))
            {
                return CombineURL(url, "pastebin.php");
            }
            return url;
        }

        public override string UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("code2", HttpUtility.UrlEncode(text));
                arguments.Add("expiry", ((char)HostSettings.ExpireTime).ToString());
                arguments.Add("format", HostSettings.TextFormat);
                arguments.Add("poster", HostSettings.Name);
                //arguments.Add("parent_pid", "");
                arguments.Add("paste", "Send");

                return GetResponse(HostSettings.URL, arguments);
            }

            return "";
        }

        public class PastebinSettings
        {
            public string URL { get; set; }
            public TimeTypes ExpireTime { get; set; }
            public string TextFormat { get; set; }
            public string Name { get; set; }

            public PastebinSettings()
            {
                ExpireTime = TimeTypes.Month;
                TextFormat = "text";
            }

            public enum TimeTypes { Day = 'd', Month = 'm', Forever = 'f' }
        }
    }
}