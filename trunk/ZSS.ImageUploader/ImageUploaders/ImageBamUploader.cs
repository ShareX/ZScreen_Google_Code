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
using System.Drawing;
using System.Linq;
using System.Text;
using ZSS.ImageUploadersLib.Helpers;
using System.Xml.Linq;

namespace ZSS.ImageUploadersLib
{
    public sealed class ImageBamUploaderOptions
    {
        public string UserKey, UserSecret, GalleryID;
        public bool NSFW { get; set; }

        public ImageBamUploaderOptions(string userKey, string userSecret)
        {
            UserKey = userKey;
            UserSecret = userSecret;
        }

        public ImageBamUploaderOptions(string userKey, string userSecret, string galleryID)
            : this(userKey, userSecret)
        {
            GalleryID = galleryID;
        }

        public string GetContentType()
        {
            return NSFW == true ? "1" : "0";
        }
    }

    public sealed class ImageBamUploader : ImageUploader
    {
        private const string Key = "3702805a5d94b0161052e7aa4c69f046";
        private const string Secret = "9b7ae269fee7f2aa5253a4d4b7608b9c";

        private const string upload = "http://www.imagebam.com/services/upload/";
        private const string generate_GID = "http://www.imagebam.com/services/generate_GID/";

        private ImageBamUploaderOptions Options { get; set; }

        public ImageBamUploader(ImageBamUploaderOptions options)
        {
            this.Options = options;
            if (string.IsNullOrEmpty(options.UserKey) || string.IsNullOrEmpty(options.UserSecret))
            {
                this.Errors.Add("In order to upload images to ImageBam, you need to register first.");
            }
        }

        public override string Name
        {
            get { return "ImageBam"; }
        }

        // http://www.imagebam.com/nav/API_uploading_photos

        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            arguments.Add("gallery", Options.GalleryID); // The gallery ID (GID) to be used. The GID must be generated before uploading a photo.
            arguments.Add("content_type", Options.GetContentType()); // Content type: SFW (0), NSFW(1)
            arguments.Add("API_key_dev", Key); // Your API-Key.
            arguments.Add("API_key_user", Options.UserKey); // The user's API key.

            string salt = GetRandomAlphanumeric(32);

            // Random string of 32 characters (a-zA-Z0-9) for higher security.
            arguments.Add("salt", salt);

            // 32 character secret by building the md5-checksum of the string consisting of your API-Secret, the user's API-Secret and the 32-character salt.
            arguments.Add("secret", GetMD5(Secret + Options.UserSecret + salt));

            string source = UploadImage(image, fileName, upload, "photo", arguments);

            return ParseResult(source);
        }

        public string CreateGalleryID()
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            arguments.Add("API_key_dev", Key); // Your API-Key.
            arguments.Add("API_key_user", Options.UserKey); // The user's API key.

            string salt = GetRandomAlphanumeric(32);

            // Random string of 32 characters (a-zA-Z0-9) for higher security.
            arguments.Add("salt", salt);

            // 32 character secret by building the md5-checksum of the string consisting of your API-Secret, the user's API-Secret and the 32-character salt.
            arguments.Add("secret", GetMD5(Secret + Options.UserSecret + salt));

            string source = GetResponse(generate_GID, arguments);

            return GetXMLValue(source, "GID");
        }

        private ImageFileManager ParseResult(string source)
        {
            ImageFileManager ifm = new ImageFileManager { Source = source };

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xdoc = XDocument.Parse(source);
                XElement xele = xdoc.Element("rsp");

                if (xele != null)
                {
                    switch (xele.AttributeFirstValue("status", "stat"))
                    {
                        case "ok":
                            string ID, URL, thumbnail, delcode;
                            ID = xele.ElementValue("ID");
                            URL = xele.ElementValue("URL");
                            thumbnail = xele.ElementValue("thumbnail");
                            delcode = xele.ElementValue("delcode");

                            ifm.ImageFileList.Add(new ImageFile(URL, ImageFile.ImageType.FULLIMAGE));
                            ifm.ImageFileList.Add(new ImageFile(thumbnail, ImageFile.ImageType.THUMBNAIL));

                            break;
                        case "fail":
                            string code, msg;
                            code = xele.Element("err").Attribute("code").Value;
                            msg = xele.Element("err").Attribute("msg").Value;

                            base.Errors.Add(msg);

                            break;
                    }
                }
            }

            return ifm;
        }
    }
}