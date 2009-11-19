using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZScreenLib.Helpers;
using System.Diagnostics;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] files = new string[] { "1080p.png", "theroyal.png" };
            foreach (string f in files)
            {
                Bitmap img = new Bitmap(@"..\..\" + f);
                Debug.WriteLine(string.Format("Testing: {0} {1}x{2}", f, img.Width, img.Height));
                Bitmap test = (Bitmap)img.Clone();
                using (MyTimer timer = new MyTimer("Unsafe", false)) AddHighlighting(img, Color.Yellow, false);
                test = (Bitmap)img.Clone();
                using (MyTimer timer = new MyTimer("Unsafe SaveTransparency", false)) AddHighlighting(img, Color.Yellow, true);
                test = (Bitmap)img.Clone();
                using (MyTimer timer = new MyTimer("Marshal ", false)) AddHighlighting2(img, Color.Yellow, false);
                test = (Bitmap)img.Clone();
                using (MyTimer timer = new MyTimer("Marshal SaveTransparency", false)) AddHighlighting2(img, Color.Yellow, true);
                pictureBox1.Image = test;
            }
        }

        public static Image AddHighlighting(Bitmap bmp)
        {
            return AddHighlighting(bmp, Color.Yellow, false);
        }

        public static Image AddHighlighting(Bitmap bmp, Color color, bool saveTransparency)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int length = bmpData.Stride * bmpData.Height;
            int r, g, b, a;

            unsafe
            {
                byte* p = (byte*)(void*)bmpData.Scan0;

                for (int i = 0; i < length; i += 4)
                {
                    r = i + 2; g = i + 1; b = i; a = i + 3;

                    p[r] = Math.Min(p[r], color.R);
                    p[g] = Math.Min(p[g], color.G);
                    p[b] = Math.Min(p[b], color.B);

                    if (!saveTransparency)
                    {
                        p[a] = Math.Max(p[a], color.A);
                    }
                }
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static Image AddHighlighting2(Bitmap bmp, Color color, bool saveTransparency)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int r, g, b, a, length = bmpData.Stride * bmpData.Height;
            byte[] buffer = new byte[length];
            Marshal.Copy(bmpData.Scan0, buffer, 0, length);

            for (int i = 0; i < length; i += 4)
            {
                r = i + 2; g = i + 1; b = i; a = i + 3;

                buffer[r] = Math.Min(buffer[r], color.R);
                buffer[g] = Math.Min(buffer[g], color.G);
                buffer[b] = Math.Min(buffer[b], color.B);

                if (!saveTransparency)
                {
                    buffer[a] = Math.Max(buffer[a], color.A);
                }
            }

            Marshal.Copy(buffer, 0, bmpData.Scan0, length);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}