using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ZSS.ImageUploadersLib
{
    public class TCPClient
    {
        private TcpClient client;
        private Uri url;
        private string boundary;
        private byte[] postMethod, headerBytes, request, requestEnd;

        public TCPClient()
        {
            client = new TcpClient();
        }

        private void PreparePackets(int length)
        {
            postMethod = Encoding.Default.GetBytes("POST " + url.AbsolutePath + " HTTP/1.1\r\n");

            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add(HttpRequestHeader.ContentType, "multipart/form-data, boundary=" + boundary);
            headers.Add(HttpRequestHeader.Host, url.DnsSafeHost);
            headers.Add(HttpRequestHeader.ContentLength, (request.Length + length + requestEnd.Length).ToString());
            headers.Add(HttpRequestHeader.Connection, "Keep-Alive");
            headers.Add(HttpRequestHeader.CacheControl, "no-cache");

            headerBytes = headers.ToByteArray();
        }

        private string GetMimeType(ImageFormat format)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == format.Guid) return codec.MimeType;
            }
            return "image/unknown";
        }

        private void BuildRequest(string fileFormName, string fileName, string fileMimeType)
        {
            request = MakeFileInputContent(fileFormName, fileName, fileMimeType);
        }

        private void BuildRequestEnd(Dictionary<string, string> arguments)
        {
            StringBuilder stringRequest = new StringBuilder();

            foreach (KeyValuePair<string, string> argument in arguments)
            {
                stringRequest.Append(MakeInputContent(argument.Key, argument.Value));
            }

            stringRequest.Append(string.Format("\r\n{0}--", boundary));

            requestEnd = Encoding.Default.GetBytes(stringRequest.ToString());
        }

        private string MakeInputContent(string name, string value)
        {
            return string.Format("{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, name, value);
        }

        private byte[] MakeFileInputContent(string name, string filename, string contentType)
        {
            string format = string.Format("{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                boundary, name, filename, contentType);

            MemoryStream stream = new MemoryStream();

            byte[] bytes = Encoding.Default.GetBytes(format);
            stream.Write(bytes, 0, bytes.Length);

            return stream.ToArray();
        }

        public void UploadImage(Image image, string link, string fileFormName, Dictionary<string, string> arguments)
        {
            using (image)
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);

                url = new Uri(link);

                boundary = "--------------------" + DateTime.Now.Ticks.ToString("x");

                BuildRequest(fileFormName, "file", GetMimeType(image.RawFormat));
                BuildRequestEnd(arguments);

                PreparePackets((int)stream.Length);

                Send(stream, arguments);
            }
        }

        private string Send(Stream stream, Dictionary<string, string> arguments)
        {
            client.Connect(url.DnsSafeHost, 80);

            using (NetworkStream networkStream = client.GetStream())
            {
                networkStream.Write(postMethod, 0, postMethod.Length);
                networkStream.Write(headerBytes, 0, headerBytes.Length);
                networkStream.Write(request, 0, request.Length);

                byte[] buffer = new byte[(int)Math.Min(4096, stream.Length)];

                using (stream)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    while (bytesRead > 0)
                    {
                        networkStream.Write(buffer, 0, bytesRead);
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                    }
                }

                networkStream.Write(requestEnd, 0, requestEnd.Length);

                string response = string.Empty;
                int networkBytesRead = networkStream.Read(buffer, 0, buffer.Length);

                while (networkBytesRead > 0)
                {
                    response += Encoding.Default.GetString(buffer);
                    networkBytesRead = networkStream.Read(buffer, 0, buffer.Length);
                }

                return response;
            }
        }
    }
}