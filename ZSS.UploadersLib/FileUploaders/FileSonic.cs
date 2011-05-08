using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public class FileSonic : FileUploader
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private const string APIURL = "http://api.filesonic.com/upload";

        public FileSonic(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult ur = new UploadResult();

            string url = GetUploadURL();

            if (!string.IsNullOrEmpty(url))
            {
                ur.Source = UploadData(stream, url, fileName);

                if (!string.IsNullOrEmpty(ur.Source))
                {
                    ur.URL = ur.Source; // TODO: FileSonic response
                }
            }
            else
            {
                Errors.Add("GetUploadURL failed.");
            }

            return ur;
        }

        public string GetUploadURL()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("method", "getUploadUrl");
            args.Add("format", "xml");
            args.Add("u", Username);
            args.Add("p", Password);

            string response = SendGetRequest(APIURL, args);

            XDocument xd = XDocument.Parse(response);
            return xd.GetValue("FSApi_Upload/getUploadUrl/response/url");
        }
    }
}