using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Web;

namespace UploadersLib.HelperClasses
{
    public class TwitterAuthInfo
    {
        [Category("Twitter OAuth Settings for Tweeting"), Description("Description of your Twitter Account")]
        public string AccountName { get; set; }
        [Category("Twitter OAuth Settings for Tweeting"), Description("Token used for the Authorization Page")]
        public string OAuthToken { get; set; }
        [Category("Twitter OAuth Settings for Tweeting"), Browsable(false), PasswordPropertyText(true)]
        public string Token { get; set; }
        [Category("Twitter OAuth Settings for Tweeting"), Description("Enter PIN here from the Authorization Page that you get after pressing Add button")]
        public string PIN { get; set; }
        [Category("Twitter OAuth Settings for Tweeting"), Description("TokenSecret is automatically retrieved when you press the Authorize button"), PasswordPropertyText(true)]
        public string TokenSecret { get; set; }
        [Category("Twitter Account settings for Twitter based Image Uploaders"), Description("Twitter Username")]
        public string UserName { get; set; }
        [Category("Twitter Account settings for Twitter based Image Uploaders"), Description("Twitter Password"), PasswordPropertyText(true)]
        public string Password { get; set; }

        public TwitterAuthInfo()
        {
            this.AccountName = "New Account";
        }

        public override string ToString()
        {
            return this.AccountName;
        }
    }

    public class oAuthTwitter : OAuthBase
    {
        public enum Method { GET, POST };

        public const string REQUEST_TOKEN = "http://twitter.com/oauth/request_token";
        public const string AUTHORIZE = "http://twitter.com/oauth/authorize";
        public const string ACCESS_TOKEN = "http://twitter.com/oauth/access_token";

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public bool Enabled { get; set; }
        public TwitterAuthInfo AuthInfo { get; set; }

        public oAuthTwitter(string consumerKey, string consumerSecret)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
            this.AuthInfo = new TwitterAuthInfo();
        }

        public oAuthTwitter(string consumerKey, string consumerSecret, TwitterAuthInfo authInfo)
            : this(consumerKey, consumerSecret)
        {
            this.AuthInfo = authInfo;
        }

        public override string ToString()
        {
            return AuthInfo.AccountName;
        }

        /// <summary>
        /// Get the link to Twitter's authorization page for this application.
        /// </summary>
        /// <returns>The url with a valid request token, or a null string.</returns>
        public string AuthorizationLinkGet()
        {
            string ret = null;

            // First let's get a REQUEST token.
            string response = oAuthWebRequest(Method.GET, REQUEST_TOKEN, String.Empty);
            if (response.Length > 0)
            {
                //response contains token and token secret.  We only need the token.
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    AuthInfo.OAuthToken = qs["oauth_token"]; // tuck this away for later
                    ret = AUTHORIZE + "?oauth_token=" + qs["oauth_token"];// +"&oauth_callback=oob";
                }
            }
            return ret;
        }

        /// <summary>
        /// Exchange the request token for an access token.
        /// </summary>
        /// <param name="authToken">The oauth_token is supplied by Twitter's authorization page following the callback.</param>
        public TwitterAuthInfo AccessTokenGet(ref TwitterAuthInfo acc)
        {
            this.AuthInfo = acc;
            this.AuthInfo.Token = acc.OAuthToken;
            string response = oAuthWebRequest(Method.GET, ACCESS_TOKEN, String.Empty);

            if (response.Length > 0)
            {
                //Store the Token and Token Secret
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    this.AuthInfo.Token = qs["oauth_token"];
                }
                if (qs["oauth_token_secret"] != null)
                {
                    this.AuthInfo.TokenSecret = qs["oauth_token_secret"];
                }
            }

            return this.AuthInfo;
        }

        /// <summary>
        /// Submit a web request using oAuth.
        /// </summary>
        /// <param name="method">GET or POST</param>
        /// <param name="url">The full url, including the querystring.</param>
        /// <param name="postData">Data to post (querystring format)</param>
        /// <returns>The web server response.</returns>
        public string oAuthWebRequest(Method method, string url, string postData)
        {
            string outUrl = "";
            string querystring = "";
            string ret = "";

            //Setup postData for signing.
            //Add the postData to the querystring.
            if (method == Method.POST)
            {
                if (postData.Length > 0)
                {
                    //Decode the parameters and re-encode using the oAuth UrlEncode method.
                    NameValueCollection qs = HttpUtility.ParseQueryString(postData);
                    postData = "";
                    foreach (string key in qs.AllKeys)
                    {
                        if (postData.Length > 0)
                        {
                            postData += "&";
                        }
                        qs[key] = HttpUtility.UrlDecode(qs[key]);
                        qs[key] = this.UrlEncode(qs[key]);
                        postData += key + "=" + qs[key];
                    }
                    if (url.IndexOf("?") > 0)
                    {
                        url += "&";
                    }
                    else
                    {
                        url += "?";
                    }
                    url += postData;
                }
            }
            else if (method == Method.GET && !String.IsNullOrEmpty(postData))
            {
                url += "?" + postData;
            }

            Uri uri = new Uri(url);

            string nonce = this.GenerateNonce();
            string timeStamp = this.GenerateTimeStamp();

            //Generate Signature
            string sig = this.GenerateSignature(uri,
                this.ConsumerKey,
                this.ConsumerSecret,
                this.AuthInfo.Token,
                this.AuthInfo.TokenSecret,
                method.ToString(),
                timeStamp,
                nonce,
                this.AuthInfo.PIN,
                out outUrl,
                out querystring);

            querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);

            //Convert the querystring to postData
            if (method == Method.POST)
            {
                postData = querystring;
                querystring = "";
            }

            if (querystring.Length > 0)
            {
                outUrl += "?";
            }

            ret = WebRequest(method, outUrl + querystring, postData);

            return ret;
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string WebRequest(Method method, string url, string postData)
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Proxy = Uploader.ProxySettings.GetWebProxy;
            //webRequest.UserAgent  = "Identify your application please.";
            //webRequest.Timeout = 20000;

            if (method == Method.POST)
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";

                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }
            }

            responseData = WebResponseGet(webRequest);

            webRequest = null;

            return responseData;
        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        public string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }

        public void Reset()
        {
            ConsumerKey = ConsumerSecret = String.Empty;
            this.AuthInfo = new TwitterAuthInfo();
        }
    }
}