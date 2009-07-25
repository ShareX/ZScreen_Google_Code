using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZSS.TextUploadersLib.Helpers
{
    // REMINDER: DONT FCUKING BE A NOSTRADAMUS 

    public class TextInfo
    {
        private TextInfo() { }

        public string LocalString { get; set; }
        public string RemoteString { get; set; }
        public string LocalPath { get; set; }
        /// <summary>
        /// URL of the Text: pastebin URL, paste2 URL
        /// </summary>
        public string RemotePath { get; set; }

        public static TextInfo FromFile(string filePath)
        {
            TextInfo text = new TextInfo();
            if (File.Exists(filePath))
            {
                text.LocalString = File.ReadAllText(filePath);
            }
            text.LocalPath = filePath;
            return text; 
        }

        public static TextInfo FromString(string localSting)
        {
            TextInfo text = new TextInfo();
            text.LocalString = localSting;
            return text;
        }

        public static TextInfo FromClipboard()
        {
            TextInfo text = new TextInfo();
            text.LocalString = System.Windows.Forms.Clipboard.GetText();
            return text;
        }
    }

}
