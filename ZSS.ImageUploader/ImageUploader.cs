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
 * http://www.sythe.org/showthread.php?t=509358 */
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
using ZSS.ImageUploadersLib.Helpers;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace ZSS.ImageUploadersLib
{
    public abstract class ImageUploaderOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public abstract class ImageUploader : IUploader
    {
        /// <summary>
        /// List of Errors logged by ImageUploaders
        /// </summary>
        public List<string> Errors { get; private set; }

        [XmlIgnore]
        public WebProxy ProxySettings { get; set; }

        /// <summary>
        /// API or Anonymous. Default: Anonymous
        /// </summary>
        protected UploadMode UploadMode { get; set; }

        private const string mEndStr = "\r\n";

        /// <summary>
        /// The Email property allows you to upload images directly to someones account.
        /// </summary>
        public abstract string Name { get; }
        protected string mFileName = "image";
        private bool RandomizeFileName { get; set; }

        public event ProgressEventHandler ProgressChanged;
        public delegate void ProgressEventHandler(int progress);

        public void ReportProgress(int progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(progress);
            }
        }

        protected ImageUploader()
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

        public abstract ImageFileManager UploadImage(Image image);

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

        protected string GetXMLVal(string input, string tag)
        {
            return Regex.Match(input, String.Format("(?<={0}>).+(?=</{0})", tag)).Value;
        }

        protected string GetResponse(string url, IDictionary<string, string> arguments)
        {
            string postData = arguments.Aggregate("?", (current, arg) => current + arg.Key + "=" + arg.Value + "&");
            string data = url + postData.Substring(0, postData.Length - 1);
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            Uri postUri = new Uri(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUri);
            request.Proxy = ProxySettings;
            request.Method = "POST";
            request.UserAgent = Application.ProductName + " " + Application.ProductVersion; // "Mozilla/5.0 (compatible; MSIE 7.0; Windows NT 6.0; WOW64; SV1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; InfoPath.1; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            using (WebResponse response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader responseReader = new StreamReader(responseStream))
            {
                return responseReader.ReadToEnd();
            }
        }

        protected string GetMD5(string text)
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

        protected string PostImage(Image image, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            try
            {
                ImageFormat imageFormat = image.RawFormat;

                string postData = "?" + string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());
                string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                string postHeader = string.Format("--{0}\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\nContent-Type: {3}\n\n",
                    boundary, fileFormName ?? "file", this.mFileName, GetMimeType(imageFormat) ?? "application/octet-stream");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + postData);
                request.Proxy = this.ProxySettings;
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";

                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
                byte[] boundaryBytes = Encoding.ASCII.GetBytes("\n--" + boundary + "\n");

                using (MemoryStream imageStream = new MemoryStream())
                {
                    image.Save(imageStream, imageFormat);
                    image.Dispose();
                    imageStream.Position = 0;

                    request.ContentLength = postHeaderBytes.Length + imageStream.Length + boundaryBytes.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                        byte[] buffer = new byte[(int)Math.Min(4096, imageStream.Length)];
                        int bytesRead = imageStream.Read(buffer, 0, buffer.Length);
                        while (bytesRead > 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                            bytesRead = imageStream.Read(buffer, 0, buffer.Length);
                        }

                        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    }
                }

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.Errors.Add(ex.Message);
            }

            return "";
        }

        protected string PostImage2(Image image, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            try
            {
                ImageFormat imageFormat = image.RawFormat;

                string boundary = Guid.NewGuid().ToString();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = this.ProxySettings;
                request.PreAuthenticate = true;
                request.AllowWriteStreamBuffering = true;
                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                request.Method = "POST";

                string header = string.Format("--{0}", boundary);
                string footer = string.Format("--{0}--", boundary);

                StringBuilder contents = new StringBuilder();
                contents.AppendLine(header);

                string postHeader = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"", fileFormName, this.mFileName);
                contents.AppendLine(postHeader);

                Encoding encoding = Encoding.GetEncoding("iso-8859-1");

                string fileContentType = GetMimeType(imageFormat);
                contents.AppendLine(string.Format("Content-Type: {0}\n", fileContentType));

                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, imageFormat);
                    image.Dispose();

                    contents.AppendLine(encoding.GetString(stream.ToArray()));
                }

                foreach (KeyValuePair<string, string> argument in arguments)
                {
                    contents.AppendLine(header);
                    contents.AppendLine(string.Format("Content-Disposition: form-data; name=\"{0}\"\n\n{1}\n", argument.Key, argument.Value));
                }

                contents.AppendLine(footer);

                byte[] bytes = encoding.GetBytes(contents.ToString());
                request.ContentLength = bytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Errors.Add(ex.Message);
            }

            return "";
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