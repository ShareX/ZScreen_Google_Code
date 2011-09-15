using System.Windows.Forms;
using UploadersAPILib;
using UploadersLib;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    public partial class DestSelector : UserControl
    {
        Timer tmrDropDownClose = new Timer() { Interval = 5000 };

        public DestSelector()
        {
            InitializeComponent();
        }

        private void tsbDestConfig_Click(object sender, System.EventArgs e)
        {
            UploadersConfigForm form = new UploadersConfigForm(Engine.MyWorkflow.OutputsConfig, ZKeys.GetAPIKeys());
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

        public ToolStripMenuItem GetOutputTsmi(ToolStripDropDownButton tsddb, OutputEnum et)
        {
            foreach (ToolStripMenuItem tsmi in tsddb.DropDownItems)
            {
                if ((OutputEnum)tsmi.Tag == et)
                {
                    return tsmi;
                }
            }
            return new ToolStripMenuItem();
        }

        public ToolStripMenuItem GetClipboardContentTsmi(ToolStripDropDownButton tsddb, ClipboardContentEnum et)
        {
            foreach (ToolStripMenuItem tsmi in tsddb.DropDownItems)
            {
                if ((ClipboardContentEnum)tsmi.Tag == et)
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
            ToolStripMenuItem tsmiORemoteHost = GetOutputTsmi(tsddbOutputs, OutputEnum.RemoteHost);
            ToolStripMenuItem tsmiOSharedFolder = GetOutputTsmi(tsddbOutputs, OutputEnum.SharedFolder);

            ToolStripMenuItem tsmiCCData = GetClipboardContentTsmi(tsddbClipboardContent, ClipboardContentEnum.Data);
            ToolStripMenuItem tsmiCCLocal = GetClipboardContentTsmi(tsddbClipboardContent, ClipboardContentEnum.Local);
            ToolStripMenuItem tsmiCCRemote = GetClipboardContentTsmi(tsddbClipboardContent, ClipboardContentEnum.Remote);

            tsmiCCLocal.Enabled = tsmiOLocalDisk.Checked;
            if (!tsmiCCLocal.Enabled)
            {
                // if data is not stored in Local Disk then nothing file path related can be stored in Clipboard
                tsmiCCLocal.Checked = false;
            }

            tsmiCCRemote.Enabled = tsmiORemoteHost.Checked || tsmiOSharedFolder.Checked;
            if (!tsmiCCRemote.Enabled)
            {
                // if data is not stored in Remote Host then nothing URL related can be stored in Clipboard
                tsmiCCRemote.Checked = false;
            }

            tsddbDestImage.Enabled = tsmiORemoteHost.Checked && tsmiCCRemote.Enabled;
            tsddDestFile.Enabled = tsmiORemoteHost.Checked && tsmiCCRemote.Enabled;
            tsddDestText.Enabled = tsmiORemoteHost.Checked && tsmiCCRemote.Enabled;
            tsddbLinkFormat.Enabled = tsmiOClipboard.Checked && !tsmiCCData.Checked;
            tsddbDestLink.Enabled = tsmiORemoteHost.Checked && tsmiCCRemote.Enabled;

            tsddbClipboardContent.Enabled = tsmiOClipboard.Checked;

            DestSelectorHelper.UpdateToolStripDest(tsddbClipboardContent);
        }

        private void tsddbLinkFormat_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RestrictToOneCheck(tsddbLinkFormat, e);
        }

        private void DestSelector_Load(object sender, System.EventArgs e)
        {
            foreach (ToolStripItem tsi in tsDest.Items)
            {
                if (tsi is ToolStripDropDownButton)
                {
                    ToolStripDropDownButton tsddb = tsi as ToolStripDropDownButton;
                    tsddb.MouseHover += new System.EventHandler(tsddb_MouseHover);
                    if (!Engine.AppConf.SupportMultipleDestinations)
                    {
                        tsddb.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddb_DropDownItemClickedRestrictToOneItem);
                    }
                }
            }
        }

        private void tsddb_DropDownItemClickedRestrictToOneItem(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripDropDownButton tsddb = sender as ToolStripDropDownButton;
            RestrictToOneCheck(tsddb, e);
        }

        private void tsddb_MouseHover(object sender, System.EventArgs e)
        {
            ToolStripDropDownButton tsddb = sender as ToolStripDropDownButton;

            foreach (ToolStripItem tsi in tsDest.Items)
            {
                if (tsi is ToolStripDropDownButton)
                {
                    ToolStripDropDownButton tsddb2 = tsi as ToolStripDropDownButton;
                    if (tsddb.Text != tsddb2.Text)
                    {
                        tsddb.DropDown.Close();
                    }
                }
            }

            tsddb.ShowDropDown();
            tsddb.DropDown.AutoClose = false;
        }

        private void tsDest_MouseLeave(object sender, System.EventArgs e)
        {
            tmrDropDownClose.Tick += new System.EventHandler(tmrDropDownClose_Tick);
            tmrDropDownClose.Start();
        }

        private void tmrDropDownClose_Tick(object sender, System.EventArgs e)
        {
            foreach (ToolStripItem tsi in tsDest.Items)
            {
                if (tsi is ToolStripDropDownButton)
                {
                    ToolStripDropDownButton tsddb = tsi as ToolStripDropDownButton;
                    tsddb.DropDown.Close();
                }
            }
        }
    }
}