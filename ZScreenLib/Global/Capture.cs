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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZScreenLib.Helpers;
using System.Threading;

namespace ZScreenLib
{
    public static class Capture
    {
        public static Image CaptureScreen(bool showCursor)
        {
            Image img = CaptureRectangle(User32.GetDesktopWindow(), GraphicsMgr.GetScreenBounds());
            if (showCursor) DrawCursor(img);
            return img;
        }

        public static Image CaptureWindow(IntPtr handle, bool showCursor)
        {
            return CaptureWindow(handle, showCursor, 0);
        }

        public static Image CaptureWindow(IntPtr handle, bool showCursor, int margin)
        {
            Rectangle windowRect = User32.GetWindowRectangle(handle);
            windowRect = windowRect.AddMargin(margin);

            /*
            Image img = new Bitmap(windowRect.Width, windowRect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(windowRect.Location, new Point(0, 0), windowRect.Size, CopyPixelOperation.SourceCopy);
            */

            Image img = CaptureRectangle(User32.GetDesktopWindow(), windowRect);
            if (showCursor) DrawCursor(img, windowRect.Location);

            /*
            img = PrintWindow(handle);
            img = MakeBackgroundTransparent(handle, img);
            */

            return img;
        }

        public static Image CaptureRectangle(IntPtr handle, Rectangle rect)
        {
            // Get the hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
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
            User32.ReleaseDC(handle, hdcSrc);
            // Get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // Free up the Bitmap object
            GDI.DeleteObject(hBitmap);
            return img;
        }

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public static Image CaptureActiveWindow()
        {
            IntPtr handle = User32.GetForegroundWindow();

            if (handle.ToInt32() > 0)
            {
                if (Engine.conf.ActiveWindowPreferDWM && Engine.HasAero)
                {
                    return CaptureWithDWM(handle);
                }
                else
                {
                    return CaptureWithGDI(handle);
                }
            }

            return null;
        }

        public static Image CaptureWithDWM(IntPtr handle)
        {
            FileSystem.AppendDebug("Capturing with DWM");
            Image windowImage = null;
            Bitmap redBGImage = null;

            Rectangle windowRect = User32.GetWindowRectangle(handle);

            if (Engine.HasAero && Engine.conf.ActiveWindowClearBackground)
            {
                windowImage = CaptureWindowWithTransparency(handle, windowRect, out redBGImage, Engine.conf.ActiveWindowCleanTransparentCorners);
            }

            if (windowImage == null)
            {
                Console.WriteLine("Standard capture (no transparency)");
                windowImage = CaptureRectangle(User32.GetDesktopWindow(), windowRect);
                if (Engine.conf.ShowCursor) DrawCursor(windowImage, windowRect.Location);
            }

            const int cornerSize = 5;

            if (Engine.conf.ActiveWindowCleanTransparentCorners && windowRect.Width > cornerSize * 2 && windowRect.Height > cornerSize * 2)
            {
                Console.WriteLine("Clean transparent corners");

                if (redBGImage == null)
                {
                    using (Form form = new Form())
                    {
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.ShowInTaskbar = false;
                        form.BackColor = Color.Red;
                        User32.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, 0);
                        form.Show();
                        User32.ActivateWindowRepeat(handle, 250);

                        redBGImage = CaptureWindow(handle, false) as Bitmap;
                    }
                }

                Image result = new Bitmap(windowImage.Width, windowImage.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.Clear(Color.Transparent);
                    // Remove the transparent pixels in the four corners
                    RemoveCorner(redBGImage, g, 0, 0, cornerSize, Corner.TopLeft);
                    RemoveCorner(redBGImage, g, windowImage.Width - cornerSize, 0, windowImage.Width, Corner.TopRight);
                    RemoveCorner(redBGImage, g, 0, windowImage.Height - cornerSize, cornerSize, Corner.BottomLeft);
                    RemoveCorner(redBGImage, g, windowImage.Width - cornerSize, windowImage.Height - cornerSize, windowImage.Width, Corner.BottomRight);
                    g.DrawImage(windowImage, 0, 0);
                }
                windowImage = result;
            }

            if (Engine.conf.ActiveWindowIncludeShadows)
            {
                // Draw shadow manually to be able to have shadows in every case
                windowImage = GraphicsMgr.AddBorderShadow((Bitmap)windowImage);
            }

            if (Engine.conf.ActiveWindowShowCheckers)
            {
                windowImage = ImageEffects.DrawCheckers(windowImage);
            }

            return windowImage;
        }

