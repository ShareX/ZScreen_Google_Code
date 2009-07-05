using System.Xml.Serialization;
using System;

namespace ZSS.TextUploaderLib.Helpers
{
    interface ITextUploader
    {
        string UploadText(string text);
        string UploadTextFromClipboard();
        string UploadTextFromFile(string filePath);
        string ToErrorString();
    }
}