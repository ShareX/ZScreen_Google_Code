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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace ScreenCapture
{
    public static class Screenshot
    {
        public static bool AutoCrop = true;
        public static bool DrawCursor = false;

        public static Image GetRectangle(Rectangle rect)
        {
            if (AutoCrop)
            {
                Rectangle bounds = CaptureHelpers.GetScreenBounds();
                rect = Rectangle.Intersect(bounds, rect);
            }

            Image img = GetRectangleNative(rect);

            if (DrawCursor)
            {
                DrawCursorToImage(img);
            }

            return img;
        }

        public static Image GetFullscreen()
        {
            Rectangle bounds = CaptureHelpers.GetScreenBounds();

            return GetRectangle(bounds);
        }

        public static Image GetActiveWindow()
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();

            if (handle.ToInt32() > 0)
            {
                Rectangle rect = CaptureHelpers.GetWindowRectangle(handle);

                return GetRectangle(rect);
            }

            return null;
        }

        // Managed can't use SourceCopy | CaptureBlt because of .NET bug
        public static Image GetRectangleManaged(Rectangle rect)
        {
            Image img = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppRgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);
            }

            return img;
        }

        public static Image GetRectangleNative(Rectangle rect)
        {
            return GetRectangleNative(NativeMethods.GetDesktopWindow(), rect);
        }

        public static Image GetRectangleNative(IntPtr handle, Rectangle rect)
        {
            Image img = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(img))
            {
                IntPtr hdcDest = g.GetHdc();
                IntPtr hdcSrc = NativeMethods.GetWindowDC(handle);
                GDI.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSrc, rect.X, rect.Y, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
                g.ReleaseHdc(hdcDest);
                NativeMethods.ReleaseDC(handle, hdcSrc);
            }

            return img;
        }

        public static Image GetRectangleNative2(IntPtr handle, Rectangle rect)
        {
            // Get the hDC of the target window
            IntPtr hdcSrc = NativeMethods.GetWindowDC(handle);
            // Create a device context we can copy to
            IntPtr hdcDest = GDI.CreateCompatibleDC(hdcSrc);
            // Create a bitmap we can copy it to
            IntPtr hBitmap = GDI.CreateCompatibleBitmap(hdcSrc, rect.Width, rect.Height);
            // Select the bitmap object
            IntPtr hOld = GDI.SelectObject(hdcDest, hBitmap);
            // BitBlt over
            GDI.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSrc, rect.Left, rect.Top, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
            // Restore selection
            GDI.SelectObject(hdcDest, hOld);
            // Clean up
            GDI.DeleteDC(hdcDest);
            NativeMethods.ReleaseDC(handle, hdcSrc);
            // Get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // Free up the Bitmap object
            GDI.DeleteObject(hBitmap);

            return img;
        }

        private static void DrawCursorToImage(Image img)
        {
            DrawCursorToImage(img, Point.Empty);
        }

        private static void DrawCursorToImage(Image img, Point offset)
        {
            using (NativeMethods.MyCursor cursor = NativeMethods.CaptureCursor())
            {
                cursor.Position.Offset(-offset.X, -offset.Y);

                using (Graphics g = Graphics.FromImage(img))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(cursor.Bitmap, cursor.Position);
                }
            }
        }
    }
}