        public static Image CaptureWithGDI(IntPtr handle)
        {
            FileSystem.AppendDebug("Capturing with GDI");
            Rectangle windowRect = new WindowRectangle(handle).CalculateWindowRectangle();
            Image windowImage = null;

            if (Engine.conf.ActiveWindowClearBackground)
            {
                windowImage = CaptureWindowWithTransparencyGDI(handle, windowRect);
            }

            if (windowImage == null)
            {
                windowImage = CaptureRectangle(User32.GetDesktopWindow(), windowRect);

                if (Engine.conf.ShowCursor)
                {
                    DrawCursor(windowImage, windowRect.Location);
                }
            }

            return windowImage;
        }

        /// <summary>
        /// Make a full-size thumbnail of the captured window on a new topmost form, and capture 
        /// this new form with a black and then white background. Then compute the transparency by
        /// difference between the black and white versions.
        /// This method has several advantages: 
        /// - the full form is captured even if it is obscured on the Windows desktop
        /// - there is no problem with unpredictable Z-order anymore (the background and 
        ///   the window to capture are drawn on the same window)
        /// - it is possible to determine whether the form is animated and avoid a corrupted image
        /// </summary>
        /// <param name="handle">handle of the window to capture</param>
        /// <param name="windowRect">the bounds of the window</param>
        /// <param name="redBGImage">the window captured with a red background</param>
        /// <param name="captureRedBGImage">whether to do the capture of the window with a red background</param>
        /// <returns>the captured window image</returns>
        private static Image CaptureWindowWithTransparency(IntPtr handle, Rectangle windowRect, out Bitmap redBGImage, bool captureRedBGImage)
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
                User32.DwmRegisterThumbnail(form.Handle, handle, out thumb);
                PSIZE size;
                User32.DwmQueryThumbnailSourceSize(thumb, out size);

                if (size.x <= 0 || size.y <= 0)
                {
                    return null;
                }

                form.Location = new Point(windowRect.X, windowRect.Y);
                form.Size = new Size(size.x, size.y);

                form.Show();

                User32.DWM_THUMBNAIL_PROPERTIES props = new User32.DWM_THUMBNAIL_PROPERTIES();
                props.dwFlags = User32.DWM_TNP_VISIBLE | User32.DWM_TNP_RECTDESTINATION | User32.DWM_TNP_OPACITY;
                props.fVisible = true;
                props.opacity = (byte)255;

                props.rcDestination = new RECT(0, 0, size.x, size.y);
                User32.DwmUpdateThumbnailProperties(thumb, ref props);

                User32.ActivateWindowRepeat(handle, 250);
                Bitmap whiteBGImage = CaptureWindow(form.Handle, Engine.conf.ShowCursor) as Bitmap;
                //whiteBGImage.Save(@"c:\users\nicolas\documents\imageWhite.png");

                form.BackColor = Color.Black;
                form.Refresh();
                User32.ActivateWindowRepeat(handle, 250);
                Bitmap blackBGImage = CaptureWindow(form.Handle, Engine.conf.ShowCursor) as Bitmap;
                //blackBGImage.Save(@"c:\users\nicolas\documents\imageBlack.png");

                if (captureRedBGImage)
                {
                    form.BackColor = Color.Red;
                    form.Refresh();
                    User32.ActivateWindowRepeat(handle, 250);
                    redBGImage = CaptureWindow(form.Handle, Engine.conf.ShowCursor) as Bitmap;
                }

                form.BackColor = Color.White;
                form.Refresh();
                User32.ActivateWindowRepeat(handle, 250);
                Bitmap whiteBGImage2 = CaptureWindow(form.Handle, Engine.conf.ShowCursor) as Bitmap;

                // Don't do transparency calculation if an animated picture is detected
                if (whiteBGImage.AreBitmapsEqual(whiteBGImage2))
                {
                    windowImage = GraphicsMgr.ComputeOriginal(whiteBGImage, blackBGImage);
                    //windowImage.Save(@"c:\users\nicolas\documents\imageZResult.png");
                }
                else
                {
                    Console.WriteLine("Detected animated image => cannot compute transparency");
                    form.Close();
                    Application.DoEvents();
                    Image result = new Bitmap(whiteBGImage.Width, whiteBGImage.Height, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        // redraw the image on a black background to avoid transparent pixels artifacts
                        g.Clear(Color.Black);
                        g.DrawImage(whiteBGImage, 0, 0);
                    }
                    windowImage = result;
                }

