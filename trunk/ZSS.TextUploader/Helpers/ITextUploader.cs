using System.Xml.Serialization;
using System;

namespace ZSS.TextUploadersLib.Helpers
{
    interface ITextUploader
    {
        string UploadText(TextFile text);
        string UploadTextFromClipboard();
        string UploadTextFromFile(string filePath);
        string ToErrorString();
    }
}