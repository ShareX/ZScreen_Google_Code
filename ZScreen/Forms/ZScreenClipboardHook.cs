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
                string cbText = Clipboard.GetText();
                if (cbText != Engine.zPreviousClipboardText || string.IsNullOrEmpty(cbText))
                {
                    bool uploadImage = Clipboard.ContainsImage() && Engine.conf.MonitorImages;
                    bool uploadText = Clipboard.ContainsText() && Engine.conf.MonitorText;
                    bool uploadFile = Clipboard.ContainsFileDropList() && Engine.conf.MonitorFiles;
                    bool shortenUrl = Clipboard.ContainsText() && FileSystem.IsValidLink(cbText) && cbText.Length > Engine.conf.ShortenUrlAfterUploadAfter && Engine.conf.MonitorUrls;
                    if (uploadImage || uploadText || uploadFile || shortenUrl)
                    {
                        UploadUsingClipboard();
                        ClipboardUnhook();
                    }
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