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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using UploadersLib.Helpers;
using System.ComponentModel;

namespace UploadersLib.ImageUploaders
{
    public class FlickrUploader : ImageUploader
    {
        private const string API_Key = "009382d913746758f23d0ba9906b9fde";
        private const string API_Secret = "7a147b763b1c7ebc";
        private const string API_URL = "http://api.flickr.com/services/rest/";
        private const string API_Auth_URL = "http://www.flickr.com/services/auth/";
        private const string API_Upload_URL = "http://api.flickr.com/services/upload/";

        public AuthInfo Auth = new AuthInfo();
        public FlickrSettings Settings = new FlickrSettings();
        public string Frob;

        public FlickrUploader() { }

        public FlickrUploader(AuthInfo auth, FlickrSettings settings)
        {
            this.Auth = auth;
            this.Settings = settings;
        }

        public override string Name
        {
            get { return "Flickr"; }
        }

        #region Auth

        /// <summary>
        /// Returns the credentials attached to an authentication token.
        /// http://www.flickr.com/services/api/flickr.auth.checkToken.html
        /// </summary>
        public AuthInfo CheckToken(string token)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "flickr.auth.checkToken");
            args.Add("api_key", API_Key);
            args.Add("auth_token", token);
            args.Add("api_sig", GetAPISig(args));

            string response = GetResponse(API_URL, args);

            this.Auth = new AuthInfo(ParseResponse(response, "auth"));

