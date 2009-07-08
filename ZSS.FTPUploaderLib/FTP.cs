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
using System.Text;

namespace ZSS
{
    public class FTP
    {
        private const int BufferSize = 2048;

        private readonly FTPAccount Account;

        private string FTPAddress { get { return string.Format("ftp://{0}:{1}", Account.Server, Account.Port); } }

        public FTP()
        {
            Account = new FTPAccount();
        }

        public FTP(ref FTPAccount acc)
        {
            Account = acc;
        }

        public void Upload(Stream stream, string remoteName)
        {
            try
            {
                Uri uri = new Uri(CombineURL(FTPAddress, Account.Path, remoteName));
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.ContentLength = stream.Length;
                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = !Account.IsActive;
                request.Credentials = new NetworkCredential(Account.Username, Account.Password);

                using (stream)
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytes = stream.Read(buffer, 0, BufferSize);

                    while (bytes > 0)
                    {
                        requestStream.Write(buffer, 0, bytes);
                        bytes = stream.Read(buffer, 0, BufferSize);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UploadFile(string filePath, string remoteName)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            Upload(stream, remoteName);
        }

        public void UploadText(string text, string remoteName)
        {
            MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(text), false);
            Upload(stream, remoteName);
        }

        public void DownloadFile(string fileName, string filePath)
        {
            try
            {
                Uri uri = new Uri(CombineURL(FTPAddress, Account.Path, fileName));
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = !Account.IsActive;
                request.Credentials = new NetworkCredential(Account.Username, Account.Password);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytes = stream.Read(buffer, 0, BufferSize);

                    while (bytes > 0)
                    {
                        fileStream.Write(buffer, 0, bytes);
                        bytes = stream.Read(buffer, 0, BufferSize);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DeleteFile(string fileName)
        {
            Uri uri = new Uri(CombineURL(FTPAddress, fileName));
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);

            request.GetResponse();
        }

        public void Rename(string fileName, string newFileName)
        {
            Uri uri = new Uri(CombineURL(FTPAddress, fileName));
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = newFileName;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);

            request.GetResponse();
        }

        public long GetFileSize(string fileName)
        {
            Uri uri = new Uri(CombineURL(FTPAddress, fileName));
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.ContentLength;
            }
        }

        public string[] ListDirectory()
        {
            List<string> directories = new List<string>();
            string url = CombineURL(FTPAddress, Account.Path);
            if (!url.EndsWith("/")) url += "/";
            Uri uri = new Uri(url);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.KeepAlive = false;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.UsePassive = !Account.IsActive;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                while (!reader.EndOfStream)
                {
                    directories.Add(reader.ReadLine());
                }

                return directories.ToArray();
            }
        }

        public void MakeMultiDirectory(string dirName)
        {
            string path = "";
            string[] dirs = dirName.Split('/');
            foreach (string dir in dirs)
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    path += dir + "/";
                    MakeDirectory(path);
                }
            }
        }

        public void MakeDirectory(string dirName)
        {
            try
            {
                Uri uri = new Uri(CombineURL(FTPAddress, dirName));
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(Account.Username, Account.Password);

                request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
    }
}