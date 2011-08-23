using System;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace ZScreenLib
{
    public partial class ProxyConfig : Form
    {
        public ProxyInfo Proxy { get; set; }

        public ProxyConfig()
        {
            InitializeComponent();
            Proxy = new ProxyInfo(Environment.UserName, "", ZAppHelper.GetDefaultWebProxyHost(), ZAppHelper.GetDefaultWebProxyPort());
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