            return this.Auth;
        }

        /// <summary>
        /// Returns a frob to be used during authentication.
        /// http://www.flickr.com/services/api/flickr.auth.getFrob.html
        /// </summary>
        public string GetFrob()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "flickr.auth.getFrob");
            args.Add("api_key", API_Key);
            args.Add("api_sig", GetAPISig(args));

            string response = GetResponse(API_URL, args);

            this.Frob = ParseResponse(response, "frob").Value;

            return this.Frob;
        }

        /// <summary>
        /// Get the full authentication token for a mini-token.
        /// http://www.flickr.com/services/api/flickr.auth.getFullToken.html
        /// </summary>
        public AuthInfo GetFullToken(string frob)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "flickr.auth.getFullToken");
            args.Add("api_key", API_Key);
            args.Add("mini_token", frob);
            args.Add("api_sig", GetAPISig(args));

            string response = GetResponse(API_URL, args);

            this.Auth = new AuthInfo(ParseResponse(response, "auth"));

            return this.Auth;
        }

        /// <summary>
        /// Returns the auth token for the given frob, if one has been attached.
        /// http://www.flickr.com/services/api/flickr.auth.getToken.html
        /// </summary>
        /// <returns></returns>
        public AuthInfo GetToken(string frob)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "flickr.auth.getToken");
            args.Add("api_key", API_Key);
            args.Add("frob", frob);
            args.Add("api_sig", GetAPISig(args));

            string response = GetResponse(API_URL, args);

            this.Auth = new AuthInfo(ParseResponse(response, "auth"));

            return this.Auth;
        }

        public AuthInfo GetToken()
        {
            return GetToken(this.Frob);
        }

        public string GetAuthLink()
        {
            return GetAuthLink(Permission.Write);
        }

        public string GetAuthLink(Permission perm)
        {
            if (!string.IsNullOrEmpty(Frob))
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("api_key", API_Key);
                args.Add("perms", perm.ToString().ToLowerInvariant());
                args.Add("frob", this.Frob);
                args.Add("api_sig", GetAPISig(args));

                return string.Format("{0}?{1}", API_Auth_URL, string.Join("&", args.Select(x => x.Key + "=" + x.Value).ToArray()));
            }

            return null;
        }

        public string GetPhotosLink(string userID)
        {
            return CombineURL("http://www.flickr.com/photos", userID);
        }

        public string GetPhotosLink()
        {
            return GetPhotosLink(this.Auth.UserID);
        }

        #endregion

        #region Helpers

        private string GetAPISig(Dictionary<string, string> args)
        {
            return GetMD5(args.OrderBy(x => x.Key).Aggregate(API_Secret, (x, x2) => x + x2.Key + x2.Value));
        }

        private XElement ParseResponse(string response, string field)
        {
            if (!string.IsNullOrEmpty(response))
            {
                XDocument xdoc = XDocument.Parse(response);
                XElement xele = xdoc.Element("rsp");

                if (xele != null)
                {
                    switch (xele.AttributeFirstValue("status", "stat"))
                    {
                        case "ok":
                            return xele.Element(field);
                        case "fail":
                            string code, msg;
                            XElement err = xele.Element("err");
                            code = err.AttributeValue("code");
                            msg = err.AttributeValue("msg");
                            throw new Exception(string.Format("Code: {0}, Message: {1}", code, msg));
                    }
                }
            }

            return null;
        }

        #endregion

        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("api_key", API_Key);
            args.Add("auth_token", this.Auth.Token);

            if (!string.IsNullOrEmpty(Settings.Title)) args.Add("title", Settings.Title);
            if (!string.IsNullOrEmpty(Settings.Description)) args.Add("description", Settings.Description);
            if (!string.IsNullOrEmpty(Settings.Tags)) args.Add("tags", Settings.Tags);
            if (!string.IsNullOrEmpty(Settings.IsPublic)) args.Add("is_public", Settings.IsPublic);
            if (!string.IsNullOrEmpty(Settings.IsFriend)) args.Add("is_friend", Settings.IsFriend);
            if (!string.IsNullOrEmpty(Settings.IsFamily)) args.Add("is_family", Settings.IsFamily);
            if (!string.IsNullOrEmpty(Settings.SafetyLevel)) args.Add("safety_level", Settings.SafetyLevel);
            if (!string.IsNullOrEmpty(Settings.ContentType)) args.Add("content_type", Settings.ContentType);
            if (!string.IsNullOrEmpty(Settings.Hidden)) args.Add("hidden", Settings.Hidden);

            args.Add("api_sig", GetAPISig(args));

            string response = UploadImage(image, fileName, API_Upload_URL, "photo", args);

            string photoid = ParseResponse(response, "photoid").Value;

            string url = CombineURL(GetPhotosLink(), photoid);

            string url2 = CombineURL(url, "sizes/o");

            return new ImageFileManager(url2, response);
        }

        public class AuthInfo
        {
            [Description("Token string"), ReadOnly(true)]
            public string Token { get; set; }

            [Description("Permission"), ReadOnly(true)]
            public string Permission { get; set; }

            [Description("User ID that can be used in a URL")]
            public string UserID { get; set; }

            [Description("Your Flickr username"), ReadOnly(true)]
            public string Username { get; set; }

            [Description("Full name"), ReadOnly(true)]
            public string Fullname { get; set; }

            public AuthInfo() { }

            public AuthInfo(XElement element)
            {
                Token = element.ElementValue("token");
                Permission = element.ElementValue("perms");
                XElement user = element.Element("user");
                UserID = user.AttributeValue("nsid");
                Username = user.AttributeValue("username");
                Fullname = user.AttributeValue("fullname");
            }
        }

        public class FlickrSettings
        {
            /// <summary>
            /// The title of the photo.
            /// </summary>
            [Description("The title of the photo.")]
            public string Title { get; set; }

            /// <summary>
            /// A description of the photo. May contain some limited HTML.
            /// </summary>
            [Description("A description of the photo. May contain some limited HTML.")]
            public string Description { get; set; }

            /// <summary>
            /// A space-seperated list of tags to apply to the photo.
            /// </summary>
            [Description("A space-seperated list of tags to apply to the photo.")]
            public string Tags { get; set; }

            /// <summary>
            /// Set to 0 for no, 1 for yes. Specifies who can view the photo.
            /// </summary>
            [Description("Set to 0 for no, 1 for yes. Specifies who can view the photo.")]
            public string IsPublic { get; set; }

            /// <summary>
            /// Set to 0 for no, 1 for yes. Specifies who can view the photo.
            /// </summary>
            [Description("Set to 0 for no, 1 for yes. Specifies who can view the photo.")]
            public string IsFriend { get; set; }

            /// <summary>
            /// Set to 0 for no, 1 for yes. Specifies who can view the photo.
            /// </summary>
            [Description("Set to 0 for no, 1 for yes. Specifies who can view the photo.")]
            public string IsFamily { get; set; }

            /// <summary>
            /// Set to 1 for Safe, 2 for Moderate, or 3 for Restricted.
            /// </summary>
            [Description("Set to 1 for Safe, 2 for Moderate, or 3 for Restricted.")]
            public string SafetyLevel { get; set; }

            /// <summary>
            /// Set to 1 for Photo, 2 for Screenshot, or 3 for Other.
            /// </summary>
            [Description("Set to 1 for Photo, 2 for Screenshot, or 3 for Other.")]
            public string ContentType { get; set; }

            /// <summary>
            /// Set to 1 to keep the photo in global search results, 2 to hide from public searches.
            /// </summary>
            [Description("Set to 1 to keep the photo in global search results, 2 to hide from public searches.")]
            public string Hidden { get; set; }
        }

        public enum Permission
        {
            None,
            /// <summary>
            /// Permission to read private information
            /// </summary>
            Read,
            /// <summary>
            /// Permission to add, edit and delete photo metadata (includes 'read')
            /// </summary>
            Write,
            /// <summary>
            /// Permission to delete photos (includes 'write' and 'read')
            /// </summary>
            Delete
        }
    }
}