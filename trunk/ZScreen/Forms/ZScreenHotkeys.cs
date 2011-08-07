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
                        RegisterHotkey(hotkey);
                        break;
                }
            }
        }
    }
}