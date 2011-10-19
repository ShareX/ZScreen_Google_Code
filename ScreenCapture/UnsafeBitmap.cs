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
using System.Drawing.Imaging;

namespace ScreenCapture
{
    public unsafe class UnsafeBitmap : IDisposable
    {
        private Bitmap bitmap;
        private BitmapData bitmapData;
        private ColorBgra* pointer;
        private int stride;
        private Rectangle rect;
        private bool isLocked;

        public int Length
        {
            get
            {
                return rect.Width * rect.Height;
            }
        }

        public UnsafeBitmap(Bitmap bitmap, bool lockBitmap = false, ImageLockMode imageLockMode = ImageLockMode.ReadWrite)
        {
            this.bitmap = bitmap;

            if (lockBitmap) Lock(imageLockMode);
        }

        public void Lock(ImageLockMode imageLockMode = ImageLockMode.ReadWrite)
        {
            if (!isLocked)
            {
                isLocked = true;
                rect = new Rectangle(Point.Empty, bitmap.Size);
                bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                pointer = (ColorBgra*)bitmapData.Scan0.ToPointer();
                stride = bitmapData.Stride;
            }
        }

        public void Unlock()
        {
            if (isLocked)
            {
                bitmap.UnlockBits(bitmapData);
                bitmapData = null;
                pointer = null;
                isLocked = false;
            }
        }

        public ColorBgra GetPixel(int x, int y)
        {
            return pointer[y * stride + x];
        }

        public ColorBgra GetPixel(int i)
        {
            return pointer[i];
        }

        public void SetPixel(int x, int y, ColorBgra color)
        {
            pointer[y * stride + x] = color;
        }

        public void SetPixel(int i, ColorBgra color)
        {
            pointer[i] = color;
        }

        public void SetPixel(int i, uint color)
        {
            pointer[i] = color;
        }

        public void Dispose()
        {
            Unlock();
        }
    }
}