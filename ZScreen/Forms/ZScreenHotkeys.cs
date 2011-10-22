using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        public HotkeyManager HotkeyManager { get; private set; }

        public static int mHKSelectedRow = -1;

        private void InitHotkeys()
        {
            HotkeyManager = new HotkeyManager(this, ZAppType.ZScreen);

            HotkeyManager.AddHotkey(ZScreenHotkey.EntireScreen, Engine.ConfigUI.HotkeyEntireScreen2, CaptureEntireScreen);
            HotkeyManager.AddHotkey(ZScreenHotkey.ActiveWindow, Engine.ConfigUI.HotkeyActiveWindow2, CaptureActiveWindow);
            HotkeyManager.AddHotkey(ZScreenHotkey.RectangleRegion, Engine.ConfigUI.HotkeyRectangleRegion2, CaptureRectRegion);

            HotkeyManager.AddHotkey(ZScreenHotkey.RectangleRegionLast, Engine.ConfigUI.RectangleRegionLast2, CaptureRectRegionLast);
            HotkeyManager.AddHotkey(ZScreenHotkey.SelectedWindow, Engine.ConfigUI.HotkeySelectedWindow2, CaptureSelectedWindow);
            HotkeyManager.AddHotkey(ZScreenHotkey.FreehandRegion, Engine.ConfigUI.HotkeyFreeHandRegion2, CaptureFreeHandRegion);

            HotkeyManager.AddHotkey(ZScreenHotkey.ClipboardUpload, Engine.ConfigUI.HotkeyClipboardUpload2, UploadUsingClipboardOrGoogleTranslate);
            HotkeyManager.AddHotkey(ZScreenHotkey.AutoCapture, Engine.ConfigUI.HotkeyAutoCapture2, ShowAutoCapture);
            HotkeyManager.AddHotkey(ZScreenHotkey.DropWindow, Engine.ConfigUI.HotkeyDropWindow2, ShowDropWindow);

            HotkeyManager.AddHotkey(ZScreenHotkey.ScreenColorPicker, Engine.ConfigUI.HotkeyScreenColorPicker2, ShowScreenColorPicker);
            HotkeyManager.AddHotkey(ZScreenHotkey.TwitterClient, Engine.ConfigUI.HotkeyTwitterClient2, Adapter.ShowTwitterClient);
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