                User32.DwmUnregisterThumbnail(thumb);
                blackBGImage.Dispose();
                whiteBGImage.Dispose();
                whiteBGImage2.Dispose();
            }

            return windowImage;
        }

        private static Image CaptureWindowWithTransparencyGDI(IntPtr handle, Rectangle windowRect)
        {
            Image windowImage = null;
            Bitmap whiteBGImage = null, blackBGImage = null, white2BGImage = null;

            try
            {
                using (new Freeze(handle))
                using (Form form = new Form())
                {
                    form.BackColor = Color.White;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.ShowInTaskbar = false;

                    User32.WINDOWPLACEMENT wp = new User32.WINDOWPLACEMENT();
                    User32.GetWindowPlacement(handle, ref wp);
                    int offset = Engine.conf.ActiveWindowIncludeShadows && wp.showCmd != (int)User32.SHOWWINDOW.SW_MAXIMIZE ? 20 : 0;

                    windowRect = windowRect.AddMargin(offset);
                    windowRect.Intersect(GraphicsMgr.GetScreenBounds());

                    User32.ShowWindow(form.Handle, (int)User32.WindowShowStyle.ShowNormalNoActivate);
                    User32.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, User32.SWP_NOACTIVATE);
                    Application.DoEvents();
                    whiteBGImage = (Bitmap)CaptureRectangle(User32.GetDesktopWindow(), windowRect);

                    form.BackColor = Color.Black;
                    Application.DoEvents();
                    blackBGImage = (Bitmap)CaptureRectangle(User32.GetDesktopWindow(), windowRect);

                    if (!Engine.conf.ActiveWindowGDIFreezeWindow)
                    {
                        form.BackColor = Color.White;
                        Application.DoEvents();
                        white2BGImage = (Bitmap)CaptureRectangle(User32.GetDesktopWindow(), windowRect);
                    }
                }

                if (Engine.conf.ActiveWindowGDIFreezeWindow || whiteBGImage.AreBitmapsEqual(white2BGImage))
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

                if (Engine.conf.ShowCursor)
                {
                    windowRect.X += windowRectCropped.X;
                    windowRect.Y += windowRectCropped.Y;
                    DrawCursor(windowImage, windowRect.Location);
                }

                if (Engine.conf.ActiveWindowShowCheckers)
                {
                    windowImage = ImageEffects.DrawCheckers(windowImage);
                }
            }

            return windowImage;
        }

        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            RECT rc;
            User32.GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            bool succeeded = User32.PrintWindow(hwnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            if (!succeeded)
            {
                gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Point.Empty, bmp.Size));
            }
            IntPtr hRgn = GDI.CreateRectRgn(0, 0, 0, 0);
            User32.GetWindowRgn(hwnd, hRgn);
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

        private static Image DrawCursor(Image img)
        {
            return DrawCursor(img, Point.Empty);
        }

        private static Image DrawCursor(Image img, Point offset)
        {
            using (User32.MyCursor cursor = User32.CaptureCursor())
            {
                cursor.Position.Offset(-offset.X, -offset.Y);
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(cursor.Bitmap, cursor.Position);
                }
            }

            return img;
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

        public enum Corner { TopLeft, TopRight, BottomLeft, BottomRight };

        /// <summary>
        /// Removes a corner from the clipping region of the given graphics object.
        /// </summary>
        /// <param name="bmp">The bitmap with the form corners masked in red</param>
        private static void RemoveCorner(Bitmap bmp, Graphics g, int minx, int miny, int maxx, Corner corner)
        {
            int[] shape;
            if (corner == Corner.TopLeft || corner == Corner.TopRight)
            {
                shape = new int[5] { 5, 3, 2, 1, 1 };
            }
            else
            {
                shape = new int[5] { 1, 1, 2, 3, 5 };
            }

            int maxy = miny + 5;
            if (corner == Corner.TopLeft || corner == Corner.BottomLeft)
            {
                for (int y = miny; y < maxy; y++)
                {
                    for (int x = minx; x < minx + shape[y - miny]; x++)
                    {
                        RemoveCornerPixel(bmp, g, y, x);
                    }
                }
            }
            else
            {
                for (int y = miny; y < maxy; y++)
                {
                    for (int x = maxx - 1; x >= maxx - shape[y - miny]; x--)
                    {
                        RemoveCornerPixel(bmp, g, y, x);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a pixel from the clipping region of the given graphics object, if
        /// the bitmap is red at the coordinates of the pixel.
        /// </summary>
        /// <param name="bmp">The bitmap with the form corners masked in red</param>
        private static void RemoveCornerPixel(Bitmap bmp, Graphics g, int y, int x)
        {
            var color = bmp.GetPixel(x, y);
            // detect a shade of red (the color is darker because of the window's shadow)
            if (color.R > 0 && color.G == 0 && color.B == 0)
            {
                Region region = new Region(new Rectangle(x, y, 1, 1));
                g.SetClip(region, CombineMode.Exclude);
            }
        }

        #endregion
    }
}