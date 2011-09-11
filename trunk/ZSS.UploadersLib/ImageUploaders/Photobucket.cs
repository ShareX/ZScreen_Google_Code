using System;
using System.Collections.Generic;
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

        public OAuthInfo AuthInfo { get; set; }

        public string GetAuthorizationURL()
        {
            AuthInfo = new OAuthInfo("149828681", "d2638b653e88315aac528087e9db54e3");
            return GetAuthorizationURL(URLRequestToken, URLAuthorize, AuthInfo, null, HttpMethod.POST);
        }

        public bool GetAccessToken(string verificationCode)
        {
            AuthInfo.AuthVerifier = verificationCode;
            return GetAccessToken(URLAccessToken, AuthInfo, HttpMethod.POST);
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            return null;
        }

        public string UploadMediaToAlbum(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("identifier", ""); // Album identifier.
            args.Add("type", "image"); // Media type. Options are image, video, or base64.

            // Optional
            args.Add("title", ""); // Searchable title to set on the media. Maximum 250 characters.
            args.Add("description", ""); // Searchable description to set on the media. Maximum 2048 characters.
            args.Add("scramble", "false"); // Indicates if the filename should be scrambled. Options are true or false.
            args.Add("degrees", ""); // Degrees of rotation in 90 degree increments.
            args.Add("size", ""); // Size to resize an image to. (Images can only be made smaller.)

            return UploadData(stream, "", fileName, "uploadfile", args);
        }
    }
}