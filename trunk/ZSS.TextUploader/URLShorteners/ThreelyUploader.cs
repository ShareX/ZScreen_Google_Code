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
    public sealed class ThreelyUploader : TextUploader
    {
        public const string Hostname = "3.ly";

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

        public override string TesterString
        {
            get { return "http://code.google.com/p/zscreen"; }
        }

        public override string UploadText(TextFile text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("api", "em5893833");
                arguments.Add("u", text.LocalString);
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