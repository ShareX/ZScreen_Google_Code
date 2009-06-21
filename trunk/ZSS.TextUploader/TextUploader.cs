using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploaders.Helpers;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace ZSS.TextUploaders
{
    public abstract class TextUploader : ITextUploader
    {
        public List<string> Errors { get; protected set; }

        public abstract string Name { get; }

        public abstract string UploadText(string txt);

        public virtual object Settings { get; set; }

        public string UploadTextFromClipboard()
        {
            string link = "";
            if (Clipboard.ContainsText())
            {
                link = UploadText(Clipboard.GetText());
            }
            return link;
        }

        public string UploadTextFromFile(string filePath)
        {
            string link = "";
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                link = UploadText(text);
            }
            return link;
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        protected string GetResponse(string url, Dictionary<string, string> arguments)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;
                request.Method = "post";

                string post = string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());
                byte[] data = Encoding.UTF8.GetBytes(post);

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream response = request.GetRequestStream();
                response.Write(data, 0, data.Length);
                response.Close();

                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                res.Close();

                return res.ResponseUri.OriginalString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return "";
        }

        protected string CombineURL(string url1, string url2)
        {
            if (string.IsNullOrEmpty(url1) || string.IsNullOrEmpty(url2))
            {
                return "";
            }
            if (url1.EndsWith("/"))
            {
                url1 = url1.Substring(0, url1.Length - 1);
            }
            if (url2.StartsWith("/"))
            {
                url2 = url2.Remove(0, 1);
            }
            return url1 + "/" + url2;
        }
    }
}