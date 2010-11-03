#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2010 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System.IO;
using System.Linq;

namespace HistoryLib
{
    public static class Helpers
    {
        public static bool WriteFile(Stream stream, string filePath)
        {
            if (stream != null && !string.IsNullOrEmpty(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Position = 0;
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }

                    return true;
                }
            }

            return false;
        }

        private static string[] ImageFileExtensions = { "jpg", "jpeg", "png", "gif", "bmp", "ico", "tif", "tiff" };

        public static bool IsImageFile(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            return ImageFileExtensions.Any(x => ext.EndsWith(x));
        }

        private static string[] TextFileExtensions = { "txt", "rtf", "log", "doc", "docx" };

        public static bool IsTextFile(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            return TextFileExtensions.Any(x => ext.EndsWith(x));
        }
    }
}