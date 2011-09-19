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

        private string URLAuth = string.Empty;
        private const string URLAccessToken = URLAPI + "/oauth/token";

        public OAuthInfo AuthInfo { get; set; }
        public MinusOptions Config { get; private set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public Minus(MinusOptions config)
        {
            this.Config = config;
            this.AuthInfo = config.AuthInfo;
        }

        public Minus(MinusOptions config, string username, string password)
            : this(config)
        {
            this.UserName = username;
            this.Password = password;
        }

        public string GetAuthorizationURL()
        {
            URLAuth = string.Format("{0}/oauth/token?grant_type=password&client_id={1}&client_secret={2}&scope=upload_new&username={3}&password={4}",
                URLAPI,
               Config.AuthInfo.ConsumerKey,
                Config.AuthInfo.ConsumerSecret,
                this.UserName,
                this.Password);

            return URLAuth;
        }

        public bool GetAccessToken(string verificationCode = null)
        {
            string response = SendGetRequest(URLAuth);
            MinusAuthToken mat = JsonConvert.DeserializeObject<MinusAuthToken>(response);

            if (mat != null)
            {
                Config.AuthInfo.UserToken = mat.access_token;
                Config.AuthInfo.UserSecret = mat.refresh_token;
            }

            return mat != null;
        }

        public List<MinusFolder> ReadFolderList()
        {
            MinusFolderListResponse mflr = GetUserFolderList();

            if (mflr.results.Length > 0)
            {
                Config.FolderList.Clear();
                for (int i = 0; i < mflr.results.Length; i++)
                {
                    Config.FolderList.Add(mflr.results[i]);
                }
                Config.FolderID = 0;
            }

            return Config.FolderList;
        }

        public void RefreshAccessToken()
        {
            string url = string.Format("{0}/oauth/token?grant_type=refresh_token&client_id={1}&client_secret={2}&scope=read_public&refresh_token={3}",
                URLAPI, Config.AuthInfo.ConsumerKey, Config.AuthInfo.ConsumerSecret, Config.AuthInfo.UserToken);

            string response = SendGetRequest(url);
            MinusAuthToken mat = JsonConvert.DeserializeObject<MinusAuthToken>(response);

            if (mat != null)
            {
                Config.AuthInfo.UserToken = mat.access_token;
                Config.AuthInfo.UserSecret = mat.refresh_token;
            }
        }

        public MinusUser GetActiveUser()
        {
            string url = URLAPI + "/activeuser?bearer_token=" + Config.AuthInfo.UserToken;
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusUser>(response);
        }

        public MinusUser GetUser(string slug)
        {
            string url = URLAPI + "/users/" + slug;
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusUser>(response);
        }

        private string GetActiveUserFolderURL()
        {
            MinusUser user = Config.MinusUser != null ? Config.MinusUser : Config.MinusUser = GetActiveUser();
            string url = URLAPI + "/users/" + user.slug + "/folders?bearer_token=" + Config.AuthInfo.UserToken;
            Console.WriteLine(url);
            return url;
        }
        public MinusFolderListResponse GetUserFolderList()
        {
            string response = SendGetRequest(GetActiveUserFolderURL());
            return JsonConvert.DeserializeObject<MinusFolderListResponse>(response);
        }

        public MinusFolder CreateFolder(string name, bool is_public)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("name", name);
            args.Add("is_public", is_public.ToString().ToLower());

            MinusFolder dir = null;
            string response = SendPostRequestURLEncoded(GetActiveUserFolderURL(), args);
            if (!string.IsNullOrEmpty(response))
            {
                dir = JsonConvert.DeserializeObject<MinusFolder>(response);
                if (dir != null)
                {
                    Config.FolderList.Add(dir);
                }
            }
            return dir;
        }

        public bool DeleteFolder(string id)
        {
            string url = GetFolderLinkFromID(id);
            string resp = SendDeleteRequest(url);
            return !string.IsNullOrEmpty(resp);
        }

        public MinusFileListResponse GetFiles(string folderId)
        {
            string url = URLAPI + "/folders/" + folderId + "/files?bearer_token=" + Config.AuthInfo.UserToken;
            string response = SendGetRequest(url);
            return JsonConvert.DeserializeObject<MinusFileListResponse>(response);
        }

        private string GetFolderLinkFromID(string id)
        {
            return URLAPI + "/folders/" + id + "/files?bearer_token=" + Config.AuthInfo.UserToken;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            string url = GetFolderLinkFromID(Config.FolderList[Config.FolderID].id);

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("caption", fileName);
            args.Add("filename", fileName);

            string response = UploadData(stream, url, fileName, "file", args);

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                MinusFile minusFile = JsonConvert.DeserializeObject<MinusFile>(response);

                if (minusFile != null)
                {
                    result.URL = minusFile.url_rawfile;
                    result.ThumbnailURL = minusFile.url_thumbnail;
                }
            }

            return result;
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

    public class MinusOptions
    {
        public OAuthInfo AuthInfo { get; set; }
        public MinusUser MinusUser { get; set; }
        public List<MinusFolder> FolderList = new List<MinusFolder>();
        public int FolderID { get; set; }
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
        public long storage_used { get; set; }
        public long storage_quota { get; set; }

        public override string ToString()
        {
            return this.username;
        }
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

        public override string ToString()
        {
            return this.name;
        }
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

        public override string ToString()
        {
            return this.url_rawfile;
        }
    }
}