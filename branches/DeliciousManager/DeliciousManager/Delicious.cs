using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib;
using UploadersLib.Helpers;
using System.Collections.Specialized;
using System.Web;
using System.Reflection;

namespace DeliciousManager
{
    public class Delicious : Uploader
    {
        private const string ConsumerKey = "dj0yJmk9NndmeFl2MDJkOXI4JmQ9WVdrOVluTTJUWE5uTjJjbWNHbzlPVFEwTkRVeU5UQXomcz1jb25zdW1lcnNlY3JldCZ4PTI2";
        private const string SharedSecret = "c05ea709ffaeb523e156c07c83d97537a3f3cf85";
        private const string OAUTH_URL = "https://api.login.yahoo.com/oauth/v2/";

        public string Token, TokenSecret;

        public bool GetRequestToken()
        {
            OAuthBase oauth = new OAuthBase();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("oauth_nonce", oauth.GenerateNonce());
            arguments.Add("oauth_timestamp", oauth.GenerateTimeStamp());
            arguments.Add("oauth_consumer_key", ConsumerKey);
            arguments.Add("oauth_signature_method", "plaintext");
            arguments.Add("oauth_signature", SharedSecret + "&");
            arguments.Add("oauth_version", "1.0");
            arguments.Add("xoauth_lang_pref", "en-us");
            arguments.Add("oauth_callback", "oob");

            string url = OAUTH_URL + "get_request_token";

            string response = GetResponseString(url, arguments);

            if (!string.IsNullOrEmpty(response))
            {
                NameValueCollection qs = HttpUtility.ParseQueryString(response);

                if (qs["oauth_token"] != null)
                {
                    Token = qs["oauth_token"];
                }

                if (qs["oauth_token_secret"] != null)
                {
                    TokenSecret = qs["oauth_token_secret"];
                }

                return true;
            }

            return false;
        }

        public string GetUserPermissionLink()
        {
            if (string.IsNullOrEmpty(Token))
            {
                GetRequestToken();
            }

            OAuthBase oauth = new OAuthBase();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("oauth_token", Token);
            arguments.Add("oauth_nonce", oauth.GenerateNonce());
            arguments.Add("oauth_timestamp", oauth.GenerateTimeStamp());
            arguments.Add("oauth_consumer_key", ConsumerKey);
            arguments.Add("oauth_signature_method", "plaintext");
            arguments.Add("oauth_signature", SharedSecret + "&");
            arguments.Add("oauth_version", "1.0");
            arguments.Add("xoauth_lang_pref", "en-us");
            arguments.Add("oauth_callback", "oob");

            string url = OAUTH_URL + "request_auth";

            url += "?" + string.Join("&", arguments.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value)).ToArray());

            return url;
        }

        public AccessTokenResponse GetAccessToken(string verifier)
        {
            OAuthBase oauth = new OAuthBase();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("oauth_consumer_key", ConsumerKey);
            arguments.Add("oauth_signature_method", "PLAINTEXT");
            arguments.Add("oauth_version", "1.0");
            arguments.Add("oauth_verifier", verifier);
            arguments.Add("oauth_token", Token);
            arguments.Add("oauth_nonce", oauth.GenerateNonce());
            arguments.Add("oauth_timestamp", oauth.GenerateTimeStamp());
            arguments.Add("oauth_signature", SharedSecret + "&" + TokenSecret);

            string url = OAUTH_URL + "get_token";

            string response = GetResponseString(url, arguments);

            if (!string.IsNullOrEmpty(response))
            {
                return ParseAccessTokenResponse(response);
            }

            return null;
        }

        public AccessTokenResponse ParseAccessTokenResponse(string response)
        {
            NameValueCollection qs = HttpUtility.ParseQueryString(response);

            AccessTokenResponse atr = new AccessTokenResponse();

            foreach (FieldInfo fi in typeof(AccessTokenResponse).GetFields())
            {
                if (qs[fi.Name] != null)
                {
                    fi.SetValue(atr, qs[fi.Name]);
                }
            }

            return atr;
        }

        public class AccessTokenResponse
        {
            /// <summary>
            /// The Access Token provides access to protected resources accessible through Yahoo! Web services.
            /// </summary>
            public string oauth_token;

            /// <summary>
            /// The secret associated with the Access Token provided in hexstring format.
            /// </summary>
            public string oauth_token_secret;

            /// <summary>
            /// The persistent credential used by Yahoo! to identify the Consumer after a User has authorized access to private data.
            /// Include this credential in your request to refresh the Access Token once it expires.
            /// </summary>
            public string oauth_session_handle;

            /// <summary>
            /// Lifetime of the Access Token in seconds (3600, or 1 hour).
            /// </summary>
            public string oauth_expires_in;

            /// <summary>
            /// Lifetime of the oauth_session_handle in seconds.
            /// </summary>
            public string oauth_authorization_expires_in;

            /// <summary>
            /// The introspective GUID of the currently logged in User.
            /// </summary>
            public string xoauth_yahoo_guid;
        }
    }
}