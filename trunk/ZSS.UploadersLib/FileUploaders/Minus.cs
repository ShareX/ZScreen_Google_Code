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

        private const string URLAuthorize = URLAPI + "/oauth/token";
        private const string URLAccessToken = URLAPI + "/oauth/token";

        public MinusAccountInfo AccountInfo { get; set; }
        public OAuthInfo AuthInfo { get; set; }

        public string FolderID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Minus(OAuthInfo oauth)
        {
            this.AuthInfo = oauth;
        }

        public Minus(OAuthInfo oauth, string username, string password)
            : this(oauth)
        {
            this.UserName = username;
            this.Password = password;
        }

        public Minus(OAuthInfo oauth, MinusAccountInfo account)
            : this(oauth)
        {
            this.AccountInfo = account;
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

        public MinusFolderListResponse GetUserFolderList()
        {
            MinusUser user = AccountInfo != null ? AccountInfo.UserAccount : GetActiveUser();
            string url = URLAPI + "/users/" + user.slug + "/folders?bearer_token=" + AuthInfo.AuthToken;
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusFolderListResponse>(response);
        }

        private string GetActiveUserFolderURL()
        {
            MinusUser user = AccountInfo != null ? AccountInfo.UserAccount : GetActiveUser();
            string url = URLAPI + "/users/" + user.slug + "/folders?bearer_token=" + AuthInfo.AuthToken;
            return url;
        }

        public MinusFolder CreateFolder(string name, bool is_public)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("name", name);
            args.Add("is_public", is_public.ToString());

            string response = SendPostRequest(GetActiveUserFolderURL(), args);
            MinusFolder dir = JsonConvert.DeserializeObject<MinusFolder>(response);
            return dir;
        }

        public MinusFileListResponse GetFiles(string folderId)
        {
            string url = URLAPI + "/folders/" + folderId + "/files?bearer_token=" + AuthInfo.AuthToken;
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusFileListResponse>(response);
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            string url = URLAPI + "/folders/" + this.FolderID + "/files?bearer_token=" + AuthInfo.AuthToken;

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
                    result.ThumbnailURL = minusFile.url_thumbnail;
                }
            }

            return result;
        }

        public string GetAuthorizationURL()
        {
            string url = string.Format("{0}?grant_type=password&client_id={1}&client_secret={2}&scope=modify_all&username={3}&password={4}",
                URLAuthorize,
                AuthInfo.ConsumerKey,
                AuthInfo.ConsumerSecret,
                this.UserName,
                this.Password);

            string response = SendGetRequest(url);
            MinusAuthToken mat = JsonConvert.DeserializeObject<MinusAuthToken>(response);

            AuthInfo.AuthToken = mat.access_token;
            return url;
        }

        public bool GetAccessToken(string verificationCode = null)
        {
            AuthInfo.AuthVerifier = verificationCode;
            return GetAccessToken(URLAccessToken, AuthInfo);
        }
    }

    public class MinusAuthToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expire_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
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

    public class MinusFolderListResponse : MinusListResponse
    {
        public MinusFolder[] results { get; set; }
    }

    public class MinusFileListResponse : MinusListResponse
    {
        public MinusFile[] results { get; set; }
    }

    public class MinusAccountInfo
    {
        public MinusUser UserAccount { get; set; }
        public List<string> FolderList = new List<string>();
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