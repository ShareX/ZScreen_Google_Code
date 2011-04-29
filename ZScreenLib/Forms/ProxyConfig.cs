using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib.HelperClasses;

namespace ZScreenLib
{
    public partial class ProxyConfig : Form
    {
        public ProxyInfo Proxy { get; set; }

        public ProxyConfig()
        {
            InitializeComponent();
            Proxy = new ProxyInfo(Environment.UserName, "", Adapter.GetDefaultWebProxyHost(), Adapter.GetDefaultWebProxyPort());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ProxyConfig_Load(object sender, EventArgs e)
        {
            pgProxy.SelectedObject = Proxy;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
