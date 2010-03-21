using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using System.Drawing;

namespace ZUploader
{
    public static class Uploaders
    {
        public static ImageFileManager UploadImage(Image image, string fileName)
        {
            ImageUploader imageUploader = null;

            switch (UploadManager.ImageUploader)
            {
                /*
            case ImageDestType.FLICKR:
                imageUploader = new FlickrUploader(Engine.conf.FlickrAuthInfo, Engine.conf.FlickrSettings);
                break;
            case ImageDestType.IMAGEBAM:
                ImageBamUploaderOptions imageBamOptions = new ImageBamUploaderOptions(Engine.conf.ImageBamApiKey, Engine.conf.ImageBamSecret,
                    Adapter.GetImageBamGalleryActive()) { NSFW = Engine.conf.ImageBamContentNSFW };
                imageUploader = new ImageBamUploader(imageBamOptions);
                break;
                 */
                case ImageDestType.IMAGEBIN:
                    imageUploader = new ImageBin();
                    break;
                /*
            case ImageDestType.IMAGESHACK:
                imageUploader = new ImageShackUploader(Engine.IMAGESHACK_KEY, Engine.conf.ImageShackRegistrationCode, Engine.conf.UploadMode);
                ((ImageShackUploader)imageUploader).Public = Engine.conf.ImageShackShowImagesInPublic;
                break;
                 */
                case ImageDestType.IMG1:
                    imageUploader = new Img1Uploader();
                    break;
                case ImageDestType.IMGUR:
                    imageUploader = new Imgur();
                    break;
                /*
            case ImageDestType.TINYPIC:
                imageUploader = new TinyPicUploader(Engine.TINYPIC_ID, Engine.TINYPIC_KEY, Engine.conf.UploadMode);
                ((TinyPicUploader)imageUploader).Shuk = Engine.conf.TinyPicShuk;
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
                 * */
                default:
                    break;
            }

            if (imageUploader != null)
            {
                return imageUploader.UploadImage(image, fileName);
            }

            return null;
        }
    }
}
