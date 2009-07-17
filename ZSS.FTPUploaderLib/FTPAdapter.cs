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
using System.Net;
using System.Text;
using System.ComponentModel;

namespace ZSS
{
    public class FTPOptions
    {
        public FTPOptions() { }

        public FTPOptions(FTPAccount acc, WebProxy proxy)
        {
            this.Account = acc;
            this.ProxySettings = proxy;
        }

        public FTPAccount Account { get; set; }
        public WebProxy ProxySettings { get; set; }
    }

    public class FTPAdapter
    {
        public event ProgressEventHandler ProgressChanged;
        public delegate void ProgressEventHandler(UploadProgress progress);

        public class UploadProgress
        {
            public int Percentage;
            public string URL;
        }

        public event StringEventHandler FTPOutput;
        public delegate void StringEventHandler(string text);

        public FTPOptions Options;

        private const int BufferSize = 2048;

        public FTPAdapter(FTPOptions options)
        {
            this.Options = options;
        }

        private class ProgressManager
        {
            public UploadProgress Progress = new UploadProgress();

            public bool ChangeProgress(Stream stream)
            {
                return ChangeProgress(stream.Position, stream.Length);
            }

            public bool ChangeProgress(long position, long length)
            {
                int percentage = (int)((double)position / length * 100);
                if (percentage != Progress.Percentage)
                {
                    Progress.Percentage = percentage;
                    return true;
                }
                return false;
            }
        }

        private void ReportProgress(ProgressManager progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(progress.Progress);
            }
        }

