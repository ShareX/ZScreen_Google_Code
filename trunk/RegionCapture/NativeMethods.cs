﻿#region License Information (GPL v2)

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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ScreenCapture
{
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static Rectangle GetWindowRect(IntPtr handle)
        {
            RECT rect;
            GetWindowRect(handle, out rect);
            return rect.ToRectangle();
        }

        /// <summary> The RegisterHotKey function defines a system-wide hot key </summary>
        /// <param name="hwnd">Handle to the window that will receive WM_HOTKEY messages generated by the hot key.</param>
        /// <param name="id">Specifies the identifier of the hot key.</param>
        /// <param name="fsModifiers">Specifies keys that must be pressed in combination with the key
        /// specified by the 'vk' parameter in order to generate the WM_HOTKEY message.</param>
        /// <param name="vk">Specifies the virtual-key code of the hot key</param>
        /// <returns><c>true</c> if the function succeeds, otherwise <c>false</c></returns>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms646309(VS.85).aspx"/>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern ushort GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern ushort GlobalDeleteAtom(ushort nAtom);

        [Flags]
        public enum DwmWindowAttribute
        {
            NCRenderingEnabled = 1,
            NCRenderingPolicy,
            TransitionsForceDisabled,
            AllowNCPaint,
            CaptionButtonBounds,
            NonClientRtlLayout,
            ForceIconicRepresentation,
            Flip3DPolicy,
            ExtendedFrameBounds,
            HasIconicBitmap,
            DisallowPeek,
            ExcludedFromPeek,
            Last
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        public static bool GetExtendedFrameBounds(IntPtr handle, out Rectangle rectangle)
        {
            RECT rect;
            int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.ExtendedFrameBounds, out rect, Marshal.SizeOf(typeof(RECT)));
            rectangle = rect.ToRectangle();
            return result >= 0;
        }

        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        public enum SHOWWINDOW : uint
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        public static bool IsWindowMaximized(IntPtr handle)
        {
            NativeMethods.WINDOWPLACEMENT wp = new NativeMethods.WINDOWPLACEMENT();
            NativeMethods.GetWindowPlacement(handle, ref wp);
            return wp.showCmd == (int)NativeMethods.SHOWWINDOW.SW_MAXIMIZE;
        }

        public static Rectangle MaximizedWindowFix(IntPtr handle, Rectangle windowRect)
        {
            if (NativeMethods.IsWindowMaximized(handle))
            {
                Rectangle screenRect = Screen.FromRectangle(windowRect).Bounds;

                if (windowRect.X < screenRect.X)
                {
                    windowRect.Width -= (screenRect.X - windowRect.X) * 2;
                    windowRect.X = screenRect.X;
                }

                if (windowRect.Y < screenRect.Y)
                {
                    windowRect.Height -= (screenRect.Y - windowRect.Y) * 2;
                    windowRect.Y = screenRect.Y;
                }

                windowRect.Intersect(screenRect);
            }

            return windowRect;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }

            public Size Size { get { return new Size(Width, Height); } }
            public Point Location { get { return new Point(Left, Top); } }

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }

            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
              ^ ((Width << 0x1a) | (Width >> 6))
              ^ ((Height << 7) | (Height >> 0x19));
            }

            #region Operator overloads

            public static implicit operator Rectangle(RECT rect)
            {
                return rect.ToRectangle();
            }

            public static implicit operator RECT(Rectangle rect)
            {
                return FromRectangle(rect);
            }

            #endregion Operator overloads
        }
    }
}