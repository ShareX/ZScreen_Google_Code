using System;
using System.Collections.Generic;
using ZSS.TextUploaderLib;
using ZSS.TextUploaderLib.Helpers;

namespace ZSS.TextUploaderLib.URLShorteners
{
    [Serializable]
    public sealed class IsgdUploader : TextUploader
    {
        public const string Hostname = "is.gd";
      
        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (IsgdUploaderSettings)value;
            }
        }

        public IsgdUploaderSettings HostSettings = new IsgdUploaderSettings();

        public IsgdUploader()
        {
            HostSettings.URL = "http://is.gd/api.php";
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
                arguments.Add("longurl", text);            

                return GetResponse2(HostSettings.URL, arguments);
            }
            return "";
        }

        [Serializable]
        public class IsgdUploaderSettings
        {
            public string URL { get; set; }
        }
    }
}