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

using System.Collections.Generic;
using System.IO;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class ImageShackUploader : ImageUploader
    {
        private string DeveloperKey { get; set; }
        private string RegistrationCode { get; set; }

        /// <summary>
        /// Public/private marker of your video/picture. True means public, false means private.
        /// </summary>
        public bool Public { get; set; }

        public ImageShackUploader(string developerKey, string registrationCode)
        {
            DeveloperKey = developerKey;
            RegistrationCode = registrationCode;
        }

        public override string Name
        {
            get { return "ImageShack"; }
        }

        public override UploadResult UploadImage(Stream stream, string fileName)
        {
            UploadResult ifm = new UploadResult();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("key", DeveloperKey);
            arguments.Add("public", Public ? "yes" : "no");
            if (!string.IsNullOrEmpty(RegistrationCode)) arguments.Add("cookie", RegistrationCode);

            ifm.Source = UploadData(stream, "http://www.imageshack.us/upload_api.php", fileName, "fileupload", arguments);

            if (!string.IsNullOrEmpty(ifm.Source))
            {
                string fullimage = Helpers.GetXMLValue(ifm.Source, "image_link");
                string thumbnail = Helpers.GetXMLValue(ifm.Source, "thumb_link");

                ifm.URL = fullimage;
                ifm.ThumbnailURL = thumbnail;
            }

            return ifm;
        }
    }
}