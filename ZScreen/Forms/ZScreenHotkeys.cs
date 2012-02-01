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
            if (HotkeyManager == null)
            {
                HotkeyManager = new HotkeyManager(this, ZAppType.ZScreen);

                HotkeyManager.AddHotkey(ZScreenHotkey.EntireScreen, Engine.ConfigUI.HotkeyEntireScreen2, CaptureEntireScreen);
                HotkeyManager.AddHotkey(ZScreenHotkey.ActiveMonitor, Engine.ConfigUI.HotkeyActiveMonitor2, CaptureActiveMonitor);
                HotkeyManager.AddHotkey(ZScreenHotkey.ActiveWindow, Engine.ConfigUI.HotkeyActiveWindow2, CaptureActiveWindow);
                HotkeyManager.AddHotkey(ZScreenHotkey.RectangleRegion, Engine.ConfigUI.HotkeyRectangleRegion2, CaptureRectRegion);

                HotkeyManager.AddHotkey(ZScreenHotkey.RectangleRegionLast, Engine.ConfigUI.HotkeyRectangleRegionLast2, CaptureRectRegionLast);
                HotkeyManager.AddHotkey(ZScreenHotkey.SelectedWindow, Engine.ConfigUI.HotkeySelectedWindow2, CaptureSelectedWindow);
                HotkeyManager.AddHotkey(ZScreenHotkey.FreehandRegion, Engine.ConfigUI.HotkeyFreeHandRegion2, CaptureFreeHandRegion);

                HotkeyManager.AddHotkey(ZScreenHotkey.ClipboardUpload, Engine.ConfigUI.HotkeyClipboardUpload2, UploadUsingClipboardOrGoogleTranslate);
                HotkeyManager.AddHotkey(ZScreenHotkey.AutoCapture, Engine.ConfigUI.HotkeyAutoCapture2, ShowAutoCapture);
                HotkeyManager.AddHotkey(ZScreenHotkey.DropWindow, Engine.ConfigUI.HotkeyDropWindow2, ShowDropWindow);

                HotkeyManager.AddHotkey(ZScreenHotkey.ScreenColorPicker, Engine.ConfigUI.HotkeyScreenColorPicker2, ShowScreenColorPicker);
                HotkeyManager.AddHotkey(ZScreenHotkey.TwitterClient, Engine.ConfigUI.HotkeyTwitterClient2, Adapter.ShowTwitterClient);
                HotkeyManager.AddHotkey(ZScreenHotkey.RectangleRegionClipboard, Engine.ConfigUI.HotkeyCaptureRectangeRegionClipboard2, CaptureRectRegionClipboard);

                string failedHotkeys;

                if (HotkeyManager.IsHotkeyRegisterFailed(out failedHotkeys))
                {
                    MessageBox.Show(string.Format("Unable to register hotkey(s):\r\n\r\n" + failedHotkeys +
                        "\r\n\r\nPlease select a different hotkey or quit the conflicting application and reopen {0}.", Application.ProductName),
                        "Hotkey register failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}