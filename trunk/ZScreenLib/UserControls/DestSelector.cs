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

        public void EnableDisableDestControls(ToolStripItemClickedEventArgs e = null)
        {
            tsddbClipboardContent.Enabled = tsmiClipboard.Checked;

            for (int i = 0; i < tsddbClipboardContent.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbClipboardContent.DropDownItems[i];
                ClipboardContentEnum cct = (ClipboardContentEnum)tsmi.Tag;
                if (cct == ClipboardContentEnum.Local)
                {
                    tsmi.Enabled = tsmiFile.Checked;
                    if (!tsmi.Enabled)
                    {
                        tsmi.Checked = false;
                    }
                }
                if (tsmi.Checked)
                {
                    tsddbDestImage.Enabled = cct == ClipboardContentEnum.Remote;
                    tsddbLinkFormat.Enabled = cct != ClipboardContentEnum.Data;
                    tsddDestFile.Enabled = cct == ClipboardContentEnum.Remote;
                    tsddDestText.Enabled = cct == ClipboardContentEnum.Remote;
                    tsddbDestLink.Enabled = cct == ClipboardContentEnum.Remote;
                }
            }

            DestSelectorHelper.UpdateToolStripDest(tsddbClipboardContent);
        }

        private void tsddbLinkFormat_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RestrictToOneCheck(tsddbLinkFormat, e);
        }
    }
}