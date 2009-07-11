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
        private const int BufferSize = 2048;

        public FTPAccount Account;

        private string FTPAddress { get { return string.Format("ftp://{0}:{1}", Account.Server, Account.Port); } }

        public FTP()
        {
            Account = new FTPAccount();
        }

        public FTP(FTPAccount acc)
        {
            Account = acc;
        }

        public void Upload(Stream stream, string remoteName)
        {
            try
            {
                string url = CombineURL(FTPAddress, Account.Path, remoteName);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UploadFile(string filePath, string remoteName)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            Upload(stream, remoteName);
        }

        public void UploadText(string text, string remoteName)
        {
            MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(text), false);
            Upload(stream, remoteName);
        }

        public void DownloadFile(string fileName, string savePath)
        {
            try
            {
                string url = CombineURL(FTPAddress, fileName);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DeleteFile(string fileName)
        {
            string url = CombineURL(FTPAddress, fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);

            request.GetResponse();
        }

        public void Rename(string fileName, string newFileName)
        {
            string url = CombineURL(FTPAddress, fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = newFileName;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);

            request.GetResponse();
        }

        public long GetFileSize(string fileName)
        {
            string url = CombineURL(FTPAddress, fileName);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = new NetworkCredential(Account.Username, Account.Password);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
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

                return result.ToArray();
            }
        }

        public string[] ListDirectory()
        {
            string url = CombineURL(FTPAddress, Account.Path);
            return ListDirectory(url);
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

                return result.ToArray();
            }
        }

        public FTPLineResult[] ListDirectoryDetails()
        {
            string url = CombineURL(FTPAddress, Account.Path);
            return ListDirectoryDetails(url);
        }

        public void MakeMultiDirectory(string dirName)
        {
            string path = "";
            string[] dirs = dirName.Split('/');
            foreach (string dir in dirs)
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    path += dir + "/";
                    MakeDirectory(path);
                }
            }
        }

        public void MakeDirectory(string dirName)
        {
            try
            {
                string url = CombineURL(FTPAddress, dirName);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(Account.Username, Account.Password);

                request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static string CombineURL(string url1, string url2)
        {
            if (string.IsNullOrEmpty(url1) || string.IsNullOrEmpty(url2))
            {
                if (!string.IsNullOrEmpty(url1))
                {
                    return url1;
                }
                else if (!string.IsNullOrEmpty(url2))
                {
                    return url2;
                }
                else
                {
                    return "";
                }
            }
            if (url1.EndsWith("/"))
            {
                url1 = url1.Substring(0, url1.Length - 1);
            }
            if (url2.StartsWith("/"))
            {
                url2 = url2.Remove(0, 1);
            }
            return url1 + "/" + url2;
        }

        public static string CombineURL(params string[] urls)
        {
            return urls.Aggregate((current, arg) => CombineURL(current, arg));
        }

        public static class FTPLineParser
        {
            private static Regex unixStyle = new Regex(@"^(?<Permissions>(?<Directory>[-dl])(?<OwnerPerm>[-r][-w][-x])(?<GroupPerm>[-r][-w][-x])(?<EveryonePerm>[-r][-w][-x]))\s+(?<FileType>\d)\s+(?<Owner>\w+)\s+(?<Group>\w+)\s+(?<Size>\d+)\s+(?<Month>\w+)\s+(?<Day>\d{1,2})\s+(?<Year>(?<Hour>\d{1,2}):*(?<Minutes>\d{1,2}))\s+(?<Name>.*)$");
            private static Regex winStyle = new Regex(@"^(?<Month>\d{1,2})-(?<Day>\d{1,2})-(?<Year>\d{1,2})\s+(?<Hour>\d{1,2}):(?<Minutes>\d{1,2})(?<ampm>am|pm)\s+(?<Dir>[<]dir[>])?\s+(?<Size>\d+)?\s+(?<Name>.*)$");

            public static FTPLineResult Parse(string line)
            {
                Match match = unixStyle.Match(line);
                if (match.Success)
                {
                    return ParseMatch(match.Groups, ListStyle.Unix);
                }

                match = winStyle.Match(line);
                if (match.Success)
                {
                    return ParseMatch(match.Groups, ListStyle.Windows);
                }

                throw new Exception("Invalid line format");
            }

            private static FTPLineResult ParseMatch(GroupCollection matchGroups, ListStyle style)
            {
                FTPLineResult result = new FTPLineResult();

                result.Style = style;
                string dirMatch = style == ListStyle.Unix ? "d" : "<dir>";
                result.IsDirectory = matchGroups["Directory"].Value.Equals(dirMatch, StringComparison.InvariantCultureIgnoreCase);
                result.Permissions = matchGroups["Permissions"].Value;
                result.Name = matchGroups["Name"].Value;
                if (!result.IsDirectory) result.SetSize(matchGroups["Size"].Value);
                result.Owner = matchGroups["Owner"].Value;
                result.Group = matchGroups["Group"].Value;
                result.SetDateTime(matchGroups["Year"].Value, matchGroups["Month"].Value, matchGroups["Day"].Value);

                return result;
            }
        }

        public enum ListStyle
        {
            Unix,
            Windows
        }

        public class FTPLineResult
        {
            public ListStyle Style;
            public string Name;
            public string Permissions;
            public DateTime DateTime;
            public bool TimeInfo;
            public bool IsDirectory;
            public long Size;
            public string SizeString;
            public string Owner;
            public string Group;

            public void SetSize(string size)
            {
                this.Size = long.Parse(size);
                this.SizeString = this.Size.ToString("N0");
            }

            public void SetDateTime(string year, string month, string day)
            {
                string time = "";

                if (year.Contains(':'))
                {
                    time = year;
                    year = DateTime.Now.Year.ToString();
                    this.TimeInfo = true;
                }

                this.DateTime = DateTime.Parse(string.Format("{0}/{1}/{2} {3}", year, month, day, time));
                this.DateTime = this.DateTime.ToLocalTime();
            }
        }
    }
}