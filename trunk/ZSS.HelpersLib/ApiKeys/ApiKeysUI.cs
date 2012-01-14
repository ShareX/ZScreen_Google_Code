using System;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class ApiKeysUI : Form
    {
        public ApiKeysUI(UploadersAPIKeys config)
        {
            InitializeComponent();
            pgAppConfig.SelectedObject = config;
        }

        private void ApiKeysUI_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - API configurator";
        }
    }
}