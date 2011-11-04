using System;
using System.Windows.Forms;
using Crop;
using ZScreenLib;

namespace CropTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Crop2 crop = new Crop2(Capture.CaptureScreen(false)))
            {
                Application.Run(crop);
            }
        }
    }
}