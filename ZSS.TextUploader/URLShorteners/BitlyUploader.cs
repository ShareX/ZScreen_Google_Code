using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.Helpers;
using System.Xml.Linq;
using System.Xml;

namespace ZSS.TextUploaderLib.URLShorteners
{
    [Serializable]
    public sealed class BitlyUploader : TextUploader
    {
        public const string Hostname = "bit.ly";
        public const string APILogin = "mcored";
        public const string APIKey = "R_55cef8c7f08a07d2ecd4323084610161";

        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (BitlyUploaderSettings)value;
            }
        }

        public BitlyUploaderSettings HostSettings = new BitlyUploaderSettings();

        public BitlyUploader()
        {
            HostSettings.URL = "http://api.bit.ly/shorten";
        }

        public override string Name
        {
            get { return Hostname; }
        }

        // http://api.bit.ly/shorten?version=2.0.1&longUrl=http://code.google.com/p/zscreen&login=mcored&apiKey=R_55cef8c7f08a07d2ecd4323084610161"

        public override string UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("version", "2.0.1");
                arguments.Add("longUrl", HttpUtility.UrlEncode(text));
                arguments.Add("login", APILogin);
                arguments.Add("apiKey", APIKey);
                arguments.Add("format", "xml");
                string result = GetResponse2(HostSettings.URL, arguments);
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(result);
                XmlNode xnode = xdoc.SelectSingleNode("bitly/results/nodeKeyVal/shortUrl");
                if (xnode != null) return xnode.InnerText;
            }
            return "";
        }

        [Serializable]
        public class BitlyUploaderSettings
        {
            public string URL { get; set; }
        }
    }
}