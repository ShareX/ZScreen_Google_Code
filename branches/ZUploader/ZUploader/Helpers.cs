using System;
using System.Drawing;
using System.IO;
using System.Text;

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

        public static byte[] GetBytes(Stream input)
        {
            input.Position = 0;
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] GetBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return GetBytes(ms);
            }
        }
    }
}