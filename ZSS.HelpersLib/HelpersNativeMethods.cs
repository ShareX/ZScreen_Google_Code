using System;
using System.Runtime.InteropServices;
using System.Text;

namespace HelpersLib
{
    public static class HelpersNativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        public static string GetWindowLabel()
        {
            const int numOfChars = 256;
            IntPtr handle = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(numOfChars);

            if (GetWindowText(handle, sb, numOfChars) > 0)
            {
                return sb.ToString();
            }

            return string.Empty;
        }
    }
}