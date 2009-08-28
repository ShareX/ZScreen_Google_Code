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
using System.Windows.Forms;
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

        public event FTPAdapter.ProgressEventHandler UploadProgressChanged;

        #region Image Uploader Methods

        public bool Resume { get; set; }
        public bool EnableThumbnail { get; set; }
        public Size ThumbnailSize { get; set; }
        public bool CheckThumbnailSize { get; set; }
        public string WorkingDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Uploads an Image to the FTP. 
        /// If the method fails, it will return a list of zero images
        /// </summary>
        /// <param name="localFilePath"></param>
        /// <returns>Returns a list of images.</returns>
        public ImageFileManager UploadImage(string localFilePath)
        {
            List<ImageFile> ifl = new List<ImageFile>();

            FTPOptions fopt = new FTPOptions { Account = this.FTPAccount, ProxySettings = ProxySettings };

            FTPAdapter ftpClient = new FTPAdapter(fopt);
            ftpClient.ProgressChanged += new FTPAdapter.ProgressEventHandler(ftpClient_UploadProgressChanged);

            string fName = Path.GetFileName(localFilePath);
            string path = FTPHelpers.CombineURL(FTPAccount.FTPAddress, FTPAccount.Path, fName);
            ftpClient.UploadFile(localFilePath, path);

            ifl.Add(new ImageFile(this.FTPAccount.GetUriPath(fName), ImageFile.ImageType.FULLIMAGE));

            if (this.EnableThumbnail)
            {
                try
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
                            string url = FTPHelpers.CombineURL(FTPAccount.FTPAddress, FTPAccount.Path, Path.GetFileName(thPath));
                            ftpClient.UploadFile(thPath, url);
                        }
                        ifl.Add(new ImageFile(this.FTPAccount.GetUriPath(Path.GetFileName(thPath)), ImageFile.ImageType.THUMBNAIL));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            ImageFileManager ifm = new ImageFileManager(ifl) { LocalFilePath = localFilePath };
            return ifm;
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
        /// If the method fails, it will return a list of zero images
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Returns a list of images.</returns>
        public override string UploadText(TextInfo text)
        {
            FTPOptions fopt = new FTPOptions();
            fopt.Account = this.FTPAccount;
            fopt.ProxySettings = Uploader.ProxySettings;
            FTPAdapter ftpClient = new FTPAdapter(fopt);
            if (string.IsNullOrEmpty(text.LocalPath))
            {
                text.LocalPath = Path.Combine(WorkingDir, DateTime.Now.Ticks + ".txt");
                File.WriteAllText(text.LocalPath, text.LocalString);
            }
            string fileName = Path.GetFileName(text.LocalPath);
            string url = FTPHelpers.CombineURL(FTPAccount.FTPAddress, FTPAccount.Path, fileName);
            ftpClient.UploadText(text.LocalString, url);
            return this.FTPAccount.GetUriPath(fileName);
        }

        #endregion

        private void ftpClient_UploadProgressChanged(int progress)
        {
            UploadProgressChanged(progress);
        }

        public string ToErrorString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string err in this.Errors)
            {
                sb.AppendLine(err);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Descriptive name for the FTP Uploader
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("FTP ({0})", this.Name);
        }

    }
}