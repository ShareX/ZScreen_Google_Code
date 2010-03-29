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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Win32;
using UploadersLib.Helpers;

namespace UploadersLib
{
    public class Uploader
    {
        public delegate void ProgressEventHandler(int progress);

        public event ProgressEventHandler ProgressChanged;

        public static ProxySettings ProxySettings = new ProxySettings();

        public List<string> Errors { get; set; }

        public string UserAgent { get; set; }

        public Uploader()
        {
            this.Errors = new List<string>();
            this.UserAgent = "ZScreen";
        }

        protected void OnProgressChanged(int progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(progress);
            }
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        #region Post methods

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string GetResponse(string url, Dictionary<string, string> arguments)
        {
            string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

            byte[] data = MakeInputContent(boundary, arguments, true);

            using (HttpWebResponse response = GetResponse(url, data, boundary))
            {
                return ResponseToString(response);
            }
        }

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string GetResponse(string url)
        {
            return GetResponse(url, null);
        }

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string GetRedirectionURL(string url, Dictionary<string, string> arguments)
        {
            string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

            byte[] data = MakeInputContent(boundary, arguments, true);

            using (HttpWebResponse response = GetResponse(url, data, boundary))
            {
                if (response != null)
                {
                    return response.ResponseUri.OriginalString;
                }
            }

            return null;
        }

        private HttpWebResponse GetResponseUsingPost(string url, Stream stream, string boundary)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.AllowWriteStreamBuffering = false;
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.ContentLength = stream.Length;
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.Proxy = ProxySettings.GetWebProxy;
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.UserAgent = UserAgent;

                byte[] buffer = new byte[(int)Math.Min(4096, stream.Length)];

                stream.Position = 0;

                using (Stream requestStream = request.GetRequestStream())
                {
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        OnProgressChanged((int)((double)stream.Position / stream.Length * 100));
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                }

                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                this.Errors.Add(e.Message);
                Debug.WriteLine(e.ToString());
            }

            return null;
        }

        private HttpWebResponse GetResponse(string url, byte[] data, string boundary)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                return GetResponseUsingPost(url, stream, boundary);
            }
        }

        #endregion

        #region Get methods

        /// <summary>
        /// Method: GET
        /// </summary>
        protected string GetResponseString(string url, Dictionary<string, string> arguments)
        {
            if (arguments != null && arguments.Count > 0)
            {
                url += "?" + string.Join("&", arguments.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value)).ToArray());
            }

            using (HttpWebResponse response = GetResponseUsingGet(url))
            {
                return ResponseToString(response);
            }
        }

        /// <summary>
        /// Method: GET
        /// </summary>
        protected string GetResponseString(string url)
        {
            return GetResponseString(url, null);
        }

        private HttpWebResponse GetResponseUsingGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                request.Proxy = ProxySettings.GetWebProxy;
                request.UserAgent = UserAgent;

                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                this.Errors.Add(e.Message);
                Debug.WriteLine(e.ToString());
            }

            return null;
        }

        #endregion

        #region Upload methods

        protected string UploadImage(Image image, string fileName, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                byte[] data = stream.ToArray();
                return UploadData(data, fileName, url, fileFormName, arguments);
            }
        }

        protected string UploadData(byte[] data, string fileName, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

            using (MemoryStream stream = new MemoryStream())
            {
                byte[] bytes = MakeInputContent(boundary, arguments, false);
                stream.Write(bytes, 0, bytes.Length);

                bytes = MakeFileInputContent(boundary, fileFormName, fileName, data, true);
                stream.Write(bytes, 0, bytes.Length);

                using (HttpWebResponse response = GetResponseUsingPost(url, stream, boundary))
                {
                    return ResponseToString(response);
                }
            }
        }

        #endregion

        #region Helper methods

        private byte[] MakeInputContent(string boundary, string name, string value)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, name, value);
            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeInputContent(string boundary, Dictionary<string, string> contents, bool isFinal)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] bytes;

                if (contents != null)
                {
                    foreach (KeyValuePair<string, string> content in contents)
                    {
                        bytes = MakeInputContent(boundary, content.Key, content.Value);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                if (isFinal)
                {
                    bytes = MakeFinalBoundary(boundary);
                    stream.Write(bytes, 0, bytes.Length);
                }

                return stream.ToArray();
            }
        }

        private byte[] MakeFileInputContent(string boundary, string name, string fileName, byte[] content, bool isFinal)
        {
            return MakeFileInputContent(boundary, name, fileName, content, GetMimeType(fileName), isFinal);
        }

        private byte[] MakeFileInputContent(string boundary, string name, string fileName, byte[] content, string contentType, bool isFinal)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                boundary, name, fileName, contentType);

            using (MemoryStream stream = new MemoryStream())
            {
                byte[] bytes;

                bytes = Encoding.UTF8.GetBytes(format);
                stream.Write(bytes, 0, bytes.Length);

                stream.Write(content, 0, content.Length);

                bytes = Encoding.UTF8.GetBytes("\r\n");
                stream.Write(bytes, 0, bytes.Length);

                if (isFinal)
                {
                    bytes = MakeFinalBoundary(boundary);
                    stream.Write(bytes, 0, bytes.Length);
                }

                return stream.ToArray();
            }
        }

        private byte[] MakeFinalBoundary(string boundary)
        {
            return Encoding.UTF8.GetBytes(string.Format("--{0}--\r\n", boundary));
        }

        private string ResponseToString(WebResponse response)
        {
            if (response != null)
            {
                using (response)
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }

            return null;
        }

        private string GetMimeType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                return regKey.GetValue("Content Type").ToString();
            }

            return "application/octetstream";
        }

        private string GetMimeType(ImageFormat format)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec.MimeType;
                }
            }

            return "image/unknown";
        }

        protected string GetXMLValue(string input, string tag)
        {
            return Regex.Match(input, String.Format("(?<={0}>).+(?=</{0})", tag)).Value;
        }

        protected string GetMD5(byte[] data)
        {
            byte[] bytes = new MD5CryptoServiceProvider().ComputeHash(data);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString().ToLower();
        }

        protected string GetMD5(string text)
        {
            return GetMD5(Encoding.UTF8.GetBytes(text));
        }

        protected string GetRandomAlphanumeric(int length)
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

        protected string CombineURL(string url1, string url2)
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

        protected string CombineURL(params string[] urls)
        {
            return urls.Aggregate((current, arg) => CombineURL(current, arg));
        }

        #endregion
    }
}