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
using System.Xml.Linq;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class Imgur : ImageUploader, IOAuth
    {
        private const string URLAnonymousUpload = "https://api.imgur.com/2/upload.xml";
        private const string URLUserUpload = "https://api.imgur.com/2/account/images.xml";
        private const string URLRequestToken = "https://api.imgur.com/oauth/request_token";
        private const string URLAuthorize = "https://api.imgur.com/oauth/authorize";
        private const string URLAccessToken = "https://api.imgur.com/oauth/access_token";

        public override string Name
        {
            get { return "Imgur"; }
        }

        /// <summary>
        /// Upload method: Anonymous or User?
        /// </summary>
        public AccountType UploadMethod { get; set; }

        /// <summary>
        /// Required for Anonymous upload
        /// </summary>
        public string AnonymousKey { get; set; }

        /// <summary>
        /// Required for User upload (OAuth)
        /// </summary>
        public OAuthInfo AuthInfo { get; set; }

        public Imgur(AccountType uploadMethod, string anonymousKey, OAuthInfo oauth)
        {
            UploadMethod = uploadMethod;
            AnonymousKey = anonymousKey;
            AuthInfo = oauth;
        }

        public Imgur(string anonymousKey)
        {
            UploadMethod = AccountType.Anonymous;
            AnonymousKey = anonymousKey;
        }

        public Imgur(OAuthInfo oauth)
        {
            UploadMethod = AccountType.User;
            AuthInfo = oauth;
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            ImageFileManager ifm = null;

            switch (UploadMethod)
            {
                case AccountType.Anonymous:
                    ifm = AnonymousUpload(stream, fileName);
                    break;
                case AccountType.User:
                    ifm = UserUpload(stream, fileName);
                    break;
            }

            return ifm;
        }

        public string GetAuthorizationURL()
        {
            return GetAuthorizationURL(URLRequestToken, URLAuthorize, AuthInfo);
        }

        public bool GetAccessToken(string verificationCode)
        {
            AuthInfo.AuthVerifier = verificationCode;
            return GetAccessToken(URLAccessToken, AuthInfo);
        }

        private ImageFileManager UserUpload(Stream stream, string fileName)
        {
            if (string.IsNullOrEmpty(AuthInfo.UserToken) || string.IsNullOrEmpty(AuthInfo.UserSecret))
            {
                throw new Exception("UserToken or UserSecret is empty. Login is required.");
            }

            string query = OAuthManager.GenerateQuery(URLUserUpload, null, HttpMethod.POST, AuthInfo);

            string response = UploadData(stream, query, fileName, "image");

            return ParseResponse(response);
        }

        private ImageFileManager AnonymousUpload(Stream stream, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("key", AnonymousKey);

            string response = UploadData(stream, URLAnonymousUpload, fileName, "image", arguments);

            return ParseResponse(response);
        }

        private ImageFileManager ParseResponse(string source)
        {
            ImageFileManager ifm = new ImageFileManager { Source = source };

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xd = XDocument.Parse(source);
                XElement xe;

                if ((xe = xd.GetNode("upload|images/links")) != null)
                {
                    ifm.Add(xe.GetElementValue("original"), LinkType.FULLIMAGE);
                    ifm.Add(xe.GetElementValue("large_thumbnail"), LinkType.THUMBNAIL);
                    //ifm.Add(xele.ElementValue("small_square"), LinkType.THUMBNAIL);
                    ifm.Add(xe.GetElementValue("delete_page"), LinkType.DELETION_LINK);
                }
                else if ((xe = xd.GetElement("error")) != null)
                {
                    Errors.Add("Imgur error message: " + xe.GetElementValue("message"));
                }
            }

            return ifm;
        }
    }
}