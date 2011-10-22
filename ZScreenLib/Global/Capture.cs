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
        /// <summary>
        /// Captures Active Window and draws cursor if the option is set in WorkflowConfig
        /// </summary>
        /// <param name="WorkflowConfig">Optios for Active Window</param>
        /// <returns></returns>
        public static Image CaptureWithGDI2(Workflow WorkflowConfig)
        {
            StaticHelper.WriteLine("Capturing with GDI");
            Image tempImage = null;

            Screenshot.DrawCursor = WorkflowConfig.DrawCursor;
            if (WorkflowConfig.ActiveWindowClearBackground)
            {
                tempImage = Screenshot.CaptureActiveWindowTransparent();
            }
            else
            {
                tempImage = Screenshot.CaptureActiveWindow();
            }

            return tempImage;
        }

        /// <summary>Captures a screenshot of a window using the Windows DWM</summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image with or without cursor</returns>
        public static Image CaptureWithDWM(Workflow wfdwm)
        {
            StaticHelper.WriteLine("Capturing with DWM");
            IntPtr handle = NativeMethods.GetForegroundWindow();
            Image windowImageDwm = null;
            Bitmap redBGImage = null;

            Rectangle windowRect = CaptureHelpers.GetWindowRectangle(handle);

            if (Engine.HasAero)
            {
                if (wfdwm.ActiveWindowClearBackground)
                {
                    if (wfdwm.ActiveWindowDwmUseCustomBackground)
                    {
                        windowImageDwm = CaptureWindowWithDWM(handle, windowRect, out redBGImage,
                            wfdwm.ActiveWindowCleanTransparentCorners, wfdwm.ActiveWindowDwmBackColor);
                    }
                    else
                    {
                        windowImageDwm = CaptureWindowWithDWM(handle, windowRect, out redBGImage,
                            wfdwm.ActiveWindowCleanTransparentCorners, Color.White);
                    }
                }
            }

            if (windowImageDwm == null)
            {
                StaticHelper.WriteLine("Standard capture (no transparency)");
                windowImageDwm = Screenshot.CaptureRectangleNative(windowRect);
            }

            if (wfdwm.ActiveWindowCleanTransparentCorners)
            {
                Image result = RemoveCorners(handle, windowImageDwm, redBGImage, windowRect);
                if (result != null)
                {
                    windowImageDwm = result;
                }
            }

            if (wfdwm.ActiveWindowIncludeShadows)
            {
                // Draw shadow manually to be able to have shadows in every case
                windowImageDwm = GraphicsMgr.AddBorderShadow((Bitmap)windowImageDwm, true);

                if (wfdwm.DrawCursor)
                {
                    Point shadowOffset = GraphicsMgr.ShadowOffset;
#if DEBUG
                    StaticHelper.WriteLine("Fixed cursor position (before): " + windowRect.ToString());
#endif
                    windowRect.X -= shadowOffset.X;
                    windowRect.Y -= shadowOffset.Y;
#if DEBUG
                    StaticHelper.WriteLine("Fixed cursor position (after):  " + windowRect.ToString());
#endif
                }
            }

            if (wfdwm.DrawCursor)
            {
                CaptureHelpers.DrawCursorToImage(windowImageDwm, windowRect.Location);
            }

            return windowImageDwm;
        }

        /// <summary>
        /// Make a full-size thumbnail of the captured window on a new topmost form, and capture
        /// this new form with a black and then white background. Then compute the transparency by
        /// difference between the black and white versions.
        /// This method has these advantages:
        /// - the full form is captured even if it is obscured on the Windows desktop
        /// - there is no problem with unpredictable Z-order anymore (the background and
        ///   the window to capture are drawn on the same window)
        /// </summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <param name="windowRect">the bounds of the window</param>
        /// <param name="redBGImage">the window captured with a red background</param>
        /// <param name="captureRedBGImage">whether to do the capture of the window with a red background</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWindowWithDWM(IntPtr handle, Rectangle windowRect, out Bitmap redBGImage, bool captureRedBGImage, Color backColor)
        {
            Image windowImage = null;
            redBGImage = null;

            if (backColor != Color.White)
            {
                backColor = Color.FromArgb(255, backColor.R, backColor.G, backColor.B);
            }

            using (Form form = new Form())
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.ShowInTaskbar = false;
                form.BackColor = backColor;
                form.TopMost = true;
                form.Bounds = windowRect;

                IntPtr thumb;
                NativeMethods.DwmRegisterThumbnail(form.Handle, handle, out thumb);

                SIZE size;
                NativeMethods.DwmQueryThumbnailSourceSize(thumb, out size);

#if DEBUG
                StaticHelper.WriteLine("Rectangle Size: " + windowRect.ToString());
                StaticHelper.WriteLine("Window    Size: " + size.ToString());
#endif
                if (size.x <= 0 || size.y <= 0)
                {
                    return null;
                }

                form.Location = new Point(windowRect.X, windowRect.Y);
                form.Size = new Size(size.x, size.y);

                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();
                props.dwFlags = NativeMethods.DWM_TNP_VISIBLE | NativeMethods.DWM_TNP_RECTDESTINATION | NativeMethods.DWM_TNP_OPACITY;
                props.fVisible = true;
                props.opacity = (byte)255;
                props.rcDestination = new RECT(0, 0, size.x, size.y);

                NativeMethods.DwmUpdateThumbnailProperties(thumb, ref props);

                form.Show();
                System.Threading.Thread.Sleep(250);

                if (form.BackColor != Color.White)
                {
                    // no need for transparency; user has requested custom background color
                    NativeMethods.ActivateWindowRepeat(form.Handle, 250);
                    windowImage = Screenshot.CaptureRectangleNative(windowRect) as Bitmap;
                }
                else if (form.BackColor == Color.White)
                {
                    // transparent capture
                    NativeMethods.ActivateWindowRepeat(handle, 250);
                    Bitmap whiteBGImage = Screenshot.CaptureRectangleNative(windowRect) as Bitmap;

                    form.BackColor = Color.Black;
                    form.Refresh();
                    NativeMethods.ActivateWindowRepeat(handle, 250);
                    Bitmap blackBGImage = Screenshot.CaptureRectangleNative(windowRect) as Bitmap;

                    if (captureRedBGImage)
                    {
                        form.BackColor = Color.Red;
                        form.Refresh();
                        NativeMethods.ActivateWindowRepeat(handle, 250);
                        redBGImage = Screenshot.CaptureRectangleNative(windowRect) as Bitmap;
                    }

                    form.BackColor = Color.White;
                    form.Refresh();
                    NativeMethods.ActivateWindowRepeat(handle, 250);
                    Bitmap whiteBGImage2 = Screenshot.CaptureRectangleNative(windowRect) as Bitmap;

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

                    blackBGImage.Dispose();
                    whiteBGImage.Dispose();
                    whiteBGImage2.Dispose();
                }

                NativeMethods.DwmUnregisterThumbnail(thumb);
            }

            return windowImage;
        }

        /// <summary>Captures a screenshot of a window using Windows GDI</summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWithGDI(Workflow wfgdi, IntPtr handle, out Rectangle windowRect)
        {
            StaticHelper.WriteLine("Capturing with GDI");

            windowRect = CaptureHelpers.GetWindowRectangle(handle);

            Image windowImageGdi = null;

            if (wfgdi.ActiveWindowClearBackground)
            {
                windowImageGdi = CaptureWindowWithGDI(wfgdi, handle, out windowRect);
            }

            if (windowImageGdi == null)
            {
                using (new Freeze(wfgdi, handle))
                {
                    windowImageGdi = Screenshot.CaptureRectangleNative(windowRect);
                }

                if (wfgdi.ActiveWindowCleanTransparentCorners)
                {
                    Image result = RemoveCorners(handle, windowImageGdi, null, windowRect);
                    if (result != null)
                    {
                        windowImageGdi = result;
                    }
                }
            }

            return windowImageGdi;
        }

        /// <summary>Captures a screenshot of a window using Windows GDI. Captures transparency.</summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWindowWithGDI(Workflow wfgdi, IntPtr handle, out Rectangle windowRect)
        {
            Image windowImageGdi = null;
            Bitmap whiteBGImage = null, blackBGImage = null, white2BGImage = null;

            if (wfgdi.ActiveWindowTryCaptureChildren)
            {
                windowRect = new WindowRectangle(handle).CalculateWindowRectangle();
            }
            else
            {
                windowRect = CaptureHelpers.GetWindowRectangle(handle);
            }

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

                    whiteBGImage = (Bitmap)Screenshot.CaptureRectangleNative2(windowRect);

                    form.BackColor = Color.Black;
                    Application.DoEvents();

                    blackBGImage = (Bitmap)Screenshot.CaptureRectangleNative2(windowRect);

                    if (!wfgdi.ActiveWindowGDIFreezeWindow)
                    {
                        form.BackColor = Color.White;
                        Application.DoEvents();

                        white2BGImage = (Bitmap)Screenshot.CaptureRectangleNative2(windowRect);
                    }
                }

                if (wfgdi.ActiveWindowGDIFreezeWindow || whiteBGImage.AreBitmapsEqual(white2BGImage))
                {
                    windowImageGdi = GraphicsMgr.ComputeOriginal(whiteBGImage, blackBGImage);
                }
                else
                {
                    windowImageGdi = (Image)whiteBGImage.Clone();
                }
            }
            finally
            {
                if (whiteBGImage != null) whiteBGImage.Dispose();
                if (blackBGImage != null) blackBGImage.Dispose();
                if (white2BGImage != null) white2BGImage.Dispose();
            }

            if (windowImageGdi != null)
            {
                Rectangle windowRectCropped = GraphicsMgr.GetCroppedArea((Bitmap)windowImageGdi);
                windowImageGdi = CaptureHelpers.CropImage(windowImageGdi, windowRectCropped);

                if (wfgdi.DrawCursor)
                {
#if DEBUG
                    StaticHelper.WriteLine("Fixed cursor position (before): " + windowRect.ToString());
#endif
                    windowRect.X += windowRectCropped.X;
                    windowRect.Y += windowRectCropped.Y;
#if DEBUG
                    StaticHelper.WriteLine("Fixed cursor position (after):  " + windowRect.ToString());
#endif
                }
            }

            return windowImageGdi;
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
                        redBGImage = Screenshot.CaptureRectangleNative(windowRect) as Bitmap;
                    }
                }

                return GraphicsMgr.RemoveCorners(windowImage, redBGImage);
            }
            return null;
        }
    }
}