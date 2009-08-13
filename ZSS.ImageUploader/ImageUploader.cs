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
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using ZSS.ImageUploadersLib.Helpers;
using ZSS.UploadersLib;

namespace ZSS.ImageUploadersLib
{
    public abstract class ImageUploader : Uploader
    {
        public abstract string Name { get; }

        /// <summary>
        /// List of Errors logged by ImageUploaders
        /// </summary>
        public List<string> Errors { get; private set; }

        [XmlIgnore]
        public IWebProxy ProxySettings { get; set; }

        /// <summary>
        /// API or Anonymous. Default: Anonymous
        /// </summary>
        protected UploadMode UploadMode { get; set; }

        private string mFileName = "image";
        private bool RandomizeFileName { get; set; }

        public event ProgressEventHandler ProgressChanged;
        public delegate void ProgressEventHandler(int progress);

        protected ImageUploader()
        {
            this.Errors = new List<string>();
            this.UploadMode = UploadMode.ANONYMOUS;
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

        public void ReportProgress(int progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(progress);
            }
        }

        protected string PostImage(Image image, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (MemoryStream stream = new MemoryStream())
                {
                    byte[] bytes;

                    string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

                    foreach (KeyValuePair<string, string> argument in arguments)
                    {
                        bytes = MakeInputContent(boundary, argument.Key, argument.Value);
                        stream.Write(bytes, 0, bytes.Length);
                    }

                    using (image)
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        image.Save(imageStream, image.RawFormat);

                        bytes = MakeFileInputContent(boundary, fileFormName, mFileName, imageStream.ToArray(), GetMimeType(image.RawFormat));
                        stream.Write(bytes, 0, bytes.Length);
                    }

                    bytes = Encoding.Default.GetBytes(string.Format("--{0}--\r\n", boundary));
                    stream.Write(bytes, 0, bytes.Length);

                    stream.Position = 0;

                    request.ContentLength = stream.Length;
                    request.ContentType = "multipart/form-data; boundary=" + boundary;
                    request.Method = "POST";
                    request.Proxy = this.ProxySettings;
                    request.UserAgent = Application.ProductName + " " + Application.ProductVersion;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Errors.Add(ex.Message);
            }

            return "";
        }

        protected string PostImage2(Image image, string url, string fileFormName, Dictionary<string, string> arguments)
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

        protected string PostImage3(Image image, string url, string fileFormName, Dictionary<string, string> arguments)
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
            return string.Join("\r\n", Errors.ToArray());
        }
    }

    public abstract class ImageUploaderOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}