using System.Collections.Generic;
using UploadersLib.HelperClasses;

namespace UploadersLib.TextServices
{
    public class MyTwitter : Uploader
    {
        private const string APIVersion = "1";
        private const string URLRequestToken = "http://twitter.com/oauth/request_token";
        private const string URLAuthorize = "http://twitter.com/oauth/authorize";
        private const string URLAccessToken = "http://twitter.com/oauth/access_token";
        private const string URLTweet = "http://api.twitter.com/" + APIVersion + "/statuses/update.xml";

        public OAuthInfo AuthInfo { get; set; }

        public string GetAuthorizationURL()
        {
            return GetAuthorizationURL(URLRequestToken, URLAuthorize, AuthInfo);
        }

        public bool GetAccessToken(string verificationCode)
        {
            AuthInfo.AuthVerifier = verificationCode;
            return GetAccessToken(URLAccessToken, AuthInfo);
        }

        public string TweetMessage(string message)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("status", message);

            string query = OAuthManager.GenerateQuery(URLTweet, args, HttpMethod.GET, AuthInfo);

            string response = GetResponse(query, args);

            // TODO: Twitter

            return null;
        }
    }
}