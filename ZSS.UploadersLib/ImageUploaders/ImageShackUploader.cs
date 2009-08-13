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
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Xml;
using UploadersLib.Helpers;

namespace UploadersLib.ImageUploaders
{
    public sealed class ImageShackUploader : ImageUploader
    {
        private string DeveloperKey { get; set; }
        private string RegistrationCode { get; set; }

        /// <summary>
        /// Toggle where images will be added to the public profile
        /// </summary>
        public bool Public { get; set; }

        public ImageShackUploader(string developerKey, string registrationCode)
        {
            this.DeveloperKey = developerKey;
            this.RegistrationCode = registrationCode;
        }

        public ImageShackUploader(string developerKey, string registrationCode, UploadMode mode)
            : this(developerKey, registrationCode)
        {
            this.UploadMode = mode;
        }

        public override string Name
        {
            get { return "ImageShack"; }
        }

        private string Email { get; set; }

        private const string URLStandard = "http://imageshack.us/index.php";
        private const string URLUnifiedAPI = "http://imageshack.us/upload_api.php";

        /// <summary>
        /// Uploads Image according to Upload Mode
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            switch (this.UploadMode)
            {
                case UploadMode.API:
                    return UploadImageAPI(image, fileName);
                case UploadMode.ANONYMOUS:
                    return UploadImageAnonymous(image, fileName);
            }

            return null;
        }

        /// <summary>
        /// API Method to upload images to ImageShack
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageFileManager UploadImageAPI(Image image, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();
            bool oldValue = ServicePointManager.Expect100Continue;

            try
            {
                ServicePointManager.Expect100Continue = false;
                CookieContainer cookies = new CookieContainer();
                Dictionary<string, string> arguments = new Dictionary<string, string>
                {
                    {"MAX_FILE_SIZE", "13145728"},
                    {"refer", ""},
                    {"brand", ""},
                    {"optimage", "1"},
                    {"rembar", "1"},
                    {"submit", "host it!"},
                    {"optsize", "resample"},
                    {"xml", "yes"},
                    {"public", Public ? "yes" : "no"}
                };

                if (!string.IsNullOrEmpty(Email)) arguments.Add("email", Email);
                if (!string.IsNullOrEmpty(RegistrationCode)) arguments.Add("cookie", RegistrationCode);
                if (!string.IsNullOrEmpty(DeveloperKey)) arguments.Add("key", DeveloperKey);

                ifm.Source = UploadImage(image, fileName, URLUnifiedAPI, "fileupload", arguments);

                string fullimage = GetXMLValue(ifm.Source, "image_link");
                string thumbnail = GetXMLValue(ifm.Source, "thumb_link");

                if (!string.IsNullOrEmpty(fullimage)) ifm.ImageFileList.Add(new ImageFile(fullimage, ImageFile.ImageType.FULLIMAGE));
                if (!string.IsNullOrEmpty(thumbnail)) ifm.ImageFileList.Add(new ImageFile(thumbnail, ImageFile.ImageType.THUMBNAIL));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.Errors.Add(ex.Message);
            }
            finally
            {
                ServicePointManager.Expect100Continue = oldValue;
            }

            return ifm;
        }

        /// <summary>
        /// Anonymous Method to upload images to ImageShack
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageFileManager UploadImageAnonymous(Image image, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();
            bool oldValue = ServicePointManager.Expect100Continue;

            try
            {
                ServicePointManager.Expect100Continue = false;
                Dictionary<string, string> arguments = new Dictionary<string, string>() 
                { 
                    { "MAX_FILE_SIZE", "13145728" },
                    { "refer", "" },
                    { "brand", "" },
                    { "optimage", "1" },
                    { "rembar", "1" },
                    { "submit", "host it!" },
                    { "optsize", "resample" },
                    { "xml", "yes" }
                };

                if (!string.IsNullOrEmpty(Email)) arguments.Add("email", Email);
                if (!Public) arguments.Add("public", "no");

                ifm.Source = UploadImage(image, fileName, URLStandard, "fileupload", arguments);

                string fullimage = GetXMLValue(ifm.Source, "image_link");
                string thumbnail = GetXMLValue(ifm.Source, "thumb_link");

                if (!string.IsNullOrEmpty(fullimage)) ifm.ImageFileList.Add(new ImageFile(fullimage, ImageFile.ImageType.FULLIMAGE));
                if (!string.IsNullOrEmpty(thumbnail)) ifm.ImageFileList.Add(new ImageFile(thumbnail, ImageFile.ImageType.THUMBNAIL));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.Errors.Add(ex.Message);
            }
            finally
            {
                ServicePointManager.Expect100Continue = oldValue;
            }

            return ifm;
        }
    }
}