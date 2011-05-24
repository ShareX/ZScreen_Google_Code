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
            if (Engine.MyUploadersConfig == null)
            {
                Engine.MyUploadersConfig = UploadersConfig.Load(Engine.UploaderConfigPath);
            }
        }

        private void btnOpenUploadersConfig_Click(object sender, System.EventArgs e)
        {
            new UploadersConfigForm(Engine.MyUploadersConfig, ZKeys.GetAPIKeys()).Show();
        }
    }
}