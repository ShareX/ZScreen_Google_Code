using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploader.Helpers;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace ZSS.TextUploader
{
    public abstract class TextUploader : ITextUploader
    {
        public List<string> Errors { get; protected set; }

        public abstract string UploadText(string txt);

        public Settings HostSettings { get; set; }

        public class Settings { };

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
                string text = File.ReadAllText(filePath, Encoding.UTF8);
                link = UploadText(text);
            }
            return link;
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