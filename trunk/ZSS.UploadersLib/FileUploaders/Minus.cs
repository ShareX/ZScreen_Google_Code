using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public class Minus : FileUploader, IOAuth
    {
        private const string APIVersion = "2";
        private const string URLAPI = "https://minus.com/api/v" + APIVersion;

        private const string URLRequestToken = URLAPI + "/oauth/request_token";
        private const string URLAuthorize = URLAPI + "/oauth/authorize";
        private const string URLAccessToken = URLAPI + "/oauth/access_token";

        public OAuthInfo AuthInfo { get; set; }
        public string FolderID { get; set; }

        public Minus(OAuthInfo oauth)
        {
            this.AuthInfo = oauth;
        }

        public MinusUser GetActiveUser()
        {
            string url = URLAPI + "/activeuser";
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusUser>(response);
        }

        public MinusUser GetUser(string slug)
        {
            string url = URLAPI + "/users/" + slug;
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusUser>(response);
        }

        public MinusFileListResponse GetFiles(string folderId)
        {
            string url = URLAPI + "/folders/" + folderId + "/files";
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusFileListResponse>(response);
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            // https://minus.com/api/v2/folders/0FQHJakL/files
            string url = URLAPI + "/folders/" + this.FolderID + "/files";

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("caption", fileName);
            args.Add("filename", fileName);

            string response = UploadData(stream, url, fileName, "file", args);

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                MinusFile minusFile = JsonConvert.DeserializeObject<MinusFile>(response);

                if (minusFile != null && !string.IsNullOrEmpty(minusFile.id))
                {
                    result.URL = minusFile.url_rawfile;
                }
            }

            return result;
        }

        public string GetAuthorizationURL()
        {
            return GetAuthorizationURL(URLRequestToken, URLAuthorize, AuthInfo);
        }

        public bool GetAccessToken(string verificationCode = null)
        {
            AuthInfo.AuthVerifier = verificationCode;
            return GetAccessToken(URLAccessToken, AuthInfo);
        }
    }

    public abstract class MinusListResponse
    {
        public int page { get; set; }
        public string next { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int pages { get; set; }
        public string previous { get; set; }
    }

    public class MinusFileListResponse : MinusListResponse
    {
        public MinusFile[] results { get; set; }
    }

    public class MinusUser
    {
        public string username { get; set; }
        public string display_name { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string slug { get; set; }
        public string fb_profile_link { get; set; }
        public string fb_username { get; set; }
        public string twitter_screen_name { get; set; }
        public int visits { get; set; }
        public int karma { get; set; }
        public int shared { get; set; }
        public string folders { get; set; }
        public string url { get; set; }
        public string avatar { get; set; }
        public int storage_used { get; set; }
        public int storage_quota { get; set; }
    }

    public class MinusFolder
    {
        public string id { get; set; }
        public string thumbnail_url { get; set; }
        public string name { get; set; }
        public bool is_public { get; set; }
        public int view_count { get; set; }
        public string creator { get; set; }
        public int file_count { get; set; }
        public DateTime date_last_updated { get; set; }
        public string files { get; set; }
        public string url { get; set; }
    }

    public class MinusFile
    {
        public string id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string caption { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int filesize { get; set; }
        public string mimetype { get; set; }
        public string folder { get; set; }
        public string url { get; set; }
        public DateTime uploaded { get; set; }
        public string url_rawfile { get; set; }
        public string url_thumbnail { get; set; }
    }
}