using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class Photobucket : ImageUploader, IOAuth
    {
        private const string URLRequestToken = "http://api.photobucket.com/login/request";
        private const string URLAuthorize = "http://photobucket.com/apilogin/login";
        private const string URLAccessToken = "http://api.photobucket.com/login/access";

        public string AlbumID { get; set; }
        public PhotobucketAccountInfo AccountInfo = new PhotobucketAccountInfo();

        public OAuthInfo AuthInfo { get; set; }

        public Photobucket(OAuthInfo oauth)
        {
            AuthInfo = oauth;
        }

        public Photobucket(OAuthInfo oauth, PhotobucketAccountInfo accountInfo)
            : this(oauth)
        {
            AccountInfo = accountInfo;
        }

        public string GetAuthorizationURL()
        {
            return GetAuthorizationURL(URLRequestToken, URLAuthorize, AuthInfo, null, HttpMethod.POST);
        }

        public bool GetAccessToken(string verificationCode)
        {
            AuthInfo.AuthVerifier = verificationCode;

            NameValueCollection nv = GetAccessTokenEx(URLAccessToken, AuthInfo, HttpMethod.POST);

            if (nv != null)
            {
                AccountInfo.Subdomain = nv["subdomain"];
                return !string.IsNullOrEmpty(AccountInfo.Subdomain);
            }

            return false;
        }

        public PhotobucketAccountInfo GetAccountInfo()
        {
            return AccountInfo;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            string response = UploadMedia(stream, fileName, AlbumID);

            return null;
        }

        public string UploadMedia(Stream stream, string fileName, string album)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("type", "image"); // Media type. Options are image, video, or base64.

            /*
            // Optional
            args.Add("title", ""); // Searchable title to set on the media. Maximum 250 characters.
            args.Add("description", ""); // Searchable description to set on the media. Maximum 2048 characters.
            args.Add("scramble", "false"); // Indicates if the filename should be scrambled. Options are true or false.
            args.Add("degrees", ""); // Degrees of rotation in 90 degree increments.
            args.Add("size", ""); // Size to resize an image to. (Images can only be made smaller.)
            */

            string url = string.Format("http://{0}/album/{1}/upload", AccountInfo.Subdomain, album);
            string query = OAuthManager.GenerateQuery(url, args, HttpMethod.POST, AuthInfo);

            return UploadData(stream, query, fileName, "uploadfile");
        }
    }

    public class PhotobucketAccountInfo
    {
        public string Subdomain = string.Empty;
    }
}