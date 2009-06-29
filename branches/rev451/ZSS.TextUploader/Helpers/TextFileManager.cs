using System.Collections.Generic;

namespace ZSS.TextUploader.Helpers
{
    public class TextFileManager
    {
        private List<TextFile> TextFileList = new List<TextFile>();

        public TextFileManager(List<TextFile> list)
        {
            this.TextFileList = list;
        }
    }
}