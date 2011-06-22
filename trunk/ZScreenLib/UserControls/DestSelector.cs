using System.Windows.Forms;
using UploadersAPILib;
using UploadersLib;
using System;

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

        private void RestrictToOneCheck(ToolStripDropDownButton tsddb, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsddb.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddb.DropDownItems[i];
                tsmi.Checked = tsmi == e.ClickedItem && !((ToolStripMenuItem)e.ClickedItem).Checked;
            }
        }

        private void tsddDestLinks_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RestrictToOneCheck(tsddbDestLink, e);
        }

        private void tsddbOutputType_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // RestrictToOneCheck(tsddbClipboardContent, e);
            EnableDisableDestControls();
        }

        public void EnableDisableDestControls(ToolStripItemClickedEventArgs e = null)
        {
            for (int i = 0; i < tsddbOutputType.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbOutputType.DropDownItems[i];
                if (tsmi.Checked)
                {
                    OutputTypeEnum cct = (OutputTypeEnum)tsmi.Tag;

                    // tsddbDestImage.Enabled = cct == ClipboardContentEnum.RemoteFilePath;
                    // tsddbLinkFormat.Enabled = cct == OutputTypeEnum.RemoteFilePath;
                    // tsddDestFile.Enabled = cct == ClipboardContentEnum.RemoteFilePath;
                    // tsddDestText.Enabled = cct == ClipboardContentEnum.RemoteFilePath;
                    // tsddbDestLink.Enabled = cct == ClipboardContentEnum.RemoteFilePath;
                }
            }
        }

        private void tsddbLinkFormat_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RestrictToOneCheck(tsddbLinkFormat, e);
        }
    }
}