using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using HelpersLib;
using ZScreenLib;
using UploadersAPILib;

namespace ZScreen4
{
    public partial class ZScreenMain : Form
    {
        public ZScreenMain()
        {
            InitializeComponent();
        }

        private void ZScreenMain_Load(object sender, EventArgs e)
        {
            TrayMenuLoadItems();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnCaptureScreen_Click(object sender, EventArgs e)
        {

        }

        private void btnDestinations_Click(object sender, EventArgs e)
        {
          //  new UploadersConfigForm(Engine.conf.UploadersConfig2, ZKeys.GetAPIKeys()) { Icon = this.Icon }.Show();
        }
    }
}
