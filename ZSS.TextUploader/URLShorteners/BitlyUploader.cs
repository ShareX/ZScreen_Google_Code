using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.Helpers;

namespace ZSS.TextUploaderLib.URLShorteners
{
    [Serializable]
    public sealed class BitlyUploader : TextUploader
    {
        public const string Hostname = "bit.ly";
        public const string APILogin = "McoreD";
        public const string APIKey = "R_55cef8c7f08a07d2ecd4323084610161";

        // http://api.bit.ly/shorten?version=2.0.1&longUrl=http://cnn.com&login=bitlyapidemo&apiKey=R_0da49e0a9118ff35f52f629d2d71bf07 
        // http://api.bit.ly/shorten?version=2.0.1?longUrl=http://code.google.com/p/zscreen&login=mcored&apiKey=R_55cef8c7f08a07d2ecd4323084610161"

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
            HostSettings.URL = "http://api.bit.ly/shorten?version=2.0.1";
        }

        public override string Name
        {
            get { return Hostname; }
        }

        public override string TesterString
        {
            get { return "http://code.google.com/p/zscreen/wiki/TipsTricks"; }
        }

        public override string UploadText(TextFile text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("longUrl", HttpUtility.UrlEncode(text.LocalString));
                arguments.Add("login", APILogin);
                arguments.Add("apiKey", APIKey);                
                return GetResponse2(HostSettings.URL, arguments);
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