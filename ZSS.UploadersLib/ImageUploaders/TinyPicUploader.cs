﻿#region License Information (GPL v2)

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

#endregion License Information (GPL v2)

using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml.Linq;
using UploadersLib.Helpers;

namespace UploadersLib.ImageUploaders
{
    public sealed class TinyPicUploader : ImageUploader
    {
        public string TinyPicID { get; set; }
        public string TinyPicKey { get; set; }
        public string Shuk { get; set; }

        private const string URLAPI = "http://api.tinypic.com/api.php";

        public TinyPicUploader(string id, string key, string shuk)
        {
            TinyPicID = id;
            TinyPicKey = key;
            Shuk = shuk;
        }

        public TinyPicUploader(string id, string key) : this(id, key, string.Empty) { }

        public override string Name
        {
            get { return "TinyPic"; }
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();

            string action = "getuploadkey", tpid = TinyPicID, tpk = TinyPicKey;
            string upk = GetUploadKey(action, tpid, tpk);

            if (!string.IsNullOrEmpty(upk))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();

                if (string.IsNullOrEmpty(Shuk))
                {
                    action = "upload";
                }
                else
                {
                    action = "userupload";
                    arguments.Add("shuk", Shuk);
                }

                arguments.Add("action", action);
                arguments.Add("tpid", tpid);
                arguments.Add("sig", UploadHelpers.GetMD5(action + tpid + tpk));
                arguments.Add("responsetype", "XML");
                arguments.Add("upk", upk);
                arguments.Add("type", "image");
                arguments.Add("tags", string.Empty);

                ifm.Source = UploadData(stream, fileName, URLAPI, "uploadfile", arguments);

                if (!string.IsNullOrEmpty(ifm.Source) && CheckResponse(ifm.Source))
                {
                    string fullimage = UploadHelpers.GetXMLValue(ifm.Source, "fullsize");
                    string thumbnail = UploadHelpers.GetXMLValue(ifm.Source, "thumbnail");

                    ifm.Add(fullimage, LinkType.FULLIMAGE);
                    ifm.Add(thumbnail, LinkType.THUMBNAIL);
                }
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
                { "sig", UploadHelpers.GetMD5(action + tpid + tpk) },
                { "email", email },
                { "pass", password }
            };

            string response = GetResponse(URLAPI, args);

            if (!string.IsNullOrEmpty(response))
            {
                string result = UploadHelpers.GetXMLValue(response, "shuk");

                return HttpUtility.HtmlEncode(result);
            }

            return string.Empty;
        }

        private string GetUploadKey(string action, string tpid, string tpk)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("action", action);
            args.Add("tpid", tpid);
            args.Add("sig", UploadHelpers.GetMD5(action + tpid + tpk));
            args.Add("responsetype", "XML");

            string response = GetResponseString(URLAPI, args);

            if (!string.IsNullOrEmpty(response) && CheckResponse(response))
            {
                string upk = UploadHelpers.GetXMLValue(response, "uploadkey");

                if (string.IsNullOrEmpty(upk))
                {
                    Errors.Add("Upload key is empty.");
                }

                return upk;
            }
            else
            {
                Errors.Add("Unable to get upload key.");
            }

            return null;
        }

        private bool CheckResponse(string response)
        {
            bool result = false;

            XDocument xDoc = XDocument.Parse(response);
            XElement xEle = xDoc.Element("response");
            string status = xEle.ElementValue("status");

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "OK")
                {
                    result = true;
                }
                else if (status == "FAIL")
                {
                    string errorcode = xEle.ElementValue("errorcode");
                    if (!string.IsNullOrEmpty(errorcode))
                    {
                        int code;
                        if (int.TryParse(errorcode, out code))
                        {
                            this.Errors.Add(GetErrorMessage(code));
                        }
                    }
                    result = false;
                }
            }

            return result;
        }

        private string GetErrorMessage(int errorCode)
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