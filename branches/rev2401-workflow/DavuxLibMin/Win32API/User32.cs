using System;
using System.Runtime.InteropServices;

namespace DavuxLib.Win32API
{
    public class User32
    {
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void LockWorkStation();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr OpenInputDesktop(DesktopAccountFlags dwFlags, bool fInherit,
           DesktopDesiredAccess dwDesiredAccess);

        [Flags]
        public enum DesktopAccountFlags : uint
        {
            DF_ALLOWOTHERACCOUNTHOOK = 1,
        }

        [Flags]
        public enum DesktopDesiredAccess : uint
        {
            DESKTOP_CREATEMENU = 0x04,
            DESKTOP_CREATEWINDOW = 0x02,
            DESKTOP_ENUMERATE = 0x40,
            DESKTOP_HOOKCONTROL = 0x08,
            DESKTOP_READOBJECTS = 0x01,
            DESKTOP_WRITEOBJECTS = 0x80,
            DESKTOP_SWITCHDESKTOP = 0x100,
        }

        [DllImport("user32.dll")]
        public static extern bool RegisterShellHookWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int DeregisterShellHookWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern int RegisterWindowMessage(string message);

        public enum ShellHookMessages
        {
            HSHELL_WINDOWCREATED = 1,
            HSHELL_WINDOWDESTROYED = 2,
            HSHELL_ACTIVATESHELLWINDOW = 3,
            HSHELL_WINDOWACTIVATED = 4,
            HSHELL_GETMINRECT = 5,
            HSHELL_REDRAW = 6,
            HSHELL_TASKMAN = 7,
            HSHELL_LANGUAGE = 8,
            HSHELL_SYSMENU = 9,
            HSHELL_ENDTASK = 10,
            HSHELL_ACCESSIBILITYSTATE = 11,
            HSHELL_APPCOMMAND = 12,
            HSHELL_WINDOWREPLACED = 13,
            HSHELL_WINDOWREPLACING = 14,
            HSHELL_HIGHBIT = 0x8000,
            HSHELL_FLASH = HSHELL_REDRAW | HSHELL_HIGHBIT,
            HSHELL_RUDEAPPACTIVATED = HSHELL_WINDOWACTIVATED | HSHELL_HIGHBIT,
        }
    }
}