using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ZScreenLib.Helpers;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.Helpers;
using ZSS;

namespace ZScreenLib
{
   public static class Adapter
    {
       public static bool CheckFTPAccounts()
       {
           return Program.conf.FTPAccountList.Count > 0 && Program.conf.FTPSelected >= 0 && Program.conf.FTPAccountList.Count > Program.conf.FTPSelected;
       }

       public static bool CheckFTPAccounts(ref MainAppTask task)
       {
           bool result = CheckFTPAccounts();
           if (!result) task.Errors.Add("An FTP account does not exist or not selected properly.");
           return result;
       }

       public static bool CheckDekiWikiAccounts()
       {
           return Program.conf.DekiWikiAccountList.Count > 0 && Program.conf.DekiWikiSelected >= 0 &&
               Program.conf.DekiWikiAccountList.Count > Program.conf.DekiWikiSelected;
       }

       public static bool CheckDekiWikiAccounts(ref MainAppTask task)
       {
           bool result = CheckDekiWikiAccounts();
           if (!result) task.Errors.Add("A Mindtouch account does not exist or not selected properly.");
           return result;
       }

        internal static bool ImageSoftwareEnabled()
        {
            if (Program.conf.ImageEditor == null) return false;
            return Program.DISABLED_IMAGE_EDITOR != Program.conf.ImageEditor.Name;
        }

        /// <summary>
        /// Returns a WebProxy object based on active ProxyInfo and if Proxy is enabled, returns default system proxy otherwise
        /// </summary>
        public static IWebProxy GetProxySettings()
        {
            if (Program.conf.ProxyEnabled)
            {
                ProxyInfo acc = Program.conf.ProxyActive;
                if (acc != null)
                {
                    NetworkCredential cred = new NetworkCredential(acc.UserName, acc.Password);
                    return new WebProxy(acc.GetAddress(), true, null, cred);
                }
            }
            return WebRequest.DefaultWebProxy;
        }

        /// <summary>
        /// Quick Method to shorten a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string TryShortenURL(string url)
        {
            if (Program.conf.LimitLongURL == 0 || Program.conf.LimitLongURL > 0 && url.Length > Program.conf.LimitLongURL ||
                Program.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL)
            {
                TextUploader tu = Program.conf.UrlShortenersList[Program.conf.UrlShortenerSelected];
                tu.ProxySettings = Adapter.GetProxySettings();
                if (tu != null)
                {
                    string temp = tu.UploadText(TextInfo.FromString(url));
                    if (!string.IsNullOrEmpty(temp))
                    {
                        url = temp;
                    }
                }
            }
            return url;
        }
    }
}
