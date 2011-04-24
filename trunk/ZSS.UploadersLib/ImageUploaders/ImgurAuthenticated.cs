#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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
using System.IO;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class ImgurAuthenticated : ImageUploader
    {
        public override string Name
        {
            get { return "Imgur (Authenticated)"; }
        }

        public OAuthInfo AuthInfo { get; set; }

        private const string URLRequestToken = "https://api.imgur.com/oauth/request_token";
        private const string URLAuthorize = "https://api.imgur.com/oauth/authorize";
        private const string URLAccessToken = "https://api.imgur.com/oauth/access_token";
        private const string URLUpload = "http://api.imgur.com/2/account/images";

        public ImgurAuthenticated(OAuthInfo oauth)
        {
            AuthInfo = oauth;
        }

        public string GetAuthorizationURL()
        {
            string url = OAuthManager.GenerateQuery(URLRequestToken, null, HttpMethod.GET, AuthInfo);

            string response = GetResponseString(url);

            if (!string.IsNullOrEmpty(response))
            {
                return OAuthManager.GetAuthorizationURL(response, AuthInfo, URLAuthorize);
            }

            return null;
        }

        public bool GetAccessToken(string verificationCode)
        {
            if (string.IsNullOrEmpty(AuthInfo.AuthToken))
            {
                throw new Exception("Auth token is empty. Get Authorization URL first.");
            }

            AuthInfo.AuthVerifier = verificationCode;

            string url = OAuthManager.GenerateQuery(URLAccessToken, null, HttpMethod.GET, AuthInfo);

            string response = GetResponseString(url);

            if (!string.IsNullOrEmpty(response))
            {
                return OAuthManager.ParseAccessTokenResponse(response, AuthInfo);
            }

            return false;
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            if (string.IsNullOrEmpty(AuthInfo.UserToken) || string.IsNullOrEmpty(AuthInfo.UserSecret))
            {
                throw new Exception("UserToken or UserSecret is empty. Login is required.");
            }

            ImageFileManager ifm = new ImageFileManager();

            string query = OAuthManager.GenerateQuery(URLUpload, null, HttpMethod.POST, AuthInfo);

            string response = UploadData(stream, query, fileName, "image");

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                // TODO: parse
            }

            return ifm;
        }
    }
}