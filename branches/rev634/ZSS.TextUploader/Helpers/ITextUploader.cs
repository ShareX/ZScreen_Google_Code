using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZSS.TextUploader.Helpers
{
    interface ITextUploader
    {
        TextFileManager UploadTextFromClipboard(string cb);
        TextFileManager UploadTextFromFile(string filePath);
        string ToErrorString();
    }
}
