using System.Windows.Forms;
using UploadersAPILib;
using UploadersLib;
using ZScreenLib.Properties;

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
            UploadersConfigForm form = new UploadersConfigForm(Engine.MyUploadersConfig, ZKeys.GetAPIKeys());
            form.Icon = Resources.zss_main;
            form.Show();
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

        private ToolStripMenuItem GetOutputTsmi(ToolStripDropDownButton tsddb, OutputEnum o)
        {
            foreach (ToolStripMenuItem tsmi in tsddb.DropDownItems)
            {
                if ((OutputEnum)tsmi.Tag == o)
                {
                    return tsmi;
                }
            }
            return new ToolStripMenuItem();
        }

        public void EnableDisableDestControls(ToolStripItemClickedEventArgs e = null)
        {
            ToolStripMenuItem tsmiOClipboard = GetOutputTsmi(tsddbOutputs, OutputEnum.Clipboard);
            ToolStripMenuItem tsmiOLocalDisk = GetOutputTsmi(tsddbOutputs, OutputEnum.LocalDisk);
            ToolStripMenuItem tsmiORemote = GetOutputTsmi(tsddbOutputs, OutputEnum.RemoteHost);

            tsddbClipboardContent.Enabled = tsmiOClipboard.Checked;

            for (int i = 0; i < tsddbClipboardContent.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbClipboardContent.DropDownItems[i];
                ClipboardContentEnum cct = (ClipboardContentEnum)tsmi.Tag;
                if (cct == ClipboardContentEnum.Local)
                {
                    // if data is not stored in Local Disk then nothing file path related can be stored in Clipboard
                    tsmi.Enabled = tsmiOLocalDisk.Checked;
                    if (!tsmi.Enabled)
                    {
                        tsmi.Checked = false;
                    }
                }
                if (cct == ClipboardContentEnum.Remote)
                {
                    // if data is not stored in Remote Host then nothing URL related can be stored in Clipboard
                    tsmi.Enabled = tsmiORemote.Checked;
                    if (!tsmi.Enabled)
                    {
                        tsmi.Checked = false;
                    }
                }
                if (tsmi.Checked)
                {
                    tsddbDestImage.Enabled = tsmiORemote.Checked && cct == ClipboardContentEnum.Remote;
                    tsddbLinkFormat.Enabled = tsmiORemote.Checked && cct != ClipboardContentEnum.Data;
                    tsddDestFile.Enabled = tsmiORemote.Checked && cct == ClipboardContentEnum.Remote;
                    tsddDestText.Enabled = tsmiORemote.Checked && cct == ClipboardContentEnum.Remote;
                    tsddbDestLink.Enabled = tsmiORemote.Checked && cct == ClipboardContentEnum.Remote;
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