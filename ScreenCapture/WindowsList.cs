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
using System.Drawing;
using System.Linq;
using System.Text;
using HelpersLib;

namespace ScreenCapture
{
    public class WindowsList
    {
        public List<IntPtr> IgnoreWindows { get; set; }

        private List<WindowInfo> windows;

        public WindowsList()
        {
            IgnoreWindows = new List<IntPtr>();
        }

        public WindowsList(IntPtr ignoreWindow)
            : this()
        {
            IgnoreWindows.Add(ignoreWindow);
        }

        public List<WindowInfo> GetWindowsList()
        {
            windows = new List<WindowInfo>();
            NativeMethods.EnumWindowsProc ewp = new NativeMethods.EnumWindowsProc(EvalWindows);
            NativeMethods.EnumWindows(ewp, IntPtr.Zero);
            return windows;
        }

        public List<WindowInfo> GetVisibleWindowsList()
        {
            List<WindowInfo> windows = GetWindowsList();
            List<WindowInfo> visibleWindows = new List<WindowInfo>();

            foreach (WindowInfo window in windows)
            {
                if (window.IsVisible && !string.IsNullOrEmpty(window.Text))
                {
                    string className = window.ClassName;

                    if (!string.IsNullOrEmpty(className) && !className.Equals("Progman", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Rectangle rect = window.Rectangle;

                        if (rect.Width > 0 && rect.Height > 0 && CaptureHelpers.GetScreenBounds().Contains(rect))
                        {
                            visibleWindows.Add(window);
                        }
                    }
                }
            }

            return visibleWindows;
        }

        private bool EvalWindows(IntPtr hWnd, IntPtr lParam)
        {
            foreach (IntPtr window in IgnoreWindows)
            {
                if (hWnd == window) return true;
            }

            windows.Add(new WindowInfo(hWnd));

            return true;
        }
    }
}