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

namespace ZSS
{
    [Serializable()]
    public class FTPAccount
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        [Category("FTP"), PasswordPropertyText(true)]
        public string Password { get; set; }

        private string mPath = "/";
        [Category("FTP"), Description("FTP Path: (ex: / or /htdocs/screenshots)")]
        public string Path { get { return mPath; } set { mPath = value; } }

        private string mHttpPath = "%/";
        [Category("FTP"), Description("HTTP Path: (ex: brandonz.net/screenshots)")]
        public string HttpPath { get { return mHttpPath; } set { mHttpPath = value; } }

        private int mPort = 21;
        [Category("FTP"), Description("Port Number")]
        public int Port { get { return mPort; } set { mPort = value; } }

        [Category("FTP"), Description("Set True if Active, False if Passive")]
        public bool IsActive { get; set; }

        public FTPAccount() { }

        public FTPAccount(string name)
        {
            this.Name = name;
        }

        public string getUriPath(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(HttpPath))
            {
                if (!HttpPath.Contains("http://"))
                    sb.Append("http://");
                sb.Append(HttpPath.Replace("%", Server));
            }

            sb.Append("/");

            fileName = System.Text.RegularExpressions.Regex.Replace(fileName, " ", "%20");
            sb.Append(fileName);

            return sb.ToString();
        }
    }
}