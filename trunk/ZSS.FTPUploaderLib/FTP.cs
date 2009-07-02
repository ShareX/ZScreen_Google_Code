using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ZSS
{
    public class FTP
    {
        /// <summary>
        /// Transfer buffer size
        /// </summary>
        private const int BufferSize = 2048;

        private readonly FTPAccount mAccount;

        public FTP()
        {
            mAccount = new FTPAccount();
        }

        public FTP(ref FTPAccount acc)
        {
            mAccount = acc;
        }

        public void UploadFile(string fileName, string remoteName)
        {
            byte[] buffer = new byte[BufferSize];

            FileInfo fi = new FileInfo(fileName);

            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + mAccount.Path + "/" + fi.Name);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.ContentLength = fi.Length;
            request.KeepAlive = false;
            request.UseBinary = true;
            request.UsePassive = !mAccount.IsActive;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            using (FileStream fs = fi.OpenRead())
            using (Stream stream = request.GetRequestStream())
            {
                int contentLength = fs.Read(buffer, 0, BufferSize);

                while (contentLength != 0)
                {
                    stream.Write(buffer, 0, contentLength);
                    contentLength = fs.Read(buffer, 0, BufferSize);
                }

                stream.Close();
                fs.Close();
            }
        }

        public void DownloadFile(string fileName, string filePath)
        {
            byte[] buffer = new byte[BufferSize];

            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + mAccount.Path + "/" + fileName);

            using (FileStream fStream = new FileStream(filePath, FileMode.Create))
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UseBinary = true;
                request.UsePassive = !mAccount.IsActive;
                request.KeepAlive = false;
                request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    int bytes = stream.Read(buffer, 0, BufferSize);

                    while (bytes > 0)
                    {
                        fStream.Write(buffer, 0, bytes);
                        bytes = stream.Read(buffer, 0, BufferSize);
                    }

                    stream.Close();
                    fStream.Close();
                    response.Close();
                }
            }
        }

        public void DeleteFile(string fileName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(stream))
            {
                streamReader.Close();
                stream.Close();
                response.Close();
            }
        }

        public void Rename(string fileName, string newFileName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = newFileName;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                stream.Close();
            }
            response.Close();
        }

        public long GetFileSize(string fileName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                long fileSize = response.ContentLength;

                stream.Close();
                response.Close();

                return fileSize;
            }
        }

        public string[] ListDirectory()
        {
            List<string> directories = new List<string>();
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + mAccount.Path + "/");
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.UseBinary = true;
            request.UsePassive = !mAccount.IsActive;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            WebResponse response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                while (!reader.EndOfStream)
                {
                    directories.Add(reader.ReadLine());
                }

                reader.Close();
                response.Close();

                return directories.ToArray();
            }
        }

        private void MakeDirectory(string dirName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + dirName + "/");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                stream.Close();
            }
            response.Close();
        }
    }
}