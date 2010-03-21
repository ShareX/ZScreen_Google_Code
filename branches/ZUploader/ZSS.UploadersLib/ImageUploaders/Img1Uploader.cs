using System.Drawing;
using UploadersLib.Helpers;

namespace UploadersLib.ImageUploaders
{
    public sealed class Img1Uploader : ImageUploader
    {
        public override string Name
        {
            get { return "Img1"; }
        }

        private string uploadURL = "http://img1.us/?app";

        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();
            string response = UploadImage(image, fileName, uploadURL, "fileup", null);
            ifm.Source = response;

            if (!string.IsNullOrEmpty(response))
            {
                string lastLine = response.Remove(0, response.LastIndexOf('\n') + 1);
                ifm.ImageFileList.Add(new ImageFile(lastLine, LinkType.FULLIMAGE));
                ifm.ImageFileList.Add(new ImageFile(lastLine.Replace("/i/", "/thumbs/"), LinkType.THUMBNAIL));
            }

            return ifm;
        }
    }
}