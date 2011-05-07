#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace HelpersLib
{
    public static class Helpers
    {
        private static readonly object ClipboardLock = new object();
        private static readonly Random Random = new Random();

        private static bool IsValidFile(string path, Type enumType)
        {
            string ext = Path.GetExtension(path);

            if (!string.IsNullOrEmpty(ext) && ext.Length > 1)
            {
                ext = ext.Remove(0, 1);
                return Enum.GetNames(enumType).Any(x => ext.Equals(x, StringComparison.InvariantCultureIgnoreCase));
            }

            return false;
        }

        public static bool IsImageFile(string path)
        {
            return IsValidFile(path, typeof(ImageFileExtensions));
        }

        public static bool IsTextFile(string path)
        {
            return IsValidFile(path, typeof(TextFileExtensions));
        }

        public static bool CopyTextSafely(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    lock (ClipboardLock)
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(text); // ok (unused)
                    }
                }
                catch (ExternalException)
                {
                    return false;
                }
            }

            return true;
        }

        public static void CopyFileToClipboard(string path)
        {
            try
            {
                Clipboard.SetFileDropList(new StringCollection() { path });
            }
            catch { }
        }

        public static void CopyImageFileToClipboard(string path)
        {
            try
            {
                using (Image img = Image.FromFile(path))
                {
                    Clipboard.SetImage(img);
                }
            }
            catch { }
        }

        public static void CopyTextFileToClipboard(string path)
        {
            string text = File.ReadAllText(path);
            CopyTextSafely(text);
        }

        /// <summary>Function to get a Rectangle of all the screens combined</summary>
        public static Rectangle GetScreenBounds()
        {
            Point topLeft = new Point(0, 0);
            Point bottomRight = new Point(0, 0);
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.X < topLeft.X) topLeft.X = screen.Bounds.X;
                if (screen.Bounds.Y < topLeft.Y) topLeft.Y = screen.Bounds.Y;
                if ((screen.Bounds.X + screen.Bounds.Width) > bottomRight.X) bottomRight.X = screen.Bounds.X + screen.Bounds.Width;
                if ((screen.Bounds.Y + screen.Bounds.Height) > bottomRight.Y) bottomRight.Y = screen.Bounds.Y + screen.Bounds.Height;
            }
            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        public static string AddZeroes(int number, int digits = 2)
        {
            return number.ToString("d" + digits);
        }

        public static string HourTo12(int hour)
        {
            if (hour == 0)
            {
                return (12).ToString();
            }
            else if (hour > 12)
            {
                return AddZeroes(hour - 12);
            }

            return AddZeroes(hour);
        }

        public static string GetRandomString(string chars, int length)
        {
            StringBuilder sb = new StringBuilder();

            while (length-- > 0)
            {
                sb.Append(GetRandomChar(chars));
            }

            return sb.ToString();
        }

        public static char GetRandomChar(string chars)
        {
            return chars[Random.Next(chars.Length)];
        }

        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        public const string AlphabetCapital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Numbers = "0123456789";
        public const string Alphanumeric = Alphabet + AlphabetCapital + Numbers;

        /// <summary>0123456789</summary>
        public static string GetRandomNumber(int length)
        {
            return GetRandomString(Numbers, length);
        }

        /// <summary>abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789</summary>
        public static string GetRandomAlphanumeric(int length)
        {
            return GetRandomString(Alphanumeric, length);
        }

        public static string ReplaceIllegalChars(string filename, char replace)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in filename)
            {
                if (IsCharValid(c))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(replace);
                }
            }

            return sb.ToString();
        }

        /// <summary>Valid chars: abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789()-._!</summary>
        public static bool IsCharValid(char c)
        {
            string chars = Alphanumeric + "()-._!";

            foreach (char c2 in chars)
            {
                if (c == c2)
                {
                    return true;
                }
            }

            return false;
        }

        public static string NormalizeString(string text, bool convertSpace = true, bool isFolderPath = false)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in text)
            {
                // @ is for HttpHomePath we use in FTP Account
                if (IsCharValid(c) || (isFolderPath && (c == Path.DirectorySeparatorChar || c == '/' || c == '.' || c == '@')))
                {
                    result.Append(c);
                }
                else if (c == ' ')
                {
                    result.Append('_');
                }
            }

            while (result.Length > 0 && result[0] == '.')
            {
                result.Remove(0, 1);
            }

            return result.ToString();
        }

        public static string GetXMLValue(string input, string tag)
        {
            return Regex.Match(input, String.Format("(?<={0}>).+?(?=</{0})", tag)).Value;
        }

        public static string GetMD5(byte[] data)
        {
            byte[] bytes = new MD5CryptoServiceProvider().ComputeHash(data);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString().ToLower();
        }

        public static string GetMD5(string text)
        {
            return GetMD5(Encoding.UTF8.GetBytes(text));
        }

        public static string CombineURL(string url1, string url2)
        {
            if (string.IsNullOrEmpty(url1) || string.IsNullOrEmpty(url2))
            {
                if (!string.IsNullOrEmpty(url1))
                {
                    return url1;
                }
                else if (!string.IsNullOrEmpty(url2))
                {
                    return url2;
                }

                return string.Empty;
            }

            if (url1.EndsWith("/"))
            {
                url1 = url1.Substring(0, url1.Length - 1);
            }

            if (url2.StartsWith("/"))
            {
                url2 = url2.Remove(0, 1);
            }

            return url1 + "/" + url2;
        }

        public static string CombineURL(params string[] urls)
        {
            return urls.Aggregate((current, arg) => CombineURL(current, arg));
        }

        public static string GetMimeType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                return regKey.GetValue("Content Type").ToString();
            }

            return "application/octet-stream";
        }

        public static int GetEnumLength<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        public static string[] GetEnumDescriptions<T>()
        {
            List<string> descriptions = new List<string>();
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            foreach (int value in Enum.GetValues(enumType))
            {
                descriptions.Add(((Enum)Enum.ToObject(enumType, value)).GetDescription());
            }

            return descriptions.ToArray();
        }

        public static string GetForegroundWindowText()
        {
            IntPtr hWnd = NativeMethods.GetForegroundWindow();
            if (hWnd.ToInt32() > 0)
            {
                int length = NativeMethods.GetWindowTextLength(hWnd);
                if (length > 0)
                {
                    StringBuilder sb = new StringBuilder(length + 1);
                    if (NativeMethods.GetWindowText(hWnd, sb, sb.Capacity) > 0)
                    {
                        return sb.ToString();
                    }
                }
            }

            return null;
        }

        public const string UnreservedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_.~";

        public static string URLEncode(string text)
        {
            StringBuilder result = new StringBuilder();

            foreach (char symbol in text)
            {
                if (UnreservedCharacters.Contains(symbol))
                {
                    result.Append(symbol);
                }
                else
                {
                    result.AppendFormat("%{0:X2}", (int)symbol);
                }
            }

            return result.ToString();
        }
    }
}