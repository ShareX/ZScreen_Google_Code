using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;
using Newtonsoft.Json;

namespace UploadersLib.URLShorteners
{
    public class DebliURLShortener : URLShortener
    {
        private const string APIURL = "http://deb.li/rpc/json";

        public override string Host
        {
            get { return UrlShortenerType.Debli.GetDescription(); }
        }

        public override string ShortenURL(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("add_url", url);

                string response = SendPostRequest(APIURL, args);
                
            }

            return null;
        }
    }

    public class DebliRequest
    {
        public string add_url { get; set; }
    }
}
