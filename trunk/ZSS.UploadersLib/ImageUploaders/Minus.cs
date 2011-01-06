using System.IO;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class Minus : ImageUploader
    {
        public override string Name
        {
            get { return "Minus"; }
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();
            ifm.Source = UploadData(stream, "http://min.us/api/UploadItem", fileName);
            // TODO: Not completed
            return ifm;
        }
    }
}