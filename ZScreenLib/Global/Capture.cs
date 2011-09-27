#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
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
using System.Windows.Forms;
using GraphicsMgrLib;
using ScreenCapture;
using ZScreenLib.Helpers;

namespace ZScreenLib
{
    public static class Capture
    {
        public static Image CaptureScreen(bool showCursor)
        {
            Image img = CaptureRectangle(GraphicsMgr.GetScreenBounds());
            if (showCursor)
            {
                DrawCursor(img);
            }
            return img;
        }

        public static Image CaptureWindow(IntPtr handle, bool showCursor)
        {
            return CaptureWindow(handle, showCursor, 0);
        }

        public static Image CaptureWindow(IntPtr handle, bool showCursor, int margin)
        {
            Rectangle windowRect = NativeMethods.GetWindowRectangle(handle);
            windowRect = NativeMethods.MaximizedWindowFix(handle, windowRect);
            windowRect.Inflate(margin, margin);
            Image img = CaptureRectangle(windowRect);
            if (showCursor) DrawCursor(img, windowRect.Location);
            return img;
        }

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

        public static Image CaptureActiveWindow(Workflow prof)
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();

            if (handle.ToInt32() > 0)
            {
                if (prof.ActiveWindowPreferDWM && Engine.HasAero)
                {
                    return CaptureWithDWM(prof, handle);
                }
                else
                {
                    return CaptureWithGDI(prof, handle);
                }
            }

            return null;
        }

        /// <summary>
        /// Captures a screenshot of a window using Windows GDI
        /// </summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        public static Image CaptureWithGDI(Workflow prof, IntPtr handle)
        {
            Engine.MyLogger.WriteLine("Capturing with GDI");
            Rectangle windowRect;

            if (prof.ActiveWindowTryCaptureChildren)
            {
                windowRect = new WindowRectangle(handle).CalculateWindowRectangle();
            }
            else
            {
                windowRect = NativeMethods.GetWindowRectangle(handle);
            }

            windowRect = NativeMethods.MaximizedWindowFix(handle, windowRect);

            Engine.MyLogger.WriteLine("Window rectangle: " + windowRect.ToString());

            Image windowImage = null;

            if (prof.ActiveWindowClearBackground)
            {
                windowImage = CaptureWindowWithTransparencyGDI(prof, handle, windowRect);
            }

            if (windowImage == null)
            {
                using (new Freeze(prof, handle))
                {
                    windowImage = CaptureRectangle(windowRect);
                }

                if (prof.ActiveWindowCleanTransparentCorners)
                {
                    Image result = RemoveCorners(handle, windowImage, null, windowRect);
                    if (result != null)
                    {
                        windowImage = result;
                    }
                }

                if (prof.ActiveWindowIncludeShadows)
                {
                    // Draw shadow manually to be able to have shadows in every case
                    windowImage = GraphicsMgr.AddBorderShadow((Bitmap)windowImage, true);
                }

                if (prof.ShowCursor)
                {
                    DrawCursor(windowImage, windowRect.Location);
                }
            }

            return windowImage;
        }

        /// <summary>
        /// Captures a screenshot of a window using Windows GDI. Captures transparency.
        /// </summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWindowWithTransparencyGDI(Workflow prof, IntPtr handle, Rectangle windowRect)
        {
            Image windowImage = null;
            Bitmap whiteBGImage = null, blackBGImage = null, white2BGImage = null;

            try
            {
                using (new Freeze(prof, handle))
                using (Form form = new Form())
                {
                    form.BackColor = Color.White;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.ShowInTaskbar = false;

                    int offset = prof.ActiveWindowIncludeShadows && !NativeMethods.IsWindowMaximized(handle) ? 20 : 0;

                    windowRect.Inflate(offset, offset);
                    windowRect.Intersect(GraphicsMgr.GetScreenBounds());

                    NativeMethods.ShowWindow(form.Handle, (int)NativeMethods.WindowShowStyle.ShowNormalNoActivate);
                    NativeMethods.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, NativeMethods.SWP_NOACTIVATE);
                    Application.DoEvents();
                    whiteBGImage = (Bitmap)CaptureRectangle(NativeMethods.GetDesktopWindow(), windowRect);

                    form.BackColor = Color.Black;
                    Application.DoEvents();
                    blackBGImage = (Bitmap)CaptureRectangle(NativeMethods.GetDesktopWindow(), windowRect);

                    if (!prof.ActiveWindowGDIFreezeWindow)
                    {
                        form.BackColor = Color.White;
                        Application.DoEvents();
                        white2BGImage = (Bitmap)CaptureRectangle(NativeMethods.GetDesktopWindow(), windowRect);
                    }
                }

                if (prof.ActiveWindowGDIFreezeWindow || whiteBGImage.AreBitmapsEqual(white2BGImage))
                {
                    windowImage = GraphicsMgr.ComputeOriginal(whiteBGImage, blackBGImage);
                }
                else
                {
                    windowImage = (Image)whiteBGImage.Clone();
                }
            }
            finally
            {
                if (whiteBGImage != null) whiteBGImage.Dispose();
                if (blackBGImage != null) blackBGImage.Dispose();
                if (white2BGImage != null) white2BGImage.Dispose();
            }

