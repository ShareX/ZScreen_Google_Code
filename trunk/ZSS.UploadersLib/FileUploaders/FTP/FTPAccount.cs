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
using System.Text.RegularExpressions;

namespace UploadersLib
{
    [Serializable]
    public class FTPAccount
    {
        [Category("FTP"), Description("Shown in the list as: Name - Server:Port")]
        public string Name { get; set; }

        [Category("FTP"), Description("Host, e.g. brandonz.net")]
        public string Host { get; set; }

        [Category("FTP"), Description("Port Number"), DefaultValue(21)]
        public int Port { get; set; }

        [Category("FTP")]
        public string UserName { get; set; }

        [Category("FTP"), PasswordPropertyText(true)]
        public string Password { get; set; }

        [Category("FTP"), Description("FTP/HTTP Sub-folder Path, e.g. screenshots. SubFolderPath will be automatically appended to HttpHomePath if HttpHomePath does not start with @"), DefaultValue("")]
        public string SubFolderPath { get; set; }

        [Category("FTP"), Description("HTTP Home Path, % = Host e.g. brandonz.net or %\nURL = HttpHomePath (+ SubFolderPath, if HttpHomePath does not start with @) + FileName\nURL = Host + SubFolderPath + FileName (if HttpHomePath is empty)"), DefaultValue("")]
        public string HttpHomePath { get; set; }

        [Category("FTP"), Description("Set true for active or false for passive"), DefaultValue(false)]
        public bool IsActive { get; set; }

        [Category("FTP"), Description("ftp://Host:Port"), Browsable(false)]
        public string FTPAddress
        {
            get
            {
                return string.Format("ftp://{0}:{1}", Host, Port);
            }
        }

        private string exampleFilename = "screenshot.jpg";

        [Category("FTP"), Description("Preview of the FTP Path based on the settings above")]
        public string PreviewFtpPath
        {
            get
            {
                return FTPHelpers.CombineURL(this.FTPAddress, this.SubFolderPath, exampleFilename);
            }
        }

        [Category("FTP"), Description("Preview of the HTTP Path based on the settings above")]
        public string PreviewHttpPath
        {
            get
            {
                return GetUriPath(exampleFilename);
            }
        }

        public FTPAccount()
        {
            Port = 21;
            SubFolderPath = string.Empty;
            HttpHomePath = string.Empty;
            IsActive = false;
        }

        public FTPAccount(string name)
            : this()
        {
            this.Name = name;
        }

        public string GetUriPath(string fileName)
        {
            return GetUriPath(fileName, false);
        }

        public string GetUriPath(string fileName, bool customPath)
        {
            string path = string.Empty;
            string host = this.Host;
            string folderPath = this.SubFolderPath;

            if (host.StartsWith("ftp."))
            {
                host = host.Remove(0, 4);
            }

            if (this.HttpHomePath.StartsWith("@") || customPath)
            {
                folderPath = string.Empty;
            }

            if (string.IsNullOrEmpty(this.HttpHomePath))
            {
                path = FTPHelpers.CombineURL(host, folderPath, fileName);
            }
            else
            {
                fileName = fileName.Replace(" ", "%20");

                string httppath = this.HttpHomePath.Replace("%", host);

                path = FTPHelpers.CombineURL(httppath.StartsWith("@") ? httppath.Substring(1) : httppath,
                                              folderPath, fileName);
            }

            if (!path.StartsWith("http://"))
            {
                path = "http://" + path;
            }

            return path;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}:{2}", this.Name, this.Host, this.Port);
        }
    }
}