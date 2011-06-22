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

        private void tsddbClipboardContent_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RestrictToOneCheck(tsddbClipboardContent, e);
            EnableDisableDestControls();
        }

        public void EnableDisableDestControls()
        {
            for (int i = 0; i < tsddbClipboardContent.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbClipboardContent.DropDownItems[i];
                if (tsmi.Checked)
                {
                    ClipboardContentType cct = (ClipboardContentType)tsmi.Tag;
                    tsddbDestImage.Enabled = cct == ClipboardContentType.RemoteFilePath;
                    tsddDestFile.Enabled = cct == ClipboardContentType.RemoteFilePath;
                    tsddDestText.Enabled = cct == ClipboardContentType.RemoteFilePath;
                    tsddbDestLink.Enabled = cct == ClipboardContentType.RemoteFilePath;
                }
            }
        }
    }
}