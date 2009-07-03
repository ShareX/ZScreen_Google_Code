using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace ZSS
{
    public class KeyboardHook : IDisposable
    {
        public event KeyEventHandler KeyDownEvent, KeyUpEvent;
        //public event MouseEventHandler MouseDownEvent, MouseUpEvent;

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private HookProc HookProcedure;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x105;

        private IntPtr keyboardHookHandle = IntPtr.Zero;

        public KeyboardHook()
        {
            keyboardHookHandle = SetHook(KeyboardHookProc);
        }

        private IntPtr SetHook(HookProc hookProc)
        {
            HookProcedure = hookProc;
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, HookProcedure, GetModuleHandle(currentModule.ModuleName), 0);
            }
        }

        private IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                Keys keyData = (Keys)Marshal.ReadInt32(lParam) | Control.ModifierKeys;
                KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
                if (KeyDownEvent != null && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
                {
                    KeyDownEvent(null, keyEventArgs);
                }
                if (KeyUpEvent != null && (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP))
                {
                    KeyUpEvent(null, keyEventArgs);
                }
                if (keyEventArgs.Handled || keyEventArgs.SuppressKeyPress)
                {
                    return keyboardHookHandle;
                }
            }
            return CallNextHookEx(keyboardHookHandle, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(keyboardHookHandle);
        }
    }
}