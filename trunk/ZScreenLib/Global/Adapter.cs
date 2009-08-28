﻿#region License Information (GPL v2)
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

        public static void CopyImageToClipboard(Image img)
        {
            if (img != null)
            {
                try
                {
                    ImageOutput.PrepareClipboardObject();
                    ImageOutput.CopyToClipboard(img);
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.Message);
                }
            }
        }

        public static void CopyImageToClipboard(string f)
        {
            if (File.Exists(f))
            {
                Image img = Image.FromFile(f);
                Clipboard.SetImage(img);
                img.Dispose();
                FileSystem.AppendDebug(string.Format("Saved {0} as an Image to Clipboard...", f));
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
                ni.Text = task.Job.GetDescription();
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
                Bitmap img = (Bitmap)GraphicsMgr.DrawProgressIcon(UploadManager.GetAverageProgress());
                ni.Icon = Icon.FromHandle(img.GetHicon());
            }

        }
        public static void SaveImage(Image img)
        {
            if (img != null)
            {
                ImageOutput.SaveWithDialog(img);
            }
        }

        #endregion

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
                if (status != null && status.StartsWith("550") && acc.AutoCreateFolder)
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

        public static bool CheckFTPAccounts()
        {
            return CheckList(Engine.conf.FTPAccountList, Engine.conf.FTPSelected);
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
                TinyPicUploader tpu = new TinyPicUploader(Engine.TINYPIC_ID, Engine.TINYPIC_KEY, UploadMode.API);
                if (Engine.conf.RememberTinyPicUserPass)
                {
                    Engine.conf.TinyPicUserName = ub.UserName;
                    Engine.conf.TinyPicPassword = ub.Password;
                }
                return tpu.UserAuth(ub.UserName, ub.Password);
            }
            return string.Empty;
        }

        /// <summary>
        /// Method to update TinyPic Shuk; Run periodically
        /// </summary>
        public static void UpdateTinyPicShuk()
        {
            if (Engine.conf.RememberTinyPicUserPass && !string.IsNullOrEmpty(Engine.conf.TinyPicUserName) &&
                !string.IsNullOrEmpty(Engine.conf.TinyPicPassword))
            {
                TinyPicUploader tpu = new TinyPicUploader(Engine.TINYPIC_ID, Engine.TINYPIC_KEY, UploadMode.API);
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

        public static string TryShortenURL(string url)
        {
            if (!string.IsNullOrEmpty(url) && (Engine.conf.LimitLongURL == 0 || Engine.conf.LimitLongURL > 0 && url.Length > Engine.conf.LimitLongURL ||
                Engine.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL))
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
            return Engine.conf.MakeTinyURL || Engine.conf.ClipboardUriMode == ClipboardUriType.FULL_TINYURL;
        }

        #endregion



        public static bool CheckList<T>(List<T> list, int selected)
        {
            return list.Count > 0 && selected >= 0 && list.Count > selected;
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

        #region Proxy Methods

        /// <summary>
        /// Returns a WebProxy object based on active ProxyInfo and if Proxy is enabled, returns default system proxy otherwise
        /// </summary>
        public static IWebProxy GetProxySettings()
        {
            if (Engine.conf.ProxyEnabled)
            {
                ProxyInfo acc = Engine.conf.ProxyActive;
                if (acc != null)
                {
                    NetworkCredential cred = new NetworkCredential(acc.UserName, acc.Password);
                    return new WebProxy(acc.GetAddress(), true, null, cred);
                }
                else
                {
                    return HttpWebRequest.DefaultWebProxy;
                }
            }
            return null;
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
            else
            {
                if (name == UploadersLib.FTPUploader.Hostname)
                {
                    if (Engine.conf.FTPAccountList.Count > 0)
                    {
                        FTPAccount acc = new FTPAccount();
                        if (Engine.conf.FTPSelected >= 0)
                        {
                            acc = Engine.conf.FTPAccountList[Engine.conf.FTPSelected];
                        }
                        else
                        {
                            acc = Engine.conf.FTPAccountList[0];
                        }
                        return new UploadersLib.FTPUploader(acc);
                    }
                }
            }
            return null;
        }

        public static bool ImageSoftwareEnabled()
        {
            if (Engine.conf.ImageEditor == null) return false;
            return Engine.DISABLED_IMAGE_EDITOR != Engine.conf.ImageEditor.Name;
        }

        public static void DeleteFile(string fp)
        {
            if (File.Exists(fp))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(fp, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                                                                   Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
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
                    FileSystem.AppendDebug(ex);
                }
            }
        }

        #endregion

    }
}