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
using System.Xml;
using UploadersLib.Helpers;

namespace UploadersLib.URLShorteners
{
    [Serializable]
    public sealed class OwlyUploader : TextUploader
    {
        public static readonly string Hostname = UrlShortenerType.OWLY.GetDescription();
        private const string APILogin = "mcored";
        private const string APIKey = "R_55cef8c7f08a07d2ecd4323084610161";

        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (OwlyUploaderSettings)value;
            }
        }

        public OwlyUploaderSettings HostSettings = new OwlyUploaderSettings();

        public OwlyUploader()
        {
            HostSettings.URL = "http://ow.ly/url/shorten-url";
        }

        public override string ToString()
        {
            return HostSettings.Name;
        }

        // http://api.bit.ly/shorten?version=2.0.1&longUrl=http://code.google.com/p/zscreen&login=mcored&apiKey=R_55cef8c7f08a07d2ecd4323084610161"

        public override string UploadText(TextInfo text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("version", "2.0.1");
                arguments.Add("longUrl", text.LocalString);
                arguments.Add("login", APILogin);
                arguments.Add("apiKey", APIKey);
                arguments.Add("format", "xml");

                string result = GetResponseString(HostSettings.URL, arguments);

                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(result);
                XmlNode xnode = xdoc.SelectSingleNode("bitly/results/nodeKeyVal/shortUrl");
                if (xnode != null)
                {
                    return xnode.InnerText;
                }
            }

            return string.Empty;
        }

        [Serializable]
        public class OwlyUploaderSettings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }

            public OwlyUploaderSettings()
            {
                Name = Hostname;
            }
        }
    }
}