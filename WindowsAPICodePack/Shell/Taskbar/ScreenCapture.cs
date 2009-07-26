//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Helper class to capture a control or window as System.Drawing.Bitmap
    /// </summary>
    public sealed class ScreenCapture
    {
        /// <summary>
        /// Captures a screenshot of the specified window at the specified
        /// bitmap size.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="bitmapSize">The requested bitmap size.</param>
        /// <returns>A screen capture of the window.</returns>
        public static Bitmap GrabWindowBitmap(IntPtr hwnd, System.Drawing.Size bitmapSize)
        {
            if (bitmapSize.Height <= 0 || bitmapSize.Width <= 0)
                return null;

            System.Diagnostics.Debug.WriteLine(hwnd.ToString());
            IntPtr windowDC = IntPtr.Zero;
            IntPtr targetDC = IntPtr.Zero;
            Graphics targetGr = null;

            try
            {
                System.Drawing.Size realWindowSize;
                TabbedThumbnailNativeMethods.GetClientSize(hwnd, out realWindowSize);

                if (realWindowSize == System.Drawing.Size.Empty)
                    realWindowSize = new System.Drawing.Size(200, 200);

                windowDC = TabbedThumbnailNativeMethods.GetWindowDC(hwnd);


                Bitmap targetBitmap = null;

                if (bitmapSize == System.Drawing.Size.Empty)
                    targetBitmap = new Bitmap(realWindowSize.Width, realWindowSize.Height);
                else
                    targetBitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);

                targetGr = Graphics.FromImage(targetBitmap);

                targetDC = targetGr.GetHdc();
                uint operation = 0x00CC0020 /*SRCCOPY*/ | 0x40000000 /*CAPTUREBLT*/;

                System.Drawing.Size ncArea = WindowUtilities.GetNonClientArea(hwnd);

                bool success = TabbedThumbnailNativeMethods.StretchBlt(targetDC, 0, 0, targetBitmap.Width, targetBitmap.Height,
                    windowDC, ncArea.Width, ncArea.Height, realWindowSize.Width, realWindowSize.Height, operation);

                return targetBitmap;
            }
            finally
            {
                if (windowDC != IntPtr.Zero)
                    TabbedThumbnailNativeMethods.ReleaseDC(hwnd, windowDC);
                if (targetDC != IntPtr.Zero)
                    ShellNativeMethods.DeleteObject(targetDC);
                if (targetGr != null)
                {
                    targetGr.ReleaseHdc();
                    targetGr.Dispose();
                }
            }
        }

        /// <summary>
        /// Grabs a snapshot of a WPF UIElement and returns the image as Bitmap.
        /// </summary>
        /// <param name="element">Represents the element to take the snapshot from.</param>
        /// <param name="dpix">Represents the X DPI value used to capture this snapshot.</param>
        /// <param name="dpiy">Represents the Y DPI value used to capture this snapshot.</param>
        /// <param name="width">The requested bitmap width.</param>
        /// /// <param name="height">The requested bitmap height.</param>
        /// <returns>Returns the bitmap (PNG format).</returns>
        public static Bitmap GrabWindowBitmap(UIElement element, int dpix, int dpiy, int width, int height)
        {
            RenderTargetBitmap rendertarget = null;

            // create the renderer.
            if (element.RenderSize.Height != 0 && element.RenderSize.Width != 0)
            {
                rendertarget = new RenderTargetBitmap((int)element.RenderSize.Width,
                        (int)element.RenderSize.Height, dpix, dpiy, PixelFormats.Default);
            }
            else
                return null;    // 0 sized element. Probably hidden

            // Render the element on screen.
            rendertarget.Render(element);

            BitmapEncoder bmpe;

            bmpe = new PngBitmapEncoder();
            bmpe.Frames.Add(BitmapFrame.Create(rendertarget));

            // Create a MemoryStream with the image.
            // Returning this as a MemoryStream makes it easier to save the image to a file or simply display it anywhere.
            MemoryStream fl = new MemoryStream();
            bmpe.Save(fl);

            Bitmap bmp = new Bitmap(fl);

            fl.Close();

            return (Bitmap)bmp.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }

        /// <summary>
        /// Resizes the given bitmap while maintaining the aspect ratio.
        /// </summary>
        /// <param name="originalHBitmap">Original/source bitmap</param>
        /// <param name="newWidth">Maximum width for the new image</param>
        /// <param name="maxHeight">Maximum height for the new image</param>
        /// <param name="resizeIfWider">If true and requested image is wider than the source, the new image is resized accordingly.</param>
        /// <returns></returns>
        public static Bitmap ResizeImageWithAspect(IntPtr originalHBitmap, int newWidth, int maxHeight, bool resizeIfWider)
        {
            Bitmap originalBitmap = Bitmap.FromHbitmap(originalHBitmap);

            if (resizeIfWider)
            {
                if (originalBitmap.Width <= newWidth)
                    newWidth = originalBitmap.Width;
            }

            int newHeight = originalBitmap.Height * newWidth / originalBitmap.Width;

            if (newHeight > maxHeight) // Height resize if necessary
            {
                newWidth = originalBitmap.Width * maxHeight / originalBitmap.Height;
                newHeight = maxHeight;
            }

            // Create the new image with the sizes we've calculated
            Bitmap resizedImage = (Bitmap)originalBitmap.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
            originalBitmap.Dispose();

            return resizedImage;
        }
    }
}
