namespace ZSS.TextUploaders.Helpers
{
    public class TextFile
    {
        public TextFile(string localText)
        {
            this.LocalText = localText;
        }

        public TextFile(string localText, string url)
            : this(localText)
        {
            this.URL = url;
        }

        public string LocalText { get; set; }
        public string RemoteText { get; set; }
        public string LocalFilePath { get; set; }
        /// <summary>
        /// URL of the Text: pastebin URL, paste2 URL
        /// </summary>
        public string URL { get; set; }
    }
}