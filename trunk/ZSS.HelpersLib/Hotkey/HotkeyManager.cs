using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpersLib.Hotkey
{
    public enum ZUploaderHotkey
    {
        ClipboardUpload,
        FileUpload,
        PrintScreen,
        ActiveWindow,
        RectangleRegion,
        RoundedRectangleRegion,
        EllipseRegion,
        TriangleRegion,
        DiamondRegion,
        PolygonRegion,
        FreeHandRegion
    }

    public class HotkeyManager
    {
        private List<HotkeySetting> settings = new List<HotkeySetting>();

        private HotkeyForm hotkeyForm;

        public HotkeyManager(HotkeyForm hotkeyForm)
        {
            this.hotkeyForm = hotkeyForm;
        }

        public void AddHotkey(ZUploaderHotkey hotkeyEnum, HotkeySetting hotkeySetting, Action action, ToolStripMenuItem menuItem = null)
        {
            hotkeySetting.Tag = (int)hotkeyEnum;
            hotkeySetting.MenuItem = menuItem;
            settings.Add(hotkeySetting);
            hotkeySetting.UpdateMenuItemShortcut();
            hotkeyForm.RegisterHotkey(hotkeySetting.Hotkey, action, (int)hotkeyEnum);
        }

        public void ChangeHotkey(ZUploaderHotkey hotkeyEnum, Keys newHotkey)
        {
            foreach (HotkeySetting hotkeySetting in settings)
            {
                if (hotkeySetting.Tag == (int)hotkeyEnum)
                {
                    hotkeySetting.Hotkey = newHotkey;
                    hotkeySetting.UpdateMenuItemShortcut();
                }
            }

            hotkeyForm.ChangeHotkey((int)hotkeyEnum, newHotkey);
        }
    }
}