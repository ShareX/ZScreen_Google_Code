namespace ZSS.TextUploader.Helpers
{
    interface ITextUploader
    {
        TextFileManager UploadTextFromClipboard(string cb);
        TextFileManager UploadTextFromFile(string filePath);
        string ToErrorString();
    }
}