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
using System.Text.RegularExpressions;

namespace ZSS
{
    public class FTP
    {
        public event StringEventHandler FTPOutput;
        public delegate void StringEventHandler(string text);

        private const int BufferSize = 2048;

        public FTPAccount Account;

        public string FTPAddress { get { return string.Format("ftp://{0}:{1}", Account.Server, Account.Port); } }

        public FTP()
        {
            Account = new FTPAccount();
        }

        public FTP(FTPAccount acc)
        {
            Account = acc;
        }

        public void Upload(Stream stream, string url)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Account.Username, Account.Password);
                request.KeepAlive = false;
                request.UsePassive = !Account.IsActive;

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

                WriteOutput("Upload: " + url);
            }
            catch (Exception ex)
            {
                WriteOutput("Error - Upload: " + ex.Message);
            }
        }

        public void UploadFile(string filePath, string url)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            Upload(stream, url);
        }

        public void UploadText(string text, string url)
        {
            MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(text), false);
            Upload(stream, url);
        }

        public void DownloadFile(string url, string savePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(Account.Username, Account.Password);
                request.KeepAlive = false;
                request.UsePassive = !Account.IsActive;

                using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
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

                WriteOutput(string.Format("DownloadFile: {0} - {1}", url, savePath));
            }
            catch (Exception ex)
            {
                WriteOutput("Error - DownloadFile: " + ex.Message);
            }
        }

        public void DeleteFile(string url)
        {
            string filename = FTPHelpers.GetFileName(url);
            if (filename == "." || filename == "..") return;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);
            request.KeepAlive = false;

            request.GetResponse();

            WriteOutput("DeleteFile: " + url);
        }

        public void RemoveDirectory(string url)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);
            request.KeepAlive = false;

            request.GetResponse();

            WriteOutput("RemoveDirectory: " + url);
        }

        public void RemoveDirectoryFull(string url)
        {
            string[] files = ListDirectory(url);
            string path = url.Substring(0, url.LastIndexOf('/'));

            foreach (string file in files)
            {
                DeleteFile(FTPHelpers.CombineURL(path, file));
            }

            RemoveDirectory(url);

            WriteOutput("RemoveDirectoryFull: " + url);
        }

        public void Rename(string url, string newFileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = newFileName;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);
            request.KeepAlive = false;

            request.GetResponse();

            WriteOutput(string.Format("Rename: {0} - {1}", url, newFileName));
        }

        public long GetFileSize(string url)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);
            request.KeepAlive = false;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                WriteOutput("GetFileSize: " + url);

                return response.ContentLength;
            }
        }

        public string[] ListDirectory(string url)
        {
            List<string> result = new List<string>();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);
            request.KeepAlive = false;
            request.UsePassive = !Account.IsActive;

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(reader.ReadLine());
                }

                WriteOutput("ListDirectory: " + url);

                return result.ToArray();
            }
        }

        public FTPLineResult[] ListDirectoryDetails(string url)
        {
            List<FTPLineResult> result = new List<FTPLineResult>();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);
            request.KeepAlive = false;
            request.UsePassive = !Account.IsActive;

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(FTPLineParser.Parse(reader.ReadLine()));
                }

                WriteOutput("ListDirectoryDetails: " + url);

                return result.ToArray();
            }
        }

        public void MakeDirectory(string url)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(Account.Username, Account.Password);
                request.KeepAlive = false;

                request.GetResponse();

                WriteOutput("MakeDirectory: " + url);
            }
            catch (Exception ex)
            {
                WriteOutput("Error - MakeDirectory: " + ex.Message);
            }
        }

        public void MakeMultiDirectory(string dirName) //TODO
        {
            string path = "";
            string[] dirs = dirName.Split('/');
            foreach (string dir in dirs)
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    //path += dir + "/";
                    MakeDirectory(path);
                }
            }

            WriteOutput("MakeMultiDirectory: " + dirName);
        }

        public void WriteOutput(string text)
        {
            if (FTPOutput != null)
            {
                FTPOutput(string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), text));
            }
        }
    }
}