        public bool Upload(Stream stream, string url)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Proxy = Options.ProxySettings;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Options.Account.Username, Options.Account.Password);
                request.KeepAlive = false;
                request.UsePassive = !Options.Account.IsActive;

                using (stream)
                using (Stream requestStream = request.GetRequestStream())
                {
                    ProgressManager progress = new ProgressManager();

                    byte[] buffer = new byte[BufferSize];
                    int bytes = stream.Read(buffer, 0, BufferSize);

                    while (bytes > 0)
                    {
                        requestStream.Write(buffer, 0, bytes);

                        if (progress.ChangeProgress(stream)) ReportProgress(progress);

                        bytes = stream.Read(buffer, 0, BufferSize);
                    }
                }

                WriteOutput("Upload: " + url);
                return true;
            }
            catch (Exception ex)
            {
                WriteOutput(string.Format("Error: {0} - Upload: {1}", ex.Message, url));
            }
            return false;
        }

        public bool UploadFile(string filePath, string url)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            return Upload(stream, url);
        }

        public bool UploadText(string text, string url)
        {
            MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(text), false);
            return Upload(stream, url);
        }

        #region Async Methods

        private class AsyncUploadHelper
        {
            public BackgroundWorker BackgroundWorker;
            public Stream Stream;
            public string URL;
        }

        public void AsyncUpload(Stream stream, string url)
        {
            BackgroundWorker bw = new BackgroundWorker { WorkerReportsProgress = true };
            bw.DoWork += new DoWorkEventHandler(bw_AsyncUploadDoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_AsyncUploadProgressChanged);
            AsyncUploadHelper upload = new AsyncUploadHelper { BackgroundWorker = bw, Stream = stream, URL = url };
            bw.RunWorkerAsync(upload);
        }

        private void bw_AsyncUploadDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                AsyncUploadHelper upload = (AsyncUploadHelper)e.Argument;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(upload.URL);
                request.Proxy = this.Options.ProxySettings;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
                request.KeepAlive = false;
                request.UsePassive = !this.Options.Account.IsActive;

                using (upload.Stream)
                using (Stream requestStream = request.GetRequestStream())
                {
                    ProgressManager progress = new ProgressManager();

                    byte[] buffer = new byte[BufferSize];
                    int bytes = upload.Stream.Read(buffer, 0, BufferSize);

                    while (bytes > 0)
                    {
                        requestStream.Write(buffer, 0, bytes);

                        if (progress.ChangeProgress(upload.Stream))
                        {
                            upload.BackgroundWorker.ReportProgress(progress.Progress.Percentage, progress.Progress);
                        }

                        bytes = upload.Stream.Read(buffer, 0, BufferSize);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void bw_AsyncUploadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UploadProgress progress = (UploadProgress)e.UserState;
            if (ProgressChanged != null)
            {
                ProgressChanged(progress);
            }
        }

        public void AsyncUploadFile(string filePath, string url)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            AsyncUpload(stream, url);
        }

        public void AsyncUploadText(string text, string url)
        {
            MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(text), false);
            AsyncUpload(stream, url);
        }

        #endregion

        public bool DownloadFile(string url, string savePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Proxy = this.Options.ProxySettings;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
                request.KeepAlive = false;
                request.UsePassive = !this.Options.Account.IsActive;

                using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                {
                    ProgressManager progress = new ProgressManager();

                    byte[] buffer = new byte[BufferSize];
                    int bytes = stream.Read(buffer, 0, BufferSize), totalBytes = 0;

                    while (bytes > 0)
                    {
                        fileStream.Write(buffer, 0, bytes);

                        totalBytes += bytes;
                        if (progress.ChangeProgress(totalBytes, response.ContentLength)) ReportProgress(progress);

                        bytes = stream.Read(buffer, 0, BufferSize);
                    }
                }

                WriteOutput(string.Format("DownloadFile: {0} -> {1}", url, savePath));
                return true;
            }
            catch (Exception ex)
            {
                WriteOutput(string.Format("Error: {0} - DownloadFile: {1} -> {2}", ex.Message, url, savePath));
            }
            return false;
        }

        public void DeleteFile(string url)
        {
            string filename = FTPHelpers.GetFileName(url);
            if (filename == "." || filename == "..") return;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Proxy = this.Options.ProxySettings;
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
            request.KeepAlive = false;

            request.GetResponse();

            WriteOutput("DeleteFile: " + url);
        }

        public void RemoveDirectory(string url)
        {
            string filename = FTPHelpers.GetFileName(url);
            if (filename == "." || filename == "..") return;

            List<FTPLineResult> files = ListDirectoryDetails(url);
            string path = FTPHelpers.GetDirectoryName(url);

            foreach (FTPLineResult file in files)
            {
                if (file.IsDirectory)
                {
                    RemoveDirectory(FTPHelpers.CombineURL(url, file.Name));
                }
                else
                {
                    DeleteFile(FTPHelpers.CombineURL(url, file.Name));
                }
            }

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Proxy = this.Options.ProxySettings;
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
            request.KeepAlive = false;

            request.GetResponse();

            WriteOutput("RemoveDirectory: " + url);
        }

        public void Rename(string url, string newFileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Proxy = this.Options.ProxySettings;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = newFileName;
            request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
            request.KeepAlive = false;

            request.GetResponse();

            WriteOutput(string.Format("Rename: {0} -> {1}", url, newFileName));
        }

        public long GetFileSize(string url)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Proxy = this.Options.ProxySettings;
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
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

            url = FTPHelpers.AddSlash(url, FTPHelpers.SlashType.Suffix);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Proxy = this.Options.ProxySettings;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
            request.KeepAlive = false;
            request.UsePassive = !this.Options.Account.IsActive;

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

        public List<FTPLineResult> ListDirectoryDetails(string url)
        {
            List<FTPLineResult> result = new List<FTPLineResult>();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Proxy = this.Options.ProxySettings;
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
            request.KeepAlive = false;
            request.UsePassive = !this.Options.Account.IsActive;

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(FTPLineParser.Parse(reader.ReadLine()));
                }

                WriteOutput("ListDirectoryDetails: " + url);

                return result;
            }
        }

        public bool MakeDirectory(string url)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(this.Options.Account.Username, this.Options.Account.Password);
                request.KeepAlive = false;

                request.GetResponse();

                WriteOutput("MakeDirectory: " + url);
                return true;
            }
            catch (Exception ex)
            {
                WriteOutput(string.Format("Error: {0} - MakeDirectory: {1}", ex.Message, url));
            }
            return false;
        }

        public void MakeMultiDirectory(string dirName)
        {
            string path = "";
            string[] dirs = dirName.Split('/');
            foreach (string dir in dirs)
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    path = FTPHelpers.CombineURL(path, dir);
                    MakeDirectory(FTPHelpers.CombineURL(Options.Account.FTPAddress, path));
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