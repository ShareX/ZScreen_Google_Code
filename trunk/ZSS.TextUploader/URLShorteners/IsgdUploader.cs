﻿using System;
using System.Collections.Generic;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.Helpers;

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

        public override string TesterString
        {
            get { return "http://code.google.com/p/zscreen"; }
        }

        public override string UploadText(TextFile text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("longurl", text.LocalString);            

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
