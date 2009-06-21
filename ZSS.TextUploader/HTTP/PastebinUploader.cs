using ZSS.TextUploader.Helpers;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.IO;

namespace ZSS.TextUploader
{
    public class PastebinUploader : TextUploader.HTTPTextUploader
    {
        /// <summary>
        /// Destination URL
        /// </summary>
        public string URL { get; set; }

        public PastebinUploader()
        {
            this.URL = "http://pastebin.com/pastebin.php";
        }

        public PastebinUploader(string url)
        {
            this.URL = url;
        }

        public override string Name
        {
            get { return "Pastebin"; }
        }

        protected override ZSS.TextUploader.Helpers.TextFileManager UploadText(string text)
        {
            List<TextFile> textFiles = new List<TextFile>();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.URL);
                request.AllowAutoRedirect = true;
                request.Method = "post";

                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("code2", HttpUtility.UrlEncode(text));
                arguments.Add("expiry", "d");       // d = 1 day, m = 1 month, f = forever
                arguments.Add("format", "csharp");  // default format: text
                arguments.Add("parent_pid", "");
                arguments.Add("paste", "Send");
                arguments.Add("poster", "name");

                string post = string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());
                byte[] data = Encoding.UTF8.GetBytes(post);

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream response = request.GetRequestStream();
                response.Write(data, 0, data.Length);
                response.Close();

                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                res.Close();

                textFiles.Add(new TextFile(text, res.ResponseUri.OriginalString));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            TextFileManager tfm = new TextFileManager(textFiles);
            return tfm;
        }

    }
}