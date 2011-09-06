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
    public class SplashForm : Form
    {
        private static Thread splashThread;
        private static SplashForm splashForm;

        public SplashForm(Image splashImage)
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(splashImage.Width, splashImage.Height);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackgroundImage = splashImage;
            this.Name = "SplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "SplashForm";
            this.TopMost = true;
            this.ResumeLayout(false);
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

        private static void DoShowSplash()
        {
            if (splashForm == null)
            {
                splashForm = new SplashForm(Resources.ZUploaderLogo);
            }

            Application.Run(splashForm);
        }

        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}