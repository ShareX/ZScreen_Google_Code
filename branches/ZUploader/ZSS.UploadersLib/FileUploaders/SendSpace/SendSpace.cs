using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace UploadersLib.FileUploaders.SendSpace
{

    public class LoginInfo
    {
        /// <summary>
        /// Session key to be sent with all method calls, user information, including the user account's capabilities
        /// </summary>
        public string SessionKey { get; set; }
        public string Username { get; set; }
        public string EMail { get; set; }
        public string MembershipType { get; set; }
        public string MembershipEnds { get; set; }
        public bool CapableUpload { get; set; }
        public bool CapableDownload { get; set; }
        public bool CapableFolders { get; set; }
        public bool CapableFiles { get; set; }
        public bool CapableHTTPS { get; set; }
        public bool CapableAddressBook { get; set; }
        public string BandwidthLeft { get; set; }
        public string DiskSpaceLeft { get; set; }
        public string DiskSpaceUsed { get; set; }
        public string Points { get; set; }

        public LoginInfo() { }

        public LoginInfo(XElement element)
        {
            SessionKey = element.ElementValue("session_key");
            Username = element.ElementValue("user_name");
            EMail = element.ElementValue("email");
            MembershipType = element.ElementValue("membership_type");
            MembershipEnds = element.ElementValue("membership_ends");
            CapableUpload = element.ElementValue("capable_upload") != "0";
            CapableDownload = element.ElementValue("capable_download") != "0";
            CapableFolders = element.ElementValue("capable_folders") != "0";
            CapableFiles = element.ElementValue("capable_files") != "0";
            CapableHTTPS = element.ElementValue("capable_https") != "0";
            CapableAddressBook = element.ElementValue("capable_addressbook") != "0";
            BandwidthLeft = element.ElementValue("bandwidth_left");
            DiskSpaceLeft = element.ElementValue("diskspace_left");
            DiskSpaceUsed = element.ElementValue("diskspace_used");
            Points = element.ElementValue("points");
        }
    }

    public class UploadInfo
    {
        public string URL { get; set; }
        public string ProgressURL { get; set; }
        public string MaxFileSize { get; set; }
        public string UploadIdentifier { get; set; }
        public string ExtraInfo { get; set; }

        public UploadInfo() { }

        public UploadInfo(XElement element)
        {
            XElement upload = element.Element("upload");
            URL = upload.AttributeValue("url");
            ProgressURL = upload.AttributeValue("progress_url");
            MaxFileSize = upload.AttributeValue("max_file_size");
            UploadIdentifier = upload.AttributeValue("upload_identifier");
            ExtraInfo = upload.AttributeValue("extra_info");
        }
    }

    public class ResponsePacket
    {
        public string Method { get; set; }
        public string Status { get; set; }
        public bool Error { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public XElement Result { get; set; }
    }
}
