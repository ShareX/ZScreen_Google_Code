using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        public HotkeyManager HotkeyManager { get; private set; }

        public static int mHKSelectedRow = -1;

        private void InitHotkeys()
        {
            HotkeyManager = new HotkeyManager(this, ZAppType.ZScreen);

            HotkeyManager.AddHotkey(ZScreenHotkey.EntireScreen, Engine.conf.HotkeyEntireScreen2, CaptureEntireScreen);
            HotkeyManager.AddHotkey(ZScreenHotkey.ActiveWindow, Engine.conf.HotkeyActiveWindow2, CaptureActiveWindow);
            HotkeyManager.AddHotkey(ZScreenHotkey.RectangleRegion, Engine.conf.HotkeyRectangleRegion2, CaptureRectRegion);

            HotkeyManager.AddHotkey(ZScreenHotkey.RectangleRegionLast, Engine.conf.RectangleRegionLast2, CaptureRectRegionLast);
            HotkeyManager.AddHotkey(ZScreenHotkey.SelectedWindow, Engine.conf.HotkeySelectedWindow2, CaptureSelectedWindow);
            HotkeyManager.AddHotkey(ZScreenHotkey.FreehandRegion, Engine.conf.HotkeyFreeHandRegion2, CaptureFreeHandRegion);

            HotkeyManager.AddHotkey(ZScreenHotkey.ClipboardUpload, Engine.conf.HotkeyClipboardUpload2, UploadUsingClipboardOrGoogleTranslate);
            HotkeyManager.AddHotkey(ZScreenHotkey.AutoCapture, Engine.conf.HotkeyAutoCapture2, ShowAutoCapture);
            HotkeyManager.AddHotkey(ZScreenHotkey.DropWindow, Engine.conf.HotkeyDropWindow2, ShowDropWindow);

            HotkeyManager.AddHotkey(ZScreenHotkey.ScreenColorPicker, Engine.conf.HotkeyScreenColorPicker2, ScreenColorPicker);
            HotkeyManager.AddHotkey(ZScreenHotkey.TwitterClient, Engine.conf.HotkeyTwitterClient2, Adapter.ShowTwitterClient);
        }

        private void UpdateHotkeys(bool resetKeys = false)
        {
            List<HotkeyInfo> hkiList = new List<HotkeyInfo>();

            foreach (ZScreenHotkey hk in Enum.GetValues(typeof(ZScreenHotkey)))
            {
            }

            StringBuilder sbErrors = new StringBuilder();

            foreach (HotkeyInfo hki in hkiList)
            {
                if (hki != null && !string.IsNullOrEmpty(hki.Error))
                {
                    sbErrors.AppendLine(hki.Error);
                }
            }

            if (sbErrors.Length > 0)
            {
                sbErrors.AppendLine("\nPlease reconfigure different hotkeys or quit the conflicting application and start over.");
                MessageBox.Show(sbErrors.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}