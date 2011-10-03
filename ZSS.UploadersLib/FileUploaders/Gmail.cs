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
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public class Gmail : FileUploader, IOAuth
    {
        private const string URLAuthorize = "https://www.google.com/accounts/OAuthAuthorizeToken";
        private const string URL_AUTH_REQUEST_BASE = "https://mail.google.com/mail/b/";
        private EmailProtocol Protocol = EmailProtocol.Smtp;
        private string URL_OAuthRequest = URL_AUTH_REQUEST_BASE;
        private string EmailAddress = string.Empty;

        public OAuthInfo AuthInfo { get; set; }

        public Gmail(string email_address, OAuthInfo oauth)
        {
            this.URL_OAuthRequest = URL_AUTH_REQUEST_BASE + email_address + "/" + this.Protocol.ToString().ToLower();
            this.EmailAddress = email_address;
            this.AuthInfo = oauth;
        }

        public string GetAuthorizationURL()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("xoauth_requestor_id", System.Web.HttpUtility.UrlEncode(EmailAddress));
            //args.Add("scope", "https://mail.google.com");
            //args.Add("xoauth_displayname", Application.ProductName);

            string url = OAuthManager.GenerateQuery(URL_OAuthRequest, args, HttpMethod.Get, AuthInfo);
            string response = SendRequest(HttpMethod.Get, url);
            if (!string.IsNullOrEmpty(response))
            {
                Console.WriteLine(url);
                string authurl = OAuthManager.GetAuthorizationURL(response, AuthInfo, URLAuthorize);
                return authurl;
            }

            return null;
        }

        public bool GetAccessToken(string verificationCode)
        {
            throw new NotImplementedException();
        }

        public override UploadResult Upload(System.IO.Stream stream, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}