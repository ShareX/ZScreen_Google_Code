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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace Updater
{
    public class FileDownloader
    {
        public string URL { get; private set; }
        public bool IsDownloading { get; private set; }
        public long FileSize { get; private set; }
        public long DownloadedSize { get; private set; }
        public double DownloadSpeed { get; private set; }

        public double DownloadPercentage
        {
            get
            {
                if (FileSize > 0)
                {
                    return (double)DownloadedSize / FileSize * 100;
                }

                return 0;
            }
        }

        public Exception LastException { get; private set; }
        public bool IsPaused { get; private set; }

        public event EventHandler FileSizeReceived;
        public event EventHandler DownloadStarted;
        public event EventHandler ProgressChanged;
        public event EventHandler DownloadCompleted;
        public event EventHandler ExceptionThrowed;

        private BackgroundWorker worker;
        private Stream stream;
        private const int bufferSize = 4096;

        public FileDownloader(string url, Stream stream)
        {
            URL = url;
            this.stream = stream;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsDownloading = false;
        }

        public void StartDownload()
        {
            if (!IsDownloading && !string.IsNullOrEmpty(URL) && !worker.IsBusy)
            {
                IsDownloading = true;
                IsPaused = false;
                worker.RunWorkerAsync();
            }
        }

        public void ResumeDownload()
        {
            if (IsDownloading)
            {
                IsPaused = false;
            }
        }

        public void PauseDownload()
        {
            if (IsDownloading)
            {
                IsPaused = true;
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            HttpWebResponse response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                response = (HttpWebResponse)request.GetResponse();
                FileSize = response.ContentLength;
                ThrowEvent(FileSizeReceived);

                if (FileSize > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    long speedTest = 0;

                    byte[] buffer = new byte[(int)Math.Min(bufferSize, FileSize)];
                    int bytesRead = 0;

                    ThrowEvent(DownloadStarted);

                    while (DownloadedSize < FileSize && !worker.CancellationPending)
                    {
                        while (IsPaused)
                        {
                            timer.Reset();
                            Thread.Sleep(10);
                        }

                        if (!timer.IsRunning)
                        {
                            timer.Start();
                        }

                        bytesRead = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, bytesRead);
                        DownloadedSize += bytesRead;
                        speedTest += bytesRead;

                        if (timer.ElapsedMilliseconds > 500)
                        {
                            DownloadSpeed = (double)speedTest / timer.ElapsedMilliseconds * 1000;
                            speedTest = 0;
                            timer.Reset();
                        }

                        ThrowEvent(ProgressChanged);
                    }

                    ThrowEvent(DownloadCompleted);
                }
            }
            catch (Exception ex)
            {
                LastException = ex;
                ThrowEvent(ExceptionThrowed);
            }
            finally
            {
                if (response != null) response.Close();
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            EventHandler eventHandler = e.UserState as EventHandler;

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }
        }

        private void ThrowEvent(EventHandler eventHandler)
        {
            worker.ReportProgress(0, eventHandler);
        }
    }
}