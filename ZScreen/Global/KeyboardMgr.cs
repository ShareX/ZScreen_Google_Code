using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ZSS.Global
{
    public static class KeyboardMgr
    {
        public static bool CheckKeys(HKcombo hkc, IntPtr lParam)
        {
            if (hkc.Mods == null) //0 mods
            {
                if (Control.ModifierKeys == Keys.None && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                    return true;
            }
            else // if(hkc.Mods.Length > 0)
            {
                if (hkc.Mods.Length == 1)
                {
                    if (Control.ModifierKeys == hkc.Mods[0] && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
                else //if (hkc.Mods.Length == 2)
                {
                    if (Control.ModifierKeys == (hkc.Mods[0] | hkc.Mods[1]) && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
            }

            return false;
        }

        public static HKcombo GetHKcombo(IntPtr lParam)
        {
            try
            {
                string[] mods = Control.ModifierKeys.ToString().Split(',');

                if (Control.ModifierKeys == Keys.None)
                {
                    return new HKcombo((Keys)Marshal.ReadInt32(lParam));
                }
                if (mods.Length == 1)
                {
                    return new HKcombo((Keys)Enum.Parse(typeof(Keys), mods[0], true), (Keys)Marshal.ReadInt32(lParam));
                }
                if (mods.Length == 2)
                {
                    return new HKcombo((Keys)Enum.Parse(typeof(Keys), mods[0], true), (Keys)Enum.Parse(typeof(Keys), mods[1], true), (Keys)Marshal.ReadInt32(lParam));
                }
            }
            catch { }

            return null;
        }
    }
}
