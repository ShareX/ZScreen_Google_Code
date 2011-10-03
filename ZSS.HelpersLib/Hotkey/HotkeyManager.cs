#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HelpersLib.Hotkey
{
    public enum ZUploaderHotkey
    {
        [Description("Clipboard Upload")]
        ClipboardUpload,
        [Description("File Upload")]
        FileUpload,
        [Description("Print Screen")]
        PrintScreen,
        [Description("Active Window")]
        ActiveWindow,
        [Description("Rectangle Region")]
        RectangleRegion,
        [Description("Rounded Rectangle Region")]
        RoundedRectangleRegion,
        [Description("Ellipse Region")]
        EllipseRegion,
        [Description("Triangle Region")]
        TriangleRegion,
        [Description("Diamond Region")]
        DiamondRegion,
        [Description("Polygon Region")]
        PolygonRegion,
        [Description("Freehand Region")]
        FreeHandRegion
    }

    public class HotkeyManager
    {
        public List<HotkeySetting> Settings = new List<HotkeySetting>();

        private HotkeyForm hotkeyForm;

        public HotkeyManager(HotkeyForm hotkeyForm)
        {
            this.hotkeyForm = hotkeyForm;
        }

        public void AddHotkey(ZUploaderHotkey hotkeyEnum, HotkeySetting hotkeySetting, Action action, ToolStripMenuItem menuItem = null)
        {
            hotkeySetting.Tag = (int)hotkeyEnum;
            hotkeySetting.MenuItem = menuItem;
            Settings.Add(hotkeySetting);
            hotkeySetting.UpdateMenuItemShortcut();
            hotkeyForm.RegisterHotkey(hotkeySetting.Hotkey, action, (int)hotkeyEnum);
        }

        public void ChangeHotkey(ZUploaderHotkey hotkeyEnum, Keys newHotkey)
        {
            foreach (HotkeySetting hotkeySetting in Settings)
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