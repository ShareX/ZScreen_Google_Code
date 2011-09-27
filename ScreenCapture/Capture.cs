using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace ScreenCapture
{
    public static class Capture
    {
        public static Image CaptureRectangle(Rectangle rect)
        {
            return CaptureRectangle(NativeMethods.GetDesktopWindow(), rect);
        }

        public static Image CaptureRectangle(IntPtr handle, Rectangle rect)
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

        private static void DrawCursor(Image img)
        {
            // DrawCursor(img, Point.Empty);
        }

        /*
        private static void DrawCursor(Image img, Point offset)
        {
            using (ScreenCapture.MyCursor cursor = NativeMethods.CaptureCursor())
            {
                cursor.Position.Offset(-offset.X, -offset.Y);
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(cursor.Bitmap, cursor.Position);
                }
            }
        }
        */
    }
}