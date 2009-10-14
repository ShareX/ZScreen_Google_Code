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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ZScreenLib.Helpers
{
    public class ThumbnailCacher
    {
        public Size ThumbnailSize { get; set; }
        public Image LoadingImage { get; set; }

        private Queue<Thumbnail> thumbnails;
        private PictureBox pictureBox;
        private int capacity;
        private BackgroundWorker worker;
        private int bufferSize = 2048;

        public delegate void ProgressChangedEventHandler(int percentage, long position, long length);
        public event ProgressChangedEventHandler ProgressChanged;

        public ThumbnailCacher(PictureBox pictureBox, Size thumbnailSize, int capacity)
        {
            this.pictureBox = pictureBox;
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            this.ThumbnailSize = thumbnailSize;
            this.capacity = capacity;
            thumbnails = new Queue<Thumbnail>(capacity);
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        public void LoadImage(string path)
        {
            StopWorker(1000);

            Thumbnail thumb = thumbnails.FirstOrDefault(x => x.Path == path);

            if (thumb != null && thumb.Image != null)
            {
                SetImage(thumb.Image);
            }
            else
            {
                if (LoadingImage != null)
                {
                    SetImage(LoadingImage);
                }

                worker.RunWorkerAsync(new Thumbnail(path));
            }
        }

        #region Private Events

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thumbnail thumb = e.Argument as Thumbnail;

            if (thumb != null && !string.IsNullOrEmpty(thumb.Path))
            {
                MemoryStream stream = null;

                try
                {
                    if (thumb.Path.StartsWith("http://") || thumb.Path.StartsWith("www."))
                    {
                        stream = DownloadFile(thumb.Path);
                    }
                    else if (File.Exists(thumb.Path))
                    {
                        stream = LoadFile(thumb.Path);
                    }

                    if (stream != null && stream.Length > 0)
                    {
                        using (Image image = Image.FromStream(stream))
                        {
                            thumb.Image = GraphicsMgr.ChangeImageSize(image, ThumbnailSize, true);
                            e.Result = thumb;
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                }
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressInfo progress = e.UserState as ProgressInfo;

            if (progress != null)
            {
                OnProgressChanged(progress.Position, progress.Length);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Thumbnail thumb = e.Result as Thumbnail;

            if (!e.Cancelled && thumb != null && thumb.Image != null)
            {
                thumbnails.Enqueue(thumb);
                SetImage(thumb.Image);
            }
        }

        #endregion

        #region Private Helpers Methods

        private MemoryStream LoadFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return LoadStream(stream, stream.Length);
            }
        }

        private MemoryStream DownloadFile(string path)
        {
            long length = GetFileSize(path);

            if (length > 0)
            {
                using (WebClient webClient = new WebClient())
                using (Stream stream = webClient.OpenRead(path))
                {
                    return LoadStream(stream, length);
                }
            }

            return null;
        }

        private MemoryStream LoadStream(Stream stream, long length)
        {
            byte[] buffer = new byte[bufferSize];
            MemoryStream memoryStream = new MemoryStream((int)length);
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (worker.CancellationPending)
                {
                    return null;
                }

                memoryStream.Write(buffer, 0, bytesRead);

                worker.ReportProgress(0, new ProgressInfo(memoryStream.Position, memoryStream.Length));
            }

            return memoryStream;
        }

        private void SetImage(Image image)
        {
            if (pictureBox != null)
            {
                pictureBox.Image = image;
            }
        }

        private long GetFileSize(string url)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Credentials = CredentialCache.DefaultCredentials;
                webRequest.Timeout = 3000;
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    return webResponse.ContentLength;
                }
            }
            catch
            {
                return -1;
            }
        }

        private void OnProgressChanged(long position, long length)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged((int)((double)position / length * 100), position, length);
            }
        }

        private void StopWorker(int timeout)
        {
            for (int i = 0; i < timeout; i++)
            {
                if (!worker.IsBusy)
                {
                    break;
                }

                worker.CancelAsync();
                Application.DoEvents();
                Thread.Sleep(1);
            }
        }

        #endregion
    }

    public class Thumbnail
    {
        public Image Image { get; set; }
        public string Path { get; set; }

        public Thumbnail(string path)
        {
            Path = path;
        }
    }

    public class ProgressInfo
    {
        public long Position { get; set; }
        public long Length { get; set; }

        public ProgressInfo(long position, long length)
        {
            Position = position;
            Length = length;
        }
    }
}