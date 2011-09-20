using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using HelpersLib;
using ZScreenLib;
using System.Windows.Forms;
using System.ComponentModel;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        private Timer tmrClipboardMonitor = new Timer() { Interval = 100, Enabled = true };

        #region Clipboard Methods

        public void ClipboardHook()
        {
            tmrClipboardMonitor.Enabled = true;
            tmrClipboardMonitor.Tick += new EventHandler(tmrClipboardMonitor_Tick);
            Engine.MyLogger.WriteLine("Registered Clipboard Monitor via " + new StackFrame(1).GetMethod().Name);
        }

        void tmrClipboardMonitor_Tick(object sender, EventArgs e)
        {
            if (IsReady)
            {
                bool uploadImage = false, uploadText = false, uploadFile = false, shortenUrl = false;

                if (Engine.conf.MonitorImages)
                {
                    uploadImage = Clipboard.ContainsImage();
                }
                if (Engine.conf.MonitorText)
                {
                    string cbText = Clipboard.GetText();
                    uploadText = Clipboard.ContainsText() && !string.IsNullOrEmpty(cbText) && cbText != Engine.zPreviousClipboardText;
                }
                if (Engine.conf.MonitorFiles)
                {
                    uploadFile = Clipboard.ContainsFileDropList();
                }
                if (Engine.conf.MonitorUrls)
                {
                    string cbText = Clipboard.GetText();
                    shortenUrl = Clipboard.ContainsText() && !string.IsNullOrEmpty(cbText) && cbText != Engine.zPreviousClipboardText && FileSystem.IsValidLink(cbText) && cbText.Length > Engine.conf.ShortenUrlAfterUploadAfter;
                }

                if (uploadImage || uploadText || uploadFile || shortenUrl)
                {
                    ClipboardUnhook();
                    UploadUsingClipboard();
                }

            }
        }

        public void ClipboardUnhook()
        {
            tmrClipboardMonitor.Enabled = false;
            Engine.MyLogger.WriteLine("Unregisterd Clipboard Monitor via " + new StackFrame(1).GetMethod().Name);
        }

        #endregion Clipboard Methods
    }
}