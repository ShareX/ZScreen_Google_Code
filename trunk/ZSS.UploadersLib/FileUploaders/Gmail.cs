using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public class Gmail : FileUploader, IOAuth
    {
        private const string URLAuthorize = "https://www.google.com/accounts/OAuthAuthorizeToken";
        private const string URL_AUTH_REQUEST_BASE = "https://mail.google.com/mail/b/";
        private EmailProtocol Protocol = EmailProtocol.Smtp;
        private string URL_OAuthRequest = URL_AUTH_REQUEST_BASE;

        public OAuthInfo AuthInfo { get; set; }

        public Gmail(string email_address, OAuthInfo oauth)
        {
            this.URL_OAuthRequest = URL_AUTH_REQUEST_BASE + email_address + "/" + this.Protocol.ToString().ToLower() + "/?xoauth_requestor_id=" + System.Web.HttpUtility.UrlEncode(email_address);
            this.AuthInfo = oauth;
        }

        public string GetAuthorizationURL()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("scope", "https://mail.google.com");
            args.Add("xoauth_displayname", Application.ProductName);

            string url = OAuthManager.GenerateQuery(URL_OAuthRequest, args, HttpMethod.Get, AuthInfo);
            string response = SendRequest(HttpMethod.Get, url);
            if (!string.IsNullOrEmpty(response))
            {
                return OAuthManager.GetAuthorizationURL(response, AuthInfo, URLAuthorize);
            }

            return null;
        }

        public bool GetAccessToken(string verificationCode)
        {
            throw new NotImplementedException();
        }

        public override UploadResult Upload(System.IO.Stream stream, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}