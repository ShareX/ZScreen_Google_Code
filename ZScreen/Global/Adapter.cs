#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.URLShorteners;
using ZSS.ImageUploadersLib;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZSS.Helpers;
using System.Net;
using ZSS.Tasks;
using ZSS.TextUploadersLib.Helpers;

namespace ZSS.Global
{
    /// <summary>
    /// Class for public static methods for use in ZScreen
    /// </summary>
    public static class Adapter
    {
        public static void TestFTPAccount(FTPAccount acc)
        {
            string msg, path = FTPHelpers.CombineURL(acc.FTPAddress, acc.Path);

            FTPOptions options = new FTPOptions { Account = acc, ProxySettings = GetProxySettings() };
            FTPAdapter ftpClient = new FTPAdapter(options);

            try
            {
                ftpClient.ListDirectory(path);
                msg = "Success!";
            }
            catch (WebException e)
            {
                string status = ((FtpWebResponse)e.Response).StatusDescription;
                if (status.StartsWith("550 Failed to change directory.") && acc.AutoCreateFolder)
                {
                    try
                    {
                        ftpClient.MakeMultiDirectory(acc.Path);
                        ftpClient.ListDirectory(path);
                        msg = "Success!\nAuto created folders: " + acc.Path;
                    }
                    catch (WebException e2)
                    {
                        msg = GetWebExceptionMessage(e2);
                    }
                }
                else
                {
                    msg = GetWebExceptionMessage(e);
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static string GetWebExceptionMessage(WebException e)
        {
            string status = ((FtpWebResponse)e.Response).StatusDescription;
            return string.Format("Status description:\n{0}\nException message:\n{1}", status, e.Message);
        }

        public static bool CheckTextUploaders()
        {
            return Program.conf.TextUploaderSelected >= 0 && Program.conf.TextUploadersList.Count > 0;
        }

        public static bool CheckURLShorteners()
        {
            return Program.conf.UrlShortenerSelected >= 0 && Program.conf.UrlShortenersList.Count > 0;
        }

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

        public static void TestDekiWikiAccount(DekiWikiAccount acc)
        {
            string msg = "Success!";

            try
            {
                DekiWiki connector = new DekiWiki(new DekiWikiOptions(acc, GetProxySettings()));
                connector.Login();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Returns a WebProxy object based on active ProxyInfo and if Proxy is enabled, returns null otherwise
        /// </summary>
        public static WebProxy GetProxySettings()
        {
            WebProxy wp = null; // Use 'null' instead of GetEmptyWebProxy. http://go.microsoft.com/fwlink/?linkid=14202'	
            if (Program.conf.ProxyEnabled)
            {
                ProxyInfo acc = Program.conf.ProxyActive;
                if (acc != null)
                {
                    NetworkCredential cred = new NetworkCredential(acc.UserName, acc.Password);
                    wp = new WebProxy(acc.GetAddress(), true, null, cred);
                }
            }
            return wp;
        }

        public static void TestProxyAccount(ProxyInfo acc)
        {
            string msg = "Success!";

            try
            {
                NetworkCredential cred = new NetworkCredential(acc.UserName, acc.Password);
                WebProxy wp = new WebProxy(acc.GetAddress(), true, null, cred);
                WebClient wc = new WebClient { Proxy = wp };
                wc.DownloadString("http://www.google.com");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        /// <summary>
        /// Method to update TinyPic Shuk; Run periodically
        /// </summary>
        public static void UpdateTinyPicShuk()
        {
            if (Program.conf.RememberTinyPicUserPass && !string.IsNullOrEmpty(Program.conf.TinyPicUserName) &&
                !string.IsNullOrEmpty(Program.conf.TinyPicPassword))
            {
                TinyPicUploader tpu = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, UploadMode.API);
                tpu.ProxySettings = Adapter.GetProxySettings();
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
            return Program.conf.MakeTinyURL || Program.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL;
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
                    if (name == TextUploadersLib.FTPUploader.Hostname)
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