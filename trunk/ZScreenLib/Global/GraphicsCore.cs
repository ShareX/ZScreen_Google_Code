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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MS.WindowsAPICodePack.Internal;

namespace ZScreenLib
{
    public static partial class User32
    {
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int CURSOR_SHOWING = 0x00000001;
        public const int GWL_STYLE = -16;
        public const ulong WS_VISIBLE = 0x10000000L;
        public const ulong WS_BORDER = 0x00800000L;
        public const ulong TARGETWINDOW = WS_BORDER | WS_VISIBLE;

        [StructLayout(LayoutKind.Sequential)]
        public struct IconInfo
        {
            public bool fIcon;         // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies 
            public Int32 xHotspot;     // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot 
            public Int32 yHotspot;     // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot 
            public IntPtr hbmMask;     // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
            public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this 
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CursorInfo
        {
            public Int32 cbSize;        // Specifies the size, in bytes, of the structure. 
            public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
            public IntPtr hCursor;      // Handle to the cursor. 
            public Point ptScreenPos;   // A POINT structure that receives the screen coordinates of the cursor. 
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);

        [DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint SHAppBarMessage(int dwMessage, out APPBARDATA pData);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public static string GetWindowLabel()
        {
            const int numOfChars = 256;
            IntPtr handle = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(numOfChars);

            if (GetWindowText(handle, sb, numOfChars) > 0)
            {
                return sb.ToString();
            }
            return string.Empty;
        }

        public static IntPtr GetWindowHandle()
        {
            const int numOfChars = 256;
            IntPtr handle = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(numOfChars);

            if (GetWindowText(handle, sb, numOfChars) > 0)
            {
                return handle;
            }
            return IntPtr.Zero;
        }

        public static MyCursor CaptureCursor()
        {
            CursorInfo cursorInfo = new CursorInfo();
            cursorInfo.cbSize = Marshal.SizeOf(cursorInfo);
            if (GetCursorInfo(out cursorInfo) && cursorInfo.flags == CURSOR_SHOWING)
            {
                IntPtr hicon = CopyIcon(cursorInfo.hCursor);
                if (hicon != IntPtr.Zero)
                {
                    IconInfo iconInfo;
                    if (GetIconInfo(hicon, out iconInfo))
                    {
                        Point position = new Point(cursorInfo.ptScreenPos.X - iconInfo.xHotspot, cursorInfo.ptScreenPos.Y - iconInfo.yHotspot);

                        using (Bitmap maskBitmap = Bitmap.FromHbitmap(iconInfo.hbmMask))
                        {
                            Bitmap resultBitmap;

                            // Is this a monochrome cursor?
                            if (maskBitmap.Height == maskBitmap.Width * 2)
                            {
                                resultBitmap = new Bitmap(maskBitmap.Width, maskBitmap.Width);

                                Graphics desktopGraphics = Graphics.FromHwnd(GetDesktopWindow());
                                IntPtr desktopHdc = desktopGraphics.GetHdc();

                                IntPtr maskHdc = GDI.CreateCompatibleDC(desktopHdc);
                                IntPtr oldPtr = GDI.SelectObject(maskHdc, maskBitmap.GetHbitmap());

                                using (Graphics resultGraphics = Graphics.FromImage(resultBitmap))
                                {
                                    IntPtr resultHdc = resultGraphics.GetHdc();

                                    // These two operation will result in a black cursor over a white background.
                                    // Later in the code, a call to MakeTransparent() will get rid of the white background.
                                    GDI.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 32, CopyPixelOperation.SourceCopy);
                                    GDI.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 0, CopyPixelOperation.SourceInvert);

                                    resultGraphics.ReleaseHdc(resultHdc);
                                }

                                IntPtr newPtr = GDI.SelectObject(maskHdc, oldPtr);
                                GDI.DeleteDC(newPtr);
                                GDI.DeleteDC(maskHdc);
                                desktopGraphics.ReleaseHdc(desktopHdc);

                                // Remove the white background from the BitBlt calls,
                                // resulting in a black cursor over a transparent background.
                                resultBitmap.MakeTransparent(Color.White);
                            }
                            else
                            {
                                resultBitmap = Icon.FromHandle(hicon).ToBitmap();
                            }

                            return new MyCursor(new Cursor(cursorInfo.hCursor), position, resultBitmap);
                        }
                    }
                }
            }

