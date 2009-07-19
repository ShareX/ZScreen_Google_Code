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
#region Shared Source Notification
/* 
 * Portions of this code is thanks to Flaming Idiots
 * http://www.sythe.org/showthread.php?t=509358  
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Xml;
using ZSS.ImageUploadersLib.Helpers;

namespace ZSS.ImageUploadersLib
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
        public override ImageFileManager UploadImage(Image image)
        {
            switch (this.UploadMode)
            {
                case UploadMode.API:
                    return UploadImageAPI(image);
                case UploadMode.ANONYMOUS:
                    return UploadImageAnonymous(image);
            }

            return null;
        }

        /// <summary>
        /// API Method to upload images to ImageShack
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageFileManager UploadImageAPI(Image image)
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

                ifm.Source = new TCPClient(this).UploadImage(image, URLUnifiedAPI, "fileupload", mFileName, arguments);
                //ifm.Source = PostImage(image, URLUnifiedAPI, "fileupload", arguments);

                string fullimage = GetXMLVal(ifm.Source, "image_link");
                string thumbnail = GetXMLVal(ifm.Source, "thumb_link");

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
        private ImageFileManager UploadImageAnonymous(Image image)
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

                ifm.Source = new TCPClient(this).UploadImage(image, URLStandard, "fileupload", mFileName, arguments);
                //ifm.Source = PostImage(image, URLStandard, "fileupload", arguments);

                string fullimage = GetXMLVal(ifm.Source, "image_link");
                string thumbnail = GetXMLVal(ifm.Source, "thumb_link");

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

        #region Old methods

        /// <summary>
        /// Old method of uploading to ImageShack
        /// </summary>
        /// <returns></returns>
        public ImageFileManager UploadImageLegacy(string filePath)
        {
            //use a new guid as the boundary
            string boundary = Guid.NewGuid().ToString().Replace("-", "");

            byte[] fileBytes;

            ImageFileManager ifm = null;
            List<ImageFile> imageFiles = new List<ImageFile>();

            try
            {
                //read the image into a stream
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] tmp = new byte[fileStream.Length];
                    fileStream.Read(tmp, 0, tmp.Length);
                    fileBytes = tmp;
                }
            }
            catch
            {
                return ifm; // empty
            }

            /*catch (FileNotFoundException e)
            {
                //MessageBox.Show(e.Message);
                return imageFiles; //empty
            }
            catch (IOException e)
            {
                //MessageBox.Show(e.Message);
                return imageFiles; //empty
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return imageFiles; //empty
            }*/

            try
            {
                string URL = URLStandard;
                //if (DeveloperKey != null)
                //    URL = URLUnifiedAPI;
                //else
                //{
                //    URL = URLStandard;
                //}
                //dumb fix... required so it doesn't give the expect 100 error
                ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(URL);
                httpRequest.Proxy = this.ProxySettings;
                MemoryStream memoryStream = new MemoryStream();
                StreamWriter streamWriter = new StreamWriter(memoryStream);

                httpRequest.Method = "POST";
                httpRequest.ContentType = String.Format("multipart/form-data; boundary={0}", boundary);
                httpRequest.UserAgent = "ZScreen [http://brandonz.net/projects/zscreen]";

                //write the file data
                WriteFile(streamWriter, memoryStream, boundary, "fileupload", filePath, fileBytes);

                //write the post data fields

                if (URL == URLStandard)
                    WritePost(streamWriter, boundary, "xml", "yes");

                if (!Public)
                    WritePost(streamWriter, boundary, "public", "no");

                if (RegistrationCode != null)
                    WritePost(streamWriter, boundary, "cookie", RegistrationCode);

                if (DeveloperKey != null)
                    WritePost(streamWriter, boundary, "key", DeveloperKey);

                //base.WritePost(streamWriter, boundary, "MAX_FILE_SIZE", (1024 * 1024).ToString()); //1MB file limit

                //optimage, optsize may be used in the future for resizing

                httpRequest.ContentLength = memoryStream.Length;

                Stream requestStream = httpRequest.GetRequestStream();

                if (!Upload(memoryStream, requestStream))
                    return null;

                memoryStream.Close();
                requestStream.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());

                //write xml data to a string
                string xmlData = streamReader.ReadToEnd();
                streamReader.Close();

                //create an xml document from the string
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlData);

                //find matching elements and create new ImageFile of type FULLIMAGE/THUMBNAIL
                foreach (XmlElement xmlElement in xmlDocument.SelectNodes("/links/*"))
                {
                    string tmpElement = xmlElement.Name;

                    switch (tmpElement)
                    {
                        case "image_link":
                            imageFiles.Add(new ImageFile(xmlElement.InnerText, ImageFile.ImageType.FULLIMAGE));
                            break;
                        case "thumb_link":
                            imageFiles.Add(new ImageFile(xmlElement.InnerText, ImageFile.ImageType.THUMBNAIL));
                            break;
                    }
                }
                //if (imageFiles.Count == 2)
                //{
                //    imageFiles.Add(ImageFile.getThumbnailForum1ImageFile(imageFiles[0].URI, imageFiles[1].URI));
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //will return an empty list
            }

            ifm = new ImageFileManager(imageFiles);
            return ifm;
        }

        #endregion
    }
}