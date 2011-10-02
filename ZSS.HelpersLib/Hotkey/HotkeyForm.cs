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
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HelpersLib
{
    public class HotkeyForm : Form
    {
        public List<HotkeyInfo> HotkeyList { get; private set; }

        public delegate void HotkeyEventHandler(KeyEventArgs e);

        public event HotkeyEventHandler HotkeyPress;

        public bool IgnoreHotkeys { get; set; }

        public HotkeyForm()
        {
            HotkeyList = new List<HotkeyInfo>();
        }

        public HotkeyInfo RegisterHotkey(Keys hotkey, Action hotkeyPress = null, int tag = -1)
        {
            if (IsHotkeyExist(hotkey)) return null;

            Keys vk = hotkey & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;

            NativeMethods.Modifiers modifiers = NativeMethods.Modifiers.None;

            if ((hotkey & Keys.Alt) == Keys.Alt) modifiers |= NativeMethods.Modifiers.Alt;
            if ((hotkey & Keys.Control) == Keys.Control) modifiers |= NativeMethods.Modifiers.Control;
            if ((hotkey & Keys.Shift) == Keys.Shift) modifiers |= NativeMethods.Modifiers.Shift;

            ushort id = 0;

            try
            {
                string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + (int)hotkey;

                id = NativeMethods.GlobalAddAtom(atomName);

                if (id == 0)
                {
                    StaticHelper.WriteLine("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error().ToString());
                    return null;
                }

                if (!NativeMethods.RegisterHotKey(Handle, (int)id, (uint)modifiers, (uint)vk))
                {
                    StaticHelper.WriteLine("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error().ToString());
                    return null;
                }

                HotkeyInfo hotkeyInfo = new HotkeyInfo(id, hotkey, hotkeyPress, tag);

                HotkeyList.Add(hotkeyInfo);

                return hotkeyInfo;
            }
            catch (Exception e)
            {
                UnregisterHotkey(id);
                StaticHelper.WriteException(e);
            }

            return null;
        }

        public bool UnregisterHotkey(ushort id)
        {
            bool result = false;

            if (id > 0)
            {
                result = NativeMethods.UnregisterHotKey(Handle, id);
                NativeMethods.GlobalDeleteAtom(id);
            }

            return result;
        }

        public bool UnregisterHotkey(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo != null)
            {
                bool result = UnregisterHotkey(hotkeyInfo.ID);
                HotkeyList.Remove(hotkeyInfo);
                return result;
            }

            return false;
        }

        public bool UnregisterHotkey(Keys key)
        {
            HotkeyInfo hotkeyInfo = GetHotkeyInfoFromKey(key);

            return UnregisterHotkey(hotkeyInfo);
        }

        public void ChangeHotkey(int tag, Keys newHotkey)
        {
            HotkeyInfo hi = GetHotkeyInfoFromTag(tag);

            if (hi != null)
            {
                UnregisterHotkey(hi);
                RegisterHotkey(newHotkey, hi.HotkeyPress, tag);
            }
        }

        public void UnregisterAllHotkeys()
        {
            for (int i = 0; i < HotkeyList.Count; i++)
            {
                UnregisterHotkey(HotkeyList[i]);
            }
        }

        public bool IsHotkeyExist(Keys key)
        {
            return HotkeyList.Any(x => x.Key == key);
        }

        public HotkeyInfo GetHotkeyInfoFromKey(Keys key)
        {
            return HotkeyList.FirstOrDefault(x => x.Key == key);
        }

        public HotkeyInfo GetHotkeyInfoFromTag(int tag)
        {
            return HotkeyList.FirstOrDefault(x => x.Tag == tag);
        }

        public HotkeyInfo GetHotkeyInfoFromID(ushort id)
        {
            return HotkeyList.FirstOrDefault(x => x.ID == id);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WM.HOTKEY && !IgnoreHotkeys)
            {
                HotkeyInfo hotkey = GetHotkeyInfoFromID((ushort)m.WParam);

                if (hotkey != null)
                {
                    if (hotkey.HotkeyPress != null) hotkey.HotkeyPress();

                    OnKeyPressed(new KeyEventArgs(hotkey.Key));
                }

                return;
            }

            base.WndProc(ref m);
        }

        protected void OnKeyPressed(KeyEventArgs e)
        {
            if (HotkeyPress != null)
            {
                HotkeyPress(e);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            UnregisterAllHotkeys();

            base.OnClosed(e);
        }
    }
}