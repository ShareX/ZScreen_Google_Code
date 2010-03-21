using System.Drawing;
using System.IO;
using System.Text;
using System;

namespace ZUploader
{
    public static class Helpers
    {
        public static bool IsValidImageFile(Stream stream)
        {
            try
            {
                using (Image img = Image.FromStream(stream, false, false))
                {
                    return true;
                }
            }
            catch { }

            return false;
        }

        private static string[] TextFileExtensions = { "txt", "log" };

        public static bool IsValidTextFile(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                foreach (string ext in TextFileExtensions)
                {
                    if (Path.GetExtension(path).ToLower().EndsWith(ext))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetRandomAlphanumeric(int length)
        {
            Random random = new Random();
            string alphanumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

            StringBuilder sb = new StringBuilder();

            while (length-- > 0)
            {
                sb.Append(alphanumeric[(int)(random.NextDouble() * alphanumeric.Length)]);
            }

            return sb.ToString();
        }
    }
}