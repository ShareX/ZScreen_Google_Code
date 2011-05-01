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
using System.Collections.Generic;
using System.IO;
using HelpersLib;
using Newtonsoft.Json;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class Dropbox : FileUploader
    {
        public override string Name
        {
            get { return "Dropbox"; }
        }

        public OAuthInfo AuthInfo { get; set; }
        public string UploadPath { get; set; }
        public string UserID { get; set; }

        private const string APIVersion = "0";
        private const string URLToken = "https://api.dropbox.com/" + APIVersion + "/token";
        private const string URLAccountInfo = "https://api.dropbox.com/" + APIVersion + "/account/info";
        private const string URLFiles = "https://api-content.dropbox.com/" + APIVersion + "/files/dropbox";
        private const string URLDownload = "http://dl.dropbox.com/u";

        public Dropbox(OAuthInfo oauth)
        {
            AuthInfo = oauth;
        }

        public Dropbox(OAuthInfo oauth, string uploadPath, string userID)
            : this(oauth)
        {
            UploadPath = uploadPath;
            UserID = userID;
        }

        public DropboxUserLogin Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("email", email);
                args.Add("password", password);

                string url = OAuthManager.GenerateQuery(URLToken, args, HttpMethod.GET, AuthInfo);

                string response = SendGetRequest(url);

                if (!string.IsNullOrEmpty(response))
                {
                    DropboxUserLogin login = JsonConvert.DeserializeObject<DropboxUserLogin>(response);

                    if (login != null)
                    {
                        AuthInfo.UserToken = login.Token;
                        AuthInfo.UserSecret = login.Secret;
                        return login;
                    }
                }
            }

            return null;
        }

        public DropboxAccountInfo GetAccountInfo()
        {
            if (!string.IsNullOrEmpty(AuthInfo.UserToken) && !string.IsNullOrEmpty(AuthInfo.UserSecret))
            {
                string url = OAuthManager.GenerateQuery(URLAccountInfo, null, HttpMethod.GET, AuthInfo);

                string response = SendGetRequest(url);

                if (!string.IsNullOrEmpty(response))
                {
                    DropboxAccountInfo account = JsonConvert.DeserializeObject<DropboxAccountInfo>(response);

                    if (account != null)
                    {
                        UserID = account.Uid.ToString();
                        return account;
                    }
                }
            }

            return null;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            if (string.IsNullOrEmpty(AuthInfo.UserToken) || string.IsNullOrEmpty(AuthInfo.UserSecret))
            {
                throw new Exception("UserToken or UserSecret is empty. Login is required.");
            }

            string url = Helpers.CombineURL(URLFiles, UploadPath);

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("file", fileName);

            string query = OAuthManager.GenerateQuery(url, args, HttpMethod.POST, AuthInfo);

            string response = UploadData(stream, query, fileName);

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                result.URL = GetDropboxURL(UserID, UploadPath, fileName);
            }

            return result;
        }

        public static string GetDropboxURL(string userID, string uploadPath, string fileName)
        {
            if (!string.IsNullOrEmpty(uploadPath))
            {
                if (uploadPath.StartsWith("Public/", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Helpers.CombineURL(URLDownload, userID, uploadPath.Substring(7), fileName);
                }
            }

            return "Upload path is private. Use \"Public\" folder to get public URL.";
        }

        public static string TidyUploadPath(string uploadPath)
        {
            if (!string.IsNullOrEmpty(uploadPath))
            {
                return uploadPath.Trim().Replace('\\', '/').Trim('/') + "/";
            }

            return string.Empty;
        }
    }

    public class DropboxUserLogin
    {
        public string Token { get; set; }
        public string Secret { get; set; }
    }

    public class DropboxAccountInfo
    {
        public string Referral_link { get; set; }
        public string Display_name { get; set; }
        public long Uid { get; set; }
        public string Country { get; set; }
        public DropboxQuotaInfo Quota_info { get; set; }
        public string Email { get; set; }
    }

    public class DropboxQuotaInfo
    {
        public long Shared { get; set; }
        public long Quota { get; set; }
        public long Normal { get; set; }
    }
}