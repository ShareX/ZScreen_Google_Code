using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugins;
using System.Drawing;
using GraphicsMgrLib;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace ImageFilters
{
    public class Blur : IPluginItem
    {
        public override string Name { get { return "Blur"; } }

        public override string Description { get { return "Blur"; } }

        private int radius;

        [Description("Blur radius in pixels.")]
        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value.Between(0, 25);
            }
        }

        private bool expand;

        [Description("Expand image to fit blurred image.")]
        public bool Expand
        {
            get
            {
                return expand;
            }
            set
            {
                expand = value;
            }
        }

        public Blur()
        {
            Radius = 2;
            Expand = true;
        }

        public override Image ApplyEffect(Image img)
        {
            return ApplyBlur((Bitmap)img, radius, expand);
        }

        public unsafe static Image ApplyBlur(Bitmap bmp, int radius, bool expand)
        {
            if (expand)
            {
                bmp = (Bitmap)GraphicsMgr.AddCanvas(bmp, radius);
            }

            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);

            using (BitmapCache src = new BitmapCache(bmp, ImageLockMode.ReadWrite))
            using (BitmapCache dst = new BitmapCache(bmp2, ImageLockMode.ReadWrite))
            {
                int[] w = CreateGaussianBlurRow(radius);
                int wlen = w.Length;

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

                if (rect.Height >= 1 && rect.Width >= 1)
                {
                    for (int y = rect.Top; y < rect.Bottom; ++y)
                    {
                        long* waSums = stackalloc long[wlen];
                        long* wcSums = stackalloc long[wlen];
                        long* aSums = stackalloc long[wlen];
                        long* bSums = stackalloc long[wlen];
                        long* gSums = stackalloc long[wlen];
                        long* rSums = stackalloc long[wlen];
                        long waSum = 0;
                        long wcSum = 0;
                        long aSum = 0;
                        long bSum = 0;
                        long gSum = 0;
                        long rSum = 0;

                        ColorBgra* dstPtr = dst.GetPointAddress(rect.Left, y);

                        for (int wx = 0; wx < wlen; ++wx)
                        {
                            int srcX = rect.Left + wx - radius;
                            waSums[wx] = 0;
                            wcSums[wx] = 0;
                            aSums[wx] = 0;
                            bSums[wx] = 0;
                            gSums[wx] = 0;
                            rSums[wx] = 0;

                            if (srcX >= 0 && srcX < src.BitmapSize.Width)
                            {
                                for (int wy = 0; wy < wlen; ++wy)
                                {
                                    int srcY = y + wy - radius;

                                    if (srcY >= 0 && srcY < src.BitmapSize.Height)
                                    {
                                        ColorBgra c = src.GetPoint(srcX, srcY);
                                        int wp = w[wy];

                                        waSums[wx] += wp;
                                        wp *= c.A + (c.A >> 7);
                                        wcSums[wx] += wp;
                                        wp >>= 8;

                                        aSums[wx] += wp * c.A;
                                        bSums[wx] += wp * c.B;
                                        gSums[wx] += wp * c.G;
                                        rSums[wx] += wp * c.R;
                                    }
                                }

                                int wwx = w[wx];
                                waSum += wwx * waSums[wx];
                                wcSum += wwx * wcSums[wx];
                                aSum += wwx * aSums[wx];
                                bSum += wwx * bSums[wx];
                                gSum += wwx * gSums[wx];
                                rSum += wwx * rSums[wx];
                            }
                        }

                        wcSum >>= 8;

                        if (waSum == 0 || wcSum == 0)
                        {
                            dstPtr->Bgra = 0;
                        }
                        else
                        {
                            int alpha = (int)(aSum / waSum);
                            int blue = (int)(bSum / wcSum);
                            int green = (int)(gSum / wcSum);
                            int red = (int)(rSum / wcSum);

                            dstPtr->Bgra = ColorBgra.BgraToUInt32(blue, green, red, alpha);
                        }

                        ++dstPtr;

                        for (int x = rect.Left + 1; x < rect.Right; ++x)
                        {
                            for (int i = 0; i < wlen - 1; ++i)
                            {
                                waSums[i] = waSums[i + 1];
                                wcSums[i] = wcSums[i + 1];
                                aSums[i] = aSums[i + 1];
                                bSums[i] = bSums[i + 1];
                                gSums[i] = gSums[i + 1];
                                rSums[i] = rSums[i + 1];
                            }

                            waSum = 0;
                            wcSum = 0;
                            aSum = 0;
                            bSum = 0;
                            gSum = 0;
                            rSum = 0;

                            int wx;
                            for (wx = 0; wx < wlen - 1; ++wx)
                            {
                                long wwx = (long)w[wx];
                                waSum += wwx * waSums[wx];
                                wcSum += wwx * wcSums[wx];
                                aSum += wwx * aSums[wx];
                                bSum += wwx * bSums[wx];
                                gSum += wwx * gSums[wx];
                                rSum += wwx * rSums[wx];
                            }

                            wx = wlen - 1;

                            waSums[wx] = 0;
                            wcSums[wx] = 0;
                            aSums[wx] = 0;
                            bSums[wx] = 0;
                            gSums[wx] = 0;
                            rSums[wx] = 0;

                            int srcX = x + wx - radius;

                            if (srcX >= 0 && srcX < src.BitmapSize.Width)
                            {
                                for (int wy = 0; wy < wlen; ++wy)
                                {
                                    int srcY = y + wy - radius;

                                    if (srcY >= 0 && srcY < src.BitmapSize.Height)
                                    {
                                        ColorBgra c = src.GetPoint(srcX, srcY);
                                        int wp = w[wy];

                                        waSums[wx] += wp;
                                        wp *= c.A + (c.A >> 7);
                                        wcSums[wx] += wp;
                                        wp >>= 8;

                                        aSums[wx] += wp * (long)c.A;
                                        bSums[wx] += wp * (long)c.B;
                                        gSums[wx] += wp * (long)c.G;
                                        rSums[wx] += wp * (long)c.R;
                                    }
                                }

                                int wr = w[wx];
                                waSum += (long)wr * waSums[wx];
                                wcSum += (long)wr * wcSums[wx];
                                aSum += (long)wr * aSums[wx];
                                bSum += (long)wr * bSums[wx];
                                gSum += (long)wr * gSums[wx];
                                rSum += (long)wr * rSums[wx];
                            }

                            wcSum >>= 8;

                            if (waSum == 0 || wcSum == 0)
                            {
                                dstPtr->Bgra = 0;
                            }
                            else
                            {
                                int alpha = (int)(aSum / waSum);
                                int blue = (int)(bSum / wcSum);
                                int green = (int)(gSum / wcSum);
                                int red = (int)(rSum / wcSum);

                                dstPtr->Bgra = ColorBgra.BgraToUInt32(blue, green, red, alpha);
                            }

                            ++dstPtr;
                        }
                    }
                }
            }

            return bmp2;
        }

        private static int[] CreateGaussianBlurRow(int amount)
        {
            int size = 1 + (amount * 2);
            int[] weights = new int[size];

            for (int i = 0; i <= amount; ++i)
            {
                // 1 + aa - aa + 2ai - ii
                weights[i] = 16 * (i + 1);
                weights[weights.Length - i - 1] = weights[i];
            }

            return weights;
        }
    }
}