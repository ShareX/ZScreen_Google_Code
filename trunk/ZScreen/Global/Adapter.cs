using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploadersLib;
using ZSS.TextUploaderLib.URLShorteners;
using ZSS.ImageUploaderLib;

namespace ZSS.Global
{
    /// <summary>
    /// Class for public static methods for use in ZScreen
    /// </summary>
    public static class Adapter
    {
        /// <summary>
        /// Quick Method to shorten a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string TryShortenURL(string url)
        {
            if (Program.conf.LimitLongURL == 0 || Program.conf.LimitLongURL > 0 && url.Length > Program.conf.LimitLongURL)
            {
                TextUploader tu = Program.conf.UrlShortenerActive;
                if (tu != null)
                {
                    string temp = tu.UploadText(url);
                    if (!string.IsNullOrEmpty(temp))
                    {
                        url = temp;
                    }
                }
            }
            return url;
        }

        /// <summary>
        /// Method to update TinyPic Shuk; Run periodically
        /// </summary>
        public static void UpdateTinyPicShuk()
        {
            if (Program.conf.RememberTinyPicUserPass && !string.IsNullOrEmpty(Program.conf.TinyPicUserName) && !string.IsNullOrEmpty(Program.conf.TinyPicPassword))
            {
                TinyPicUploader tpu = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, UploadMode.API);
                string shuk = tpu.UserAuth(Program.conf.TinyPicUserName, Program.conf.TinyPicPassword);
                if (!string.IsNullOrEmpty(shuk))
                {
                    if (Program.conf.TinyPicShuk != shuk)
                    {
                        FileSystem.AppendDebug(string.Format("Updated TinyPic Shuk from {0} to {1}", Program.conf.TinyPicShuk, shuk));
                    }
                    Program.conf.TinyPicShuk = shuk;
                }
            }
        }

        public static bool MakeTinyURL()
        {
            // LimitLongURL = 0 means make tinyURL always
            bool tinyurl = (Program.conf.ClipboardUriMode == ClipboardUriType.FULL || Program.conf.ClipboardUriMode == ClipboardUriType.THUMBNAIL) && Program.conf.MakeTinyURL;
            return tinyurl;
        }

        public static TextUploader FindTextUploader(string name)
        {
            switch (name)
            {
                case PastebinUploader.Hostname:
                    return new PastebinUploader();
                case PastebinCaUploader.Hostname:
                    return new PastebinCaUploader();
                case Paste2Uploader.Hostname:
                    return new Paste2Uploader();
                case SlexyUploader.Hostname:
                    return new SlexyUploader();
                case SniptUploader.Hostname:
                    return new SniptUploader();
                default:
                    if (name == ZSS.TextUploadersLib.FTPUploader.Hostname)
                    {
                        if (Program.conf.FTPAccountList.Count > 0)
                        {
                            FTPAccount acc = new FTPAccount();
                            if (Program.conf.FTPSelected >= 0)
                            {
                                acc = Program.conf.FTPAccountList[Program.conf.FTPSelected];
                            }
                            else
                            {
                                acc = Program.conf.FTPAccountList[0];
                            }
                            return new ZSS.TextUploadersLib.FTPUploader(acc);
                        }
                    }
                    break;
            }
            return null;
        }

        public static TextUploader FindUrlShortener(string name)
        {
            switch (name)
            {
                case BitlyUploader.Hostname:
                    return new BitlyUploader();
                case IsgdUploader.Hostname:
                    return new IsgdUploader();
                case TinyURLUploader.Hostname:
                    return new TinyURLUploader();
                case ThreelyUploader.Hostname:
                    return new ThreelyUploader();
                case KlamUploader.Hostname:
                    return new KlamUploader();
            }
            return null;
        }

        public static bool ImageSoftwareEnabled()
        {
            return Program.DISABLED_IMAGE_EDITOR != Program.conf.ImageEditor.Name;
        }
    }
}
