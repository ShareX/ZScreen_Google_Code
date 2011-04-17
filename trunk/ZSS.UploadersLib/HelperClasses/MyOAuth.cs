using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UploadersLib.HelperClasses
{
    public static class MyOAuth
    {
        private const string OAuthVersion = "1.0";

        private const string OAuthConsumerKeyKey = "oauth_consumer_key";
        private const string OAuthCallbackKey = "oauth_callback";
        private const string OAuthVersionKey = "oauth_version";
        private const string OAuthSignatureMethodKey = "oauth_signature_method";
        private const string OAuthSignatureKey = "oauth_signature";
        private const string OAuthTimestampKey = "oauth_timestamp";
        private const string OAuthNonceKey = "oauth_nonce";
        private const string OAuthTokenKey = "oauth_token";
        private const string OAuthTokenSecretKey = "oauth_token_secret";
        private const string OAuthVerifierKey = "oauth_verifier";

        private const string HMACSHA1SignatureType = "HMAC-SHA1";
        private const string PlainTextSignatureType = "PLAINTEXT";
        private const string RSASHA1SignatureType = "RSA-SHA1";

        private static Random random = new Random();

        public static string GenerateQuery(string url, Dictionary<string, string> args, string httpMethod,
            string consumerKey, string consumerSecret, string userToken, string userSecret)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add(OAuthVersionKey, OAuthVersion);
            parameters.Add(OAuthNonceKey, GenerateNonce());
            parameters.Add(OAuthTimestampKey, GenerateTimestamp());
            parameters.Add(OAuthSignatureMethodKey, HMACSHA1SignatureType);
            parameters.Add(OAuthConsumerKeyKey, consumerKey);
            parameters.Add(OAuthTokenKey, userToken);

            if (args != null)
            {
                foreach (KeyValuePair<string, string> pair in args)
                {
                    parameters.Add(pair.Key, pair.Value);
                }
            }

            StringBuilder signatureBase = new StringBuilder();
            signatureBase.AppendFormat("{0}&", httpMethod.ToUpper());
            signatureBase.AppendFormat("{0}&", Uri.EscapeDataString(url));
            string queryString = string.Join("&", parameters.Select(x => x.Key + "=" + x.Value).ToArray());
            signatureBase.AppendFormat("{0}", Uri.EscapeDataString(queryString));

            string signature = GenerateSignature(signatureBase.ToString(), consumerSecret, userSecret);

            queryString += "&" + OAuthSignatureKey + "=" + HttpUtility.UrlEncode(signature);

            return url + "?" + queryString;
        }

        private static string GenerateSignature(string signatureBase, string consumerSecret, string userSecret)
        {
            using (HMACSHA1 hmacsha1 = new HMACSHA1())
            {
                hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", HttpUtility.UrlEncode(consumerSecret),
                    string.IsNullOrEmpty(userSecret) ? string.Empty : HttpUtility.UrlEncode(userSecret)));

                byte[] dataBuffer = Encoding.ASCII.GetBytes(signatureBase.ToString());
                byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

                return Convert.ToBase64String(hashBytes);
            }
        }

        private static string GenerateTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private static string GenerateNonce()
        {
            return random.Next(123400, 9999999).ToString();
        }
    }
}