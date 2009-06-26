namespace ZSS.TextUploadersLib.Helpers
{
    public class TextFile
    {
        public TextFile(string localText)
        {            
            this.LocalString = localText;
        }

        public TextFile(string localText, string url)
            : this(localText)
        {
            this.URL = url;
        }

        public string LocalString { get; set; }
        public string RemoteString { get; set; }
        public string LocalFilePath { get; set; }
        /// <summary>
        /// URL of the Text: pastebin URL, paste2 URL
        /// </summary>
        public string URL { get; set; }
    }
}