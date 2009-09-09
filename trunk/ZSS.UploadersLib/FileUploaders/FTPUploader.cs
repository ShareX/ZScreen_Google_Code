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

namespace UploadersLib.FileUploaders
{
    public sealed class FTPUploader : FileUploader
    {
        public FTPAccount FTPAccount;

        public override string Name
        {
            get { return "FTP Uploader"; }
        }

        public FTPUploader(FTPAccount acc)
        {
            this.FTPAccount = acc;
            //this.Name = acc.Name;
        }

        public override string Upload(byte[] data, string fileName)
        {
            using (FTP ftpClient = new FTP(this.FTPAccount))
            {
                ftpClient.ProgressChanged += new FTP.FTPProgressEventHandler(x => OnProgressChanged((int)x));
                string remotePath = FTPHelpers.CombineURL(FTPAccount.SubFolderPath, fileName);

                try
                {
                    ftpClient.UploadData(data, remotePath);
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
            }

            return string.Empty;
        }

        /*
        /// <summary>
        /// Uploads an Image to the FTP
        /// If the method fails, it will return a list of zero images
        /// </summary>
        /// <returns>Returns a list of images</returns>
        public ImageFileManager UploadImage(string localFilePath)
        {
            if (this.EnableThumbnail)
            {
                Image img = null;
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
        */
    }
}