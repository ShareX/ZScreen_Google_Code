using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UploadersLib.FileUploaders
{
    public class SendSpaceUploader
    {
        /// <summary>
        /// Obtains the information needed to perform an upload.
        /// http://www.sendspace.com/dev_method.html?method=upload.getInfo
        /// </summary>
        /// <param name="sessionKey">Received from auth.login</param>
        /// <returns>URL to upload the file to, progress_url for real-time progress information, max_file_size for max size current user can upload, upload_identifier & extra_info to be passed with the upload form</returns>
        public UploadInfo UploadGetInfo(string sessionKey)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "upload.getInfo");
            args.Add("session_key", sessionKey);
            args.Add("speed_limit", SpeedLimit.ToString());

            string response = GetResponse(SENDSPACE_API_URL, args);

            ResponsePacket packet = ParseResponse(response);

            if (!packet.Error)
            {
                UploadInfo uploadInfo = new UploadInfo(packet.Result);
                return uploadInfo;
            }

            return null;
        }

        /// <summary>
        /// Obtains the basic information needed to make an anonymous upload. This method does not require authentication or login.
        /// </summary>
        /// <returns>URL to upload the file to, progress_url for real-time progress information, max_file_size for max size current user can upload, upload_identifier & extra_info to be passed in the upload form</returns>
        public UploadInfo AnonymousUploadGetInfo()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "anonymous.uploadGetInfo");
            args.Add("speed_limit", SpeedLimit.ToString());
            args.Add("api_key", SENDSPACE_API_KEY);
            args.Add("api_version", SENDSPACE_API_VERSION);
            args.Add("app_version", AppVersion);

            string response = GetResponse(SENDSPACE_API_URL, args);

            ResponsePacket packet = ParseResponse(response);

            if (!packet.Error)
            {
                UploadInfo uploadInfo = new UploadInfo(packet.Result);
                return uploadInfo;
            }

            return null;
        }

        /// <summary>
        /// http://www.sendspace.com/dev_method.html?method=upload.getInfo
        /// </summary>
        /// <param name="max_file_size">max_file_size value received in UploadGetInfo response</param>
        /// <param name="upload_identifier">upload_identifier value received in UploadGetInfo response</param>
        /// <param name="extra_info">extra_info value received in UploadGetInfo response</param>
        /// <param name="description"></param>
        /// <param name="password"></param>
        /// <param name="folder_id"></param>
        /// <param name="recipient_email">an email (or emails separated with ,) of recipient/s to receive information about the upload</param>
        /// <param name="notify_uploader">0/1 - should the uploader be notified?</param>
        /// <param name="redirect_url">page to redirect after upload will be attached upload_status=ok/fail&file_id=XXXX</param>
        /// <returns></returns>
        public Dictionary<string, string> PrepareArguments(string max_file_size, string upload_identifier, string extra_info,
            string description, string password, string folder_id, string recipient_email, string notify_uploader, string redirect_url)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("MAX_FILE_SIZE", max_file_size);
            args.Add("UPLOAD_IDENTIFIER", upload_identifier);
            args.Add("extra_info", extra_info);

            // Optional fields

            if (!string.IsNullOrEmpty(description)) args.Add("description", description);
            if (!string.IsNullOrEmpty(password)) args.Add("password", password);
            if (!string.IsNullOrEmpty(folder_id)) args.Add("folder_id", folder_id);
            if (!string.IsNullOrEmpty(recipient_email)) args.Add("recipient_email", recipient_email);
            if (!string.IsNullOrEmpty(notify_uploader)) args.Add("notify_uploader", notify_uploader);
            if (!string.IsNullOrEmpty(redirect_url)) args.Add("redirect_url", redirect_url);

            return args;
        }

        public Dictionary<string, string> PrepareArguments(string max_file_size, string upload_identifier, string extra_info)
        {
            return PrepareArguments(max_file_size, upload_identifier, extra_info, null, null, null, null, null, null);
        }

        public string Upload(byte[] data, string fileName, UploadInfo uploadInfo)
        {
            if (uploadInfo != null)
            {
                Dictionary<string, string> args = PrepareArguments(uploadInfo.MaxFileSize, uploadInfo.UploadIdentifier, uploadInfo.ExtraInfo);

                string response = UploadData(data, fileName, uploadInfo.URL, "userfile", args);

                if (!string.IsNullOrEmpty(response))
                {
                    return "http://www.sendspace.com/file/" + Regex.Match(response, @"file_id=(\w+)").Groups[1].Value;
                }
            }

            return "";
        }

        public override string Upload(byte[] data, string fileName)
        {
            return Upload(data, fileName, SendSpaceManager.UploadInfo);
        }
    }
}
