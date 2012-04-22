using System;
using System.Windows.Forms;

namespace UploadersLib
{
    public partial class ApiKeysUI : Form
    {
        UploadersAPIKeys mConfig = null;

        public ApiKeysUI(ref UploadersAPIKeys config)
        {
            mConfig = config;
            InitializeComponent();
            pgAppConfig.SelectedObject = mConfig;
        }

        private void ApiKeysUI_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - API configurator";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pgAppConfig.ResetSelectedProperty();
        }
    }
}