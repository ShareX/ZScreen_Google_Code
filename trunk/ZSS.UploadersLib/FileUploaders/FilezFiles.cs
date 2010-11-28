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

#endregion License Information (GPL v2)

using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class FilezFiles : FileUploader
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool HideFile { get; set; }

        public override string Name
        {
            get { return "Filez"; }
        }

        public FilezFiles(string username = "", string password = "", bool hideFile = true)
        {
            Username = username;
            Password = password;
            HideFile = hideFile;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            args.Add("hideFile", HideFile ? "1" : "0");

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                args.Add("username", Username);
                args.Add("userpass", Password);
            }

            string response = UploadData(stream, fileName, "http://www.filez.muffinz.eu/api/upload", "file", args);

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response) && !response.StartsWith("{\"error\""))
            {
                Dictionary<string, Dictionary<string, string>> data = new JavaScriptSerializer().Deserialize<Dictionary<string, Dictionary<string, string>>>(response);
                if (data["info"] != null)
                {
                    result.URL = data["info"]["forcedl"];
                }
            }

            return result;
        }
    }
}