using System;
using System.Drawing;

namespace ZUploader
{
    public enum EDataType
    {
        File,
        Image,
        Text
    }

    public class DataManager : IDisposable
    {
        public EDataType FileType { get; set; }
        public byte[] Data { get; set; }
        public Image Image { get; set; }
        public string Text { get; set; }
        public string FileName { get; set; }

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
            }
        }
    }
}