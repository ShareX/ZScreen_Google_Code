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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using UploadersLib.Helpers;

namespace UploadersLib
{
    [Serializable]
    public sealed class FTPUploader : TextUploader, IUploader
    {
        public FTPAccount FTPAccount;

        public string Name { get; set; }

        public static readonly string Hostname = TextDestType.FTP.GetDescription();

        public FTPUploader()
        {
            this.Errors = new List<string>();
            this.FTPAccount = new FTPAccount();
        }

        public FTPUploader(FTPAccount acc)
            : this()
        {
            this.FTPAccount = acc;
            this.Name = FTPAccount.Name;
        }

        public event ProgressEventHandler UploadProgressChanged;

        public bool EnableThumbnail { get; set; }

        public Size ThumbnailSize { get; set; }

        /// <summary>
        /// If image size smaller than thumbnail size then not make thumbnail
        /// </summary>
        public bool CheckThumbnailSize { get; set; }

        public string WorkingDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        #region Image Uploader Methods

        /// <summary>
        /// Uploads an Image to the FTP
        /// If the method fails, it will return a list of zero images
        /// </summary>
        /// <returns>Returns a list of images</returns>
        public ImageFileManager UploadImage(string localFilePath)
        {
            List<ImageFile> ifl = new List<ImageFile>();

            string fName = Path.GetFileName(localFilePath);
            string path = FTPHelpers.CombineURL(FTPAccount.Path, fName);

            try
            {
                using (FTP ftpClient = new FTP(this.FTPAccount))
                {
                    ftpClient.ProgressChanged += new FTP.FTPProgressEventHandler(ftpClient_UploadProgressChanged);
                    ftpClient.UploadFile(localFilePath, path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                this.Errors.Add(e.Message);
            }

            if (this.Errors.Count == 0)
            {
                ifl.Add(new ImageFile(this.FTPAccount.GetUriPath(fName), ImageFile.ImageType.FULLIMAGE));

                if (this.EnableThumbnail)
                {
                    Image img = LoadBitmap(localFilePath);
                    if (img != null && (!this.CheckThumbnailSize ||
                        (this.CheckThumbnailSize && (img.Width > this.ThumbnailSize.Width || img.Height > this.ThumbnailSize.Height))))
                    {
                        img = ResizeBitmap(img, ThumbnailSize);
                        StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(fName));
                        sb.Append(".th");
                        sb.Append(Path.GetExtension(fName));
                        string thPath = Path.Combine(this.WorkingDir, sb.ToString());
                        img.Save(thPath);
                        if (File.Exists(thPath))
                        {
                            string url = FTPHelpers.CombineURL(FTPAccount.Path, Path.GetFileName(thPath));
                            try
                            {
                                ftpClient.UploadFile(thPath, url);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                this.Errors.Add(e.Message);
                            }

                            if (this.Errors.Count == 0)
                            {
                                ifl.Add(new ImageFile(this.FTPAccount.GetUriPath(Path.GetFileName(thPath)), ImageFile.ImageType.THUMBNAIL));
                            }
                        }
                    }
                }
            }

            return new ImageFileManager(ifl) { LocalFilePath = localFilePath };
        }

        public Bitmap LoadBitmap(string filepath)
        {
            try
            {
                using (Image temp = Image.FromFile(filepath))
                {
                    return new Bitmap(temp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static Image ResizeBitmap(Image img, int width, int height)
        {
            Image bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            return bmp;
        }

        public static Image ResizeBitmap(Image img, Size size)
        {
            return ResizeBitmap(img, size.Width, size.Height);
        }

        #endregion

        #region Text Uploader Methods

        public override string TesterString
        {
            get { return "Testing " + this.ToString(); }
        }

        public override object Settings
        {
            get
            {
                return this.FTPAccount;
            }
            set
            {
                this.FTPAccount = (FTPAccount)value;
            }
        }

        /// <summary>
        /// Uploads Text to the FTP. 
        /// If the method fails, it will return null
        /// </summary>
        public override string UploadText(TextInfo text)
        {
            if (string.IsNullOrEmpty(text.LocalPath))
            {
                text.LocalPath = Path.Combine(WorkingDir, DateTime.Now.Ticks + ".txt");
                File.WriteAllText(text.LocalPath, text.LocalString);
            }

            string fileName = Path.GetFileName(text.LocalPath);
            string url = FTPHelpers.CombineURL(FTPAccount.Path, fileName);

            try
            {
                using (FTP ftpClient = new FTP(this.FTPAccount))
                {
                    ftpClient.ProgressChanged += new FTP.FTPProgressEventHandler(ftpClient_UploadProgressChanged);
                    ftpClient.UploadText(text.LocalString, url);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                this.Errors.Add(e.Message);
            }

            if (this.Errors.Count == 0)
            {
                return this.FTPAccount.GetUriPath(fileName);
            }

            return null;
        }

        #endregion

        private void ftpClient_UploadProgressChanged(float progress)
        {
            UploadProgressChanged((int)progress);
        }

        /// <summary>
        /// Descriptive name for the FTP Uploader
        /// </summary>
        public override string ToString()
        {
            return string.Format("FTP ({0})", this.Name);
        }
    }
}