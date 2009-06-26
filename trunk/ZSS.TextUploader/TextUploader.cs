using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploaders.Helpers;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ZSS.TextUploaders
{
    [Serializable]
    public abstract class TextUploader : ITextUploader
    {
        // ** THIS HAS TO BE UPTODATE OTHERWISE XML SERIALZING IS GOING TO FUCK UP ** 
        public static List<Type> Types = new List<Type> { typeof(FTPUploader), typeof(Paste2Uploader), typeof(PastebinCaUploader), typeof (PastebinUploader),
                                                          typeof(SlexyUploader), typeof(SniptUploader), typeof (TinyURLUploader), typeof(TextUploader)};

        public TextUploader() { }

        public List<string> Errors { get; set; }

        public abstract string Name { get; }

        /// <summary>
        /// String that is uploaded
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public abstract string UploadText(TextFile txt);

        /// <summary>
        /// String used to test the functionality
        /// </summary>
        public abstract string TesterString { get; }

        public virtual object Settings { get; set; }

        public string UploadTextFromClipboard()
        {
            string link = "";
            if (Clipboard.ContainsText())
            {
                link = UploadText(new TextFile(Clipboard.GetText()));
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string filePath = Clipboard.GetFileDropList()[0];
                if (filePath.EndsWith(".txt"))
                {
                    link = UploadTextFromFile(filePath);
                }
            }
            return link;
        }

        public string UploadTextFromFile(string filePath)
        {
            string link = "";
            if (File.Exists(filePath))
            {
                TextFile tf = new TextFile(File.ReadAllText(filePath));
                tf.LocalFilePath = filePath;
                link = UploadText(tf);
            }
            return link;
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        /// <summary>
        /// Descriptive name for the Text Uploader
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }

        protected string GetResponse(string url, Dictionary<string, string> arguments)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;
                request.Method = "POST";

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

        protected string GetResponse2(string url, Dictionary<string, string> arguments)
        {
            try
            {
                url += "?" + string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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