using System.Windows.Forms;
using UploadersAPILib;
using UploadersLib;

namespace ZScreenLib
{
    public partial class DestSelector : UserControl
    {
        public DestSelector()
        {
            InitializeComponent();
            if (Engine.MyUploadersConfig == null)
            {
                Engine.MyUploadersConfig = UploadersConfig.Load(Engine.UploadersConfigPath);
            }
        }

        private void btnOpenUploadersConfig_Click(object sender, System.EventArgs e)
        {
            new UploadersConfigForm(Engine.MyUploadersConfig, ZKeys.GetAPIKeys()).Show();
        }
    }
}