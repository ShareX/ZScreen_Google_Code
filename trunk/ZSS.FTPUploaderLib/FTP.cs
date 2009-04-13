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
using System.IO;
using System.Net;
using System.Text;

namespace ZSS
{
    public class FTP
    {
        private FTPAccount mAccount;

        public FTP()
        {
            mAccount = new FTPAccount();
        }

        public FTP(ref FTPAccount acc)
        {
            mAccount = acc;
        }

        public void UploadFile(string fileName, string remoteName)
        {
            int bufferSize = 2 * 1024;
            byte[] buffer = new byte[bufferSize];

            FileInfo fi = new FileInfo(fileName);

            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + mAccount.Path + "/" + fi.Name);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.ContentLength = fi.Length;
            request.KeepAlive = false;
            request.UseBinary = true;
            request.UsePassive = !mAccount.IsActive;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FileStream fs = fi.OpenRead();

            Stream stream = request.GetRequestStream();

            int contentLength = fs.Read(buffer, 0, bufferSize);

            while (contentLength != 0)
            {
                stream.Write(buffer, 0, contentLength);
                contentLength = fs.Read(buffer, 0, bufferSize);
            }

            stream.Close();
            fs.Close();
        }

        public void DownloadFile(string fileName, string filePath)
        {
            int bufferSize = 1024 * 2;
            byte[] buffer = new byte[bufferSize];

            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + mAccount.Path + "/" + fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            FileStream fStream = new FileStream(filePath, FileMode.Create);

            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = !mAccount.IsActive;
            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            int bytes = stream.Read(buffer, 0, bufferSize);

            while (bytes > 0)
            {
                fStream.Write(buffer, 0, bytes);
                bytes = stream.Read(buffer, 0, bufferSize);
            }

            stream.Close();
            fStream.Close();
            response.Close();
        }

        public void DeleteFile(string fileName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);

            streamReader.Close();
            stream.Close();
            response.Close();
        }

        public void Rename(string fileName, string newFileName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = newFileName;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            stream.Close();
            response.Close();
        }

        public long GetFileSize(string fileName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            long fileSize = response.ContentLength;

            stream.Close();
            response.Close();

            return fileSize;
        }

        public string[] ListDirectory()
        {
            StringBuilder result = new StringBuilder();

            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + mAccount.Path + "/");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.UseBinary = true;
            request.UsePassive = !mAccount.IsActive;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string readLine = reader.ReadLine();

            while (readLine != null)
            {
                result.Append(readLine);
                result.Append("\n");
                readLine = reader.ReadLine();
            }

            result.Remove(result.ToString().LastIndexOf('\n'), 1);

            reader.Close();
            response.Close();

            return result.ToString().Split('\n');
        }

        private void MakeDirectory(string dirName)
        {
            Uri uri = new Uri("ftp://" + mAccount.Server + ":" + mAccount.Port + "/" + dirName + "/");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(mAccount.Username, mAccount.Password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            stream.Close();
            response.Close();
        }
    }
}