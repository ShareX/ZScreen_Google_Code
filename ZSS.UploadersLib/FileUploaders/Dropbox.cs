using System;
using System.Collections.Generic;
using System.IO;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class Dropbox : FileUploader
    {
        public override string Name
        {
            get { return "Dropbox"; }
        }

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string UserToken { get; set; }
        public string UserSecret { get; set; }
        public string UploadPath { get; set; }
        public string UserID { get; set; }

        private const string APIVersion = "0";
        private const string URLToken = "https://api.dropbox.com/" + APIVersion + "/token";
        private const string URLAccountInfo = "https://api.dropbox.com/" + APIVersion + "/account/info";
        private const string URLFiles = "https://api-content.dropbox.com/" + APIVersion + "/files/dropbox";
        private const string URLDownload = "http://dl.dropbox.com/u";

        public Dropbox(string consumerKey, string consumerSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        public Dropbox(string consumerKey, string consumerSecret, string userToken, string userSecret, string path, string userID)
            : this(consumerKey, consumerSecret)
        {
            UserToken = userToken;
            UserSecret = userSecret;
            UploadPath = path;
            UserID = userID;
        }

        public DropboxUserLogin Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("oauth_consumer_key", ConsumerKey);
                args.Add("email", email);
                args.Add("password", password);

                string responseToken = GetResponseString(URLToken, args);

                DropboxUserLogin login = JSONHelper.JSONToObject<DropboxUserLogin>(responseToken);

                if (login != null)
                {
                    UserToken = login.token;
                    UserSecret = login.secret;
                }

                return login;
            }

            return null;
        }

        public DropboxAccountInfo GetAccountInfo()
        {
            if (!string.IsNullOrEmpty(UserToken) && !string.IsNullOrEmpty(UserSecret))
            {
                string url = MyOAuth.GenerateQuery(URLAccountInfo, null, "GET", ConsumerKey, ConsumerSecret, UserToken, UserSecret);

                string response = GetResponseString(url);

                DropboxAccountInfo account = JSONHelper.JSONToObject<DropboxAccountInfo>(response);

                if (account != null)
                {
                    UserID = account.uid.ToString();
                }

                return account;
            }

            return null;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            if (string.IsNullOrEmpty(UserToken) || string.IsNullOrEmpty(UserSecret)) throw new Exception("UserToken or UserSecret empty. Login is required.");

            if (UploadPath == null) UploadPath = string.Empty;
            if (!UploadPath.EndsWith("/")) UploadPath += "/";

            string url = Helpers.CombineURL(URLFiles, UploadPath);

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("file", fileName);

            string query = MyOAuth.GenerateQuery(url, args, "POST", ConsumerKey, ConsumerSecret, UserToken, UserSecret);

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
            if (uploadPath.StartsWith("Public/"))
            {
                return Helpers.CombineURL(URLDownload, userID, uploadPath.Substring(7), fileName);
            }

            return "Upload path is private. Use Public folder for get public URL.";
        }

        public class DropboxUserLogin
        {
            public string token { get; set; }
            public string secret { get; set; }
        }

        public class DropboxAccountInfo
        {
            public string referral_link { get; set; }
            public string display_name { get; set; }
            public long uid { get; set; }
            public string country { get; set; }
            public DropboxQuotaInfo quota_info { get; set; }
            public string email { get; set; }
        }

        public class DropboxQuotaInfo
        {
            public long shared { get; set; }
            public long quota { get; set; }
            public long normal { get; set; }
        }
    }
}