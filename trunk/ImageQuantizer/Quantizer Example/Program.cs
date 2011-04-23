using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using ImageQuantization;

namespace QuantizerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Image img = Image.FromFile("Colors.png");
            Stopwatch timer = new Stopwatch();

            timer.Start();
            img.Save("Colors_default.gif", ImageFormat.Gif);
            timer.Stop();
            Console.WriteLine("default: " + (int)timer.ElapsedMilliseconds);

            timer.Reset();
            timer.Start();
            OctreeQuantizer quantizer = new OctreeQuantizer(15, 4);
            using (Bitmap quantized = quantizer.Quantize(img))
            {
                quantized.Save("Colors_4bit.gif", ImageFormat.Gif);
            }
            timer.Stop();
            Console.WriteLine("4bit: " + (int)timer.ElapsedMilliseconds);

            timer.Reset();
            timer.Start();
            quantizer = new OctreeQuantizer(255, 4);
            using (Bitmap quantized = quantizer.Quantize(img))
            {
                quantized.Save("Colors_8bit.gif", ImageFormat.Gif);
            }
            timer.Stop();
            Console.WriteLine("8bit: " + (int)timer.ElapsedMilliseconds);

            timer.Reset();
            timer.Start();
            GrayscaleQuantizer gquantizer = new GrayscaleQuantizer();
            using (Bitmap quantized = gquantizer.Quantize(img))
            {
                quantized.Save("Colors_grayscale.gif", ImageFormat.Gif);
            }
            timer.Stop();
            Console.WriteLine("grayscale: " + (int)timer.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}