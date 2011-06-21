#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GraphicsMgrLib;
using Greenshot.Helpers;
using HelpersLib;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Properties;

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
                        Engine.MyLogger.WriteException(ex, "Error while copying image to clipboard");
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

                Engine.MyLogger.WriteLine(string.Format("Saved {0} as an Image to Clipboard...", filePath));
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
                sbMsg.Append(task.Job2.GetDescription());
                sbMsg.Append(" to ");
                switch (task.Job1)
                {
                    case JobLevel1.Image:
                        sbMsg.Append(task.GetActiveImageUploadersDescription());
                        break;
                    case JobLevel1.Text:
                        if (task.Job3 == WorkerTask.JobLevel3.ShortenURL)
                        {
                            sbMsg.Append(task.GetActiveLinkUploadersDescription());
                        }
                        else
                        {
                            sbMsg.Append(task.GetActiveTextUploadersDescription());
                        }
                        break;
                    case JobLevel1.File:
                        sbMsg.Append(task.GetActiveUploadersDescription<FileUploaderType>(task.MyFileUploaders));
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

        #endregion Worker Tasks

        public static string ResourcePath = Path.Combine(Application.StartupPath, "ZSS.ResourcesLib.dll");

        public static void AddToList<T>(ToolStripDropDownButton tssdb, List<T> list)
        {
            foreach (var obj in tssdb.DropDownItems)
            {
                if (obj.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem tsmi = obj as ToolStripMenuItem;
                    if (tsmi.Checked)
                    {
                        list.Add((T)tsmi.Tag);
                    }
                }
            }
        }

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
                Clipboard.SetText(tb.Text); // ok
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
                    Engine.MyLogger.WriteLine(string.Format("Tested {0} sub-folder path in {1}", sfp, account.ToString()));
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
            // TODO: Only using Image index for FTP
            return Engine.MyUploadersConfig.FTPAccountList.CheckSelected(Engine.MyUploadersConfig.FTPSelectedImage);
        }

        public static FTPAccount GetFtpAcctActive()
        {
            FTPAccount acc = null;
            if (CheckFTPAccounts())
            {
                acc = Engine.MyUploadersConfig.FTPAccountList[Engine.MyUploadersConfig.FTPSelectedImage];
            }
            return acc;
        }

        public static bool CheckFTPAccounts(WorkerTask task)
        {
            bool result = CheckFTPAccounts();
            if (!result) task.Errors.Add("An FTP account does not exist or not selected properly.");
            return result;
        }

        #endregion FTP Methods

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

        #region Proxy Methods

        public static string GetDefaultWebProxyHost()
        {
            return HttpWebRequest.DefaultWebProxy.GetProxy(new Uri("http://www.google.com")).Host;
        }

        public static int GetDefaultWebProxyPort()
        {
            return HttpWebRequest.DefaultWebProxy.GetProxy(new Uri("http://www.google.com")).Port;
        }

        public static ProxySettings CheckProxySettings()
        {
            Engine.MyLogger.WriteLine("Proxy Enabled: " + Engine.conf.ProxyConfig.ToString());
            return new ProxySettings { ProxyConfig = Engine.conf.ProxyConfig, ProxyActive = Engine.conf.ProxyActive };
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

        #endregion Proxy Methods

        public static bool ImageSoftwareEnabled()
        {
            if (Engine.conf.ImageEditor == null)
            {
                return false;
            }

            return Engine.conf.PerformActions;
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
        public static OAuthInfo TwitterGetActiveAccount()
        {
            if (Engine.MyUploadersConfig.TwitterOAuthInfoList.CheckSelected(Engine.MyUploadersConfig.TwitterSelectedAccount))
            {
                return Engine.MyUploadersConfig.TwitterOAuthInfoList[Engine.MyUploadersConfig.TwitterSelectedAccount];
            }

            return new OAuthInfo(ZKeys.TwitterConsumerKey, ZKeys.TwitterConsumerSecret);
        }

        public static void TwitterMsg(WorkerTask task)
        {
            StringBuilder sb = new StringBuilder();
            foreach (UploadResult ur in task.UploadResults)
            {
                sb.AppendLine(ur.URL);
            }
            if (sb.Length > 0)
            {
                TwitterMsg(sb.ToString());
            }
        }

        public static void TwitterMsg(string url)
        {
            OAuthInfo acc = TwitterGetActiveAccount();
            if (!string.IsNullOrEmpty(acc.UserToken))
            {
                TwitterMsg msg = new TwitterMsg(TwitterGetActiveAccount(), string.Format("{0} - Update Twitter Status...", acc.Description));
                msg.ActiveAccountName = acc.Description;
                msg.Icon = Resources.zss_main;
                msg.Config = Engine.conf.TwitterClientConfig;
                msg.FormClosed += new FormClosedEventHandler(twitterClient_FormClosed);
                msg.txtTweet.Text = url;
                msg.Show();
            }
        }

        private static void twitterClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            TwitterMsg msg = sender as TwitterMsg;
            Engine.conf.TwitterClientConfig = msg.Config;
        }

        #endregion Twitter Methods

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
                    Engine.MyLogger.WriteException(err, "Error while initializing Font and Color");
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
                Engine.MyLogger.WriteException(ex, "Error while setting Watermark Font");
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
            if (TaskbarManager.IsPlatformSupported)
            {
                CommonOpenFileDialog dlg = new CommonOpenFileDialog();
                dlg.EnsureReadOnly = true;
                dlg.IsFolderPicker = true;
                dlg.AllowNonFileSystemItems = true;
                dlg.Title = title;

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
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

        public static void TaskbarSetProgressState(Form form, TaskbarProgressBarState tbps)
        {
            if (form != null && form.ShowInTaskbar && TaskbarManager.IsPlatformSupported && Engine.zWindowsTaskbar != null)
            {
                Engine.zWindowsTaskbar.SetProgressState(tbps);
            }
        }

        public static void TaskbarSetProgressValue(Form form, int progress)
        {
            if (form != null && form.ShowInTaskbar && TaskbarManager.IsPlatformSupported && Engine.zWindowsTaskbar != null)
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
                    Engine.zJumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
                    Microsoft.WindowsAPICodePack.Taskbar.JumpList.AddToRecent(filePath);
                }
                catch (Exception ex)
                {
                    Engine.MyLogger.WriteException(ex, "Error while adding Recent Item to Windows 7 Taskbar");
                }
            }
        }

        #endregion "Windows 7 only"

        public static bool ClipboardMonitor
        {
            get
            {
                return Engine.conf.MonitorImages || Engine.conf.MonitorText || Engine.conf.MonitorFiles || Engine.conf.MonitorUrls;
            }
        }
    }
}