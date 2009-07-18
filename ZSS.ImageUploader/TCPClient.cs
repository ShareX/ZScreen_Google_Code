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
        private WebHeaderCollection headers = new WebHeaderCollection();
        private byte[] headerBytes;
        private byte[] request;
        private byte[] requestEnd;
        private byte[] postMethod;
        private string boundary;

        public TCPClient()
        {
            client = new TcpClient();
        }

        private void PreparePackets(int length)
        {
            postMethod = Encoding.Default.GetBytes("POST " + url.AbsolutePath + " HTTP/1.1\r\n");

            headers.Add(HttpRequestHeader.ContentType, "multipart/form-data, boundary=" + boundary);
            headers.Add(HttpRequestHeader.Host, url.DnsSafeHost);
            headers.Add(HttpRequestHeader.ContentLength, (request.Length + length + requestEnd.Length).ToString());
            headers.Add(HttpRequestHeader.Connection, "Keep-Alive");
            headers.Add(HttpRequestHeader.CacheControl, "no-cache");

            headerBytes = headers.ToByteArray();
        }

        private void BuildRequest(Dictionary<string, string> arguments, string fileFormName, string fileName, string imageFormat)
        {
            string header = string.Format("--{0}", boundary);

            StringBuilder stringRequest = new StringBuilder();
            stringRequest.AppendLine(header);

            string postHeader = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"", fileFormName, fileName);
            stringRequest.AppendLine(postHeader);

            stringRequest.AppendLine(string.Format("Content-Type: {0}\n", imageFormat));

            foreach (KeyValuePair<string, string> argument in arguments)
            {
                stringRequest.AppendLine(header);
                stringRequest.AppendLine(string.Format("Content-Disposition: form-data; name=\"{0}\"\n\n{1}\n", argument.Key, argument.Value));
            }

            request = Encoding.Default.GetBytes(stringRequest.ToString());
        }

        private string GetMimeType(ImageFormat format)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
                if (codec.FormatID == format.Guid) return codec.MimeType;
            return "image/unknown";
        }

        private void BuildRequestEnd()
        {
            string header = string.Format("--{0}", boundary);

            StringBuilder stringRequest = new StringBuilder();
            stringRequest.AppendLine(header);
            stringRequest.AppendLine("Content-Disposition: form-data; name=\"description\"\n");
            stringRequest.AppendLine(header);
            stringRequest.AppendLine("Content-Disposition: form-data; name=\"file_type\"\n\nimage\n");
            stringRequest.AppendLine(header + "--");

            /*string stringRequest =
                "\r\n" + boundary + "\r\n" +
                "Content-Disposition: form-data; name=\"description\"\r\n\r\n" +
                "\r\n" + boundary + "\r\n" +
                "Content-Disposition: form-data; name=\"file_type\"\r\n" + "\r\nimage" +
                "\r\n" + boundary + "--\r\n";*/

            // string stringRequest = string.Format("--{0}--", boundary);

            requestEnd = Encoding.Default.GetBytes(stringRequest.ToString());
        }

        public void UploadImage(Image image, string link, string fileFormName, Dictionary<string, string> arguments)
        {
            using (image)
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);

                url = new Uri(link);

                boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

                BuildRequest(arguments, fileFormName, "file", GetMimeType(image.RawFormat));
                BuildRequestEnd();
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