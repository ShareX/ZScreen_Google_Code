using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;

namespace DavuxLib.Win32API
{
    public class DwmAPI
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmDefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);

        public static bool DwmDefWindowProc(ref System.Windows.Forms.Message m)
        {
            IntPtr result;
            int dwmHandled = DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, out result);

            if (dwmHandled == 1)
            {
                m.Result = result;
                return true;
            }
            return false;
        }

        [DllImport("dwmapi.dll")]
        private static extern void DwmIsCompositionEnabled(ref bool pfEnabled);
        public static bool DwmIsCompositionEnabled()
        {
            bool isGlassSupported = false;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                DwmIsCompositionEnabled(ref isGlassSupported);
            }
            return isGlassSupported;
        }

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);
        public static void DwmExtendFrameIntoClientArea(Control c, MARGINS marg)
        {
            if (DwmIsCompositionEnabled())
            {
                DwmExtendFrameIntoClientArea(c.Handle, ref marg);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
    }
}

