using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DavuxLib.Controls
{
    public class TreeViewEx : TreeView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DavuxLib.Win32API.UXTheme.SetWindowTheme(Handle, "explorer", null);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // base.OnPaintBackground(pevent);
        }

        private const int WM_NCCALCSIZE = 0x0083;
        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_CTL = 2;
        private const int SB_BOTH = 3;

        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCCALCSIZE:
                    ShowScrollBar(base.Handle, SB_BOTH, 0);
                    break;
            }
            base.WndProc(ref m);
        }
    }
}