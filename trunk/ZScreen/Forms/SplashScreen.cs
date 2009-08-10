using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZSS.Forms
{
    public partial class SplashScreen : Form
    {
        public Queue<string> AsmLoads = new Queue<string>();

        public SplashScreen()
        {
            InitializeComponent();
            
        }

        private void tmrSplash_Tick(object sender, EventArgs e)
        {
            while (AsmLoads.Count > 0)
            {
                txtStatus.Text += AsmLoads.Dequeue() + "\r\n";
            }
            txtStatus.ScrollToCaret();
        }

        private void tmrApp_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
