using System.Drawing;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;
using ZUploader.Properties;

namespace ZUploader
{
    public static class Uploaders
    {
        public static ImageFileManager UploadImage(Image image, string fileName)
        {
            ImageUploader imageUploader = null;

            switch (UploadManager.ImageUploader)
            {
                case ImageDestType2.IMAGESHACK:
                    imageUploader = new ImageShackUploader("78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a", string.Empty, UploadMode.API);
                    ((ImageShackUploader)imageUploader).Public = false;
                    break;
                case ImageDestType2.TINYPIC:
                    imageUploader = new TinyPicUploader("e2aabb8d555322fa", "00a68ed73ddd54da52dc2d5803fa35ee", UploadMode.API);
                    break;
                case ImageDestType2.IMAGEBIN:
                    imageUploader = new ImageBin();
                    break;
                case ImageDestType2.IMG1:
                    imageUploader = new Img1Uploader();
                    break;
                case ImageDestType2.IMGUR:
                    imageUploader = new Imgur();
                    break;
                default:
                    break;
                /*
                case ImageDestType.FLICKR:
                    imageUploader = new FlickrUploader(Engine.conf.FlickrAuthInfo, Engine.conf.FlickrSettings);
                    break;
                case ImageDestType.IMAGEBAM:
                    ImageBamUploaderOptions imageBamOptions = new ImageBamUploaderOptions(Engine.conf.ImageBamApiKey, Engine.conf.ImageBamSecret,
                    Adapter.GetImageBamGalleryActive()) { NSFW = Engine.conf.ImageBamContentNSFW };
                    imageUploader = new ImageBamUploader(imageBamOptions);
                    break;
                case ImageDestType.TWITPIC:
                    TwitPicOptions twitpicOpt = new TwitPicOptions();
                    twitpicOpt.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    twitpicOpt.Password = Adapter.TwitterGetActiveAcct().Password;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.conf.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.conf.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitpicOpt);
                    break;
                case ImageDestType.TWITSNAPS:
                    TwitSnapsOptions twitsnapsOpt = new TwitSnapsOptions();
                    twitsnapsOpt.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    twitsnapsOpt.Password = Adapter.TwitterGetActiveAcct().Password;
                    imageUploader = new TwitSnapsUploader(twitsnapsOpt);
                    break;
                case ImageDestType.YFROG:
                    YfrogOptions yfrogOp = new YfrogOptions(Engine.IMAGESHACK_KEY);
                    yfrogOp.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    yfrogOp.Password = Adapter.TwitterGetActiveAcct().Password;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    imageUploader = new YfrogUploader(yfrogOp);
                    break;
                 */
            }

            if (imageUploader != null)
            {
                return imageUploader.UploadImage(image, fileName);
            }

            return null;
        }

        public static string UploadText(string text)
        {
            TextUploader textUploader = null;

            switch (UploadManager.TextUploader)
            {
                case TextDestType2.PASTE2:
                    textUploader = new Paste2Uploader();
                    break;
                case TextDestType2.PASTEBIN:
                    textUploader = new PastebinUploader();
                    break;
                case TextDestType2.PASTEBIN_CA:
                    textUploader = new PastebinCaUploader();
                    break;
                case TextDestType2.SLEXY:
                    textUploader = new SlexyUploader();
                    break;
                default:
                    break;
            }

            if (textUploader != null)
            {
                return textUploader.UploadText(TextInfo.FromString(text));
            }

            return null;
        }

        public static string UploadFile(byte[] data, string fileName)
        {
            FileUploader fileUploader = null;

            switch (UploadManager.FileUploader)
            {
                case FileUploaderType2.FTP:
                    fileUploader = new FTPUploader(Settings.Default.FTPAccount);
                    break;
                case FileUploaderType2.SendSpace:
                    fileUploader = new SendSpace();
                    SendSpaceManager.PrepareUploadInfo(null, null);
                    break;
                case FileUploaderType2.RapidShare:
                    fileUploader = new RapidShare(new RapidShareOptions()
                    {
                        AccountType = RapidShareAcctType.Free
                    });
                    break;
                case FileUploaderType2.FileBin:
                    fileUploader = new FileBin();
                    break;
            }

            if (fileUploader != null)
            {
                return fileUploader.Upload(data, fileName);
            }

            return null;
        }
    }
}