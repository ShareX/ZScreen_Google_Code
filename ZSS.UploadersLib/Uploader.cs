#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2010  Brandon Zimmerman

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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.HelperClasses;
using ZUploader.HelperClasses;

namespace UploadersLib
{
    public class Uploader
    {
        public delegate void ProgressEventHandler(ProgressManager progress);
        public event ProgressEventHandler ProgressChanged;

        public static ProxySettings ProxySettings = new ProxySettings();

        public const int BufferSize = 4096;

        public string UserAgent { get; set; }
        public List<string> Errors { get; private set; }
        public bool IsUploading { get; private set; }

        private bool stopUpload;

        public Uploader()
        {
            this.Errors = new List<string>();
            this.UserAgent = string.Format("{0} {1}", Application.ProductName, Application.ProductVersion);
        }

        protected void OnProgressChanged(ProgressManager progress)
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

        public void StopUpload()
        {
            if (IsUploading)
            {
                stopUpload = true;
            }
        }

        #region Post methods

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
        protected string GetResponse(string url, Dictionary<string, string> arguments)
        {
            using (HttpWebResponse response = GetResponseUsingPost(url, arguments))
            {
                return ResponseToString(response);
            }
        }

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string GetRedirectionURL(string url, Dictionary<string, string> arguments)
        {
            using (HttpWebResponse response = GetResponseUsingPost(url, arguments))
            {
                if (response != null)
                {
                    return response.ResponseUri.OriginalString;
                }
            }

            return null;
        }

        private HttpWebResponse GetResponseUsingPost(string url, Dictionary<string, string> arguments)
        {
            string boundary = CreateBoundary();
            byte[] data = MakeInputContent(boundary, arguments);
            return GetResponseUsingPost(url, data, boundary);
        }

        private HttpWebResponse GetResponseUsingPost(string url, byte[] data, string boundary)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                return GetResponseUsingPost(url, stream, boundary);
            }
        }

        private HttpWebResponse GetResponseUsingPost(string url, Stream dataStream, string boundary)
        {
            IsUploading = true;
            stopUpload = false;

            try
            {
                HttpWebRequest request = PreparePostWebRequest(url, boundary, dataStream.Length);

                using (Stream requestStream = request.GetRequestStream())
                {
                    if (!TransferData(requestStream, dataStream)) return null;
                }

                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                if (!stopUpload) Errors.Add(e.ToString());
            }
            finally
            {
                IsUploading = false;
            }

            return null;
        }

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string UploadData(Stream dataStream, string fileName, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            IsUploading = true;
            stopUpload = false;

            try
            {
                string boundary = CreateBoundary();

                byte[] bytesArguments = MakeInputContent(boundary, arguments, false);
                byte[] bytesDataOpen = MakeFileInputContentOpen(boundary, fileFormName, fileName);
                byte[] bytesDataClose = MakeFileInputContentClose(boundary);

                long contentLength = bytesArguments.Length + bytesDataOpen.Length + dataStream.Length + bytesDataClose.Length;
                HttpWebRequest request = PreparePostWebRequest(url, boundary, contentLength);

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytesArguments, 0, bytesArguments.Length);
                    requestStream.Write(bytesDataOpen, 0, bytesDataOpen.Length);
                    if (!TransferData(requestStream, dataStream)) return null;
                    requestStream.Write(bytesDataClose, 0, bytesDataClose.Length);
                }

                return ResponseToString(request.GetResponse());
            }
            catch (Exception e)
            {
                if (!stopUpload) Errors.Add(e.ToString());
            }
            finally
            {
                IsUploading = false;
            }

            return null;
        }

        #endregion Post methods

        #region Get methods

        /// <summary>
        /// Method: GET
        /// </summary>
        protected string GetResponseString(string url)
        {
            return GetResponseString(url, null);
        }

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

        private HttpWebResponse GetResponseUsingGet(string url)
        {
            IsUploading = true;

            try
            {
                return (HttpWebResponse)PrepareGetWebRequest(url).GetResponse();
            }
            catch (Exception e)
            {
                Errors.Add(e.ToString());
            }
            finally
            {
                IsUploading = false;
            }

            return null;
        }

        #endregion Get methods

        #region Helper methods

        private HttpWebRequest PreparePostWebRequest(string url, string boundary, long length)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowWriteStreamBuffering = ProxySettings.ProxyConfig != ProxyConfigType.NoProxy;
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.ContentLength = length;
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.KeepAlive = false;
            request.Method = "POST";
            request.Pipelined = false;
            request.ProtocolVersion = HttpVersion.Version11;
            request.Proxy = ProxySettings.GetWebProxy;
            request.ServicePoint.Expect100Continue = false;
            request.ServicePoint.UseNagleAlgorithm = false;
            request.Timeout = -1;
            request.UserAgent = UserAgent;

            return request;
        }

        private HttpWebRequest PrepareGetWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;
            request.Method = "GET";
            request.Proxy = ProxySettings.GetWebProxy;
            request.UserAgent = UserAgent;

            return request;
        }

        private bool TransferData(Stream requestStream, Stream dataStream)
        {
            ProgressManager progress = new ProgressManager(dataStream.Length);
            dataStream.Position = 0;
            byte[] buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = dataStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (stopUpload) return false;

                requestStream.Write(buffer, 0, bytesRead);

                if (progress.ChangeProgress(bytesRead))
                {
                    OnProgressChanged(progress);
                }
            }

            return true;
        }

        private string CreateBoundary()
        {
            return new string('-', 20) + FastDateTime.Now.Ticks.ToString("x");
        }

        private byte[] MakeInputContent(string boundary, string name, string value)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, name, value);
            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeInputContent(string boundary, Dictionary<string, string> contents, bool isFinal = true)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (string.IsNullOrEmpty(boundary)) boundary = CreateBoundary();
                byte[] bytes;

                if (contents != null)
                {
                    foreach (KeyValuePair<string, string> content in contents)
                    {
                        if (!string.IsNullOrEmpty(content.Key))
                        {
                            bytes = MakeInputContent(boundary, content.Key, content.Value);
                            stream.Write(bytes, 0, bytes.Length);
                        }
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

        private byte[] MakeFileInputContentOpen(string boundary, string fileFormName, string fileName)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                boundary, fileFormName, fileName, Helpers.GetMimeType(fileName));
            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeFileInputContentClose(string boundary)
        {
            return Encoding.UTF8.GetBytes(string.Format("\r\n--{0}--\r\n", boundary));
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
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }

            return null;
        }

        #endregion Helper methods
    }
}