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
using ZSS.TextUploaders.Helpers;
using System.Xml.Serialization;

namespace ZSS.TextUploaders
{
    [Serializable]
    public sealed class PastebinCa : TextUploader
    {
        public const string Hostname = "pastebin.ca";
        public const string APIKey = "qrsjom2g8qDYcy8P+GeNnxSZowY89gKr";

        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (PastebinCaSettings)value;
            }
        }

        public PastebinCaSettings HostSettings = new PastebinCaSettings();

        public PastebinCa()
        {
            HostSettings.URL = "http://pastebin.ca/quiet-paste.php";
        }

        public override string Name
        {
            get { return Hostname; }
        }

        public override string UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("api", APIKey);
                arguments.Add("content", HttpUtility.UrlEncode(text));
                arguments.Add("description", HostSettings.Description);
                arguments.Add("expiry", HostSettings.ExpireTime);
                arguments.Add("name", HostSettings.Name);
                arguments.Add("s", "Submit Post");
                arguments.Add("type", HostSettings.TextFormat);

                return GetResponse(HostSettings.URL, arguments);
            }

            return "";
        }

        [Serializable]
        public class PastebinCaSettings
        {
            public string URL { get; set; }
            public string Description { get; set; }
            public string ExpireTime { get; set; }
            public string TextFormat { get; set; }
            public string Name { get; set; }

            public PastebinCaSettings()
            {
                ExpireTime = "1 month";
                TextFormat = "1";
            }
        }
    }
}