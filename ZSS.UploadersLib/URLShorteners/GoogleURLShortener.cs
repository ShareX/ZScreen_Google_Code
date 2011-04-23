#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.URLShorteners
{
    public class GoogleURLShortener : TextUploader
    {
        public static readonly string Hostname = UrlShortenerType.Google.GetDescription();

        private string APIKey;

        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (GoogleURLShortenerSettings)value;
            }
        }

        public GoogleURLShortenerSettings HostSettings = new GoogleURLShortenerSettings();

        public GoogleURLShortener() { }

        public GoogleURLShortener(string key)
        {
            APIKey = key;
            HostSettings.URL = "https://www.googleapis.com/urlshortener/v1/url";
        }

        public override string ToString()
        {
            return HostSettings.Name;
        }

        public override string UploadText(TextInfo text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                string url = string.Format("{0}?key={1}", HostSettings.URL, APIKey);
                string json = string.Format("{{\"longUrl\":\"{0}\"}}", text.LocalString);
                GoogleURLShortenerResponse result = GetResponseJSON<GoogleURLShortenerResponse>(url, json);
                return result.id;
            }

            return null;
        }

        [Serializable]
        public class GoogleURLShortenerSettings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }

            public GoogleURLShortenerSettings()
            {
                Name = Hostname;
            }
        }
    }

    public class GoogleURLShortenerResponse
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string longUrl { get; set; }
        public string status { get; set; }
    }
}