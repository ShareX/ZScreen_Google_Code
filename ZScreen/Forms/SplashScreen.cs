using System;
using System.Windows.Forms;
using ZScreenGUI;

namespace ZSS.Forms
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            tmrApp.Enabled = true;
        }

        private void tmrSplash_Tick(object sender, EventArgs e)
        {
            while (Loader.AsmLoads.Count > 0)
            {
                txtStatus.Text += Loader.AsmLoads.Dequeue() + "\r\n";
            }

            txtStatus.ScrollToCaret();
        }

        private void tmrApp_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}