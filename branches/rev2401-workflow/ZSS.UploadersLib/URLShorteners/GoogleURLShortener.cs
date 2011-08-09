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

using Newtonsoft.Json;

namespace UploadersLib.URLShorteners
{
    public class GoogleURLShortener : URLShortener
    {
        private const string APIURL = "https://www.googleapis.com/urlshortener/v1/url";

        private string APIKey;

        public GoogleURLShortener(string key)
        {
            APIKey = key;
        }

        public override string ShortenURL(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                string query = string.Format("{0}?key={1}", APIURL, APIKey);
                string json = string.Format("{{\"longUrl\":\"{0}\"}}", url);

                string response = SendPostRequestJSON(query, json);

                if (!string.IsNullOrEmpty(response))
                {
                    GoogleURLShortenerResponse result = JsonConvert.DeserializeObject<GoogleURLShortenerResponse>(response);
                    if (result != null) return result.id;
                }
            }

            return null;
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