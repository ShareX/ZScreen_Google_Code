using System;
using System.Windows.Forms;
using Greenshot.Helpers;
using Greenshot.Plugin;
using HelpersLib;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {
        public string[] HotkeyNames = new string[]
        {
            "Entire Screen",
            "Active Window",
            "Crop Shot",
            "Selected Window",
            "Freehand Crop Shot",
            "Clipboard Upload",
            "Last Crop Shot",
            "Auto Capture",
            "Drop Window",
            "Language Translator",
            "Screen Color Picker",
            "Twitter Client"
        };

        public enum HotkeyJob
        {
            EntireScreen,
            ActiveWindow,
            CropShot,
            SelectedWindow,
            FreehandCropShot,
            ClipboardUpload,
            LastCropShot,
            AutoCapture,
            DropWindow,
            LanguageTranslator,
            ScreenColorPicker,
            TwitterClient
        }

        private void RegisterHotkeys(bool resetKeys)
        {
            foreach (HotkeyJob hk in Enum.GetValues(typeof(HotkeyJob)))
            {
                RegisterHotkey(hk, resetKeys);
            }
        }

        private void RegisterHotkey(HotkeyJob hotkeyEnum, bool resetKeys)
        {
            object userHotKey = Engine.conf.GetFieldValue("DefaultHotkey" + hotkeyEnum.ToString().Replace(" ", string.Empty));

            if (!resetKeys)
            {
                userHotKey = Engine.conf.GetFieldValue("Hotkey" + hotkeyEnum.ToString().Replace(" ", string.Empty));
            }

            if (userHotKey != null && userHotKey.GetType() == typeof(Keys))
            {
                Keys hotkey = (Keys)userHotKey;
                Keys vk = hotkey & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;

                Native.Modifiers modifiers = Native.Modifiers.None;

                if ((hotkey & Keys.Alt) == Keys.Alt) modifiers |= Native.Modifiers.Alt;
                if ((hotkey & Keys.Control) == Keys.Control) modifiers |= Native.Modifiers.Control;
                if ((hotkey & Keys.Shift) == Keys.Shift) modifiers |= Native.Modifiers.Shift;

                switch (hotkeyEnum)
                {
                    case HotkeyJob.ActiveWindow:
                        HotkeyHelper.RegisterHotKey(this.Handle, (uint)modifiers, (uint)vk, new HotKeyHandler(CaptureActiveWindow));
                        break;
                    case HotkeyJob.EntireScreen:
                        HotkeyHelper.RegisterHotKey(this.Handle, (uint)modifiers, (uint)vk, new HotKeyHandler(CaptureEntireScreen));
                        break;
                    case HotkeyJob.CropShot:
                        HotkeyHelper.RegisterHotKey(this.Handle, (uint)modifiers, (uint)vk, new HotKeyHandler(CaptureRectRegion));
                        break;
                }
            }
        }
    }
}