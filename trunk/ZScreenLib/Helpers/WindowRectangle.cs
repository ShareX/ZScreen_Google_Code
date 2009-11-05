#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ZScreenLib.Helpers
{
    public class WindowRectangle
    {
        private IntPtr handle;
        private Rectangle rectangle;
        private Queue windows = new Queue();
        private Queue controls = new Queue();
        private Rectangle bounds;
        private uint processId;

        public WindowRectangle(IntPtr windowHandle)
        {
            handle = windowHandle;
            bounds = GraphicsMgr.GetScreenBounds();
        }

        public Rectangle CalculateWindowRectangle()
        {
            if (handle.ToInt32() > 0)
            {
                rectangle = User32.GetWindowRectangle(handle);

                User32.GetWindowThreadProcessId(handle, out processId);

                string processName = Process.GetProcessById((int)processId).ProcessName;
                Console.WriteLine(processName);

                if (processName == "explorer")
                {
                    windows.Enqueue(new KeyValuePair<IntPtr, Rectangle>(handle, rectangle));
                }
                else
                {
                    User32.EnumWindowsProc ewpWindows = new User32.EnumWindowsProc(EvalWindows);
                    User32.EnumWindows(ewpWindows, IntPtr.Zero);
                }

                foreach (KeyValuePair<IntPtr, Rectangle> window in windows)
                {
                    rectangle = rectangle.Merge(window.Value);
                    User32.EnumWindowsProc ewpControls = new User32.EnumWindowsProc(EvalControls);
                    User32.EnumChildWindows(window.Key, ewpControls, IntPtr.Zero);
                }

                foreach (KeyValuePair<IntPtr, Rectangle> control in controls)
                {
                    rectangle = rectangle.Merge(control.Value);
                }

                rectangle.Intersect(bounds);

                //TestRectangle(rectangle);

                return rectangle;
            }

            return Rectangle.Empty;
        }

        private bool EvalWindows(IntPtr hWnd, IntPtr lParam)
        {
            if (!User32.IsWindowVisible(hWnd)) return true;

            foreach (KeyValuePair<IntPtr, Rectangle> window in windows)
            {
                if (window.Key == hWnd)
                {
                    return true;
                }
            }

            uint processId;
            User32.GetWindowThreadProcessId(hWnd, out processId);

            if (this.processId == processId)
            {
                Rectangle rect = User32.GetWindowRectangle(hWnd);
                windows.Enqueue(new KeyValuePair<IntPtr, Rectangle>(hWnd, rect));
            }

            return true;
        }

        private bool EvalControls(IntPtr hWnd, IntPtr lParam)
        {
            if (!User32.IsWindowVisible(hWnd)) return true;

            foreach (KeyValuePair<IntPtr, Rectangle> control in controls)
            {
                if (control.Key == hWnd)
                {
                    return true;
                }
            }

            Rectangle rect = User32.GetWindowRectangle(hWnd);
            controls.Enqueue(new KeyValuePair<IntPtr, Rectangle>(hWnd, rect));

            return true;
        }

        private void TestRectangle(Rectangle rect)
        {
            using (Form form = new Form())
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.ShowInTaskbar = false;
                form.BackColor = Color.Red;
                form.Opacity = 0.50;
                User32.SetWindowPos(form.Handle, handle, rect.X, rect.Y, rect.Width, rect.Height, 0);
                form.Show();
                form.BringToFront();
                MessageBox.Show(rect.ToString());
            }
        }
    }
}