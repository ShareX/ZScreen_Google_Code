#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2010 ZScreen Group

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
using System.Web;
using HelpersLib;
using System.Text.RegularExpressions;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace UploadersLib
{
    [Serializable]
    public class LocalhostAccount
    {
        [Category("Localhost"), Description("Shown in the list as: Name - LocalhostRoot:Port")]
        public string Name { get; set; }

        [Category("Localhost"), Description(@"Root folder, e.g. C:\Inetpub\wwwroot")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string LocalhostRoot { get; set; }

        [Category("Localhost"), Description("Port Number"), DefaultValue(80)]
        public int Port { get; set; }

        [Category("Localhost")]
        public string UserName { get; set; }

        [Category("Localhost"), PasswordPropertyText(true)]
        public string Password { get; set; }

        [Category("Localhost"), Description("Localhost Sub-folder Path, e.g. screenshots, %y = year, %mo = month. SubFolderPath will be automatically appended to HttpHomePath if HttpHomePath does not start with @"), DefaultValue("")]
        public string SubFolderPath { get; set; }

        [Category("Localhost"), Description("HTTP Home Path, %host = Host e.g. brandonz.net\nURL = HttpHomePath (+ SubFolderPath, if HttpHomePath does not start with @) + FileName\nURL = Host + SubFolderPath + FileName (if HttpHomePath is empty)"), DefaultValue("")]
        public string HttpHomePath { get; set; }

        [Category("Localhost"), Description("Set true for active or false for passive"), DefaultValue(false)]
        public bool IsActive { get; set; }

        [Category("Localhost"), Description("file://Host:Port"), Browsable(false)]
        public string LocalUri
        {
            get
            {
                if (string.IsNullOrEmpty(this.LocalhostRoot))
                {
                    return string.Empty;
                }

                return new Uri(LocalhostRoot).AbsoluteUri;
            }
        }

        private string exampleFilename = "screenshot.jpg";

        [Category("Localhost"), Description("Preview of the Localhost Path based on the settings above")]
        public string PreviewLocalPath
        {
            get
            {
                return GetLocalhostUri(exampleFilename);
            }
        }

        [Category("Localhost"), Description("Preview of the HTTP Path based on the settings above")]
        public string PreviewRemotePath
        {
            get
            {
                return GetUriPath(exampleFilename);
            }
        }

        public LocalhostAccount()
        {
            Port = 80;
            SubFolderPath = string.Empty;
            HttpHomePath = string.Empty;
            IsActive = false;
        }

        public LocalhostAccount(string name)
            : this()
        {
            this.Name = name;
        }

        public string GetSubFolderPath()
        {
            return NameParser.Convert(new NameParserInfo(NameParserType.Text, this.SubFolderPath) { Host = this.LocalhostRoot, IsFolderPath = true });
        }

        public string GetHttpHomePath()
        {
            return NameParser.Convert(new NameParserInfo(NameParserType.Text, this.HttpHomePath) { Host = this.LocalhostRoot, IsFolderPath = true });
        }

        public string GetUriPath(string fileName)
        {
            return GetUriPath(fileName, false);
        }

        public string GetUriPath(string fileName, bool customPath)
        {
            if (string.IsNullOrEmpty(this.LocalhostRoot))
            {
                return string.Empty;
            }

            fileName = HttpUtility.UrlEncode(fileName).Replace("+", "%20");
            string path = string.Empty;
            string httppath = string.Empty;
            string lHttpHomePath = GetHttpHomePath();
            string lFolderPath = this.GetSubFolderPath();

            if (lHttpHomePath.StartsWith("@") || customPath)
            {
                lFolderPath = string.Empty;
            }

            if (string.IsNullOrEmpty(lHttpHomePath))
            {
                httppath = this.LocalhostRoot;
            }
            else
            {
                httppath = lHttpHomePath.Replace("%host", this.LocalhostRoot).TrimStart('@');
            }
            path = FTPHelpers.CombineURL(string.Format("{0}:{1}", httppath, this.Port), lFolderPath, fileName);

            if (!path.StartsWith("http://"))
            {
                path = "http://" + path;
            }

            return path;
        }

        public string GetLocalhostPath(string fileName)
        {
            if (string.IsNullOrEmpty(LocalhostRoot))
            {
                return string.Empty;
            }
            return FTPHelpers.CombineURL(LocalhostRoot, this.GetSubFolderPath(), fileName);
        }

        public string GetLocalhostUri(string fileName)
        {
            string LocalhostAddress = this.LocalUri;

            if (string.IsNullOrEmpty(LocalhostAddress))
            {
                return string.Empty;
            }

            return FTPHelpers.CombineURL(LocalhostAddress, this.GetSubFolderPath(), fileName);
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}:{2}", this.Name, this.LocalhostRoot, this.Port);
        }
    }
}