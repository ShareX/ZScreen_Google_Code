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
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ZSS.UploadersLib
{
    public abstract class Uploader
    {
        public abstract string Upload(byte[] file, string fileName);

        public abstract string Name { get; }

        public string Upload(string filePath)
        {
            if (File.Exists(filePath))
            {
                return Upload(File.ReadAllBytes(filePath), Path.GetFileName(filePath));
            }

            return null;
        }

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

        protected string UploadFile(byte[] file, string fileName, string url, string fileFormName, Dictionary<string, string> arguments)
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

                    bytes = MakeFileInputContent(boundary, fileFormName, fileName, file);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return "";
        }

        private byte[] MakeInputContent(string boundary, string name, string value)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, name, value);

            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeFileInputContent(string boundary, string name, string fileName, byte[] content)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                boundary, name, fileName, GetMimeType(fileName));

            MemoryStream stream = new MemoryStream();
            byte[] bytes;

            bytes = Encoding.UTF8.GetBytes(format);
            stream.Write(bytes, 0, bytes.Length);

            stream.Write(content, 0, content.Length);

            bytes = Encoding.UTF8.GetBytes("\r\n");
            stream.Write(bytes, 0, bytes.Length);

            return stream.ToArray();
        }

        protected string GetMimeType(string fileName)
        {
            string mimeType = "application/octetstream";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
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
    }
}