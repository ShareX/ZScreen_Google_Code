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
using System.Linq;
using System.Text;

namespace UploadersLib.FileUploaders
{
    public class SendSpace : Uploader
    {
        private const string SENDSPACE_API_KEY = "LV6OS1R0Q3";
        private const string SENDSPACE_API_URL = "http://api.sendspace.com/rest/";
        private const string SENDSPACE_API_VERSION = "1.0";

        private string appVersion;

        public SendSpace()
        {

        }

        #region Authentication

        /// <summary>
        /// Creates a new user account. An activation/validation email will be sent automatically to the user.
        /// http://www.sendspace.com/dev_method.html?method=auth.register
        /// </summary>
        public bool AuthRegister(string username, string fullname, string email, string password)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.register");
            args.Add("api_key", SENDSPACE_API_KEY); // Received from sendspace
            args.Add("user_name", username); // a-z/A-Z/0-9, 3-20 chars
            args.Add("full_name", fullname); // a-z/A-Z/space, 3-20 chars
            args.Add("email", email); // Valid email address required
            args.Add("password", password); // Can be left empty and the API will create a unique password or enter one with 4-20 chars

            string response = GetResponse(SENDSPACE_API_URL, args);

            return true;
        }

        /// <summary>
        /// Obtains a new and random token per session. Required for login.
        /// http://www.sendspace.com/dev_method.html?method=auth.createToken
        /// </summary>
        /// <returns>A token to be used with the auth.login method</returns>
        public string AuthCreateToken()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.createToken");
            args.Add("api_key", SENDSPACE_API_KEY); // Received from sendspace
            args.Add("api_version", SENDSPACE_API_VERSION); // Value must be: 1.0
            args.Add("app_version", appVersion); // Application specific, formatting / style is up to you
            args.Add("response_format", "xml"); // Value must be: XML

            string response = GetResponse(SENDSPACE_API_URL, args);

            return "";
        }

        /// <summary>
        /// Starts a session and returns user API method capabilities -- which features the given user can and cannot use.
        /// http://www.sendspace.com/dev_method.html?method=auth.login
        /// </summary>
        /// <param name="token">Received on create token</param>
        /// <param name="username">Registered user name</param>
        /// <param name="password">Registered password</param>
        /// <returns>session_key to be sent with all method calls, user information, including the user account's capabilities</returns>
        public string AuthLogin(string token, string username, string password)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.login");
            args.Add("token", token); // Received on create token
            args.Add("user_name", username); // Registered user name
            args.Add("tokened_password", GetMD5(token + GetMD5(password))); // lowercase(md5(token+lowercase(md5(password)))) - md5 values should always be lowercase.

            string response = GetResponse(SENDSPACE_API_URL, args);

            return "";
        }

        /// <summary>
        /// Checks if a session is valid or not.
        /// http://www.sendspace.com/dev_method.html?method=auth.checksession
        /// </summary>
        /// <param name="sessionKey">Received from auth.login</param>
        public bool AuthCheckSession(string sessionKey)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.checkSession");
            args.Add("session_key", sessionKey); // Received from auth.login

            string response = GetResponse(SENDSPACE_API_URL, args);

            return true;
        }

        /// <summary>
        /// Logs out from a session.
        /// http://www.sendspace.com/dev_method.html?method=auth.logout
        /// </summary>
        /// <param name="sessionKey">Received from auth.login</param>
        public bool AuthLogout(string sessionKey)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.logout");
            args.Add("session_key", sessionKey); // Received from auth.login

            string response = GetResponse(SENDSPACE_API_URL, args);

            return true;
        }

        #endregion

        #region Upload

        /// <summary>
        /// Obtains the information needed to perform an upload.
        /// http://www.sendspace.com/dev_method.html?method=upload.getInfo
        /// </summary>
        /// <param name="sessionKey">Received from auth.login</param>
        /// <param name="speedLimit">Upload speed limit in kilobytes, 0 for unlimited</param>
        /// <returns>URL to upload the file to, progress_url for real-time progress information, max_file_size for max size current user can upload, upload_identifier & extra_info to be passed with the upload form</returns>
        public string UploadGetInfo(string sessionKey, string speedLimit)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "upload.getInfo");
            args.Add("session_key", sessionKey); // Received from auth.login
            args.Add("speed_limit", speedLimit); // Upload speed limit in kilobytes, 0 for unlimited

            string response = GetResponse(SENDSPACE_API_URL, args);

            return "";
        }

        #endregion
    }
}