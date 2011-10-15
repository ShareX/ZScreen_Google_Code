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
using HelpersLib;
using ScreenCapture;
using ZScreenLib.Helpers;

namespace ZScreenLib
{
    public static class Capture
    {
        public static Image CaptureActiveWindow(Workflow wf)
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();

            if (handle.ToInt32() > 0)
            {
                if (wf.CaptureEngineMode == CaptureEngineType.DWM && NativeMethods.IsDWMEnabled())
                {
                    return CaptureWithDWM(wf, handle);
                }
                else
                {
                    return CaptureWithGDI(wf, handle);
                }
            }

            return null;
        }

        /// <summary>Captures a screenshot of a window using the Windows DWM</summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWithDWM(Workflow wfdwm, IntPtr handle)
        {
            StaticHelper.WriteLine("Capturing with DWM");
            Image windowImage = null;
            Bitmap redBGImage = null;

            Rectangle windowRect = NativeMethods.GetWindowRectangle(handle);
            windowRect = NativeMethods.MaximizedWindowFix(handle, windowRect);

            if (Engine.HasAero && wfdwm.ActiveWindowClearBackground)
            {
                windowImage = CaptureWindowWithTransparencyDWM(handle, windowRect, out redBGImage, wfdwm.ActiveWindowCleanTransparentCorners);
            }

            if (windowImage == null)
            {
                StaticHelper.WriteLine("Standard capture (no transparency)");
                windowImage = Screenshot.GetRectangleNative(windowRect);
            }

            if (wfdwm.ActiveWindowCleanTransparentCorners)
            {
                Image result = RemoveCorners(handle, windowImage, redBGImage, windowRect);
                if (result != null)
                {
                    windowImage = result;
                }
            }

            if (windowImage != null)
            {
                if (wfdwm.ActiveWindowIncludeShadows)
                {
                    // Draw shadow manually to be able to have shadows in every case
                    windowImage = GraphicsMgr.AddBorderShadow((Bitmap)windowImage, true);
                    Point shadowOffset = GraphicsMgr.ShadowOffset;
                    windowRect.X -= shadowOffset.X;
                    windowRect.Y -= shadowOffset.Y;
                }

                if (wfdwm.ActiveWindowShowCheckers)
                {
                    windowImage = ImageEffects.DrawCheckers(windowImage);
                }

                if (wfdwm.DrawCursor)
                {
                    Screenshot.DrawCursorToImage(windowImage, windowRect.Location);
                }
            }

            return windowImage;
        }

        /// <summary>Captures a screenshot of a window using Windows GDI</summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWithGDI(Workflow wfgdi, IntPtr handle)
        {
            StaticHelper.WriteLine("Capturing with GDI");
            Rectangle windowRect;

            if (wfgdi.ActiveWindowTryCaptureChildren)
            {
                windowRect = new WindowRectangle(handle).CalculateWindowRectangle();
            }
            else
            {
                windowRect = NativeMethods.GetWindowRectangle(handle);
            }

            windowRect = NativeMethods.MaximizedWindowFix(handle, windowRect);

            StaticHelper.WriteLine("Window rectangle: " + windowRect.ToString());

            Image windowImage = null;

            if (wfgdi.ActiveWindowClearBackground)
            {
                windowImage = CaptureWindowWithTransparencyGDI(wfgdi, handle, windowRect);
            }

            if (windowImage == null)
            {
                using (new Freeze(wfgdi, handle))
                {
                    windowImage = Screenshot.GetRectangleNative(windowRect);
                }

                if (wfgdi.ActiveWindowCleanTransparentCorners)
                {
                    Image result = RemoveCorners(handle, windowImage, null, windowRect);
                    if (result != null)
                    {
                        windowImage = result;
                    }
                }

                if (wfgdi.ActiveWindowIncludeShadows)
                {
                    // Draw shadow manually to be able to have shadows in every case
                    windowImage = GraphicsMgr.AddBorderShadow((Bitmap)windowImage, true);
                }

                if (wfgdi.DrawCursor)
                {
                    Screenshot.DrawCursorToImage(windowImage, windowRect.Location);
                }
            }

            return windowImage;
        }

