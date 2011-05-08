#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.IO;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class FTPUploader : FileUploader
    {
        public FTPAccount FTPAccount;

        public FTPUploader(FTPAccount account)
        {
            FTPAccount = account;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult result = new UploadResult();

            using (FTP ftpClient = new FTP(FTPAccount))
            {
                ftpClient.ProgressChanged += new Uploader.ProgressEventHandler(x => OnProgressChanged(x));

                fileName = Helpers.ReplaceIllegalChars(fileName, '_');

                while (fileName.IndexOf("__") != -1)
                {
                    fileName = fileName.Replace("__", "_");
                }

                string remotePath = FTPHelpers.CombineURL(FTPAccount.GetSubFolderPath(), fileName);

                try
                {
                    stream.Position = 0;
                    ftpClient.UploadData(stream, remotePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    this.Errors.Add(e.Message);
                }

                if (Errors.Count == 0)
                {
                    result.URL = FTPAccount.GetUriPath(fileName);
                }
            }

            return result;
        }
    }
}