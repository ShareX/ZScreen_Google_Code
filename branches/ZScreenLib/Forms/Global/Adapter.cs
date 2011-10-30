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
using System.IO;
using System.Net;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using ZScreenLib.Properties;
using ZSS;
using System.Drawing.Printing;
using Greenshot.Helpers;
using System.Drawing;
using System.Web;
using Microsoft.Win32;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using HelpersLib;
using GraphicsMgrLib;
using System.Drawing.Imaging;
using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ZScreenLib
{
    /// <summary>
    /// Class for public static methods for use in ZScreen
    /// </summary>
    public static class Adapter
    {
        #region Worker Tasks

        public static void PrintImage(Image img)
        {
            if (img != null)
            {
                PrintHelper ph = new PrintHelper(img);
                PrinterSettings ps = ph.PrintWithDialog();
            }
        }

        public static void CopyDataToClipboard(object data)
        {
            Clipboard.SetDataObject(data, true);
        }

        public static void CopyImageToClipboard(Image img)
        {
            if (img != null)
            {
                using (Image img2 = ImageEffects.FillBackground(img, Engine.conf.ClipboardBackgroundColor))
                {
                    try
                    {
                        Clipboard.SetImage(img2);
                        //ImageOutput.PrepareClipboardObject();
                        //ImageOutput.CopyToClipboard(img2);
                    }
                    catch (Exception ex)
                    {
                        FileSystem.AppendDebug("Error while copying image to clipboard", ex);
                    }
                }
            }
        }

        public static void CopyImageToClipboard(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                using (Image img = Image.FromFile(filePath))
                {
                    CopyImageToClipboard(img);
                }

                FileSystem.AppendDebug(string.Format("Saved {0} as an Image to Clipboard...", filePath));
            }
        }

        public static void FlashNotifyIcon(NotifyIcon ni, Icon ico)
        {
            if (ni != null && ico != null)
            {
                ni.Icon = ico;
            }
        }

        public static void SetNotifyIconStatus(WorkerTask task, NotifyIcon ni, Icon ico)
        {
            if (task != null && ni != null && ico != null)
            {
                ni.Icon = ico;
                // Text length must be less than 64 characters long
                StringBuilder sbMsg = new StringBuilder();
                sbMsg.Append(task.Job.GetDescription());
                sbMsg.Append(": ");
                switch (task.JobCategory)
                {
                    case JobCategoryType.SCREENSHOTS:
                    case JobCategoryType.PICTURES:
                        sbMsg.Append(task.MyImageUploader.GetDescription());
                        break;
                    case JobCategoryType.TEXT:
                        sbMsg.Append(task.MyTextUploader.ToString());
                        break;
                    case JobCategoryType.BINARY:
                        sbMsg.Append(Path.GetFileName(task.LocalFilePath));
                        sbMsg.Append(" to ");
                        sbMsg.Append(task.MyFileUploader.GetDescription());
                        break;
                }
                ni.Text = sbMsg.ToString().Substring(0, Math.Min(sbMsg.Length, 63));
            }
        }

        public static void SetNotifyIconBalloonTip(NotifyIcon ni, string title, string msg, ToolTipIcon ico)
        {
            if (ni != null && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(msg))
            {
                ni.ShowBalloonTip(5000, title, msg, ico);
            }
        }

        public static void UpdateNotifyIconProgress(NotifyIcon ni, int progress)
        {
            if (ni != null)
            {
                using (Bitmap img = (Bitmap)GraphicsMgr.DrawProgressIcon(UploadManager.GetAverageProgress()))
                {
                    IntPtr hicon = img.GetHicon();
                    ni.Icon = Icon.FromHandle(hicon);
                    NativeMethods.DestroyIcon(hicon);
                }
            }
        }
        public static string SaveImage(Image img)
        {
            if (img != null)
            {
                return ImageOutput.SaveWithDialog(img);
            }

            return string.Empty;
        }

        #endregion

        public static string ResourcePath = Path.Combine(Application.StartupPath, "ZSS.ResourcesLib.dll");

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

        #region FTP Methods

        public static void TestFTPAccount(FTPAccount account, bool silent)
        {
            string msg;
            string sfp = account.GetSubFolderPath();
            using (FTP ftpClient = new FTP(account))
            {
                try
                {
                    DateTime time = DateTime.Now;
                    ftpClient.Test(sfp);
                    msg = "Success!";
                }
                catch (Exception e)
                {
                    if (e.Message.StartsWith("Could not change working directory to"))
                    {
                        try
                        {
                            ftpClient.MakeMultiDirectory(sfp);
                            ftpClient.Test(sfp);
                            msg = "Success!\nAuto created folders: " + sfp;
                        }
                        catch (Exception e2)
                        {
                            msg = e2.Message;
                        }
                    }
                    else
                    {
                        msg = e.Message;
                    }
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
                string ping = SendPing(account.Host, 3);
                if (!string.IsNullOrEmpty(ping))
                {
                    msg += "\n\nPing results:\n" + ping;
                }
                if (silent)
                {
                    FileSystem.AppendDebug(string.Format("Tested {0} sub-folder path in {1}", sfp, account.ToString()));
                }
                else
                {
                    MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public static string SendPing(string host)
        {
            return SendPing(host, 1);
        }

        public static string SendPing(string host, int count)
        {
            string[] status = new string[count];

            using (Ping ping = new Ping())
            {
                PingReply reply;
                //byte[] buffer = Encoding.ASCII.GetBytes(new string('a', 32));
                for (int i = 0; i < count; i++)
                {
                    reply = ping.Send(host, 3000);
                    if (reply.Status == IPStatus.Success)
                    {
                        status[i] = reply.RoundtripTime.ToString() + " ms";
                    }
                    else
                    {
                        status[i] = "Timeout";
                    }
                    Thread.Sleep(100);
                }
            }

            return string.Join(", ", status);
        }

        public static bool CheckFTPAccounts()
        {
            return CheckList(Engine.conf.FTPAccountList, Engine.conf.FtpImages);
        }

        public static FTPAccount GetFtpAcctActive()
        {
            FTPAccount acc = null;
            if (CheckFTPAccounts())
            {
                acc = Engine.conf.FTPAccountList[Engine.conf.FtpImages];
            }
            return acc;
        }

        public static bool CheckTwitterAccounts()
        {
            return CheckList(Engine.conf.TwitterAccountsList, Engine.conf.TwitterAcctSelected);
        }

        public static bool CheckFTPAccounts(ref WorkerTask task)
        {
            bool result = CheckFTPAccounts();
            if (!result) task.Errors.Add("An FTP account does not exist or not selected properly.");
            return result;
        }

        #endregion

        #region TinyPic Methods

        public static string GetTinyPicShuk()
        {
            UserPassBox ub = new UserPassBox("Enter TinyPic Email Address and Password", string.IsNullOrEmpty(Engine.conf.TinyPicUserName) ? "someone@gmail.com" : Engine.conf.TinyPicUserName, Engine.conf.TinyPicPassword) { Icon = Resources.zss_main };
            ub.ShowDialog();
            if (ub.DialogResult == DialogResult.OK)
            {
                TinyPicUploader tpu = new TinyPicUploader(Engine.TINYPIC_ID, Engine.TINYPIC_KEY);
                if (Engine.conf.RememberTinyPicUserPass)
                {
                    Engine.conf.TinyPicUserName = ub.UserName;
                    Engine.conf.TinyPicPassword = ub.Password;
                }
                return tpu.UserAuth(ub.UserName, ub.Password);
            }
            return null;
        }

        /// <summary>
        /// Method to update TinyPic Shuk; Run periodically
        /// </summary>
        public static void UpdateTinyPicShuk()
        {
            if (Engine.conf.RememberTinyPicUserPass && !string.IsNullOrEmpty(Engine.conf.TinyPicUserName) &&
                !string.IsNullOrEmpty(Engine.conf.TinyPicPassword))
            {
                TinyPicUploader tpu = new TinyPicUploader(Engine.TINYPIC_ID, Engine.TINYPIC_KEY);
                string shuk = tpu.UserAuth(Engine.conf.TinyPicUserName, Engine.conf.TinyPicPassword);
                if (!string.IsNullOrEmpty(shuk))
                {
                    if (Engine.conf.TinyPicShuk != shuk)
                    {
                        FileSystem.AppendDebug(string.Format("Updated TinyPic Shuk from {0} to {1}", Engine.conf.TinyPicShuk, shuk));
                    }
                    Engine.conf.TinyPicShuk = shuk;
                }
            }
        }

        #endregion

        #region ImageBam Methods

        public static string CreateImageBamGallery()
        {
            ImageBamUploader ibu = new ImageBamUploader(new ImageBamUploaderOptions(Engine.conf.ImageBamApiKey, Engine.conf.ImageBamSecret));
            string galleryId = ibu.CreateGalleryID();
            Engine.conf.ImageBamGallery.Add(galleryId);
            return galleryId;
        }

        public static string GetImageBamGalleryActive()
        {
            string galleryId = string.Empty;
            if (CheckImageBamGallery())
            {
                galleryId = Engine.conf.ImageBamGallery[Engine.conf.ImageBamGalleryActive];
            }
            return galleryId;
        }

        #endregion

        #region URL Shortener Methods

        public static TextUploader FindUrlShortener(string name)
        {
            if (name.Equals(BitlyUploader.Hostname))
            {
                return new BitlyUploader();
            }
            else if (name.Equals(IsgdUploader.Hostname))
            {
                return new IsgdUploader();
            }
            else if (name.Equals(JmpUploader.Hostname))
            {
                return new JmpUploader();
            }
            else if (name.Equals(KlamUploader.Hostname))
            {
                return new KlamUploader();
            }
            else if (name.Equals(OwlyUploader.Hostname))
            {
                return new OwlyUploader();
            }
            else if (name.Equals(TinyURLUploader.Hostname))
            {
                return new TinyURLUploader();
            }
            else if (name.Equals(ThreelyUploader.Hostname))
            {
                return new ThreelyUploader();
            }
            else if (name.Equals(TurlUploader.Hostname))
            {
                return new TurlUploader();
            }

            return null;
        }

        /// <summary>
        /// Attempt to shorten a URL
        /// </summary>
        /// <param name="url">Full URL</param>
        /// <returns>Shortens URL or Empty String if request failed</returns>
        public static string TryShortenURL(string url)
        {
            FileSystem.AppendDebug(string.Format("URL Length: {0}; Shortening after {1}", url.Length.ToString(), Engine.conf.ShortenUrlAfterUploadAfter));
            if (!string.IsNullOrEmpty(url))
            {
                if (Engine.conf.ShortenUrlAfterUploadAfter == 0 || Engine.conf.TwitterEnabled ||
                    (Engine.conf.ShortenUrlAfterUploadAfter > 0 && url.Length > Engine.conf.ShortenUrlAfterUploadAfter) ||
                    (Engine.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL))
                {
                    return ShortenURL(url);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Method to shorten a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ShortenURL(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                TextUploader tu = Engine.conf.UrlShortenersList[Engine.conf.UrlShortenerSelected];
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

        public static bool CheckURLShorteners()
        {
            return CheckList(Engine.conf.UrlShortenersList, Engine.conf.UrlShortenerSelected);
        }

        public static bool MakeTinyURL()
        {
            // LimitLongURL = 0 means make tinyURL always
            return Engine.conf.TwitterEnabled || Engine.conf.ShortenUrlAfterUpload || Engine.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL;
        }

        #endregion

        #region Text Uploader Methods

        public static TextUploader GetTextUploaderActive()
        {
            TextUploader uploader = null;
            if (CheckTextUploaders())
            {
                uploader = Engine.conf.TextUploadersList[Engine.conf.TextUploaderSelected];
            }
            return uploader;
        }

        #endregion

        public static bool CheckList<T>(List<T> list, int selected)
        {
            return list.Count > 0 && selected >= 0 && list.Count > selected;
        }

        public static bool FindItemInList<T>(List<T> list, string name)
        {
            foreach (T item in list)
            {
                if (item.ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckTextUploaders()
        {
            return CheckList(Engine.conf.TextUploadersList, Engine.conf.TextUploaderSelected);
        }

        public static bool CheckDekiWikiAccounts()
        {
            return CheckList(Engine.conf.DekiWikiAccountList, Engine.conf.DekiWikiSelected);
        }

        public static bool CheckDekiWikiAccounts(ref WorkerTask task)
        {
            bool result = CheckDekiWikiAccounts();
            if (!result) task.Errors.Add("A Mindtouch account does not exist or not selected properly.");
            return result;
        }

        public static bool CheckImageBamGallery()
        {
            return CheckList(Engine.conf.ImageBamGallery, Engine.conf.ImageBamGallerySelected);
        }

        public static void TestDekiWikiAccount(DekiWikiAccount acc)
        {
            string msg = "Success!";

            try
            {
                DekiWiki connector = new DekiWiki(new DekiWikiOptions(acc, CheckProxySettings().GetWebProxy));
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

        #region Proxy Methods

        public static ProxySettings CheckProxySettings()
        {
            FileSystem.AppendDebug("Proxy Enabled: " + Engine.conf.ProxyEnabled.ToString());
            return new ProxySettings { ProxyEnabled = Engine.conf.ProxyEnabled, ProxyActive = Engine.conf.ProxyActive };
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

        #endregion

        public static UserPassBox SendSpaceRegister()
        {
            UserPassBox upb = new UserPassBox("SendSpace Registration...", "John Doe", "john.doe@gmail.com", "JohnDoe", "");
            upb.ShowDialog();
            if (upb.DialogResult == DialogResult.OK)
            {
                SendSpace sendSpace = new SendSpace();
                upb.Success = sendSpace.AuthRegister(upb.UserName, upb.FullName, upb.Email, upb.Password);
                if (!upb.Success && sendSpace.Errors.Count > 0)
                {
                    MessageBox.Show(sendSpace.ToErrorString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return upb;
        }

        public static TextUploader FindTextUploader(string name)
        {
            if (name.Equals(PastebinUploader.Hostname))
            {
                return new PastebinUploader();
            }
            else if (name.Equals(PastebinCaUploader.Hostname))
            {
                return new PastebinCaUploader();
            }
            else if (name.Equals(Paste2Uploader.Hostname))
            {
                return new Paste2Uploader();
            }
            else if (name.Equals(SlexyUploader.Hostname))
            {
                return new SlexyUploader();
            }
            else if (name.Equals(SniptUploader.Hostname))
            {
                return new SniptUploader();
            }

            return null;
        }

        public static bool ImageSoftwareEnabled()
        {
            if (Engine.conf.ImageEditor == null)
            {
                return false;
            }

            return Engine.conf.ImageEditorsEnabled;
        }

        public static void DeleteFile(string fp)
        {
            if (File.Exists(fp))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(fp, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                                                                   Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
        }

        #region Twitter Methods

        /// <summary>
        /// Returns the active TwitterAuthInfo object; if nothing is active then a new TwitterAuthInfo object is returned
        /// </summary>
        /// <returns></returns>
        public static TwitterAuthInfo TwitterGetActiveAcct()
        {
            TwitterAuthInfo acc = new TwitterAuthInfo();
            if (CheckTwitterAccounts())
            {
                acc = Engine.conf.TwitterAccountsList[Engine.conf.TwitterAcctSelected];
            }
            return acc;
        }

        public static TwitterAuthInfo TwitterAuthGetPin()
        {
            // authorize ZScreen to twitter
            oAuthTwitter oAuth = new oAuthTwitter(Engine.TWITTER_CONSUMER_KEY, Engine.TWITTER_CONSUMER_SECRET);
            TwitterAuthInfo acc = TwitterGetActiveAcct();
            string authLink = oAuth.AuthorizationLinkGet();
            if (!string.IsNullOrEmpty(acc.AccountName))
            {
                oAuth.AuthInfo.AccountName = acc.AccountName;
            }
            if (CheckTwitterAccounts())
            {
                Engine.conf.TwitterAccountsList[Engine.conf.TwitterAcctSelected] = oAuth.AuthInfo;
            }
            if (!string.IsNullOrEmpty(authLink))
            {
                System.Diagnostics.Process.Start(authLink);
            }
            return oAuth.AuthInfo;
        }

        public static TwitterAuthInfo TwitterAuthSetPin(ref TwitterAuthInfo acc)
        {
            try
            {
                if (null != acc)
                {
                    oAuthTwitter oAuth = new oAuthTwitter(Engine.TWITTER_CONSUMER_KEY, Engine.TWITTER_CONSUMER_SECRET);
                    acc = oAuth.AccessTokenGet(ref acc);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Error while setting Twitter PIN", ex);
            }
            return acc;
        }

        public static void TwitterMsg(ref WorkerTask task)
        {
            if (!string.IsNullOrEmpty(task.RemoteFilePath))
            {
                TwitterMsg(task.RemoteFilePath);
            }
        }

        public static void TwitterMsg(string url)
        {
            TwitterAuthInfo acc = TwitterGetActiveAcct();
            if (!string.IsNullOrEmpty(acc.TokenSecret))
            {
                List<oAuthTwitter> oAccList = new List<oAuthTwitter>();
                foreach (TwitterAuthInfo oAuth in Engine.conf.TwitterAccountsList)
                {
                    oAccList.Add(new oAuthTwitter(Engine.TWITTER_CONSUMER_KEY, Engine.TWITTER_CONSUMER_SECRET, oAuth) { Enabled = acc.AccountName == oAuth.AccountName });
                }
                TwitterMsg msg = new TwitterMsg(oAccList, string.Format("{0} - Update Twitter Status...", acc.AccountName));
                msg.ActiveAccountName = acc.AccountName;
                msg.Icon = Resources.zss_main;
                msg.Config = Engine.conf.TwitterClientConfig;
                msg.FormClosed += new FormClosedEventHandler(twitterClient_FormClosed);
                msg.txtTweet.Text = url;
                msg.Show();
            }
        }

        static void twitterClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            TwitterMsg msg = sender as TwitterMsg;
            Engine.conf.TwitterClientConfig = msg.Config;
        }

        #endregion

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string ZScreenCliPath()
        {
            return Path.Combine(Application.StartupPath, Engine.ZScreenCLI);
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AppRevision
        {
            get
            {
                return AssemblyVersion.Split('.')[3];
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static DialogResult ShowFontDialog()
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                FontDialog fDialog = new FontDialog
                {
                    ShowColor = true
                };
                try
                {
                    fDialog.Color = XMLSettings.DeserializeColor(Engine.conf.WatermarkFontColor);
                    fDialog.Font = XMLSettings.DeserializeFont(Engine.conf.WatermarkFont);
                }
                catch (Exception err)
                {
                    FileSystem.AppendDebug("Error while initializing Font and Color", err);
                }

                result = fDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Engine.conf.WatermarkFont = XMLSettings.SerializeFont(fDialog.Font);
                    Engine.conf.WatermarkFontColor = XMLSettings.SerializeColor(fDialog.Color);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Error while setting Watermark Font", ex);
            }
            return result;
        }

        /// <summary>
        /// Method to show the appropriate Folder Browser dialog based on the OS
        /// </summary>
        /// <param name="title">Title for the folder browser</param>
        /// <returns>Folder path chosen by the user</returns>
        public static string GetDirPathUsingFolderBrowser(string title)
        {
            string newDir = string.Empty;
            if (CoreHelpers.RunningOnWin7)
            {
                CommonOpenFileDialog dlg = new CommonOpenFileDialog();
                dlg.EnsureReadOnly = true;
                dlg.IsFolderPicker = true;
                dlg.AllowNonFileSystemItems = true;
                dlg.Title = title;

                if (dlg.ShowDialog() == CommonFileDialogResult.OK)
                {
                    newDir = dlg.FileName;
                }
            }
            else
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = title;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    newDir = dlg.SelectedPath;
                }
            }
            return newDir;
        }

        #region "Windows 7 only"

        public static void TaskbarSetProgressState(TaskbarProgressBarState tbps)
        {
            if (TaskbarManager.IsPlatformSupported && Engine.zWindowsTaskbar != null)
            {
                Engine.zWindowsTaskbar.SetProgressState(tbps);
            }
        }

        public static void TaskbarSetProgressValue(int progress)
        {
            if (TaskbarManager.IsPlatformSupported && Engine.zWindowsTaskbar != null)
            {
                Engine.zWindowsTaskbar.SetProgressValue(progress, 100);
            }
        }

        public static void AddRecentItem(string filePath)
        {
            if (TaskbarManager.IsPlatformSupported && File.Exists(filePath) && Engine.zJumpList != null)
            {
                try
                {
                    Engine.zJumpList.AddToRecent(filePath);
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug("Error while adding Recent Item to Windows 7 Taskbar", ex);
                }
            }
        }

        #endregion

        public static bool ClipboardMonitor
        {
            get
            {
                return Engine.conf.MonitorImages || Engine.conf.MonitorText || Engine.conf.MonitorFiles || Engine.conf.MonitorUrls;
            }
        }
    }
}