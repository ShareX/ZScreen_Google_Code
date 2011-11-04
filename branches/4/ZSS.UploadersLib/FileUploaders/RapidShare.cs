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
        private const string rapidshareURL = "http://api.rapidshare.com/cgi-bin/rsapi.cgi";

        public AccountType AccountType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool CheckFileSize, CheckFileMD5;

        public RapidShare(AccountType accountType = AccountType.Anonymous, string username = null, string password = null)
        {
            AccountType = accountType;
            Username = username;
            Password = password;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            string url = NextUploadServer();

            if (string.IsNullOrEmpty(url))
            {
                Errors.Add("Upload server URL is empty.");
                return null;
            }

            Dictionary<string, string> args = new Dictionary<string, string>();

            if (AccountType == AccountType.User && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                args.Add("login", Username);
                args.Add("password", Password);
            }

            string response = UploadData(stream, url, fileName, "filecontent", args);

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                UploadInfo info = new UploadInfo(response);

                result.URL = info.URL;
                result.DeletionURL = info.KillCodeURL;
                result.Source = response;
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
                return string.Format("http://rs{0}.rapidshare.com/cgi-bin/upload.cgi", response);
            }

            return string.Empty;
        }

        public class UploadInfo
        {
            public string Info;
            public string FileID;
            public string URL;
            public string KillCodeURL;
            public string KillCode;
            public string Size;
            public string MD5;
            public string Status;

            public UploadInfo(string info)
            {
                Info = info;
                FileID = GetFirstValue(info, @"/files/(\d+)/");
                URL = GetFirstValue(info, @"File1\.1=(.+?)\n");
                KillCodeURL = GetFirstValue(info, @"File1\.2=(.+?)\n");
                KillCode = GetFirstValue(info, @"File1\.2=.+?killcode=(\d+)\n");
                Size = GetFirstValue(info, @"File1\.3=(\d+?)\n");
                MD5 = GetFirstValue(info, @"File1\.4=(\w+?)\n");
                Status = GetFirstValue(info, @"File1\.5=(.+?)\n");
            }

            private string GetFirstValue(string input, string pattern)
            {
                Match regex = Regex.Match(input, pattern);
                if (regex.Groups.Count > 1)
                {
                    return regex.Groups[1].Value;
                }

                return string.Empty;
            }
        }
    }
}