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
using HelpersLib;

namespace ScreenCapture
{
    public class WindowsListAdvanced
    {
        public bool IncludeControls { get; set; }

        public List<IntPtr> IgnoreWindows { get; set; }

        private List<WindowInfo> windows;

        public WindowsListAdvanced()
        {
            IgnoreWindows = new List<IntPtr>();
        }

        public List<WindowInfo> GetWindowsList()
        {
            windows = new List<WindowInfo>();
            NativeMethods.EnumWindowsProc ewp = new NativeMethods.EnumWindowsProc(EvalWindow);
            NativeMethods.EnumWindows(ewp, IntPtr.Zero);
            return windows;
        }

        private bool IsValidWindow(WindowInfo window)
        {
            return IgnoreWindows.All(x => window.Handle != x) && windows.All(x => window.Handle != x.Handle) &&
                window.IsVisible && window.Rectangle.IsValid();
        }

        private bool EvalWindow(IntPtr hWnd, IntPtr lParam)
        {
            WindowInfo window = new WindowInfo(hWnd);

            if (!IsValidWindow(window))
            {
                return true;
            }

            if (IncludeControls)
            {
                NativeMethods.EnumWindowsProc ewp = new NativeMethods.EnumWindowsProc(EvalWindow);
                NativeMethods.EnumChildWindows(hWnd, ewp, IntPtr.Zero);
            }

            windows.Add(window);

            return true;
        }
    }
}