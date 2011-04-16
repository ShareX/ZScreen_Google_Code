using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class Dropbox : FileUploader
    {
        public override string Name
        {
            get { return "Dropbox"; }
        }

        public string Email { get; set; }
        public string Password { get; set; }

        private const string Key = "0te7j9ype9lrdfn";
        private const string Secret = "r5d3aptd9a0cwp9";
        private const string Version = "0";

        private string URLToken = "https://api.dropbox.com/{0}/token";
        private string URLFiles = "https://api-content.dropbox.com/{0}/files/dropbox/{1}";

        public Dropbox(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                args.Add("oauth_consumer_key", Key);
                args.Add("email", Email);
                args.Add("password", Password);
            }

            string responseToken = GetResponseString(string.Format(URLToken, Version), args);

            UserLogin login = JSONHelper.JSONToObject<UserLogin>(responseToken);

            string path = "Test/";

            OAuthBase oauth = new OAuthBase();

            string url = "";
            string querystring = "";

            string sig = oauth.GenerateSignature(new Uri(string.Format(URLFiles, Version, path)), Key, Secret, login.token, login.secret, "POST",
                oauth.GenerateTimeStamp(), oauth.GenerateNonce(), null, out url, out querystring);

            querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);

            url += "?" + querystring;

            string response = UploadData(stream, url, fileName);

            UploadResult result = new UploadResult(response);

            return result;
        }

        public class UserLogin
        {
            public string token { get; set; }
            public string secret { get; set; }
        }
    }
}