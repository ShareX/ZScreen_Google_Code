using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploaderLib;
using ZSS.TextUploaderLib.URLShorteners;
using ZSS.ImageUploaderLib;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZSS.Helpers;
using System.Net;

namespace ZSS.Global
{
    /// <summary>
    /// Class for public static methods for use in ZScreen
    /// </summary>
    public static class Adapter
    {
        public static void TestFTPAccount(FTPAccount acc)
        {
            string msg;
            FTP ftpClient = new FTP(acc);
            ftpClient.ProxySettings = GetProxySettings();
            try
            {
                if (ftpClient.ListDirectory() != null)
                {
                    msg = "Success.";
                }
                else
                {
                    msg = "FTP Settings are not set correctly. Make sure your FTP Path exists.";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(550) File unavailable") && acc.AutoCreateFolder)
                {
                    try
                    {
                        ftpClient.MakeMultiDirectory(acc.Path);
                        if (ftpClient.ListDirectory() != null)
                        {
                            msg = "Success.\nAuto created folder: " + acc.Path;
                        }
                        else
                        {
                            msg = "FTP Settings are not set correctly. Make sure your FTP Path exists.";
                        }
                    }
                    catch (Exception ex2)
                    {
                        msg = ex2.Message;
                    }
                }
                else
                {
                    msg = ex.Message;
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static bool TestFTP(FTP ftp)
        {
            return ftp.ListDirectory() != null;
        }

        public static bool CheckFTPAccounts(ref Tasks.MainAppTask task)
        {
            if (Program.conf.FTPAccountList.Count > 0 && Program.conf.FTPSelected >= 0 && Program.conf.FTPAccountList.Count > Program.conf.FTPSelected)
            {
                return true;
            }
            else
            {
                task.Errors.Add("An FTP account does not exist or not selected properly.");
                return false;
            }
        }

        public static bool CheckDekiWikiAccounts(ref Tasks.MainAppTask task)
        {
            if (Program.conf.DekiWikiAccountList.Count > 0 && Program.conf.DekiWikiSelected != -1 && Program.conf.DekiWikiAccountList.Count > Program.conf.DekiWikiSelected)
            {
                return true;
            }
            task.Errors.Add("A Mindtouch account does not exist or not selected properly.");
            return false;
        }

        public static void TestDekiWikiAccount(DekiWikiAccount acc)
        {
            string msg = string.Empty;
            try
            {
                // Create the connector
                DekiWiki connector = new DekiWiki(ref acc);

                // Attempt to login
                connector.Login();

                // Set the success text
                msg = "Success!"; //Success
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
        /// <returns></returns>
        public static WebProxy GetProxySettings()
        {
            WebProxy wp = null;
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
                WebClient wc = new WebClient(); 
                wc.Proxy = wp;
                string html = wc.DownloadString(new Uri("http://www.google.com"));                
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
            if (Program.conf.LimitLongURL == 0 || Program.conf.LimitLongURL > 0 && url.Length > Program.conf.LimitLongURL || Program.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL)
            {
                TextUploader tu = Program.conf.UrlShortenerActive;
                tu.ProxySettings = Adapter.GetProxySettings();
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
            bool tinyurl = Program.conf.MakeTinyURL || Program.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL;
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
                    if (name == ZSS.TextUploaderLib.FTPUploader.Hostname)
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
                            return new ZSS.TextUploaderLib.FTPUploader(acc);
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

        public static void WriteTextToFile(string txt, string path)
        {
            new Thread(delegate() { WriteToFile(txt, path); }).Start();
        }

        private static void WriteToFile(string txt, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(txt);
            }
        }
    }
}