        /// <summary>Captures a screenshot of a window using Windows GDI. Captures transparency.</summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWindowWithTransparencyGDI(Workflow wfgdi, IntPtr handle, Rectangle windowRect)
        {
            Image windowImage = null;
            Bitmap whiteBGImage = null, blackBGImage = null, white2BGImage = null;

            try
            {
                using (new Freeze(wfgdi, handle))
                using (Form form = new Form())
                {
                    form.BackColor = Color.White;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.ShowInTaskbar = false;

                    int offset = wfgdi.ActiveWindowIncludeShadows && !NativeMethods.IsWindowMaximized(handle) ? 20 : 0;

                    windowRect.Inflate(offset, offset);
                    windowRect.Intersect(CaptureHelpers.GetScreenBounds());

                    NativeMethods.ShowWindow(form.Handle, (int)WindowShowStyle.ShowNormalNoActivate);
                    NativeMethods.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, NativeMethods.SWP_NOACTIVATE);
                    Application.DoEvents();
                    whiteBGImage = (Bitmap)Screenshot.GetRectangleNative(NativeMethods.GetDesktopWindow(), windowRect);

                    form.BackColor = Color.Black;
                    Application.DoEvents();
                    blackBGImage = (Bitmap)Screenshot.GetRectangleNative(NativeMethods.GetDesktopWindow(), windowRect);

                    if (!wfgdi.ActiveWindowGDIFreezeWindow)
                    {
                        form.BackColor = Color.White;
                        Application.DoEvents();
                        white2BGImage = (Bitmap)Screenshot.GetRectangleNative(NativeMethods.GetDesktopWindow(), windowRect);
                    }
                }

                if (wfgdi.ActiveWindowGDIFreezeWindow || whiteBGImage.AreBitmapsEqual(white2BGImage))
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
                windowImage = CaptureHelpers.CropImage(windowImage, windowRectCropped);

                if (wfgdi.DrawCursor)
                {
                    windowRect.X += windowRectCropped.X;
                    windowRect.Y += windowRectCropped.Y;
                    Screenshot.DrawCursorToImage(windowImage, windowRect.Location);
                }

                if (wfgdi.ActiveWindowShowCheckers)
                {
                    windowImage = ImageEffects.DrawCheckers(windowImage);
                }
            }

            return windowImage;
        }

        /// <summary>Remove the corners of a window by replacing the background of these corners by transparency.</summary>
        private static Image RemoveCorners(IntPtr handle, Image windowImage, Bitmap redBGImage, Rectangle windowRect)
        {
            const int cornerSize = 5;
            if (windowRect.Width > cornerSize * 2 && windowRect.Height > cornerSize * 2)
            {
                StaticHelper.WriteLine("Clean transparent corners");

                if (redBGImage == null)
                {
                    using (Form form = new Form())
                    {
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.ShowInTaskbar = false;
                        form.BackColor = Color.Red;

                        NativeMethods.ShowWindow(form.Handle, (int)WindowShowStyle.ShowNormalNoActivate);
                        NativeMethods.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, NativeMethods.SWP_NOACTIVATE);
                        Application.DoEvents();
                        redBGImage = Screenshot.GetRectangleNative(windowRect) as Bitmap;
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

                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();
                props.dwFlags = NativeMethods.DWM_TNP_VISIBLE | NativeMethods.DWM_TNP_RECTDESTINATION | NativeMethods.DWM_TNP_OPACITY;
                props.fVisible = true;
                props.opacity = (byte)255;

                props.rcDestination = new RECT(0, 0, size.x, size.y);
                NativeMethods.DwmUpdateThumbnailProperties(thumb, ref props);

                NativeMethods.ActivateWindowRepeat(handle, 250);
                Bitmap whiteBGImage = Screenshot.GetRectangleNative(windowRect) as Bitmap;

                form.BackColor = Color.Black;
                form.Refresh();
                NativeMethods.ActivateWindowRepeat(handle, 250);
                Bitmap blackBGImage = Screenshot.GetRectangleNative(windowRect) as Bitmap;

                if (captureRedBGImage)
                {
                    form.BackColor = Color.Red;
                    form.Refresh();
                    NativeMethods.ActivateWindowRepeat(handle, 250);
                    redBGImage = Screenshot.GetRectangleNative(windowRect) as Bitmap;
                }

                form.BackColor = Color.White;
                form.Refresh();
                NativeMethods.ActivateWindowRepeat(handle, 250);
                Bitmap whiteBGImage2 = Screenshot.GetRectangleNative(windowRect) as Bitmap;

                // Don't do transparency calculation if an animated picture is detected
                if (whiteBGImage.AreBitmapsEqual(whiteBGImage2))
                {
                    windowImage = GraphicsMgr.ComputeOriginal(whiteBGImage, blackBGImage);
                }
                else
                {
                    StaticHelper.WriteLine("Detected animated image => cannot compute transparency");
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

        private static Bitmap PrintWindow(IntPtr hwnd)
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
            IntPtr hRgn = NativeMethods.CreateRectRgn(0, 0, 0, 0);
            NativeMethods.GetWindowRgn(hwnd, hRgn);
            Region region = Region.FromHrgn(hRgn);
            if (!region.IsEmpty(gfxBmp))
            {
                gfxBmp.ExcludeClip(region);
                gfxBmp.Clear(Color.Transparent);
            }
            gfxBmp.Dispose();
            return bmp;

            /*RECT rect;
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
    }
}