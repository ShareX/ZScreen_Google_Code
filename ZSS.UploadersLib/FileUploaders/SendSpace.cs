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
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace UploadersLib.FileUploaders
{
    public sealed class SendSpace : FileUploader
    {
        private const string SENDSPACE_API_KEY = "LV6OS1R0Q3";
        private const string SENDSPACE_API_URL = "http://api.sendspace.com/rest/";
        private const string SENDSPACE_API_VERSION = "1.0";

        /// <summary>
        /// Upload speed limit in kilobytes, 0 for unlimited
        /// </summary>
        public int SpeedLimit = 0;

        public string AppVersion = "1.0";

        public SendSpace() { }

        public override string Name
        {
            get { return "SendSpace"; }
        }

        #region Helpers

        public class ResponsePacket
        {
            public string Method { get; set; }
            public string Status { get; set; }
            public bool Error { get; set; }
            public string ErrorCode { get; set; }
            public string ErrorText { get; set; }
            public XElement Result { get; set; }
        }

        public ResponsePacket ParseResponse(string response)
        {
            ResponsePacket packet = new ResponsePacket();

            XDocument xml = XDocument.Parse(response);
            packet.Result = xml.Element("result");
            packet.Method = packet.Result.Attribute("method").Value;
            packet.Status = packet.Result.Attribute("status").Value;
            packet.Error = packet.Status == "fail";

            if (packet.Error)
            {
                XElement error = packet.Result.Element("error");
                packet.ErrorCode = error.Attribute("code").Value;
                packet.ErrorText = error.Attribute("text").Value;

                base.Errors.Add(string.Format("Code: {0}, Method: {1}\r\nText: {2}", packet.ErrorCode, packet.Method, packet.ErrorText));
            }

            return packet;
        }

        public class LoginInfo
        {
            /// <summary>
            /// Session key to be sent with all method calls, user information, including the user account's capabilities
            /// </summary>
            public string SessionKey { get; set; }
            public string Username { get; set; }
            public string EMail { get; set; }
            public string MembershipType { get; set; }
            public string MembershipEnds { get; set; }
            public bool CapableUpload { get; set; }
            public bool CapableDownload { get; set; }
            public bool CapableFolders { get; set; }
            public bool CapableFiles { get; set; }
            public bool CapableHTTPS { get; set; }
            public bool CapableAddressBook { get; set; }
            public string BandwidthLeft { get; set; }
            public string DiskSpaceLeft { get; set; }
            public string DiskSpaceUsed { get; set; }
            public string Points { get; set; }

            public LoginInfo() { }

            public LoginInfo(XElement element)
            {
                SessionKey = element.ElementValue("session_key");
                Username = element.ElementValue("user_name");
                EMail = element.ElementValue("email");
                MembershipType = element.ElementValue("membership_type");
                MembershipEnds = element.ElementValue("membership_ends");
                CapableUpload = element.ElementValue("capable_upload") != "0";
                CapableDownload = element.ElementValue("capable_download") != "0";
                CapableFolders = element.ElementValue("capable_folders") != "0";
                CapableFiles = element.ElementValue("capable_files") != "0";
                CapableHTTPS = element.ElementValue("capable_https") != "0";
                CapableAddressBook = element.ElementValue("capable_addressbook") != "0";
                BandwidthLeft = element.ElementValue("bandwidth_left");
                DiskSpaceLeft = element.ElementValue("diskspace_left");
                DiskSpaceUsed = element.ElementValue("diskspace_used");
                Points = element.ElementValue("points");
            }
        }

        public class UploadInfo
        {
            public string URL { get; set; }
            public string ProgressURL { get; set; }
            public string MaxFileSize { get; set; }
            public string UploadIdentifier { get; set; }
            public string ExtraInfo { get; set; }

            public UploadInfo() { }

            public UploadInfo(XElement element)
            {
                XElement upload = element.Element("upload");
                URL = upload.AttributeValue("url");
                ProgressURL = upload.AttributeValue("progress_url");
                MaxFileSize = upload.AttributeValue("max_file_size");
                UploadIdentifier = upload.AttributeValue("upload_identifier");
                ExtraInfo = upload.AttributeValue("extra_info");
            }
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Creates a new user account. An activation/validation email will be sent automatically to the user.
        /// http://www.sendspace.com/dev_method.html?method=auth.register
        /// </summary>
        /// <param name="username">a-z/A-Z/0-9, 3-20 chars</param>
        /// <param name="fullname">a-z/A-Z/space, 3-20 chars</param>
        /// <param name="email">Valid email address required</param>
        /// <param name="password">Can be left empty and the API will create a unique password or enter one with 4-20 chars</param>
        /// <returns>true = success, false = error</returns>
        public bool AuthRegister(string username, string fullname, string email, string password)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.register");
            args.Add("api_key", SENDSPACE_API_KEY);
            args.Add("user_name", username);
            args.Add("full_name", fullname);
            args.Add("email", email);
            args.Add("password", password);

            string response = GetResponse(SENDSPACE_API_URL, args);

            if (!string.IsNullOrEmpty(response))
            {
                return !ParseResponse(response).Error;
            }

            return false;
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
            args.Add("app_version", AppVersion); // Application specific, formatting / style is up to you
            args.Add("response_format", "xml"); // Value must be: XML

            string response = GetResponse(SENDSPACE_API_URL, args);

            if (!string.IsNullOrEmpty(response))
            {
                ResponsePacket packet = ParseResponse(response);

                if (!packet.Error)
                {
                    return packet.Result.ElementValue("token");
                }
            }

            return null;
        }

        /// <summary>
        /// Starts a session and returns user API method capabilities -- which features the given user can and cannot use.
        /// http://www.sendspace.com/dev_method.html?method=auth.login
        /// </summary>
        /// <param name="token">Received on create token</param>
        /// <param name="username">Registered user name</param>
        /// <param name="password">Registered password</param>
        /// <returns>Account informations including session key</returns>
        public LoginInfo AuthLogin(string token, string username, string password)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.login");
            args.Add("token", token);
            args.Add("user_name", username);
            args.Add("tokened_password", GetMD5(token + GetMD5(password))); // lowercase(md5(token+lowercase(md5(password)))) - md5 values should always be lowercase.

            string response = GetResponse(SENDSPACE_API_URL, args);

            if (!string.IsNullOrEmpty(response))
            {
                ResponsePacket packet = ParseResponse(response);

                if (!packet.Error)
                {
                    LoginInfo loginInfo = new LoginInfo(packet.Result);
                    return loginInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if a session is valid or not.
        /// http://www.sendspace.com/dev_method.html?method=auth.checksession
        /// </summary>
        /// <param name="sessionKey">Received from auth.login</param>
        /// <returns>true = success, false = error</returns>
        public bool AuthCheckSession(string sessionKey)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.checkSession");
            args.Add("session_key", sessionKey);

            string response = GetResponse(SENDSPACE_API_URL, args);

            if (!string.IsNullOrEmpty(response))
            {
                ResponsePacket packet = ParseResponse(response);

                if (!packet.Error)
                {
                    string session = packet.Result.ElementValue("session");

                    if (!string.IsNullOrEmpty(session))
                    {
                        return session == "ok";
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Logs out from a session.
        /// http://www.sendspace.com/dev_method.html?method=auth.logout
        /// </summary>
        /// <param name="sessionKey">Received from auth.login</param>
        /// <returns>true = success, false = error</returns>
        public bool AuthLogout(string sessionKey)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "auth.logout");
            args.Add("session_key", sessionKey);

            string response = GetResponse(SENDSPACE_API_URL, args);

            if (!string.IsNullOrEmpty(response))
            {
                return !ParseResponse(response).Error;
            }

            return false;
        }

        #endregion

        #region Upload

        /// <summary>
        /// Obtains the information needed to perform an upload.
        /// http://www.sendspace.com/dev_method.html?method=upload.getInfo
        /// </summary>
        /// <param name="sessionKey">Received from auth.login</param>
        /// <returns>URL to upload the file to, progress_url for real-time progress information, max_file_size for max size current user can upload, upload_identifier & extra_info to be passed with the upload form</returns>
        public UploadInfo UploadGetInfo(string sessionKey)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "upload.getInfo");
            args.Add("session_key", sessionKey);
            args.Add("speed_limit", SpeedLimit.ToString());

            string response = GetResponse(SENDSPACE_API_URL, args);

            if (!string.IsNullOrEmpty(response))
            {
                ResponsePacket packet = ParseResponse(response);

                if (!packet.Error)
                {
                    UploadInfo uploadInfo = new UploadInfo(packet.Result);
                    return uploadInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// Obtains the basic information needed to make an anonymous upload. This method does not require authentication or login.
        /// </summary>
        /// <returns>URL to upload the file to, progress_url for real-time progress information, max_file_size for max size current user can upload, upload_identifier & extra_info to be passed in the upload form</returns>
        public UploadInfo AnonymousUploadGetInfo()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "anonymous.uploadGetInfo");
            args.Add("speed_limit", SpeedLimit.ToString());
            args.Add("api_key", SENDSPACE_API_KEY);
            args.Add("api_version", SENDSPACE_API_VERSION);
            args.Add("app_version", AppVersion);

            string response = GetResponse(SENDSPACE_API_URL, args);

            if (!string.IsNullOrEmpty(response))
            {
                ResponsePacket packet = ParseResponse(response);

                if (!packet.Error)
                {
                    UploadInfo uploadInfo = new UploadInfo(packet.Result);
                    return uploadInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// http://www.sendspace.com/dev_method.html?method=upload.getInfo
        /// </summary>
        /// <param name="max_file_size">max_file_size value received in UploadGetInfo response</param>
        /// <param name="upload_identifier">upload_identifier value received in UploadGetInfo response</param>
        /// <param name="extra_info">extra_info value received in UploadGetInfo response</param>
        /// <param name="description"></param>
        /// <param name="password"></param>
        /// <param name="folder_id"></param>
        /// <param name="recipient_email">an email (or emails separated with ,) of recipient/s to receive information about the upload</param>
        /// <param name="notify_uploader">0/1 - should the uploader be notified?</param>
        /// <param name="redirect_url">page to redirect after upload will be attached upload_status=ok/fail&file_id=XXXX</param>
        /// <returns></returns>
        public Dictionary<string, string> PrepareArguments(string max_file_size, string upload_identifier, string extra_info,
            string description, string password, string folder_id, string recipient_email, string notify_uploader, string redirect_url)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("MAX_FILE_SIZE", max_file_size);
            args.Add("UPLOAD_IDENTIFIER", upload_identifier);
            args.Add("extra_info", extra_info);

            // Optional fields

            if (!string.IsNullOrEmpty(description)) args.Add("description", description);
            if (!string.IsNullOrEmpty(password)) args.Add("password", password);
            if (!string.IsNullOrEmpty(folder_id)) args.Add("folder_id", folder_id);
            if (!string.IsNullOrEmpty(recipient_email)) args.Add("recipient_email", recipient_email);
            if (!string.IsNullOrEmpty(notify_uploader)) args.Add("notify_uploader", notify_uploader);
            if (!string.IsNullOrEmpty(redirect_url)) args.Add("redirect_url", redirect_url);

            return args;
        }

        public Dictionary<string, string> PrepareArguments(string max_file_size, string upload_identifier, string extra_info)
        {
            return PrepareArguments(max_file_size, upload_identifier, extra_info, null, null, null, null, null, null);
        }

        public string Upload(byte[] data, string fileName, UploadInfo uploadInfo)
        {
            if (uploadInfo != null)
            {
                Dictionary<string, string> args = PrepareArguments(uploadInfo.MaxFileSize, uploadInfo.UploadIdentifier, uploadInfo.ExtraInfo);

                string response;

                using (CheckProgress progress = new CheckProgress(uploadInfo.ProgressURL, this))
                {
                    response = UploadData(data, fileName, uploadInfo.URL, "userfile", args);
                }

                if (!string.IsNullOrEmpty(response))
                {
                    string fileid = Regex.Match(response, @"<file_id>(\w+)</file_id>").Groups[1].Value; // Anonymous

                    if (string.IsNullOrEmpty(fileid))
                    {
                        fileid = fileid = Regex.Match(response, @"file_id=(\w+)").Groups[1].Value; // User
                    }

                    if (!string.IsNullOrEmpty(fileid))
                    {
                        return "http://www.sendspace.com/file/" + fileid;
                    }
                }
            }

            return string.Empty;
        }

        public override string Upload(byte[] data, string fileName)
        {
            return Upload(data, fileName, SendSpaceManager.UploadInfo);
        }

        public class CheckProgress : IDisposable
        {
            private SendSpace sendSpace;
            private BackgroundWorker bw;
            private string url;
            private int interval = 1000;

            public CheckProgress(string progressURL, SendSpace sendSpace)
            {
                url = progressURL;
                this.sendSpace = sendSpace;
                bw = new BackgroundWorker { WorkerSupportsCancellation = true };
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerAsync();
            }

            private void bw_DoWork(object sender, DoWorkEventArgs e)
            {
                Thread.Sleep(1000);
                ProgressInfo progressInfo = new ProgressInfo();
                int progress, elapsed;
                DateTime time;
                while (!bw.CancellationPending)
                {
                    time = DateTime.Now;
                    try
                    {
                        progressInfo.ParseResponse(sendSpace.GetResponse(url));
                        if (progressInfo.Status != "fail" && !string.IsNullOrEmpty(progressInfo.Meter))
                        {
                            if (int.TryParse(progressInfo.Meter, out progress))
                            {
                                sendSpace.OnProgressChanged(progress);
                            }
                        }
                    }
                    catch { }
                    elapsed = (int)(DateTime.Now - time).TotalMilliseconds;
                    if (elapsed < interval)
                    {
                        Thread.Sleep(interval - elapsed);
                    }
                }
            }

            public class ProgressInfo
            {
                public string Status { get; set; }
                public string ETA { get; set; }
                public string Speed { get; set; }
                public string UploadedBytes { get; set; }
                public string TotalSize { get; set; }
                public string Elapsed { get; set; }
                public string Meter { get; set; }

                public ProgressInfo() { }

                public ProgressInfo(string response)
                {
                    ParseResponse(response);
                }

                public void ParseResponse(string response)
                {
                    XDocument xml = XDocument.Parse(response);
                    XElement element = xml.Element("progress");

                    Status = element.ElementValue("status");
                    ETA = element.ElementValue("eta");
                    Speed = element.ElementValue("speed");
                    UploadedBytes = element.ElementValue("uploaded_bytes");
                    TotalSize = element.ElementValue("total_size");
                    Elapsed = element.ElementValue("elapsed");
                    Meter = element.ElementValue("meter");
                }

                public override string ToString()
                {
                    return string.Format("Status: {0}, ETA: {1}, Speed: {2}\r\nBytes: {3}/{4}, Elapsed: {5}, Meter: {6}%",
                        Status, ETA, Speed, UploadedBytes, TotalSize, Elapsed, Meter);
                }
            }

            public void Dispose()
            {
                bw.CancelAsync();
            }
        }

        #endregion
    }
}