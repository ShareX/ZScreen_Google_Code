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
using System.ComponentModel;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.TextUploaders
{
    [Serializable]
    public sealed class PastebinUploader : TextUploader
    {
        public static readonly string Hostname = TextDestType.PASTEBIN.GetDescription();

        public override object Settings
        {
            get { return HostSettings; }
            set { HostSettings = (PastebinSettings)value; }
        }

        public PastebinSettings HostSettings = new PastebinSettings();

        private const string APIKey = "4b23be71ec78bbd4fb96735320aa09ef";
        private const string APIURL = "http://pastebin.com/api/api_post.php";
        private const string APILoginURL = "http://pastebin.com/api/api_login.php";

        public PastebinUploader()
        {
            HostSettings.URL = APIURL;
        }

        public override string ToString()
        {
            return HostSettings.Name;
        }

        public override string TesterString
        {
            get { return "Testing " + Hostname; }
        }

        public override string UploadText(TextInfo text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> args = new Dictionary<string, string>();

                args.Add("api_dev_key", APIKey); // which is your unique API Developers Key
                args.Add("api_option", "paste"); // set as 'paste', this will indicate you want to create a new paste
                args.Add("api_paste_code", text.LocalString); // this is the text that will be written inside your paste

                // Optional args
                args.Add("api_paste_name", HostSettings.Title); // this will be the name / title of your paste
                args.Add("api_paste_format", HostSettings.TextFormat); // this will be the syntax highlighting value
                args.Add("api_paste_private", HostSettings.IsPublic ? "0" : "1"); // this makes a paste public or private, public = 0, private = 1
                args.Add("api_paste_expire_date", HostSettings.ExpireTime); // this sets the expiration date of your paste

                if (string.IsNullOrEmpty(HostSettings.UserKey) && !string.IsNullOrEmpty(HostSettings.Username) && !string.IsNullOrEmpty(HostSettings.Password))
                {
                    Dictionary<string, string> loginArgs = new Dictionary<string, string>();

                    loginArgs.Add("api_dev_key", APIKey);
                    loginArgs.Add("api_user_name", HostSettings.Username);
                    loginArgs.Add("api_user_password", HostSettings.Password);

                    string loginResponse = GetResponse(APILoginURL, loginArgs);

                    if (!string.IsNullOrEmpty(loginResponse) && !loginResponse.StartsWith("Bad API request"))
                    {
                        HostSettings.UserKey = loginResponse;
                    }
                    else
                    {
                        Errors.Add("Pastebin login failed.");
                        return string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(HostSettings.UserKey))
                {
                    args.Add("api_user_key", HostSettings.UserKey); // this paramater is part of the login system
                }

                string response = GetResponse(HostSettings.URL, args);

                if (!string.IsNullOrEmpty(response) && !response.StartsWith("Bad API request") && response.IsValidUrl())
                {
                    return response;
                }
                else
                {
                    Errors.Add(response);
                }
            }

            return string.Empty;
        }

        [Serializable]
        public class PastebinSettings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }

            [Description("Paste name / title")]
            public string Title { get; set; }
            [Description("Syntax highlighting")]
            public string TextFormat { get; set; }
            [DefaultValue("N"), Description("Paste expiration\r\nN = Never, 10M = 10 Minutes, 1H = 1 Hour, 1D = 1 Day, 1M = 1 Month")]
            public string ExpireTime { get; set; }
            [Description("Paste exposure"), DefaultValue(false)]
            public bool IsPublic { get; set; }
            [Description("Account username")]
            public string Username { get; set; }
            [PasswordPropertyText(true), Description("Account password")]
            public string Password { get; set; }
            [Description("Will be created automaticly with Username && Password")]
            public string UserKey { get; set; }

            public PastebinSettings()
            {
                Name = Hostname;
                ExpireTime = "N";
            }
        }
    }
}