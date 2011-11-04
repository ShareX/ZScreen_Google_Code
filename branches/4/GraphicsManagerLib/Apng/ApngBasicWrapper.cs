/*  HaRepacker - WZ extractor and repacker
 * Copyright (C) 2009, 2010 haha01haha01
   
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace SharpApng
{
    public static class ApngBasicWrapper
    {
        public const int PIXEL_DEPTH = 4;

        public static IntPtr MarshalString(string source)
        {
            byte[] toMarshal = Encoding.ASCII.GetBytes(source);
            int size = Marshal.SizeOf(source[0]) * source.Length;
            IntPtr pnt = Marshal.AllocHGlobal(size);
            Marshal.Copy(toMarshal, 0, pnt, source.Length);
            Marshal.Copy(new byte[] { 0 }, 0, new IntPtr(pnt.ToInt32() + size), 1);
            return pnt;
        }

        public static IntPtr MarshalByteArray(byte[] source)
        {
            int size = Marshal.SizeOf(source[0]) * source.Length;
            IntPtr pnt = Marshal.AllocHGlobal(size);
            Marshal.Copy(source, 0, pnt, source.Length);
            return pnt;
        }

        public static void ReleaseData(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        public static unsafe byte[] TranslateImage(Bitmap image)
        {
            byte[] result = new byte[image.Width * image.Height * PIXEL_DEPTH];
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte* p = (byte*)data.Scan0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    result[(y * image.Width + x) * PIXEL_DEPTH] = p[x * PIXEL_DEPTH];
                    result[(y * image.Width + x) * PIXEL_DEPTH + 1] = p[x * PIXEL_DEPTH + 1];
                    result[(y * image.Width + x) * PIXEL_DEPTH + 2] = p[x * PIXEL_DEPTH + 2];
                    result[(y * image.Width + x) * PIXEL_DEPTH + 3] = p[x * PIXEL_DEPTH + 3];
                }
                p += data.Stride;
            }
            image.UnlockBits(data);
            return result;
        }

        public static void CreateFrameManaged(Bitmap source, int num, int den, int i)
        {
            IntPtr ptr = MarshalByteArray(TranslateImage(source));
            CreateFrame(ptr, num, den, i, source.Width * source.Height * PIXEL_DEPTH);
            ReleaseData(ptr);
        }

        public static void SaveApngManaged(string path, int frameCount, int width, int height, bool firstFrameHidden)
        {
            IntPtr pathPtr = MarshalString(path);
            byte firstFrame = firstFrameHidden ? (byte)1 : (byte)0;
            SaveAPNG(pathPtr, frameCount, width, height, PIXEL_DEPTH, firstFrame);
            ReleaseData(pathPtr);
        }

        private const string apngdll = "apng32.dll";
        [DllImport(apngdll)]
        public static extern void CreateFrame(IntPtr pdata, int num, int den, int i, int len);

        [DllImport(apngdll)]
        public static extern void SaveAPNG(IntPtr path, int frameCount, int width, int height, int bytesPerPixel, byte firstFrameHidden);
    }
}