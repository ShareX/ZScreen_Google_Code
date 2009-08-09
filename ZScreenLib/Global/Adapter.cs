using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ZScreenLib.Helpers;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.Helpers;
using ZSS;
using ZSS.ImageUploadersLib;
using System.Windows.Forms;

namespace ZScreenLib
{
   public static class Adapter
    {
       public static void AddToClipboardByDoubleClick(Control tp)
       {
           Control ctl = tp.GetNextControl(tp, true);
           while (ctl != null)
           {
               if (ctl.GetType() == typeof(TextBox))
               {
                   ctl.DoubleClick += TextBox_DoubleClick;
               }
               ctl = tp.GetNextControl(ctl, true);
           }
       }

       public static void TextBox_DoubleClick(object sender, EventArgs e)
       {
           TextBox tb = ((TextBox)sender);
           if (!string.IsNullOrEmpty(tb.Text))
           {
               Clipboard.SetText(tb.Text);
           }
       }

       public static bool CheckFTPAccounts()
       {
           return Loader.conf.FTPAccountList.Count > 0 && Loader.conf.FTPSelected >= 0 && Loader.conf.FTPAccountList.Count > Loader.conf.FTPSelected;
       }

       public static bool CheckFTPAccounts(ref MainAppTask task)
       {
           bool result = CheckFTPAccounts();
           if (!result) task.Errors.Add("An FTP account does not exist or not selected properly.");
           return result;
       }

       public static bool CheckDekiWikiAccounts()
       {
           return Loader.conf.DekiWikiAccountList.Count > 0 && Loader.conf.DekiWikiSelected >= 0 &&
               Loader.conf.DekiWikiAccountList.Count > Loader.conf.DekiWikiSelected;
       }

       public static bool CheckDekiWikiAccounts(ref MainAppTask task)
       {
           bool result = CheckDekiWikiAccounts();
           if (!result) task.Errors.Add("A Mindtouch account does not exist or not selected properly.");
           return result;
       }

        internal static bool ImageSoftwareEnabled()
        {
            if (Loader.conf.ImageEditor == null) return false;
            return Loader.DISABLED_IMAGE_EDITOR != Loader.conf.ImageEditor.Name;
        }

        /// <summary>
        /// Returns a WebProxy object based on active ProxyInfo and if Proxy is enabled, returns default system proxy otherwise
        /// </summary>
        public static IWebProxy GetProxySettings()
        {
            if (Loader.conf.ProxyEnabled)
            {
                ProxyInfo acc = Loader.conf.ProxyActive;
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
            if (Loader.conf.LimitLongURL == 0 || Loader.conf.LimitLongURL > 0 && url.Length > Loader.conf.LimitLongURL ||
                Loader.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL)
            {
                TextUploader tu = Loader.conf.UrlShortenersList[Loader.conf.UrlShortenerSelected];
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