            if (windowImage != null)
            {
                Rectangle windowRectCropped = GraphicsMgr.GetCroppedArea((Bitmap)windowImage);
                windowImage = GraphicsMgr.CropImage(windowImage, windowRectCropped);

                if (prof.ShowCursor)
                {
                    windowRect.X += windowRectCropped.X;
                    windowRect.Y += windowRectCropped.Y;
                    DrawCursor(windowImage, windowRect.Location);
                }

                if (prof.ActiveWindowShowCheckers)
                {
                    windowImage = ImageEffects.DrawCheckers(windowImage);
                }
            }

            return windowImage;
        }

        /// <summary>
        /// Captures a screenshot of a window using the Windows DWM
        /// </summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        public static Image CaptureWithDWM(Workflow p, IntPtr handle)
        {
            Engine.MyLogger.WriteLine("Capturing with DWM");
            Image windowImage = null;
            Bitmap redBGImage = null;

            Rectangle windowRect = NativeMethods.GetWindowRectangle(handle);
            windowRect = NativeMethods.MaximizedWindowFix(handle, windowRect);

            if (Engine.HasAero && p.ActiveWindowClearBackground)
            {
                windowImage = CaptureWindowWithTransparencyDWM(handle, windowRect, out redBGImage, p.ActiveWindowCleanTransparentCorners);
            }

            if (windowImage == null)
            {
                Engine.MyLogger.WriteLine("Standard capture (no transparency)");
                windowImage = CaptureRectangle(windowRect);
            }

            if (p.ActiveWindowCleanTransparentCorners)
            {
                Image result = RemoveCorners(handle, windowImage, redBGImage, windowRect);
                if (result != null)
                {
                    windowImage = result;
                }
            }

            if (windowImage != null)
            {
                if (p.ActiveWindowIncludeShadows)
                {
                    // Draw shadow manually to be able to have shadows in every case
                    windowImage = GraphicsMgr.AddBorderShadow((Bitmap)windowImage, true);
                    Point shadowOffset = GraphicsMgr.ShadowOffset;
                    windowRect.X -= shadowOffset.X;
                    windowRect.Y -= shadowOffset.Y;
                }

                if (p.ActiveWindowShowCheckers)
                {
                    windowImage = ImageEffects.DrawCheckers(windowImage);
                }

                if (p.ShowCursor)
                {
                    DrawCursor(windowImage, windowRect.Location);
                }
            }

            return windowImage;
        }

        /// <summary>
        /// Remove the corners of a window by replacing the background of these corners by transparency.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="windowImage"></param>
        /// <param name="redBGImage"></param>
        /// <param name="windowRect"></param>
        /// <returns></returns>
        private static Image RemoveCorners(IntPtr handle, Image windowImage, Bitmap redBGImage, Rectangle windowRect)
        {
            const int cornerSize = 5;
            if (windowRect.Width > cornerSize * 2 && windowRect.Height > cornerSize * 2)
            {
                Engine.MyLogger.WriteLine("Clean transparent corners");

                if (redBGImage == null)
                {
                    using (Form form = new Form())
                    {
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.ShowInTaskbar = false;
                        form.BackColor = Color.Red;

                        NativeMethods.ShowWindow(form.Handle, (int)NativeMethods.WindowShowStyle.ShowNormalNoActivate);
                        NativeMethods.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, NativeMethods.SWP_NOACTIVATE);
                        Application.DoEvents();
                        redBGImage = CaptureRectangle(windowRect) as Bitmap;
                    }
                }

                return GraphicsMgr.RemoveCorners(windowImage, redBGImage);
            }
            return null;
        }

        /// <summary>
        /// Make a full-size thumbnail of the captured window on a new topmost form, and capture
        /// this new form with a black and then white background. Then compute the transparency by
        /// difference between the black and white versions.
        /// This method has these advantages:
        /// - the full form is captured even if it is obscured on the Windows desktop
        /// - there is no problem with unpredictable Z-order anymore (the background and
        ///   the window to capture are drawn on the same window)
        /// Note: now that GDI capture is more robust, DWM capture is not that useful anymore.
        /// </summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <param name="windowRect">the bounds of the window</param>
        /// <param name="redBGImage">the window captured with a red background</param>
        /// <param name="captureRedBGImage">whether to do the capture of the window with a red background</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWindowWithTransparencyDWM(IntPtr handle, Rectangle windowRect, out Bitmap redBGImage, bool captureRedBGImage)
        {
            Image windowImage = null;
            redBGImage = null;

            using (Form form = new Form())
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.ShowInTaskbar = false;
                form.BackColor = Color.White;
                form.TopMost = true;
                form.Bounds = windowRect;

                IntPtr thumb;
                NativeMethods.DwmRegisterThumbnail(form.Handle, handle, out thumb);
                SIZE size;
                NativeMethods.DwmQueryThumbnailSourceSize(thumb, out size);

                if (size.x <= 0 || size.y <= 0)
                {
                    return null;
                }

                form.Location = new Point(windowRect.X, windowRect.Y);
                form.Size = new Size(size.x, size.y);

                form.Show();

                NativeMethods.DWM_THUMBNAIL_PROPERTIES props = new NativeMethods.DWM_THUMBNAIL_PROPERTIES();
                props.dwFlags = NativeMethods.DWM_TNP_VISIBLE | NativeMethods.DWM_TNP_RECTDESTINATION | NativeMethods.DWM_TNP_OPACITY;
                props.fVisible = true;
                props.opacity = (byte)255;

                props.rcDestination = new RECT(0, 0, size.x, size.y);
                NativeMethods.DwmUpdateThumbnailProperties(thumb, ref props);

                NativeMethods.ActivateWindowRepeat(handle, 250);
                Bitmap whiteBGImage = CaptureRectangle(windowRect) as Bitmap;

                form.BackColor = Color.Black;
                form.Refresh();
                NativeMethods.ActivateWindowRepeat(handle, 250);
                Bitmap blackBGImage = CaptureRectangle(windowRect) as Bitmap;

                if (captureRedBGImage)
                {
                    form.BackColor = Color.Red;
                    form.Refresh();
                    NativeMethods.ActivateWindowRepeat(handle, 250);
                    redBGImage = CaptureRectangle(windowRect) as Bitmap;
                }

                form.BackColor = Color.White;
                form.Refresh();
                NativeMethods.ActivateWindowRepeat(handle, 250);
                Bitmap whiteBGImage2 = CaptureRectangle(windowRect) as Bitmap;

                // Don't do transparency calculation if an animated picture is detected
                if (whiteBGImage.AreBitmapsEqual(whiteBGImage2))
                {
                    windowImage = GraphicsMgr.ComputeOriginal(whiteBGImage, blackBGImage);
                }
                else
                {
                    Engine.MyLogger.WriteLine("Detected animated image => cannot compute transparency");
                    form.Close();
                    Application.DoEvents();
                    Image result = new Bitmap(whiteBGImage.Width, whiteBGImage.Height, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        // Redraw the image on a black background to avoid transparent pixels artifacts
                        g.Clear(Color.Black);
                        g.DrawImage(whiteBGImage, 0, 0);
                    }
                    windowImage = result;
                }

                NativeMethods.DwmUnregisterThumbnail(thumb);
                blackBGImage.Dispose();
                whiteBGImage.Dispose();
                whiteBGImage2.Dispose();
            }

            return windowImage;
        }

        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            RECT rc;
            NativeMethods.GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            bool succeeded = NativeMethods.PrintWindow(hwnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            if (!succeeded)
            {
                gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Point.Empty, bmp.Size));
            }
            IntPtr hRgn = GDI.CreateRectRgn(0, 0, 0, 0);
            NativeMethods.GetWindowRgn(hwnd, hRgn);
            Region region = Region.FromHrgn(hRgn);
            if (!region.IsEmpty(gfxBmp))
            {
                gfxBmp.ExcludeClip(region);
                gfxBmp.Clear(Color.Transparent);
            }
            gfxBmp.Dispose();
            return bmp;

            /*
            RECT rect;
            GetWindowRect(handle, out rect);

            IntPtr hDC = GetDC(handle);
            IntPtr hDCMem = GDI.CreateCompatibleDC(hDC);
            IntPtr hBitmap = GDI.CreateCompatibleBitmap(hDC, rect.Width, rect.Height);

            IntPtr hOld = GDI.SelectObject(hDCMem, hBitmap);

            SendMessage(handle, (uint)WM.PRINT, hDCMem, (IntPtr)(PRF.CHILDREN | PRF.CLIENT | PRF.ERASEBKGND | PRF.NONCLIENT | PRF.OWNED));
            GDI.SelectObject(hDCMem, hOld);

            Bitmap bmp = Bitmap.FromHbitmap(hBitmap);

            GDI.DeleteDC(hDCMem);
            ReleaseDC(handle, hDC);

            return bmp;*/
        }

        private static void DrawCursor(Image img)
        {
            DrawCursor(img, Point.Empty);
        }

        private static void DrawCursor(Image img, Point offset)
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

        #region Clean window corners

        /// <summary>
        /// Paints 5 pixel wide red corners behind the form from which the event originates.
        /// </summary>
        /*private static void FormPaintRedCorners(object sender, PaintEventArgs e)
        {
            const int cornerSize = 5;
            Form form = sender as Form;
            if (form != null)
            {
                int width = form.Width;
                int height = form.Height;

                e.Graphics.FillRectangle(Brushes.Red, 0, 0, cornerSize, cornerSize); // top left
                e.Graphics.FillRectangle(Brushes.Red, width - 5, 0, cornerSize, cornerSize); // top right
                e.Graphics.FillRectangle(Brushes.Red, 0, height - 5, cornerSize, cornerSize); // bottom left
                e.Graphics.FillRectangle(Brushes.Red, width - 5, height - 5, cornerSize, cornerSize); // bottom right
            }
        }*/

        #endregion Clean window corners
    }
}