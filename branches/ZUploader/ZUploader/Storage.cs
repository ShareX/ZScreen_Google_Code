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

    public class DataManager
    {
        public EDataType FileType { get; set; }
        public Stream Data { get; set; }
        public Image Image { get; set; }
        public string Text { get; set; }
        public string FileName { get; set; }
    }
}