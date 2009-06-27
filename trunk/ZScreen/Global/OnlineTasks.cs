using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.Helpers;

namespace ZSS.Global
{
    public static class OnlineTasks
    {
        /// <summary>
        /// Quick Method to shorten a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ShortenURL(string url)
        {
            TextUploader tu = Program.conf.UrlShortenerActive;
            if (tu != null)
            {
                string temp = tu.UploadText(new TextFile(url));
                if (!string.IsNullOrEmpty(temp))
                {
                    url = temp;
                }
            }
            return url;
        }

        /// <summary>
        /// Method to shorten a URL according to Options
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string TryShortenURL(string url)
        {
            if (Program.conf.ClipboardUriMode == ClipboardUriType.FULL && Program.conf.MakeTinyURL)
            {
                url = ShortenURL(url);
            }
            return url;
        }
    }
}
