using System.Collections.Generic;
using System.IO;
using UploadersLib.Helpers;

namespace UploadersLib.FileUploaders
{
    public sealed class FilezFiles : FileUploader
    {
        public override string Name
        {
            get { return "Filez"; }
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("api", "ZUploader"); //does not work for some reason...

            string response = UploadData(stream, fileName, "http://www.filez.muffinz.eu/api/upload/linkOnly", "file", args);

            if (!string.IsNullOrEmpty(response))
            {
                return new UploadResult(response, response);
            }

            return null;
        }
    }
}