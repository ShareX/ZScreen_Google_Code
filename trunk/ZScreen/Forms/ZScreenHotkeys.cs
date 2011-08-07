using System;
using System.Windows.Forms;
using HelpersLib;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        private void RegisterHotkeys(bool resetKeys)
        {
            foreach (HotkeyTask hk in Enum.GetValues(typeof(HotkeyTask)))
            {
                RegisterHotkey(hk, resetKeys);
            }
        }

        private void RegisterHotkey(HotkeyTask hotkeyEnum, bool resetKeys)
        {
            object userHotKey = Engine.conf.GetFieldValue("DefaultHotkey" + hotkeyEnum.ToString().Replace(" ", string.Empty));

            if (!resetKeys)
            {
                userHotKey = Engine.conf.GetFieldValue("Hotkey" + hotkeyEnum.ToString().Replace(" ", string.Empty));
            }

            if (userHotKey != null && userHotKey.GetType() == typeof(Keys))
            {
                Keys hotkey = (Keys)userHotKey;
                switch (hotkeyEnum)
                {
                    case HotkeyTask.ActiveWindow:
                        RegisterHotkey(hotkey, CaptureActiveWindow);
                        break;
                    case HotkeyTask.CropShot:
                        RegisterHotkey(hotkey, CaptureRectRegion);
                        break;
                    case HotkeyTask.EntireScreen:
                        RegisterHotkey(hotkey, CaptureEntireScreen);
                        break;
                    case HotkeyTask.ClipboardUpload:
                        RegisterHotkey(hotkey, UploadUsingClipboardOrGoogleTranslate);
                        break;
                    case HotkeyTask.SelectedWindow:
                        RegisterHotkey(hotkey, CaptureSelectedWindow);
                        break;
                    case HotkeyTask.LastCropShot:
                        RegisterHotkey(hotkey, CaptureRectRegionLast);
                        break;
                    case HotkeyTask.AutoCapture:
                        RegisterHotkey(hotkey, ShowAutoCapture);
                        break;
                    case HotkeyTask.DropWindow:
                        RegisterHotkey(hotkey, ShowDropWindow);
                        break;
                    case HotkeyTask.FreehandCropShot:
                        RegisterHotkey(hotkey, CaptureFreeHandRegion);
                        break;
                    case HotkeyTask.LanguageTranslator:
                        RegisterHotkey(hotkey, StartWorkerTranslator);
                        break;
                    case HotkeyTask.ScreenColorPicker:
                        RegisterHotkey(hotkey, ScreenColorPicker);
                        break;
                    case HotkeyTask.TwitterClient:
                        RegisterHotkey(hotkey, Adapter.ShowTwitterClient);
                        break;
                }
            }
        }
    }
}