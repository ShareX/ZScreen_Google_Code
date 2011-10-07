using System;
using System.IO;
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
            UploadersConfigForm form = new UploadersConfigForm(Engine.Workflow.OutputsConfig, ZKeys.GetAPIKeys());
            form.Icon = Resources.zss_main;
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReconfigOutputsUI();
            }
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
            // ToolStripMenuItem tsmiCCOCR = GetClipboardContentTsmi(tsddbClipboardContent, ClipboardContentEnum.OCR);

            /*
            string mspView = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), @"Microsoft Shared\MODI\12.0\MSPVIEW.EXE");
            tsmiCCOCR.Enabled = File.Exists(mspView);
            */

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
            tsddbDestFile.Enabled = tsmiORemoteHost.Checked && tsmiCCRemote.Enabled;
            tsddbDestText.Enabled = tsmiORemoteHost.Checked && tsmiCCRemote.Enabled;
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

        public void DropDownMenusClose()
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

        #region ConfigGUI

        public bool ReconfigOutputsUI()
        {
            // Outputs > Files
            bool bHasValidFileUploader = HasValidFileUploader();

            // Outputs > Text
            bool bHasValidTextUploader = HasValidTextUploader(bHasValidFileUploader);

            // Outputs > Images
            bool bHasValidImageUploader = HasValidImageUploader(bHasValidFileUploader);

            // Outputs
            foreach (ToolStripMenuItem tsmi in tsddbOutputs.DropDownItems)
            {
                OutputEnum ut = (OutputEnum)tsmi.Tag;
                switch (ut)
                {
                    case OutputEnum.RemoteHost:
                        if (!bHasValidTextUploader && !bHasValidImageUploader)
                        {
                            tsmi.Checked = false;
                        }
                        break;
                    case OutputEnum.SharedFolder:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.LocalhostAccountList.Count > 0;
                        if (!tsmi.Enabled)
                        {
                            tsmi.Checked = false;
                        }
                        break;
                }
            }

            return false;
        }

        private bool HasValidImageUploader(bool bHasValidFileUploader)
        {
            foreach (ToolStripMenuItem tsmi in tsddbDestImage.DropDownItems)
            {
                ImageUploaderType ut = (ImageUploaderType)tsmi.Tag;
                switch (ut)
                {
                    case ImageUploaderType.FileUploader:
                        tsmi.Enabled = bHasValidFileUploader;
                        break;
                    case ImageUploaderType.FLICKR:
                        tsmi.Enabled = !string.IsNullOrEmpty(Engine.Workflow.OutputsConfig.FlickrAuthInfo.Token);
                        break;
                    case ImageUploaderType.IMAGESHACK:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.ImageShackAccountType == AccountType.Anonymous ||
                            Engine.Workflow.OutputsConfig.ImageShackAccountType == AccountType.User && !string.IsNullOrEmpty(Engine.Workflow.OutputsConfig.ImageShackRegistrationCode);
                        break;
                    case ImageUploaderType.IMGUR:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.ImgurOAuthInfo != null;
                        break;
                    case ImageUploaderType.MEDIAWIKI:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.MediaWikiAccountList.Count > 0;
                        break;
                    case ImageUploaderType.Photobucket:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.PhotobucketOAuthInfo != null;
                        break;
                    case ImageUploaderType.TINYPIC:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.TinyPicAccountType == AccountType.Anonymous ||
                            Engine.Workflow.OutputsConfig.TinyPicAccountType == AccountType.User &&
                         !string.IsNullOrEmpty(Engine.Workflow.OutputsConfig.TinyPicRegistrationCode);
                        break;
                    case ImageUploaderType.TWITPIC:
                        tsmi.Enabled = !string.IsNullOrEmpty(Engine.Workflow.OutputsConfig.TwitPicPassword);
                        break;
                    case ImageUploaderType.TWITSNAPS:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.TwitterOAuthInfoList.Count > 0;
                        break;
                    case ImageUploaderType.UPLOADSCREENSHOT:
                        break;
                    case ImageUploaderType.YFROG:
                        tsmi.Enabled = !string.IsNullOrEmpty(Engine.Workflow.OutputsConfig.YFrogPassword);
                        break;
                }
            }

            foreach (ToolStripMenuItem tsmi in tsddbDestImage.DropDownItems)
            {
                if (!tsmi.Enabled)
                {
                    tsmi.Checked = false; // if not enabled then we don't need it checked either issue 604
                }
            }

            foreach (ToolStripMenuItem tsmi in tsddbDestImage.DropDownItems)
            {
                if (tsmi.Enabled && tsmi.Checked)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasValidTextUploader(bool bHasValidFileUploader)
        {
            foreach (ToolStripMenuItem tsmi in tsddbDestText.DropDownItems)
            {
                TextUploaderType ut = (TextUploaderType)tsmi.Tag;
                switch (ut)
                {
                    case TextUploaderType.FileUploader:
                        tsmi.Enabled = bHasValidFileUploader;
                        if (!tsmi.Enabled)
                        {
                            tsmi.Checked = false;
                        }
                        break;
                }
            }

            foreach (ToolStripMenuItem tsmi in tsddbDestText.DropDownItems)
            {
                if (!tsmi.Enabled)
                {
                    tsmi.Checked = false; // if not enabled then we don't need it checked either issue 604
                }
            }

            foreach (ToolStripMenuItem tsmi in tsddbDestText.DropDownItems)
            {
                if (tsmi.Enabled && tsmi.Checked)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasValidFileUploader()
        {
            foreach (ToolStripMenuItem tsmi in tsddbDestFile.DropDownItems)
            {
                FileUploaderType ut = (FileUploaderType)tsmi.Tag;
                switch (ut)
                {
                    case FileUploaderType.CustomUploader:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.CustomUploadersList.Count > 0;
                        break;
                    case FileUploaderType.Dropbox:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.DropboxOAuthInfo != null;
                        break;
                    case FileUploaderType.FTP:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.FTPAccountList.Count > 0;
                        break;
                    case FileUploaderType.Minus:
                        tsmi.Enabled = Engine.Workflow.OutputsConfig.MinusConfig.Tokens.Count > 0;
                        break;
                    case FileUploaderType.RapidShare:
                    case FileUploaderType.SendSpace:
                        break;
                }
            }

            foreach (ToolStripMenuItem tsmi in tsddbDestFile.DropDownItems)
            {
                if (!tsmi.Enabled)
                {
                    tsmi.Checked = false; // if not enabled then we don't need it checked either issue 604
                }
            }

            foreach (ToolStripMenuItem tsmi in tsddbDestFile.DropDownItems)
            {
                if (tsmi.Enabled)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion ConfigGUI
    }
}