            return null;
        }

        private static Image DrawCursor(Image img)
        {
            return DrawCursor(img, Point.Empty);
        }

        private static Image DrawCursor(Image img, Point offset)
        {
            MyCursor cursor = CaptureCursor();
            if (cursor == null) cursor = new MyCursor();
            cursor.Position.Offset(-offset.X, -offset.Y);
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(cursor.Bitmap, cursor.Position);
            return img;
        }

        public static Image CaptureScreen(bool showCursor)
        {
            Image img = CaptureRectangle(GetDesktopWindow(), GraphicsMgr.GetScreenBounds());
            if (showCursor) DrawCursor(img);
            return img;
        }

        public static Image CaptureWindow(IntPtr handle, bool showCursor)
        {
            return CaptureWindow(handle, showCursor, 0);
        }

        public static Image CaptureWindow(IntPtr handle, bool showCursor, int offset)
        {
            Rectangle windowRect = GetWindowRectangle(handle);
            if (offset > 0) windowRect = RectangleAddOffset(windowRect, offset);

            Image img = new Bitmap(windowRect.Width, windowRect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(windowRect.Location, new Point(0, 0), windowRect.Size, CopyPixelOperation.SourceCopy);
            if (showCursor) DrawCursor(img, windowRect.Location);

            //img = PrintWindow(handle);
            img = MakeBackgroundTransparent(handle, img);

            return img;
        }

        public static Rectangle RectangleAddOffset(Rectangle rect, int offset)
        {
            rect.X -= offset;
            rect.Y -= offset;
            rect.Width += offset * 2;
            rect.Height += offset * 2;
            return rect;
        }

        public static Image CaptureRectangle(IntPtr handle, Rectangle rect)
        {
            // get the hDC of the target window
            IntPtr hdcSrc = GetWindowDC(handle);
            // create a device context we can copy to
            IntPtr hdcDest = GDI.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to
            IntPtr hBitmap = GDI.CreateCompatibleBitmap(hdcSrc, rect.Width, rect.Height);
            // select the bitmap object
            IntPtr hOld = GDI.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSrc, rect.Left, rect.Top, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
            // restore selection
            GDI.SelectObject(hdcDest, hOld);
            // clean up
            GDI.DeleteDC(hdcDest);
            ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI.DeleteObject(hBitmap);
            return img;
        }

        private static bool GetWindowRegion(IntPtr hWnd, out Region region)
        {
            IntPtr hRgn = GDI.CreateRectRgn(0, 0, 0, 0);
            RegionType regionType = (RegionType)GetWindowRgn(hWnd, hRgn);
            region = Region.FromHrgn(hRgn);
            return regionType != RegionType.ERROR && regionType != RegionType.NULLREGION;
        }

        private static Bitmap MakeBackgroundTransparent(IntPtr hWnd, Image image)
        {
            Region region;
            if (GetWindowRegion(hWnd, out region))
            {
                Bitmap result = new Bitmap(image.Width, image.Height);

                using (Graphics g = Graphics.FromImage(result))
                {
                    if (!region.IsEmpty(g))
                    {
                        RectangleF bounds = region.GetBounds(g);
                        g.Clip = region;
                        g.DrawImage(image, new RectangleF(new PointF(0, 0), bounds.Size), bounds, GraphicsUnit.Pixel);

                        return result;
                    }
                }
            }

            return (Bitmap)image;
        }

        private static Bitmap PrintWindow(IntPtr hwnd)
        {
            RECT rc;
            GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            bool succeeded = PrintWindow(hwnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            if (!succeeded)
            {
                gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Point.Empty, bmp.Size));
            }
            IntPtr hRgn = GDI.CreateRectRgn(0, 0, 0, 0);
            GetWindowRgn(hwnd, hRgn);
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

        public class MyCursor
        {
            public Cursor Cursor;
            public Point Position;
            public Bitmap Bitmap;

            public MyCursor()
            {
                this.Cursor = Cursor.Current;
                this.Position = new Point(Cursor.Position.X - this.Cursor.HotSpot.X, Cursor.Position.Y - this.Cursor.HotSpot.Y);
                this.Bitmap = Icon.FromHandle(this.Cursor.Handle).ToBitmap();
            }

            public MyCursor(Cursor cursor, Point position, Bitmap bitmap)
            {
                this.Cursor = cursor;
                this.Position = position;
                this.Bitmap = bitmap;
            }
        }

        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_LAST
        }

