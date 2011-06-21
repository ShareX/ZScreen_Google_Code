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
        }

        private void tsbDestConfig_Click(object sender, System.EventArgs e)
        {
            new UploadersConfigForm(Engine.MyUploadersConfig, ZKeys.GetAPIKeys()).Show();
        }

        private void tscbURLShortener_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            tsddLinkShortener.Text = "URL Shortner: " + tscbURLShorteners.Text;
        }
    }
}