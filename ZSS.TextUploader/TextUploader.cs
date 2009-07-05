using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploaderLib.Helpers;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using ZSS.TextUploaderLib.URLShorteners;

namespace ZSS.TextUploaderLib
{
    [Serializable]
    public abstract class TextUploader : ITextUploader
    {
        // ** THIS HAS TO BE UP-TO-DATE OTHERWISE XML SERIALIZING IS GOING TO FUCK UP ** 
        public static List<Type> Types = new List<Type> { typeof(FTPUploader), typeof(Paste2Uploader), typeof(PastebinCaUploader), typeof (PastebinUploader),
                                                          typeof(SlexyUploader), typeof(SniptUploader), typeof(TinyURLUploader), typeof(ThreelyUploader),
                                                          typeof(KlamUploader), typeof(IsgdUploader), typeof(BitlyUploader), typeof(TextUploader)};

        public TextUploader() { }

        public List<string> Errors { get; set; }

        public virtual string Name { get; set; }

        /// <summary>
        /// String that is uploaded
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public abstract string UploadText(string text);

        /// <summary>
        /// String used to test the functionality
        /// </summary>
        public virtual string TesterString
        {
            get { return "http://code.google.com/p/zscreen"; }
        }

        public virtual object Settings { get; set; }

        public string UploadTextFromClipboard()
        {
            if (Clipboard.ContainsText())
            {
                return UploadText(Clipboard.GetText());
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string filePath = Clipboard.GetFileDropList()[0];
                if (filePath.EndsWith(".txt"))
                {
                    return UploadTextFromFile(filePath);
                }
            }
            return "";
        }

        public string UploadTextFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (this.GetType() != typeof(FTPUploader))
                {
                    filePath = File.ReadAllText(filePath);
                }
                return UploadText(filePath);
            }
            return "";
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

        /// <summary>
        /// Method to retrieve Link from Header
        /// </summary>
        /// <param name="url"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method to return Source of the Response
        /// </summary>
        /// <param name="url"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
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

    public abstract class TextUploaderSettings
    {
        public string URL { get; set; }
        public string TextFormat { get; set; }
        public string Name { get; set; }
    }
}