        public static bool DWMWA_EXTENDED_FRAME_BOUNDS(IntPtr handle, out Rectangle rectangle)
        {
            RECT rect;
            int result = DwmGetWindowAttribute(handle, (int)DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS,
                out rect, Marshal.SizeOf(typeof(RECT)));
            rectangle = rect.ToRectangle();
            return result >= 0;
        }

        public static bool DWMWA_NCRENDERING_ENABLED(IntPtr handle)
        {
            bool enabled;
            int result = DwmGetWindowAttribute(handle, (int)DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_ENABLED,
                out enabled, sizeof(bool));
            if (result < 0) throw new Exception("Error: DWMWA_NCRENDERING_ENABLED");
            return enabled;
        }

        public static Rectangle GetWindowRect(IntPtr handle)
        {
            RECT rect;
            GetWindowRect(handle, out rect);
            return rect.ToRectangle();
        }

        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                return GetWindowRect(handle);
            }

            Rectangle rectangle;
            if (DWMWA_EXTENDED_FRAME_BOUNDS(handle, out rectangle))
            {
                return rectangle;
            }
            else
            {
                return GetWindowRect(handle);
            }
        }

        public static void ActivateWindow(IntPtr handle)
        {
            SetForegroundWindow(handle);
            SetActiveWindow(handle);
        }

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public static Image CaptureActiveWindow()
        {
            Image windowImage = null;
            IntPtr handle = User32.GetForegroundWindow();

            if (handle.ToInt32() > 0)
            {
                Rectangle windowRect = User32.GetWindowRectangle(handle);

                if (!CoreHelpers.RunningOnXP && Engine.conf.SelectedWindowIncludeShadows)
                {
                    windowRect = RectangleAddOffset(windowRect, 15);
                }

                using (Form form = new Form())
                {
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.ShowInTaskbar = false;

                    if (!CoreHelpers.RunningOnXP && Engine.conf.SelectedWindowCleanBackground)
                    {
                        // create form behind the window to remove the dirty Aero background
                        form.BackColor = Color.Black;
                        form.Show();
                        User32.ActivateWindowRepeat(handle, 250);
                        form.Refresh();
                        User32.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, 0);
                        Thread.Sleep(1);
                        Application.DoEvents();

                        // capture the window with a black background
                        int offset = !CoreHelpers.RunningOnXP && Engine.conf.SelectedWindowIncludeShadows ? 15 : 0;
                        Bitmap blackBGImage = User32.CaptureWindow(handle, Engine.conf.ShowCursor, offset) as Bitmap;
                        //blackBGImage.Save(@"c:\users\nicolas\documents\blackBGImage.png");

                        form.BackColor = Color.White;
                        form.Refresh();
                        User32.ActivateWindowRepeat(handle, 250);
                        Thread.Sleep(1);
                        Application.DoEvents();

                        // capture the window again with a white background this time
                        Bitmap whiteBGImage = User32.CaptureWindow(handle, Engine.conf.ShowCursor, offset) as Bitmap;
                        //whiteBGImage.Save(@"c:\users\nicolas\documents\whiteBGImage.png");

                        // compute the real window image by difference between the two previous images
                        windowImage = ComputeOriginal(whiteBGImage, blackBGImage);

                        if (Engine.conf.SelectedWindowShowCheckers)
                        {
                            windowImage = ImageEffects.DrawCheckers(windowImage, Color.White, Color.LightGray, 20);
                        }
                    }

                    if (Engine.conf.SelectedWindowCleanTransparentCorners && !Engine.conf.SelectedWindowIncludeShadows)
                    {
                        form.BackColor = Color.White;
                        form.Show();
                        User32.ActivateWindowRepeat(handle, 250);

                        // paints red corners behind the form, so that they can be recognized and removed
                        form.Paint += new PaintEventHandler(FormPaintRedCorners);
                        form.Refresh();
                        User32.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, 0);
                        Thread.Sleep(1);
                        Application.DoEvents();
                        Bitmap redCornersImage = User32.CaptureWindow(handle, false) as Bitmap;

                        using (Image result = new Bitmap(windowImage.Width, windowImage.Height, PixelFormat.Format32bppArgb))
                        {
                            using (Graphics g = Graphics.FromImage(result))
                            {
                                g.Clear(Color.Transparent);

                                // remove the transparent pixels in the four corners
                                RemoveCorner(redCornersImage, g, 0, 0, 5, Corner.TopLeft);
                                RemoveCorner(redCornersImage, g, windowImage.Width - 5, 0, windowImage.Width, Corner.TopRight);
                                RemoveCorner(redCornersImage, g, 0, windowImage.Height - 5, 5, Corner.BottomLeft);
                                RemoveCorner(redCornersImage, g, windowImage.Width - 5, windowImage.Height - 5, windowImage.Width, Corner.BottomRight);
                                g.DrawImage(windowImage, 0, 0);
                            }

                            windowImage = (Image)result.Clone();
                        }
                    }
                }

                if (windowImage == null)
                {
                    windowImage = User32.CaptureWindow(handle, Engine.conf.ShowCursor);
                }
            }

            return windowImage;
        }

        private static void ActivateWindowRepeat(IntPtr handle, int count)
        {
            User32.ActivateWindow(handle);
            for (int i = 0; User32.GetForegroundWindow() != handle && i < count; i++)
            {
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Compute the original window image from the difference between the two given images
        /// </summary>
        /// <param name="whiteBGImage">the window with a white background</param>
        /// <param name="blackBGImage">the window with a black background</param>
        /// <returns>the original window image, with restored alpha channel</returns>
        private static Bitmap ComputeOriginal(Bitmap whiteBGImage, Bitmap blackBGImage)
        {
            int width = whiteBGImage.Size.Width;
            int height = whiteBGImage.Size.Height;

            Bitmap resultImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(new Point(0, 0), blackBGImage.Size);

            // access the image data directly for faster image processing
            BitmapData blackImageData = blackBGImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData whiteImageData = whiteBGImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resultImageData = resultImage.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            IntPtr pBlackImage = blackImageData.Scan0;
            IntPtr pWhiteImage = whiteImageData.Scan0;
            IntPtr pResultImage = resultImageData.Scan0;

            int bytes = blackImageData.Stride * blackImageData.Height;
            byte[] blackBGImageRGB = new byte[bytes];
            byte[] whiteBGImageRGB = new byte[bytes];
            byte[] resultImageRGB = new byte[bytes];

            Marshal.Copy(pBlackImage, blackBGImageRGB, 0, bytes);
            Marshal.Copy(pWhiteImage, whiteBGImageRGB, 0, bytes);

            int offset = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // ARGB is in fact BGRA (little endian)
                    int r0 = blackBGImageRGB[offset + 2];
                    int g0 = blackBGImageRGB[offset + 1];
                    int b0 = blackBGImageRGB[offset + 0];
                    int r1 = whiteBGImageRGB[offset + 2];
                    int alpha = r0 - r1 + 255;

                    int resultR, resultG, resultB;
                    if (alpha != 0)
                    {
                        resultR = r0 * 255 / alpha;
                        resultG = g0 * 255 / alpha;
                        resultB = b0 * 255 / alpha;
                    }
                    else
                    {
                        resultR = 0;
                        resultG = 0;
                        resultB = 0;
                    }

                    resultImageRGB[offset + 3] = (byte)alpha;
                    resultImageRGB[offset + 2] = (byte)resultR;
                    resultImageRGB[offset + 1] = (byte)resultG;
                    resultImageRGB[offset + 0] = (byte)resultB;

                    offset += 4;
                }
            }

            Marshal.Copy(resultImageRGB, 0, pResultImage, bytes);

            blackBGImage.UnlockBits(blackImageData);
            whiteBGImage.UnlockBits(whiteImageData);
            resultImage.UnlockBits(resultImageData);

            return resultImage;
        }

        #region Clean window corners

        /// <summary>
        /// Paints 5 pixel wide red corners behind the form from which the event originates.
        /// </summary>
        private static void FormPaintRedCorners(object sender, PaintEventArgs e)
        {
            const int cornerSize = 5;
            Form form = sender as Form;
            if (form != null)
            {
                int width = form.Width;
                int height = form.Height;
                e.Graphics.FillRectangle(Brushes.Red, 0, 0, cornerSize, cornerSize);
                e.Graphics.FillRectangle(Brushes.Red, width - 5, 0, cornerSize, cornerSize);
                e.Graphics.FillRectangle(Brushes.Red, 0, height - 5, cornerSize, cornerSize);
                e.Graphics.FillRectangle(Brushes.Red, width - 5, height - 5, cornerSize, cornerSize);
            }
        }

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
            if (color.R > 64 && color.G == 0 && color.B == 0)
            {
                Region region = new Region(new Rectangle(x, y, 1, 1));
                g.SetClip(region, CombineMode.Exclude);
            }
        }

        #endregion

        #region Taskbar

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        public enum ABMsg
        {
            ABM_NEW = 0,
            ABM_REMOVE = 1,
            ABM_QUERYPOS = 2,
            ABM_SETPOS = 3,
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
            ABM_ACTIVATE = 6,
            ABM_GETAUTOHIDEBAR = 7,
            ABM_SETAUTOHIDEBAR = 8,
            ABM_WINDOWPOSCHANGED = 9,
            ABM_SETSTATE = 10
        }

        public enum ABEdge
        {
            ABE_LEFT = 0,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }

        public enum ABState
        {
            ABS_MANUAL = 0,
            ABS_AUTOHIDE = 1,
            ABS_ALWAYSONTOP = 2,
            ABS_AUTOHIDEANDONTOP = 3,
        }

        public enum TaskBarEdge
        {
            Bottom,
            Top,
            Left,
            Right
        }

        public static Rectangle GetTaskbarRectangle()
        {
            APPBARDATA abd = new APPBARDATA();
            SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, out abd);
            return abd.rc.ToRectangle();
        }

        /// <summary>
        /// Method returns information about the Window's TaskBar.
        /// </summary>
        /// <param name="taskBarEdge">Location of the TaskBar(Top,Bottom,Left,Right).</param>
        /// <param name="height">Height of the TaskBar.</param>
        /// <param name="autoHide">AutoHide property of the TaskBar.</param>
        private static void GetTaskBarInfo(out TaskBarEdge taskBarEdge, out int height, out bool autoHide)
        {
            APPBARDATA abd = new APPBARDATA();

            height = 0;
            taskBarEdge = TaskBarEdge.Bottom;
            autoHide = false;

            uint ret = SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, out abd);
            switch (abd.uEdge)
            {
                case (int)ABEdge.ABE_BOTTOM:
                    taskBarEdge = TaskBarEdge.Bottom;
                    height = abd.rc.Height;
                    break;
                case (int)ABEdge.ABE_TOP:
                    taskBarEdge = TaskBarEdge.Top;
                    height = abd.rc.Bottom;
                    break;
                case (int)ABEdge.ABE_LEFT:
                    taskBarEdge = TaskBarEdge.Left;
                    height = abd.rc.Width;
                    break;
                case (int)ABEdge.ABE_RIGHT:
                    taskBarEdge = TaskBarEdge.Right;
                    height = abd.rc.Width;
                    break;
            }

            abd = new APPBARDATA();
            uint uState = SHAppBarMessage((int)ABMsg.ABM_GETSTATE, out abd);
            switch (uState)
            {
                case (int)ABState.ABS_ALWAYSONTOP:
                    autoHide = false;
                    break;
                case (int)ABState.ABS_AUTOHIDE:
                    autoHide = true;
                    break;
                case (int)ABState.ABS_AUTOHIDEANDONTOP:
                    autoHide = true;
                    break;
                case (int)ABState.ABS_MANUAL:
                    autoHide = false;
                    break;
            }
        }

        #endregion
    }
}