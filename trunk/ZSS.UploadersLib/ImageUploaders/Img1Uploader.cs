#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2010  Brandon Zimmerman

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

using System.IO;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class Img1Uploader : ImageUploader
    {
        public override string Name
        {
            get { return "Img1"; }
        }

        private const string uploadURL = "http://img1.us/?app";

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();
            string response = UploadData(stream, fileName, uploadURL, "fileup", null);
            ifm.Source = response;

            if (!string.IsNullOrEmpty(response))
            {
                string lastLine = response.Remove(0, response.LastIndexOf('\n') + 1).Trim();
                ifm.Add(lastLine, LinkType.FULLIMAGE);
            }

            return ifm;
        }
    }
}