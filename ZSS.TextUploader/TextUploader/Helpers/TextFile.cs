using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZSS.TextUploader.Helpers
{

   public class TextFile
    {
        public string LocalText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemoteText { get; set; }
        public string LocalFilePath {get; set;}
        /// <summary>
        /// URL of the Text: pastebin url, paste2 url
        /// </summary>
        public string URL { get; set; }
    }
}
