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
using System.Collections.Generic;
using System.IO;
using System.Text;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class DekiWikiUploader
    {
        public DekiWikiOptions Options { get; set; }

        private List<string> Errors { get; set; }

        public string WorkingDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public DekiWikiUploader(DekiWikiOptions options)
        {
            this.Options = options;
            this.Errors = new List<string>();
        }

        public UploadResult UploadImage(string localFilePath)
        {
            // Create the connector
            DekiWiki connector = new DekiWiki(this.Options);

            // Get the file name to save
            string fName = Path.GetFileName(localFilePath);

            // Upload the image
            connector.UploadImage(localFilePath, fName);

            // Create the file manager object
            UploadResult ur = new UploadResult() { URL = this.Options.Account.getUriPath(fName) };

            return ur;
        }

        public string ToErrorString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string err in this.Errors)
            {
                sb.AppendLine(err);
            }
            return sb.ToString();
        }
    }
}