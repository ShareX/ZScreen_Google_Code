using System.IO;

namespace CodeWorks
{
    public class TextInfo
    {
        public string FilePath { get; set; }
        public bool IsDifferent { get; set; }
        public string CurrentText { get; set; }
        public string NewText { get; set; }

        public TextInfo() { }

        public TextInfo(string filePath)
        {
            FilePath = filePath;
            CurrentText = File.ReadAllText(FilePath);
        }
    }
}