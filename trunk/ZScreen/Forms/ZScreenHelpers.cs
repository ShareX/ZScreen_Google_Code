using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HelpersLib;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using UploadersLib.OtherServices;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        protected override void WndProc(ref Message m)
        {
            if (IsReady)
            {
                // defined in winuser.h
                const int WM_DRAWCLIPBOARD = 0x308;
                const int WM_CHANGECBCHAIN = 0x030D;

                switch (m.Msg)
                {
                    case WM_DRAWCLIPBOARD:
                        try
                        {
                            string cbText = Clipboard.GetText();
                            bool uploadImage = Clipboard.ContainsImage() && Engine.conf.MonitorImages;
                            bool uploadText = Clipboard.ContainsText() && Engine.conf.MonitorText;
                            bool uploadFile = Clipboard.ContainsFileDropList() && Engine.conf.MonitorFiles;
                            bool shortenUrl = Clipboard.ContainsText() && FileSystem.IsValidLink(cbText) && cbText.Length > Engine.conf.ShortenUrlAfterUploadAfter && Engine.conf.MonitorUrls;
                            if (uploadImage || uploadText || uploadFile || shortenUrl)
                            {
                                if (cbText != Engine.zPreviousClipboardText || string.IsNullOrEmpty(cbText))
                                {
                                    UploadUsingClipboard();
                                }
                            }
                        }
                        catch (ExternalException ex)
                        {
                            // Copying a field definition in Access 2002 causes this sometimes?
                            Engine.MyLogger.WriteException(ex, "InteropServices.ExternalException in ZScreen.WndProc");
                            return;
                        }
                        catch (Exception ex)
                        {
                            Engine.MyLogger.WriteException(ex, "Error monitoring clipboard");
                            return;
                        }
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;

                    case WM_CHANGECBCHAIN:
                        if (m.WParam == nextClipboardViewer)
                            nextClipboardViewer = m.LParam;
                        else
                            SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;

                    case NativeMethods.WM_SYSCOMMAND:
                        if (m.WParam.ToInt32() == NativeMethods.SC_MINIMIZE) // Minimize button handling
                        {
                            switch (Engine.AppConf.WindowButtonActionMinimize)
                            {
                                case WindowButtonAction.ExitApplication:
                                    CloseMethod = CloseMethod.MinimizeButton;
                                    Close();
                                    break;
                                case WindowButtonAction.MinimizeToTaskbar:
                                    WindowState = FormWindowState.Minimized;
                                    break;
                                case WindowButtonAction.MinimizeToTray:
                                    Hide();
                                    break;
                            }

                            m.Result = IntPtr.Zero;
                            return;
                        }
                        break;
                }
            }

            base.WndProc(ref m);
        }

        public void DisableFeatures()
        {
            tcCapture.TabPages.Remove(tpFreehandCropShot);
        }

        public GoogleTranslateGUI GetGTGUI()
        {
            if (Loader.MyGTGUI == null || Loader.MyGTGUI.IsDisposed)
            {
                Loader.MyGTGUI = new GoogleTranslateGUI(Engine.MyGTConfig, ZKeys.GetAPIKeys()) { Icon = this.Icon };
            }

            if (Engine.MyGTConfig.GoogleLanguages == null || Engine.MyGTConfig.GoogleLanguages.Count < 1)
            {
                Engine.MyGTConfig.GoogleLanguages = new GoogleTranslate(ZKeys.GoogleTranslateKey).GetLanguages();
            }

            return Loader.MyGTGUI;
        }

        public static void OpenHistory()
        {
            // if Engine.conf is null then open use default amount
            int maxNum = 100;
            if (Engine.conf != null)
            {
                maxNum = Engine.conf.HistoryMaxNumber;
            }
            new HistoryLib.HistoryForm(Engine.HistoryPath, maxNum, string.Format("{0} - History", Engine.GetProductName())).Show();
        }

        private void OpenLastSource(UploadResult.SourceType sType)
        {
            OpenSource(UploadManager.UploadResultLast, sType);
        }

        private bool OpenSource(UploadResult ifm, UploadResult.SourceType sType)
        {
            if (ifm != null)
            {
                string path = ifm.GetSource(Engine.TempDir, sType);
                if (!string.IsNullOrEmpty(path))
                {
                    if (sType == UploadResult.SourceType.TEXT || sType == UploadResult.SourceType.HTML)
                    {
                        StaticHelper.LoadBrowser(path);
                        return true;
                    }

                    if (sType == UploadResult.SourceType.STRING)
                    {
                        Clipboard.SetText(path); // ok
                        return true;
                    }
                }
            }

            return false;
        }

        public void ProxyAdd(ProxyInfo acc)
        {
            Engine.conf.ProxyList.Add(acc);
            ucProxyAccounts.AccountsList.Items.Add(acc);
            ucProxyAccounts.AccountsList.SelectedIndex = ucProxyAccounts.AccountsList.Items.Count - 1;
        }
    }
}