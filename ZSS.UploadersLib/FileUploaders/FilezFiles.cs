using System.Collections.Generic;
using System.IO;
using UploadersLib.HelperClasses;
using System.Web.Script.Serialization;

namespace UploadersLib.FileUploaders
{
    public class FilezOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool HideFile { get; set; }
    }

    public sealed class FilezFiles : FileUploader
    {
        private FilezOptions Options { get; set; }

        public override string Name
        {
            get { return "Filez"; }
        }

        public FilezFiles(){ Options = new FilezOptions(); }
        public FilezFiles(FilezOptions options){ Options = options; }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            if (Options.HideFile == true)
            {
                args.Add("hideFile", "1");
            }
            else
            {
                args.Add("hideFile", "0");
            }
            if (!string.IsNullOrEmpty(Options.Username) && !string.IsNullOrEmpty(Options.Password))
            {
                args.Add("username", Options.Username);
                args.Add("userpass", Options.Password);
            }


            ImageFileManager ifm = new ImageFileManager();
            string response = UploadData(stream, fileName, "http://www.filez.muffinz.eu/api/upload", "file", args);
            ifm.Source = response;
            if (!response.StartsWith("{\"error\""))
            {
                Dictionary<string, Dictionary<string, string>> data = new JavaScriptSerializer().Deserialize<Dictionary<string, Dictionary<string, string>>>(response);
                if (data["info"] != null)
                {
                    return new UploadResult(response, data["info"]["dl"]);
                }
            }

            return null;
        }
    }
}