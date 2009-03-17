using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
