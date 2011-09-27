using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ScreenCapture;

namespace GraphicsMgrLib
{
    public static class GraphicsMgrNativeMethods
    {
        [DllImport("user32.dll")]
        public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        public enum RegionType
        {
            ERROR = 0,
            NULLREGION = 1,
            SIMPLEREGION = 2,
            COMPLEXREGION = 3
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        public static bool GetWindowRegion(IntPtr hWnd, out Region region)
        {
            IntPtr hRgn = GDI.CreateRectRgn(0, 0, 0, 0);
            RegionType regionType = (RegionType)GetWindowRgn(hWnd, hRgn);
            region = Region.FromHrgn(hRgn);
            return regionType != RegionType.ERROR && regionType != RegionType.NULLREGION;
        }
    }
}