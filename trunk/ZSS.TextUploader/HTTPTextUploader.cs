using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploader.Helpers;
using System.Net;
using System.IO;

namespace ZSS.TextUploader
{
    public abstract class HTTPTextUploader : ITextUploader
    {
        public abstract string Name { get; }
        public List<string> Errors { get; protected set; }

        protected string GetResponse(string url, IDictionary<string, string> arguments)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.AllowAutoRedirect = false;
            request.Method = "POST";

            string post = arguments.Aggregate("?", (current, arg) => current + arg.Key + "=" + arg.Value + "&");
            post = post.Substring(0, post.Length - 1);
            byte[] data = Encoding.ASCII.GetBytes(post);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream response = request.GetRequestStream();

            response.Write(data, 0, data.Length);

            response.Close();

            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            res.Close();
            // note that there is no need to hook up a StreamReader and
            // look at the response data, since it is of no need

            if (res.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine(res.Headers["location"]);
            }
            else
            {
                Console.WriteLine("Error");
            }
            return "";
        }

        protected abstract TextFileManager UploadText(string txt);

        public TextFileManager UploadTextFromClipboard(string cb)
        {
            TextFileManager tfm = UploadText(cb);
            return tfm;
        }

        public TextFileManager UploadTextFromFile(string filePath)
        {
            TextFileManager tfm;
            using (StreamReader sr = new StreamReader(filePath))
            {
                tfm = UploadText(sr.ReadToEnd());
            }
            return tfm;
        }

        public string ToErrorString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string err in this.Errors)
            {
                sb.AppendLine(err);
            }
            return sb.ToString();
        }
    }
}