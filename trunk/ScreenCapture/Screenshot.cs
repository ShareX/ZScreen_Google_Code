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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using HelpersLib;

namespace ScreenCapture
{
    public static partial class Screenshot
    {
        public static bool RemoveOutsideScreenArea = true;
        public static bool DrawCursor = false;

        public static Image GetRectangle(Rectangle rect)
        {
            if (RemoveOutsideScreenArea)
            {
                Rectangle bounds = CaptureHelpers.GetScreenBounds();
                rect = Rectangle.Intersect(bounds, rect);
            }

            Image img = GetRectangleNative(rect);

            if (DrawCursor)
            {
                Point cursorOffset = CaptureHelpers.FixScreenCoordinates(rect.Location);
                DrawCursorToImage(img, cursorOffset);
            }

            return img;
        }

        public static Image GetFullscreen()
        {
            Rectangle bounds = CaptureHelpers.GetScreenBounds();

            return GetRectangle(bounds);
        }

        public static Image GetWindow(IntPtr handle)
        {
            if (handle.ToInt32() > 0)
            {
                Rectangle rect = CaptureHelpers.GetWindowRectangle(handle);

                return GetRectangle(rect);
            }

            return null;
        }

        public static Image GetActiveWindow()
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();

            return GetWindow(handle);
        }

        // Managed can't use SourceCopy | CaptureBlt because of .NET bug
        public static Image GetRectangleManaged(Rectangle rect)
        {
            Image img = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

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
            Image img = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                IntPtr hdcSrc = NativeMethods.GetWindowDC(handle);
                IntPtr hdcDest = g.GetHdc();
                NativeMethods.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSrc, rect.X, rect.Y, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
                NativeMethods.ReleaseDC(handle, hdcSrc);
                g.ReleaseHdc(hdcDest);
            }

            return img;
        }

        public static Image GetRectangleNative2(IntPtr handle, Rectangle rect)
        {
            IntPtr hdcSrc = NativeMethods.GetWindowDC(handle);
            IntPtr hdcDest = NativeMethods.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = NativeMethods.CreateCompatibleBitmap(hdcSrc, rect.Width, rect.Height);
            IntPtr hOld = NativeMethods.SelectObject(hdcDest, hBitmap);
            NativeMethods.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSrc, rect.Left, rect.Top, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
            NativeMethods.SelectObject(hdcDest, hOld);
            NativeMethods.DeleteDC(hdcDest);
            NativeMethods.ReleaseDC(handle, hdcSrc);
            Image img = Image.FromHbitmap(hBitmap);
            NativeMethods.DeleteObject(hBitmap);

            return img;
        }

        public static void DrawCursorToImage(Image img)
        {
            DrawCursorToImage(img, Point.Empty);
        }

        public static void DrawCursorToImage(Image img, Point offset)
        {
            using (MyCursor cursor = NativeMethods.CaptureCursor())
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