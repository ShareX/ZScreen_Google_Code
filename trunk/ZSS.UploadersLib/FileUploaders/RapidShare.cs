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

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public class RapidShareOptions
    {
        public RapidShareAcctType AccountType = RapidShareAcctType.Free;
        public string PremiumUsername { get; set; }
        public string CollectorsID { get; set; }
        public string Password { get; set; }
        public bool CheckFileSize, CheckFileMD5;
    }

    public sealed class RapidShare : FileUploader
    {
        private string rapidshareURL = "http://api.rapidshare.com/cgi-bin/rsapi.cgi";
        private RapidShareOptions Options { get; set; }

        public override string Name
        {
            get { return "RapidShare"; }
        }

        public RapidShare()
        {
            Options = new RapidShareOptions();
        }

        public RapidShare(RapidShareOptions options)
        {
            Options = options;
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

            args.Add("rsapi_v1", "1");

            if (Options.AccountType == RapidShareAcctType.Collectors && !string.IsNullOrEmpty(Options.CollectorsID) && !string.IsNullOrEmpty(Options.Password))
            {
                args.Add("freeaccountid", Options.CollectorsID);
                args.Add("password", Options.Password);
            }
            else if (Options.AccountType == RapidShareAcctType.Premium && !string.IsNullOrEmpty(Options.PremiumUsername) && !string.IsNullOrEmpty(Options.Password))
            {
                args.Add("login", Options.PremiumUsername);
                args.Add("password", Options.Password);
            }

            string response = UploadData(stream, fileName, url, "filecontent", args);

            if (!string.IsNullOrEmpty(response))
            {
                UploadInfo info = new UploadInfo(response);
                UploadResult ur = new UploadResult
                {
                    URL = info.URL,
                    DeletionURL = info.KillCodeURL,
                    Source = response
                };

                return ur;
            }

            return null;
        }

        private string NextUploadServer()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("sub", "nextuploadserver_v1");

            string response = GetResponseString(rapidshareURL, args);

            if (!string.IsNullOrEmpty(response))
            {
                return string.Format("http://rs{0}l3.rapidshare.com/cgi-bin/upload.cgi", response);
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