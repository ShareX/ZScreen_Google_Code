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
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class FileBin : FileUploader
    {
        public override string Name
        {
            get { return "FileBin"; }
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("MAX_FILE_SIZE", "82428800");

            string response = UploadData(stream, fileName, "http://filebin.ca/upload.php", "file", args);

            if (!string.IsNullOrEmpty(response))
            {
                string url = response.Substring(response.LastIndexOf(' ') + 1).Trim();
                return new UploadResult(response, url);
            }

            return null;
        }
    }
}