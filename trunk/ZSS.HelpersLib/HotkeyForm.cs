using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public HotkeyForm()
        {
            HotkeyList = new List<HotkeyInfo>();
        }

        public bool RegisterHotkey(Keys hotkey)
        {
            Keys vk = hotkey & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;

            Native.Modifiers modifiers = Native.Modifiers.None;

            if ((hotkey & Keys.Alt) == Keys.Alt) modifiers |= Native.Modifiers.Alt;
            if ((hotkey & Keys.Control) == Keys.Control) modifiers |= Native.Modifiers.Control;
            if ((hotkey & Keys.Shift) == Keys.Shift) modifiers |= Native.Modifiers.Shift;

            return RegisterHotkey(hotkey, (uint)vk, (uint)modifiers);
        }

        private bool RegisterHotkey(Keys hotkey, uint vk, uint modifiers)
        {
            ushort id = 0;

            try
            {
                string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + (int)hotkey;
                id = NativeMethods.GlobalAddAtom(atomName);
                if (id == 0)
                {
                    throw new Exception("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error().ToString());
                }

                if (!NativeMethods.RegisterHotKey(Handle, (int)id, modifiers, vk))
                {
                    throw new Exception("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error().ToString());
                }

                HotkeyList.Add(new HotkeyInfo(id, hotkey));

                return true;
            }
            catch (Exception e)
            {
                UnregisterHotkey(id);
                Debug.WriteLine(e);
            }

            return false;
        }

        private bool UnregisterHotkey(ushort id)
        {
            bool result = false;

            if (id > 0)
            {
                result = NativeMethods.UnregisterHotKey(Handle, id);
                NativeMethods.GlobalDeleteAtom(id);
            }

            return result;
        }

        public void RemoveAllHotkeys()
        {
            for (int i = 0; i < HotkeyList.Count; i++)
            {
                UnregisterHotkey(HotkeyList[i].ID);
                HotkeyList.RemoveAt(i);
            }
        }

        private HotkeyInfo GetHotkeyInfoFromID(ushort id)
        {
            return HotkeyList.FirstOrDefault(x => x.ID == id);
        }

        private HotkeyInfo GetHotkeyInfoFromKey(Keys key)
        {
            return HotkeyList.FirstOrDefault(x => x.Key == key);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Native.WM_HOTKEY)
            {
                HotkeyInfo hotkey = GetHotkeyInfoFromID((ushort)m.WParam);

                if (hotkey != null)
                {
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
            RemoveAllHotkeys();

            base.OnClosed(e);
        }
    }
}