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

    public enum ZScreenHotkey
    {
        [Description("Capture Entire Screen")]
        EntireScreen,
        [Description("Capture Active Window")]
        ActiveWindow,
        [Description("Capture Rectangular Region")]
        RectangleRegion,
        [Description("Capture Last Rectangular Region")]
        RectangleRegionLast,
        [Description("Capture Selected Window")]
        SelectedWindow,
        [Description("Capture Shape")]
        FreehandRegion,
        [Description("Clipboard Upload")]
        ClipboardUpload,
        [Description("Auto Capture")]
        AutoCapture,
        [Description("Drop Window")]
        DropWindow,
        [Description("Color Picker")]
        ScreenColorPicker,
        [Description("Twitter Client")]
        TwitterClient
    }

    public enum JBirdHotkey
    {
        [Description("")]
        Workflow
    }

    public class HotkeyManager
    {
        public ZAppType Host = ZAppType.ZUploader;
        public List<HotkeySetting> Settings = new List<HotkeySetting>();

        private HotkeyForm hotkeyForm;

        public HotkeyManager(HotkeyForm hotkeyForm, ZAppType host)
        {
            this.hotkeyForm = hotkeyForm;
            this.Host = host;
        }

        public void AddHotkey(ZUploaderHotkey hotkeyEnum, HotkeySetting hotkeySetting, Action action, ToolStripMenuItem menuItem = null)
        {
            AddHotkey((int)hotkeyEnum, hotkeySetting, action, menuItem);
        }

        public void AddHotkey(ZScreenHotkey hotkeyEnum, HotkeySetting hotkeySetting, Action action, ToolStripMenuItem menuItem = null)
        {
            AddHotkey((int)hotkeyEnum, hotkeySetting, action, menuItem);
        }

        public void AddHotkey(JBirdHotkey hotkeyEnum, HotkeySetting hotkeySetting, Action action, ToolStripMenuItem menuItem = null)
        {
            AddHotkey((int)hotkeyEnum, hotkeySetting, action, menuItem);
        }

        private void AddHotkey(int hotkeyId, HotkeySetting hotkeySetting, Action action, ToolStripMenuItem menuItem = null)
        {
            hotkeySetting.Tag = hotkeyId;
            hotkeySetting.Action = action;
            hotkeySetting.MenuItem = menuItem;
            Settings.Add(hotkeySetting);
            hotkeySetting.UpdateMenuItemShortcut();
            hotkeySetting.IsActive = hotkeyForm.RegisterHotkey(hotkeySetting.Hotkey, action, hotkeyId);
        }

        public HotkeyStatus UpdateHotkey(HotkeySetting setting)
        {
            setting.UpdateMenuItemShortcut();
            setting.IsActive = hotkeyForm.ChangeHotkey(setting.Tag, setting.Hotkey, setting.Action);
            return setting.IsActive;
        }
    }
}