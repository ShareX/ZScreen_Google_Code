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
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ZSS
{
    [Serializable]
    public class FTPAccount
    {
        [Category("FTP"), Description("In list will be visible like this: Name - Server:Port")]
        public string Name { get; set; }

        [Category("FTP"), Description("Host")]
        public string Server { get; set; }

        [Category("FTP"), Description("Port number"), DefaultValue(21)]
        public int Port { get; set; }

        [Category("FTP")]
        public string Username { get; set; }

        [Category("FTP"), PasswordPropertyText(true)]
        public string Password { get; set; }

        [Category("FTP"), Description("FTP path: (ex: screenshots or /htdocs/screenshots)\nEmpty = Use main directory"), DefaultValue("")]
        public string Path { get; set; }

        [Category("FTP"), Description("HTTP path: (ex: brandonz.net/screenshots or %/screenshots)\n" +
            "% = Server, Empty = Auto guess HTTP path (Server + FTP path)"), DefaultValue("")]
        public string HttpPath { get; set; }

        [Category("FTP"), Description("Set true for active or false for passive"), DefaultValue(false)]
        public bool IsActive { get; set; }

        [Category("FTP"), Description("If the folder does not exist it will be created automatically when you press the Test button"), DefaultValue(true)]
        public bool AutoCreateFolder { get; set; }

        public FTPAccount()
        {
            Port = 21;
            Path = "";
            HttpPath = "";
            IsActive = false;
            AutoCreateFolder = true;
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
            if (!string.IsNullOrEmpty(HttpPath))
            {
                fileName = fileName.Replace(" ", "%20");
                string path = FTPHelpers.CombineURL(HttpPath.Replace("%", Server), fileName);
                if (!path.StartsWith("http://")) path = "http://" + path;
                return path;
            }

            return AutoGuessPath(fileName, customPath);
        }

        private string AutoGuessPath(string fileName, bool customPath)
        {
            string path = "";
            if (!customPath) path = Path;
            fileName = FTPHelpers.CombineURL(Server, path, fileName);
            if (!fileName.StartsWith("http://")) fileName = "http://" + fileName;
            return fileName;
        }

        public override string ToString()
        {
            return this.Name + " - " + this.Server + ":" + this.Port;
        }
    }
}