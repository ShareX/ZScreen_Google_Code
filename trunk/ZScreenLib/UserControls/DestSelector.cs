using System.Windows.Forms;
using UploadersLib;
using UploadersAPILib;

namespace ZScreenLib
{
    public partial class DestSelector : UserControl
    {
        public DestSelector()
        {
            InitializeComponent();
        }

        private void btnOpenUploadersConfig_Click(object sender, System.EventArgs e)
        {
            new UploadersConfigForm(Engine.conf.UploadersConfig, ZKeys.GetAPIKeys()).Show();
        }
    }
}