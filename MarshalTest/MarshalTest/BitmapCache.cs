using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

namespace Test
{
    public unsafe class BitmapCache : IDisposable
    {
        public bool IsCached { get; private set; }
        public long Length { get; private set; }
        public IntPtr Scan0 { get; private set; }
        public ColorBgra* Pointer { get; set; }
        public int PixelCount { get; private set; }

        private Bitmap bmp;
        private BitmapData bmpData;
        private ImageLockMode lockMode;

        public BitmapCache(Bitmap bmp, ImageLockMode lockMode)
        {
            this.bmp = bmp;
            PixelCount = bmp.Width * bmp.Height;
            this.lockMode = lockMode;
            LockImage();
        }

        private void LockImage()
        {
            Rectangle rect = new Rectangle(new Point(0, 0), bmp.Size);
            bmpData = bmp.LockBits(rect, lockMode, PixelFormat.Format32bppArgb);
            Scan0 = bmpData.Scan0;
            Pointer = (ColorBgra*)Scan0;
            Length = bmpData.Stride * bmpData.Height;
            IsCached = true;
        }

        public ColorBgra* GetPointer(int x, int y)
        {
            return unchecked(x + (ColorBgra*)(bmpData.Scan0) + (y * bmpData.Stride));
        }

        public void Dispose()
        {
            if (IsCached)
            {
                bmp.UnlockBits(bmpData);
            }
        }
    }
}