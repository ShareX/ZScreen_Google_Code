using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZSS.TextUploaderLib;
using ZSS.TextUploaderLib.Helpers;

namespace ZSS.TextUploaderLib.URLShorteners
{
    [Serializable]
    public sealed class ThreelyUploader : TextUploader
    {
        public const string Hostname = "3.ly";
        public const string APIKey = "em5893833";

        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (ThreelyUploaderSettings)value;
            }
        }

        public ThreelyUploaderSettings HostSettings = new ThreelyUploaderSettings();

        public ThreelyUploader()
        {
            HostSettings.URL = "http://3.ly";
        }

        public override string Name
        {
            get { return Hostname; }
        }

        public override string UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("api", APIKey);
                arguments.Add("u", text);
                return GetResponse2(HostSettings.URL, arguments);
            }
            return "";
        }

        [Serializable]
        public class ThreelyUploaderSettings
        {
            public string URL { get; set; }
        }
    }
}