using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZUploader.Properties;

namespace ZUploader
{
    public partial class SplashForm : Form
    {
        private static Thread splashThread;
        private static SplashForm splashForm;

        public SplashForm(Image splashImage)
        {
            InitializeComponent();
            BackgroundImage = splashImage;
        }

        public static void ShowSplash()
        {
            if (splashThread == null)
            {
                splashThread = new Thread(DoShowSplash);
                splashThread.IsBackground = true;
                splashThread.Start();
            }
        }

        private static void DoShowSplash()
        {
            if (splashForm == null)
            {
                splashForm = new SplashForm(Resources.ZUploaderLogo);
            }

            Application.Run(splashForm);
        }

        public static void CloseSplash()
        {
            if (splashForm.InvokeRequired)
            {
                splashForm.Invoke(new MethodInvoker(CloseSplash));
            }
            else
            {
                Application.ExitThread();
            }
        }
    }
}