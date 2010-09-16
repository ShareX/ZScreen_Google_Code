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

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using UploadersLib.Helpers;

namespace UploadersLib.TextUploaders
{
    [Serializable]
    public sealed class PastebinUploader : TextUploader
    {
        public static readonly string Hostname = TextDestType.PASTEBIN.GetDescription();

        public override object Settings
        {
            get
            {
                return HostSettings;
            }
            set
            {
                HostSettings = (PastebinSettings)value;
            }
        }

        public PastebinSettings HostSettings = new PastebinSettings();

        public PastebinUploader()
        {
            HostSettings.URL = "http://pastebin.com/api_public.php";
        }

        public PastebinUploader(string url)
        {
            HostSettings.URL = url;
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

                // this is simply the text that you paste on pastebin
                arguments.Add("paste_code", text.LocalString);
                // for adding a title or name to your paste
                arguments.Add("paste_name", HostSettings.Author);
                // for sending confirmation email with paste link
                arguments.Add("paste_email", HostSettings.Email);
                // for using a certain subdomain
                arguments.Add("paste_subdomain", HostSettings.Subdomain);
                // for making it public (0) or private (1)
                arguments.Add("paste_private", HostSettings.IsPublic ? "0" : "1");
                // for adding expiration date. N = Never, 10M = 10 Minutes, 1H = 1 Hour, 1D = 1 Day, 1M = 1 Month
                arguments.Add("paste_expire_date", HostSettings.ExpireTime);
                // for adding syntax highlighting
                arguments.Add("paste_format ", HostSettings.TextFormat);

                string response = GetResponse(HostSettings.URL, arguments);

                if (response.StartsWith("ERROR"))
                {
                    Errors.Add(response);
                }
                else
                {
                    return response;
                }
            }

            return string.Empty;
        }

        public List<TextFormat> DownloadTextFormats()
        {
            List<TextFormat> textFormats = new List<TextFormat>();
            try
            {
                WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };
                string source = webClient.DownloadString(HostSettings.URL);
                Match match = Regex.Match(source, "-</option>(.+?)</select>");
                if (match.Success)
                {
                    MatchCollection matches = Regex.Matches(match.Groups[1].Value, "value=\"(.+?)\">(.+?)</");
                    foreach (Match m in matches)
                    {
                        if (m.Success)
                        {
                            textFormats.Add(new TextFormat { Value = m.Groups[1].Value, Name = m.Groups[2].Value });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return textFormats;
        }

        public string DownloadTextContent(string url)
        {
            try
            {
                WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };
                string source = webClient.DownloadString(url);
                Match match = Regex.Match(source, "<textarea.+?>(.*?)</textarea>", RegexOptions.Singleline);
                if (match.Success)
                {
                    return HttpUtility.HtmlDecode(match.Groups[1].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }

        public struct TextFormat
        {
            public string Value;
            public string Name;
        }

        [Serializable]
        public class PastebinSettings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }
            /// <summary>format</summary>
            public string TextFormat { get; set; }
            /// <summary>poster</summary>
            public string Author { get; set; }
            /// <summary>expiry</summary>
            public string ExpireTime { get; set; }
            public bool IsPublic { get; set; }
            public string Subdomain { get; set; }
            public string Email { get; set; }

            public PastebinSettings()
            {
                Name = Hostname;
                TextFormat = "text";
                ExpireTime = "N";
            }
        }
    }
}