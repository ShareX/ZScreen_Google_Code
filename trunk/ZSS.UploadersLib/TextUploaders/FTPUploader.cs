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
using System.Text;
using UploadersLib.Helpers;
using ZSS;

namespace UploadersLib
{
    [Serializable]
    public sealed class FTPUploader : TextUploader
    {
        public FTPAccount FTPAccount;

        public const string Hostname = "FTP";

        public FTPUploader()
        {
            this.Errors = new List<string>();
            this.FTPAccount = new FTPAccount();
        }

        public FTPUploader(FTPAccount acc)
            : this()
        {
            this.FTPAccount = acc;
        }

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
            fopt.ProxySettings = this.ProxySettings;
            FTPAdapter ftpClient = new FTPAdapter(fopt);
            string fileName = Path.GetFileName(text.LocalPath);
            string url = FTPHelpers.CombineURL(FTPAccount.FTPAddress, FTPAccount.Path, fileName);
            ftpClient.UploadText(text.LocalString, url);
            return this.FTPAccount.GetUriPath(fileName);
        }

        /// <summary>
        /// Descriptive name for the FTP Uploader
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("FTP ({0})", FTPAccount.Name);
        }
    }
}