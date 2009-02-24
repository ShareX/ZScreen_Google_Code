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
#region Shared Source Notification
/* Portions of this code is thanks to Flaming Idiots
 * http://www.sythe.org/showthread.php?t=509358  
 */
#endregion

using System;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using ZSS.ImageUploader.Helpers;
using System.ComponentModel;

namespace ZSS.ImageUploader
{
    public enum UploadMode
    {
        [Description("User")]
        API,
        [Description("Anonymous")]
        ANONYMOUS
    }

    public abstract class HTTPUploader : IUploader
    {
        /// <summary>
        /// List of Errors logged by ImageUploader
        /// </summary>
        public List<string> Errors { get; protected set; }
        /// <summary>
        /// API or Anonymous. Default: Anonymous
        /// </summary>
        public UploadMode UploadMode { get; set; }

        private const string mEndStr = "\r\n";

        /// <summary>
        /// The Email property allows you to upload images directly to someones account.
        /// </summary>
        public abstract string Name { get; }
        private string mFileName = "image";
        public bool RandomizeFileName { get; set; }

        public HTTPUploader()
        {
            this.Errors = new List<string>();
            this.UploadMode = UploadMode.ANONYMOUS;
        }

        protected string GetMimeType(ImageFormat format)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
                if (codec.FormatID == format.Guid) return codec.MimeType;
            return "image/unknown";
        }

        public abstract ImageFileManager UploadImage(Image image, ImageFormat format);

        public ImageFileManager UploadImage(Image image)
        {
            ImageFileManager imageFiles = UploadImage(image, image.RawFormat);
            return imageFiles;
        }

        public ImageFileManager UploadImage(string filePath)
        {
            if (!this.RandomizeFileName)
            {
                mFileName = Path.GetFileName(filePath);
            }
            ImageFileManager ifm = UploadImage(Image.FromFile(filePath));
            ifm.LocalFilePath = filePath;
            return ifm;
        }

        public string GetXMLVal(string input, string tag)
        {
            return Regex.Match(input, String.Format("(?<={0}>).+(?=</{0})", tag)).Value;
        }

        protected string GetResponse(string url, IDictionary<string, string> arguments)
        {
            string postData = arguments.Aggregate("?", (current, arg) => current + arg.Key + "=" + arg.Value + "&"); ;
            string data = url + postData.Substring(0, postData.Length - 1);
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            Uri postUri = new Uri(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUri);
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 7.0; Windows NT 6.0; WOW64; SV1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; InfoPath.1; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader responseReader = new StreamReader(responseStream);
            string returnValue = responseReader.ReadToEnd();
            responseStream.Close();
            return returnValue;
        }

        protected string GetResponse2(string url, IDictionary<string, string> arguments)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.AllowAutoRedirect = false;
            request.Method = "POST";

            string post = arguments.Aggregate("?", (current, arg) => current + arg.Key + "=" + arg.Value + "&");
            post = post.Substring(0, post.Length - 1);
            byte[] data = System.Text.Encoding.ASCII.GetBytes(post);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream response = request.GetRequestStream();

            response.Write(data, 0, data.Length);

            response.Close();

            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            res.Close();
            // note that there is no need to hook up a StreamReader and
            // look at the response data, since it is of no need

            if (res.StatusCode == HttpStatusCode.Found)
            {
                Console.WriteLine(res.Headers["location"]);
            }
            else
            {
                Console.WriteLine("Error");
            }
            return "";
        }

        public string GetMD5(string text)
        {
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            bytes = crypto.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString().ToLower();
        }

        protected string PostImage(Stream imageStream, string uploadUrl, string fileFormName, string contentType,
            IDictionary<string, string> arguments, CookieContainer cookies, string referer)
        {
            string returnValue = "";
            try
            {
                string postData = arguments.Aggregate("?", (current, arg) => current + arg.Key + "=" + arg.Value + "&");
                Uri postUri = new Uri(uploadUrl + postData.Substring(0, postData.Length - 1));
                string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                string postHeader = string.Format("--{0}\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\nContent-Type: {3}\n\n", boundary, fileFormName ?? "file", this.mFileName, contentType ?? "application/octet-stream");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUri);
                request.CookieContainer = cookies;
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 7.0; Windows NT 6.0; WOW64; SV1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; InfoPath.1; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                request.Referer = referer;

                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
                byte[] boundaryBytes = Encoding.ASCII.GetBytes("\n--" + boundary + "\n");

                request.ContentLength = postHeaderBytes.Length + imageStream.Length + boundaryBytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                byte[] buffer = new byte[((int)Math.Min(4096, imageStream.Length)) - 1];
                int bytesRead = 0;
                int bytesReadTotal = 0;
                while (true)
                {
                    bytesRead = imageStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;
                    requestStream.Write(buffer, 0, bytesRead);
                    bytesReadTotal += bytesRead;
                }
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);
                returnValue = responseReader.ReadToEnd();
                responseStream.Close();
            }
            catch (Exception ex)
            {
                this.Errors.Add(ex.Message);
                Console.WriteLine(ex.Message);
            }
            return returnValue;
        }

        public string ToErrorString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string err in this.Errors)
            {
                sb.AppendLine(err);
            }
            return sb.ToString();
        }

        #region "Microsoft .NET Framework v2.0 Compatible methods"
        protected bool Upload(MemoryStream memoryStream, Stream requestStream)
        {
            try
            {
                int count;
                byte[] tmp = new byte[4096]; //4 KiB

                //position at beginning of the stream
                memoryStream.Position = 0L;

                //possibly add in a progress bar...
                do
                {
                    count = memoryStream.Read(tmp, 0, tmp.Length);
                    requestStream.Write(tmp, 0, count);
                }
                while (count > 0);

                //success
                return true;
            }
            catch
            {
                //failure
                return false;
            }
        }

        protected void WriteFile(StreamWriter streamWriter, MemoryStream memoryStream, string boundary, string field, string fileName, byte[] fileBytes)
        {
            streamWriter.Write("--" + boundary + mEndStr);
            streamWriter.Write("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", field, fileName, mEndStr);
            streamWriter.Write("Content-Type: application/octet-stream {0}{0}", mEndStr);
            //streamWriter.Write("Content-Transfer-Encoding: binary{0}{0}", mEndStr);
            streamWriter.Flush();

            memoryStream.Write(fileBytes, 0, fileBytes.Length);

            streamWriter.Write(mEndStr);
            streamWriter.Write("--{0}--{1}", boundary, mEndStr);
            streamWriter.Flush();
        }

        protected void WritePost(StreamWriter streamWriter, string boundary, string field, string data)
        {
            streamWriter.Write("--" + boundary + mEndStr);
            streamWriter.Write("Content-Disposition: form-data; name=\"{0}\"{1}", field, mEndStr);
            streamWriter.Write("Content-Type: text/plain; charset=utf-8{0}", mEndStr);
            streamWriter.Write("Content-Transfer-Encoding: 8bit{0}{0}", mEndStr);
            streamWriter.Write("{0}{1}", data, mEndStr);
            streamWriter.Write("--{0}--{1}", boundary, mEndStr);
            streamWriter.Flush();
        }
        #endregion
    }
}