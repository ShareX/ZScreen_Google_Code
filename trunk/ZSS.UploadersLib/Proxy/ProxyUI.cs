using System;
using System.Net;
using System.Windows.Forms;
using HelpersLib;

namespace UploadersLib.HelperClasses
{
    public partial class ProxyUI : Form
    {
        public ProxyInfo Proxy { get; set; }

        public ProxyUI()
        {
            InitializeComponent();
            WebProxy proxy = ZAppHelper.GetDefaultWebProxy();
            Proxy = new ProxyInfo(Environment.UserName, "", proxy.Address.Host, proxy.Address.Port);
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