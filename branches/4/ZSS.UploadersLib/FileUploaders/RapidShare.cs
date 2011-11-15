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

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class RapidShare : FileUploader
    {
        private const string rapidshareURL = "https://api.rapidshare.com/cgi-bin/rsapi.cgi";
        private const string rapidshareUploadURL = "https://rs{0}.rapidshare.com/cgi-bin/rsapi.cgi";

        public string Username { get; set; }
        public string Password { get; set; }

        public RapidShare(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Errors.Add("RapidShare account username or password is empty.");
                return null;
            }

            string url = NextUploadServer();

            if (string.IsNullOrEmpty(url))
            {
                Errors.Add("RapidShare next upload server URL is empty.");
                return null;
            }

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("sub", "upload");
            args.Add("login", Username);
            args.Add("password", Password);

            string response = UploadData(stream, url, fileName, "filecontent", args);

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                if (response.StartsWith("ERROR: "))
                {
                    Errors.Add(response.Substring(7));
                }
                else if (response.StartsWith("COMPLETE\n"))
                {
                    RapidShareUploadInfo info = new RapidShareUploadInfo(response);
                    result.URL = info.URL;
                }
            }

            return result;
        }

        private string NextUploadServer()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("sub", "nextuploadserver");

            string response = SendGetRequest(rapidshareURL, args);

            if (!string.IsNullOrEmpty(response))
            {
                return string.Format(rapidshareUploadURL, response);
            }

            return string.Empty;
        }

        public class RapidShareUploadInfo
        {
            public string FileID; // The file ID (you might get an already existing file ID if the identical file already exists in your account AND in the same folder.)
            public string FileName;
            public string FileSize; // Received size in bytes.
            public string MD5; // Lower case MD5 hex of the sent data.
            public string URL;

            public RapidShareUploadInfo(string response)
            {
                response = response.Substring(9).Trim('\n');

                string[] split = response.Split(',');

                if (split.Length > 3)
                {
                    FileID = split[0];
                    FileName = split[1];
                    FileSize = split[2];
                    MD5 = split[3];
                    URL = string.Format("https://rapidshare.com/files/{0}/{1}", FileID, FileName);
                }
            }
        }
    }
}