#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace ZSS.UploadersLib
{
    public abstract class Uploader
    {
        #region Until FileUploader.cs

        public virtual string Name
        {
            get { return null; }
        }

        public virtual string Upload(byte[] data, string fileName)
        {
            return null;
        }

        public string Upload(string filePath)
        {
            if (File.Exists(filePath))
            {
                return Upload(File.ReadAllBytes(filePath), Path.GetFileName(filePath));
            }

            return null;
        }

        #endregion

        #region Protected Methods

        protected string GetResponse(string url, Dictionary<string, string> arguments)
        {
            try
            {
                url += "?" + string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return "";
        }

        protected string GetRedirectionURL(string url, Dictionary<string, string> arguments)
        {
            try
            {
                url += "?" + string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (WebResponse response = request.GetResponse())
                {
                    return response.ResponseUri.OriginalString;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return "";
        }

        protected string UploadData(byte[] data, string fileName, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (MemoryStream stream = new MemoryStream())
                {
                    byte[] bytes;

                    string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

                    if (arguments != null)
                    {
                        foreach (KeyValuePair<string, string> argument in arguments)
                        {
                            bytes = MakeInputContent(boundary, argument.Key, argument.Value);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    }

                    bytes = MakeFileInputContent(boundary, fileFormName, fileName, data);
                    stream.Write(bytes, 0, bytes.Length);

                    bytes = Encoding.UTF8.GetBytes(string.Format("--{0}--\r\n", boundary));
                    stream.Write(bytes, 0, bytes.Length);

                    stream.Position = 0;

                    request.ContentLength = stream.Length;
                    request.ContentType = "multipart/form-data; boundary=" + boundary;
                    request.Method = "POST";

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        byte[] buffer = new byte[(int)Math.Min(4096, stream.Length)];

                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        while (bytesRead > 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                            bytesRead = stream.Read(buffer, 0, buffer.Length);
                        }
                    }
                }

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return "";
        }

        #endregion

        #region Public Static Methods

        public static byte[] MakeInputContent(string boundary, string name, string value)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, name, value);

            return Encoding.UTF8.GetBytes(format);
        }

        public static byte[] MakeFileInputContent(string boundary, string name, string fileName, byte[] content)
        {
            return MakeFileInputContent(boundary, name, fileName, content, GetMimeType(fileName));
        }

        public static byte[] MakeFileInputContent(string boundary, string name, string fileName, byte[] content, string contentType)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                boundary, name, fileName, contentType);

            MemoryStream stream = new MemoryStream();
            byte[] bytes;

            bytes = Encoding.UTF8.GetBytes(format);
            stream.Write(bytes, 0, bytes.Length);

            stream.Write(content, 0, content.Length);

            bytes = Encoding.UTF8.GetBytes("\r\n");
            stream.Write(bytes, 0, bytes.Length);

            return stream.ToArray();
        }

        public static string GetXMLValue(string input, string tag)
        {
            return Regex.Match(input, String.Format("(?<={0}>).+(?=</{0})", tag)).Value;
        }

        public static string GetMimeType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                return regKey.GetValue("Content Type").ToString();
            }
            return "application/octetstream";
        }

        public static string GetMimeType(ImageFormat format)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == format.Guid) return codec.MimeType;
            }
            return "image/unknown";
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
                else
                {
                    return "";
                }
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

        #endregion
    }
}