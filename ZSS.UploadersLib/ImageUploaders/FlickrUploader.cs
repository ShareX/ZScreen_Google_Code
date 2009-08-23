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

namespace UploadersLib.ImageUploaders
{
    public class FlickrUploader : ImageUploader
    {
        private const string API_Key = "009382d913746758f23d0ba9906b9fde";
        private const string API_Secret = "7a147b763b1c7ebc";
        private const string API_URL = "http://api.flickr.com/services/rest/";
        private const string API_Auth_URL = "http://www.flickr.com/services/auth/";
        private const string API_Upload_URL = "http://api.flickr.com/services/upload/";

        public string Token;
        public string Frob;

        public FlickrUploader() { }

        public FlickrUploader(string token)
        {
            this.Token = token;
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
        /// <param name="token"></param>
        /// <returns></returns>
        public AuthInfo CheckToken(string token)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "flickr.auth.checkToken");
            args.Add("api_key", API_Key);
            args.Add("auth_token", token);
            args.Add("api_sig", GetAPISig(args));

            string response = GetResponse(API_URL, args);

            AuthInfo auth = new AuthInfo(ParseResponse(response, "auth"));

            return auth;
        }

        /// <summary>
        /// Returns a frob to be used during authentication.
        /// http://www.flickr.com/services/api/flickr.auth.getFrob.html
        /// </summary>
        /// <returns>frob</returns>
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
        /// <param name="frob"></param>
        /// <returns></returns>
        public AuthInfo GetFullToken(string frob)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "flickr.auth.getFullToken");
            args.Add("api_key", API_Key);
            args.Add("mini_token", frob);
            args.Add("api_sig", GetAPISig(args));

            string response = GetResponse(API_URL, args);

            AuthInfo auth = new AuthInfo(ParseResponse(response, "auth"));

            return auth;
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

            AuthInfo auth = new AuthInfo(ParseResponse(response, "auth"));

            this.Token = auth.Token;

            return auth;
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
                            throw new Exception(string.Format("Code: {0} - Message: {1}", code, msg));
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
            args.Add("auth_token", Token);
            args.Add("api_sig", GetAPISig(args));

            string response = UploadImage(image, fileName, API_Upload_URL, "photo", args);

            string photoid = ParseResponse(response, "photoid").Value;

            throw new NotImplementedException();
        }

        public class AuthInfo
        {
            public string Token { get; set; }
            public string Permission { get; set; }
            public string UserID { get; set; }
            public string Username { get; set; }
            public string Fullname { get; set; }

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