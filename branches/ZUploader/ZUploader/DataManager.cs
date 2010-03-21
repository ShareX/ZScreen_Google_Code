using System;
using System.Drawing;
using System.IO;

namespace ZUploader
{
    public enum EDataType
    {
        Data,
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