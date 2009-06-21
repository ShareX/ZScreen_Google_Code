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
        public string URL { get; set; }

        public Settings HostSettings = new Settings();

        public PastebinUploader()
        {
            this.URL = CreateURL("http://pastebin.com");
        }

        public PastebinUploader(string url)
        {
            this.URL = CreateURL(url);
        }

        public override string Name
        {
            get { return "pastebin.com"; }
        }

        private string CreateURL(string url)
        {
            return CombineURL(url, "pastebin.php");
        }

        public override string UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.URL);
                    request.AllowAutoRedirect = true;
                    request.Method = "post";

                    Dictionary<string, string> arguments = new Dictionary<string, string>();
                    arguments.Add("code2", HttpUtility.UrlEncode(text));
                    arguments.Add("expiry", ((char)HostSettings.ExpireTime).ToString());
                    arguments.Add("format", HostSettings.TextFormat);
                    arguments.Add("poster", HostSettings.Name);
                    //arguments.Add("parent_pid", "");
                    //arguments.Add("paste", "Send");

                    string post = string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());
                    byte[] data = Encoding.UTF8.GetBytes(post);

                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    Stream response = request.GetRequestStream();
                    response.Write(data, 0, data.Length);
                    response.Close();

                    HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                    res.Close();

                    return res.ResponseUri.OriginalString;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return "";
        }

        public class Settings
        {
            public TimeTypes ExpireTime { get; set; }
            public string TextFormat { get; set; }
            public string Name { get; set; }

            public Settings()
            {
                ExpireTime = TimeTypes.Month;
                TextFormat = "text";
            }

            public enum TimeTypes { Day = 'd', Month = 'm', Forever = 'f' }
        }
    }
}