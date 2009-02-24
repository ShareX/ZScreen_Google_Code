using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using ZSS.ImageUploader;
using System.Drawing;
using System.Windows.Forms;

namespace ZSS
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageUploaderExample.Test());
        }
    }
}