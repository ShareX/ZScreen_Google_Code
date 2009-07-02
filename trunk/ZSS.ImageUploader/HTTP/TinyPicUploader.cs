#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2009  McoreD, Brandon Zimmerman

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
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using ZSS.ImageUploaders.Helpers;

namespace ZSS.ImageUploaders
{
    public sealed class TinyPicUploader : HTTPUploader
    {
        public string TinyPicID { get; set; }
        public string TinyPicKey { get; set; }
        public string Shuk { get; set; }

        private string URLAPI = "http://api.tinypic.com/api.php";

        // public TinyPicUploader() { }

        public TinyPicUploader(string id, string key)
        {
            this.TinyPicID = id;
            this.TinyPicKey = key;
        }

        public TinyPicUploader(string id, string key, UploadMode mode)
            : this(id, key)
        {
            this.UploadMode = mode;
        }

        public override string Name
        {
            get { return "TinyPic"; }
        }

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
        /// API Method to upload images to TinyPic
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageFileManager UploadImageAPI(Image image)
        {
            ImageFileManager ifm = new ImageFileManager();

            MemoryStream imgStream = new MemoryStream();
            bool oldValue = ServicePointManager.Expect100Continue;
            List<ImageFile> imageFiles = new List<ImageFile>();

            try
            {
                ImageFormat imageFormat = image.RawFormat;
                image.Save(imgStream, imageFormat);
                image.Dispose();
                imgStream.Position = 0;

                string action = "getuploadkey", tpid = TinyPicID, tpk = TinyPicKey;

                Dictionary<string, string> args = new Dictionary<string, string>()
                {                 
                    { "action", action },
                    { "tpid", tpid },
                    { "tpk", tpk },
                    { "sig", GetMD5(action + tpid + tpk) }
                };

                string response = GetResponse(URLAPI, args);
                string upk = GetXMLVal(response, "uploadkey");
                if (string.IsNullOrEmpty(upk))
                {
                    throw new Exception("Upload Key is Empty.");
                }

                if (String.IsNullOrEmpty(Shuk))
                    action = "upload"; //anonymous upload
                else
                    action = "userupload"; //user upload

                ServicePointManager.Expect100Continue = false;
                CookieContainer cookies = new CookieContainer();
                Dictionary<string, string> arguments = new Dictionary<string, string>() 
                {                 
                    { "action", action },
                    { "tpid", tpid },
                    //{ "tpk", tpk },
                    { "sig", GetMD5(action + tpid + tpk) },
                    { "upk", upk },
                    { "tags", "" },
                    { "type", "image" }
                };

                if (!string.IsNullOrEmpty(Shuk))
                    arguments.Add("shuk", Shuk);

                ifm.Source = PostImage(imgStream, URLAPI, "uploadfile", GetMimeType(imageFormat), arguments, cookies, "http://tinypic.com/");
                string fullSize = GetXMLVal(ifm.Source, "fullsize"); // Regex.Match(imgSource, "(?<=fullsize>).+(?=</fullsize)").Value;
                string thumbNail = GetXMLVal(ifm.Source, "thumbnail"); // Regex.Match(imgSource, "(?<=thumbnail>).+(?=</thumbnail)").Value;

                if (!string.IsNullOrEmpty(fullSize))
                    ifm.ImageFileList.Add(new ImageFile(fullSize, ImageFile.ImageType.FULLIMAGE));
                if (!string.IsNullOrEmpty(thumbNail))
                    ifm.ImageFileList.Add(new ImageFile(thumbNail, ImageFile.ImageType.THUMBNAIL));
            }
            catch (Exception ex)
            {
                this.Errors.Add(ex.Message);
            }
            finally
            {
                ServicePointManager.Expect100Continue = oldValue;
                imgStream.Dispose();
            }

            return ifm;
        }

        /// <summary>
        /// Anonymous Method to upload images to TinyPic
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageFileManager UploadImageAnonymous(Image image)
        {
            ImageFormat imageFormat = image.RawFormat;
            MemoryStream imgStream = new MemoryStream();
            image.Save(imgStream, imageFormat);
            image.Dispose();
            imgStream.Position = 0;
            bool oldValue = ServicePointManager.Expect100Continue;
            List<ImageFile> imageFiles = new List<ImageFile>();
            string imgSource = "";

            try
            {
                ServicePointManager.Expect100Continue = false;
                CookieContainer cookies = new CookieContainer();
                Dictionary<string, string> arguments = new Dictionary<string, string>()
                {
                    { "domain_lang", "en" },
                    { "action", "upload" },
                    { "MAX_FILE_SIZE", "200000000" },
                    { "file_type", "image" },
                    { "dimension", "1600" }
                };
                imgSource = PostImage(imgStream, "http://s5.tinypic.com/plugin/upload.php", "the_file", GetMimeType(imageFormat), arguments, cookies, "");
                string imgIval = Regex.Match(imgSource, "(?<=ival\" value=\").+(?=\" />)").Value;
                string imgPic = Regex.Match(imgSource, "(?<=pic\" value=\").+(?=\" />)").Value;
                string imgType = Regex.Match(imgSource, "(?<=ext\" value=\").*(?=\" />)").Value;
                if ((imgIval != "") && (imgPic != ""))
                {
                    if (imgType == "") imgType = ".jpg";
                    string fullimage = "http://i" + imgIval + ".tinypic.com/" + imgPic + imgType;
                    string thumbnail = "http://i" + imgIval + ".tinypic.com/" + imgPic + "_th" + imgType;
                    imageFiles.Add(new ImageFile(fullimage, ImageFile.ImageType.FULLIMAGE));
                    imageFiles.Add(new ImageFile(thumbnail, ImageFile.ImageType.THUMBNAIL));
                }
            }
            catch (Exception ex)
            {
                this.Errors.Add(ex.Message);
            }
            finally { ServicePointManager.Expect100Continue = oldValue; }
            imgStream.Dispose();

            ImageFileManager ifm = new ImageFileManager(imageFiles) { Source = imgSource };
            return ifm;
        }

        public string UserAuth(string email, string password)
        {
            string action = "userauth", tpid = TinyPicID, tpk = TinyPicKey;

            Dictionary<string, string> args = new Dictionary<string, string>()
            {                 
                { "action", action },
                { "tpid", tpid },
                { "sig", GetMD5(action + tpid + tpk) },
                { "email", email },
                { "pass", password }
            };

            string response = GetResponse(URLAPI, args);
            string result = GetXMLVal(response, "shuk");
            if (string.IsNullOrEmpty(result))
            {
                //throw new Exception("Userauth key is Empty.");
            }

            return HttpUtility.UrlEncode(result);
        }
    }
}