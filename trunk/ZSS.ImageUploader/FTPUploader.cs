#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZSS.ImageUploader
{
    public class FTPUploader : IUploader
    {
        private FTPAccount mFTPAccount = null;

        public FTPUploader(FTPAccount acc)
        {
            this.mFTPAccount = acc;
        }

        public bool Resume { get; set; }
        public bool EnableThumbnail { get; set; }

        public string WorkingDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Uploads an Image to the FTP. 
        /// If the method fails, it will return a list of zero images
        /// </summary>
        /// <param name="localFilePath"></param>
        /// <returns>Returns a list of images.</returns>
        public List<ImageFile> UploadImage(string localFilePath)
        {
            // Create a new ImageFile List
            List<ImageFile> ifl = new List<ImageFile>();

            try
            {
                FTP ftpClient = new FTP(ref this.mFTPAccount);
                //removed binary mode code line

                string fName = Path.GetFileName(localFilePath);
                ftpClient.OpenUpload(localFilePath, fName, this.Resume);
                //int perc = 0;
                while (ftpClient.DoUpload() > 0)
                {
                    //perc = (int)(((ff.BytesTotal) * 100) / ff.FileSize);
                }

                ifl.Add(new ImageFile(this.mFTPAccount.getUriPath(fName), ImageFile.ImageType.FULLIMAGE));

                if (this.EnableThumbnail)
                {
                    // load img to memory
                    Bitmap img = LoadBitmap(localFilePath);
                    double sf = 128.0 / img.Width;
                    img = ResizeBitmap(img, (int)(img.Width * sf), (int)(img.Height * sf));
                    StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(fName));
                    sb.Append(".th");
                    sb.Append(Path.GetExtension(fName));
                    string thPath = Path.Combine(this.WorkingDir, sb.ToString());
                    img.Save(thPath);
                    if (File.Exists(thPath))
                    {
                        ftpClient.OpenUpload(thPath, Path.GetFileName(thPath), this.Resume);
                        while (ftpClient.DoUpload() > 0)
                        {
                            // Do nothing
                        }
                    }
                    ifl.Add(new ImageFile(this.mFTPAccount.getUriPath(Path.GetFileName(thPath)), ImageFile.ImageType.THUMBNAIL));
                    ifl.Add(ImageFile.getThumbnailForum1ImageFile(this.mFTPAccount.getUriPath(fName),
                        this.mFTPAccount.getUriPath(Path.GetFileName(thPath))));
                }

                //We do not want to disconnect here. We would rather have the connection time out.
                ftpClient.Disconnect();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.WriteLine(Program.replaceErrorMessages(ex.Message), "ZScreen FTP");
                //MessageBox.Show(Program.replaceErrorMessages(ex.Message), "ZScreen FTP");
            }

            return ifl;
        }

        public Bitmap LoadBitmap(string filepath)
        {
            Bitmap img = null;
            try
            {
                Image temp = Image.FromFile(filepath);
                img = new Bitmap(temp);
                temp.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return img;
        }

        public Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }

    }
}
