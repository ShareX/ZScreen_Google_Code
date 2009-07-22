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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using ZSS.ImageUploadersLib.Helpers;
using System.Collections;

namespace ZSS.ImageUploadersLib
{
    public sealed class TinyPicUploader : ImageUploader
    {
        public string TinyPicID { get; set; }
        public string TinyPicKey { get; set; }
        public string Shuk { get; set; }

        private const string URLAPI = "http://api.tinypic.com/api.php";

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
            bool oldValue = ServicePointManager.Expect100Continue;

            try
            {
                string action = "getuploadkey", tpid = TinyPicID, tpk = TinyPicKey;

                Dictionary<string, string> args = new Dictionary<string, string>()
                {
                    { "action", action },
                    { "tpid", tpid },
                    { "tpk", tpk },
                    { "sig", GetMD5(action + tpid + tpk) }
                };

                string response = GetResponse(URLAPI, args);


                if (string.IsNullOrEmpty(response)) throw new Exception("Unable to get upload key.");

                if (GetXMLValue(response, "status") == "OK")
                {

                    string upk = GetXMLValue(response, "uploadkey");

                    if (string.IsNullOrEmpty(upk))
                    {
                        throw new Exception("Upload Key is Empty.");
                    }

                    if (string.IsNullOrEmpty(Shuk))
                    {
                        action = "upload"; //anonymous upload
                    }
                    else
                    {
                        action = "userupload"; //user upload
                    }

                    ServicePointManager.Expect100Continue = false;

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
                    {
                        arguments.Add("shuk", Shuk);
                    }

                    ifm.Source = PostImage(image, URLAPI, "uploadfile", arguments);
                    //ifm.Source = new TCPClient(this).UploadImage(image, URLAPI, "uploadfile", mFileName, arguments);

                    string fullimage = GetXMLValue(ifm.Source, "fullsize");
                    string thumbnail = GetXMLValue(ifm.Source, "thumbnail");

                    if (!string.IsNullOrEmpty(fullimage)) ifm.ImageFileList.Add(new ImageFile(fullimage, ImageFile.ImageType.FULLIMAGE));
                    if (!string.IsNullOrEmpty(thumbnail)) ifm.ImageFileList.Add(new ImageFile(thumbnail, ImageFile.ImageType.THUMBNAIL));
                }
                else if(GetXMLValue(response, "status") == "FAIL")
                {
                    int errorCode = -1;
                    Int32.TryParse(GetXMLValue(response, "errorcode"), out errorCode);

                    this.Errors.Add(GetErrorMessage(errorCode));
                }
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
        /// Anonymous Method to upload images to TinyPic
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
                    { "domain_lang", "en" },
                    { "action", "upload" },
                    { "MAX_FILE_SIZE", "200000000" },
                    { "file_type", "image" },
                    { "dimension", "1600" }
                };

                ifm.Source = PostImage(image, "http://s5.tinypic.com/plugin/upload.php", "the_file", arguments);
                //ifm.Source = new TCPClient(this).UploadImage(image, "http://s5.tinypic.com/plugin/upload.php", "the_file", mFileName, arguments);

                string imgIval = Regex.Match(ifm.Source, "(?<=ival\" value=\").+(?=\" />)").Value;
                string imgPic = Regex.Match(ifm.Source, "(?<=pic\" value=\").+(?=\" />)").Value;
                string imgType = Regex.Match(ifm.Source, "(?<=ext\" value=\").*(?=\" />)").Value;
                if ((imgIval != "") && (imgPic != ""))
                {
                    if (imgType == "") imgType = ".jpg";
                    string fullimage = "http://i" + imgIval + ".tinypic.com/" + imgPic + imgType;
                    string thumbnail = "http://i" + imgIval + ".tinypic.com/" + imgPic + "_th" + imgType;
                    ifm.ImageFileList.Add(new ImageFile(fullimage, ImageFile.ImageType.FULLIMAGE));
                    ifm.ImageFileList.Add(new ImageFile(thumbnail, ImageFile.ImageType.THUMBNAIL));
                }
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

            if (!string.IsNullOrEmpty(response))
            {
                string result = GetXMLValue(response, "shuk");

                return HttpUtility.UrlEncode(result);
            }

            return "";
        }

        public string GetErrorMessage(int errorCode)
        {
            switch (errorCode)
            {
                case 1: 
                    return "Bad Authorization: Missing or invalid authentication parameter (tpid, tpk, sig, upk).";
                case 2:
                    return "Bad Action: Invalid or no action passed.";
                case 3:
                    return "Bad Type: Invalid or no type passed.";
                case 4:
                    return "Bad File: No upload file included in POST when required.";
                case 5: 
                    return "Bad Term: No search term included with search request.";
                case 6: 
                    return "Bad Media Key: Media key not included in a request that required a media key.";
                case 7:
                    return "Bad Upload Key: Invalid or no upload key in a request that required an upload key.";
                case 8: 
                    return "Bad User Credentials: Invalid or no user credentials (username or pass) in a request that required username/pass to be valid.";
                case 9:
                    return "Insufficient Permission: Action called that the specific tpid does not have permission to perform.";
                case 10:
                    return "Image Processing Error: Uploaded image fails to process correctly.";
                case 11: 
                    return "Video Encoding Error: Unable to encode video.";
                case 12: 
                    return "Bad Album: Invalid or no albumid is passed when required.";
                case 20: 
                    return "Bad Email: Invalid or no email passed in register action.";
                case 21: 
                    return "Duplicate Email: Email passed in register action is already in use by another registered user.";
                case 22:
                    return "Bad Password: Password passed in register action is less than six characters.";
                case 23:
                    return "Bad Birth Date: Birth date fields passed in register action combine to make an invalid date (e.g., February 30, 1990).";
                case 24:
                    return "User too Young: Birth date fields passed in register action place a user below the minimum age of 14 years.";
                case 25:
                    return "Terms Agreement: Terms parameter for register action is either blank, non-existent, or does not say \"yes\"";
                case 26: 
                    return "Terms of Service Violation: originatingip parameter contains an IP address on the TinyPic banned list, or is in a nation that is banned from registering.";
                case 27:
                    return "Bad IP: originationip parameter contains an invalid IP address.";
                case 28:
                    return "Bad Album Name: Album name that is passed in a user album action is too short (less than two characters).";
                case 99:
                    return "Internal Error: An internal error occurred in processing request.";
                case 100:
                    return "No File: mediakey is passed in an action, but the media is not found/no longer available.";
                default:
                    return "No error message was provided.";
            }
        }
    }
}