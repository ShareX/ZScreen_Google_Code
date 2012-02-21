using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HelpersLib;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersLib;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenGUI
{
    public partial class ZScreenOptionsUI : Form
    {
        #region 0 Properties

        private const string FILTER_XML_FILES = "XML Files(*.xml)|*.xml";
        private ZScreenOptions Config = null;
        private List<TreeNode> Nodes = new List<TreeNode>();
        private List<TabPage> TabPages = new List<TabPage>();

        #endregion 0 Properties

        #region 1 Constructors

        public ZScreenOptionsUI(ZScreenOptions config)
        {
            InitializeComponent();

            this.Config = config;

            // General

            chkStartWin.Checked = RegistryHelper.CheckStartWithWindows();
            chkShellExt.Checked = RegistryHelper.CheckShellContextMenu();

            chkOpenMainWindow.Checked = Engine.ConfigApp.ShowMainWindow;

            if (Loader.MainForm.IsReady && !Engine.ConfigApp.ShowInTaskbar)
            {
                this.chkWindows7TaskbarIntegration.Checked = false; // Windows 7 Taskbar Integration cannot work without showing in Taskbar
            }

            cbShowHelpBalloonTips.Checked = Engine.ConfigUI.ShowHelpBalloonTips;
            cbAutoSaveSettings.Checked = Config.AutoSaveSettings;
            chkWindows7TaskbarIntegration.Checked = TaskbarManager.IsPlatformSupported && Engine.ConfigApp.Windows7TaskbarIntegration;
            chkWindows7TaskbarIntegration.Enabled = TaskbarManager.IsPlatformSupported;

            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboMinimizeButtonAction.Items.AddRange(typeof(WindowButtonAction).GetEnumDescriptions());
            }
            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboCloseButtonAction.Items.AddRange(typeof(WindowButtonAction).GetEnumDescriptions());
            }
            cboCloseButtonAction.SelectedIndex = (int)Engine.ConfigApp.WindowButtonActionClose;
            cboMinimizeButtonAction.SelectedIndex = (int)Engine.ConfigApp.WindowButtonActionMinimize;

            chkCheckUpdates.Checked = Engine.ConfigUI.CheckUpdates;
            if (cboReleaseChannel.Items.Count == 0)
            {
                cboReleaseChannel.Items.AddRange(typeof(ReleaseChannelType).GetEnumDescriptions());
                cboReleaseChannel.SelectedIndex = (int)Engine.ConfigUI.ReleaseChannel;
            }

            // Workflow
            nudFlashIconCount.Value = Config.FlashTrayCount;
            chkCaptureFallback.Checked = Config.CaptureEntireScreenOnError;
            chkShowPopup.Checked = Config.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Config.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Config.ShowUploadDuration;
            cbCompleteSound.Checked = Config.CompleteSound;
            chkTwitterEnable.Checked = Config.TwitterEnabled;

            // Directory Indexer
            pgIndexer.SelectedObject = config.IndexerConfig;

            // Effects
            pgWorkflowImageEffects.SelectedObject = Config.ConfigImageEffects;

            // Paths
            ConfigurePaths();

            // History
            nudHistoryMaxItems.Value = Config.HistoryMaxNumber;
            cbHistorySave.Checked = Config.HistorySave;
        }

        #endregion 1 Constructors

        #region 1 Helpers

        private void AddNodesToList(TreeNodeCollection nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Nodes.Add(nodes[i]);
                if (nodes[i].Nodes.Count > 0)
                {
                    AddNodesToList(nodes[i].Nodes);
                }
            }
        }

        #region Backup & Restore

        private void AppSettingsExport()
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = FILTER_XML_FILES };
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dlg.FileName = Engine.SettingsFileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Engine.ConfigUI.Write(dlg.FileName);
            }
        }

        private void AppSettingsImport()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = FILTER_XML_FILES,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLSettings temp = XMLSettings.Read(dlg.FileName);
                temp.FirstRun = false;
                Engine.ConfigUI = temp;
                Loader.MainForm.ZScreen_ConfigGUI();
            }
        }

        private void UploadersConfigExport()
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = FILTER_XML_FILES };
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dlg.FileName = Engine.UploadersConfigFileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Engine.ConfigUploaders.Save(dlg.FileName);
            }
        }

        private void UploadersConfigImport()
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = FILTER_XML_FILES };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                UploadersConfig temp = UploadersConfig.Load(dlg.FileName);
                if (temp != null)
                {
                    Engine.ConfigUploaders = temp;
                }
            }
        }

        private void WorkflowConfigExport()
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = FILTER_XML_FILES };
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dlg.FileName = Engine.WorkflowConfigFileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Engine.ConfigWorkflow.Write(dlg.FileName);
            }
        }

        private void WorkflowConfigImport()
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = FILTER_XML_FILES };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Workflow temp = Workflow.Read(dlg.FileName);
                Engine.ConfigWorkflow = temp;
            }
        }

        #endregion Backup & Restore

        #region Check Updates

        public void CheckUpdates()
        {
            lblUpdateInfo.Text = "Checking for Updates...";
            BackgroundWorker updateThread = new BackgroundWorker { WorkerReportsProgress = true };
            updateThread.DoWork += new DoWorkEventHandler(updateThread_DoWork);
            updateThread.ProgressChanged += new ProgressChangedEventHandler(updateThread_ProgressChanged);
            updateThread.RunWorkerAsync();
        }

        private void updateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            NewVersionWindowOptions nvwo = new NewVersionWindowOptions { MyIcon = Resources.zss_main, MyImage = Resources.main };
            UpdateChecker updateChecker = new UpdateChecker(ZLinks.URL_UPDATE, Application.ProductName,
                new Version(Adapter.AssemblyVersion),
                Engine.ConfigUI.ReleaseChannel, Adapter.CheckProxySettings().GetWebProxy, nvwo);

            updateChecker.CheckUpdate();

            string status;
            if (updateChecker.UpdateInfo.Status == UpdateStatus.UpdateCheckFailed)
            {
                status = "Update check failed";
            }
            else
            {
                status = updateChecker.UpdateInfo.ToString();
            }

            worker.ReportProgress(1, status);
        }

        private void updateThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    lblUpdateInfo.Text = (string)e.UserState;
                    break;
            }
        }

        #endregion Check Updates

        internal void ConfigurePaths()
        {
            Engine.InitializeDefaultFolderPaths(dirCreation: false);

            txtImagesDir.Text = Engine.ImagesDir;
            txtLogsDir.Text = Engine.LogsDir;

            if (Engine.ConfigApp.PreferSystemFolders)
            {
                txtRootFolder.Text = Engine.SettingsDir;
                gbRoot.Text = "Settings";
            }
            else
            {
                txtRootFolder.Text = Engine.ConfigApp.RootDir;
                gbRoot.Text = "Root";
            }

            btnRelocateRootDir.Enabled = !Engine.ConfigApp.PreferSystemFolders;
            gbRoot.Enabled = !Engine.IsPortable;
            gbImages.Enabled = !Engine.IsPortable;
            gbLogs.Enabled = !Engine.IsPortable;

            chkDeleteLocal.Checked = Config.DeleteLocal;
            txtImagesFolderPattern.Text = Engine.ConfigWorkflow.SaveFolderPattern;
        }

        private void LoadSettingsDefault()
        {
            Engine.ConfigUI = new XMLSettings();
            Loader.MainForm.ZScreen_ConfigGUI();
            Engine.ConfigUI.FirstRun = false;
        }

        #endregion 1 Helpers

        private void BtnBrowseImagesDirClick(object sender, EventArgs e)
        {
            string oldDir = txtImagesDir.Text;
            string dirNew = Path.Combine(Adapter.GetDirPathUsingFolderBrowser("Configure Custom Images Directory..."),
                                         "Images");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.ConfigUI.UseCustomImagesDir = true;
                Engine.ConfigUI.CustomImagesDir = dirNew;
                FileSystem.MoveDirectory(oldDir, txtImagesDir.Text);
                ConfigurePaths();
            }
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldRootDir = txtRootFolder.Text;
            string dirNew = Adapter.GetDirPathUsingFolderBrowser("Configure Root directory...");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.SetRootFolder(dirNew);
                txtRootFolder.Text = Engine.ConfigApp.RootDir;
                FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
                ConfigurePaths();
                Engine.ConfigUI = XMLSettings.Read();
                Loader.MainForm.ZScreen_ConfigGUI();
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists(Engine.HistoryPath))
            {
                if (MessageBox.Show(
                    "Do you really want to delete History?\r\nHistory file path: " + Engine.HistoryPath,
                    "ZScreen - History",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(Engine.HistoryPath);
                }
            }
        }

        private void btnDeleteSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to revert settings to default values?", Application.ProductName,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LoadSettingsDefault();
            }
        }

        private void btnMoveImageFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileSystem.ManageImageFolders(Engine.RootImagesDir))
                {
                    MessageBox.Show("Files successfully moved to save folders.");
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex, "Error while moving image files");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOutputsConfigExport_Click(object sender, EventArgs e)
        {
            WorkflowConfigExport();
        }

        private void btnOutputsConfigImport_Click(object sender, EventArgs e)
        {
            WorkflowConfigImport();
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            AppSettingsExport();
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            AppSettingsImport();
        }

        private void btnUploadersConfigExport_Click(object sender, EventArgs e)
        {
            UploadersConfigExport();
        }

        private void btnUploadersConfigImport_Click(object sender, EventArgs e)
        {
            UploadersConfigImport();
        }

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ZAppHelper.OpenFolder(FileSystem.GetImagesDir());
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ZAppHelper.OpenFolder(Engine.LogsDir);
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            ZAppHelper.OpenFolder(txtRootFolder.Text);
        }

        private void cbAutoSaveSettings_CheckedChanged(object sender, EventArgs e)
        {
            Config.AutoSaveSettings = cbAutoSaveSettings.Checked;
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CheckUpdates = chkCheckUpdates.Checked;
        }

        private void cbCloseButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigApp.WindowButtonActionClose = (WindowButtonAction)cboCloseButtonAction.SelectedIndex;
        }

        private void cbCompleteSound_CheckedChanged(object sender, EventArgs e)
        {
            Config.CompleteSound = cbCompleteSound.Checked;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Config.DeleteLocal = chkDeleteLocal.Checked;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Config.HistorySave = cbHistorySave.Checked;
        }

        private void cbMinimizeButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigApp.WindowButtonActionMinimize = (WindowButtonAction)cboMinimizeButtonAction.SelectedIndex;
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigApp.ShowMainWindow = chkOpenMainWindow.Checked;
        }

        private void cboReleaseChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ReleaseChannel = (ReleaseChannelType)cboReleaseChannel.SelectedIndex;
            CheckUpdates();
        }

        private void cbShowHelpBalloonTips_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShowHelpBalloonTips = cbShowHelpBalloonTips.Checked;
            Loader.MainForm.ttZScreen.Active = Engine.ConfigUI.ShowHelpBalloonTips;
        }

        private void cbShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            Config.ShowBalloonTip = chkShowPopup.Checked;
            gbOptionsBalloonTip.Enabled = chkShowPopup.Checked;
        }

        private void cbShowUploadDuration_CheckedChanged(object sender, EventArgs e)
        {
            Config.ShowUploadDuration = cbShowUploadDuration.Checked;
        }

        private void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryHelper.SetStartWithWindows(chkStartWin.Checked);
        }

        private void chkBalloonTipOpenLink_CheckedChanged(object sender, EventArgs e)
        {
            Config.BalloonTipOpenLink = chkBalloonTipOpenLink.Checked;
        }

        private void chkCaptureFallback_CheckedChanged(object sender, EventArgs e)
        {
            Config.CaptureEntireScreenOnError = chkCaptureFallback.Checked;
        }

        private void chkShellExt_CheckedChanged(object sender, EventArgs e)
        {
            RegistryHelper.SetShellContextMenu(chkShellExt.Checked);
        }

        private void chkTwitterEnable_CheckedChanged(object sender, EventArgs e)
        {
            Config.TwitterEnabled = chkTwitterEnable.Checked;
        }

        private void chkWindows7TaskbarIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (Loader.MainForm.IsReady)
            {
                if (chkWindows7TaskbarIntegration.Checked)
                {
                    Engine.ConfigApp.ShowInTaskbar = true;
                    // Application requires to be shown in Taskbar for Windows 7 Integration
                }
                Engine.ConfigApp.Windows7TaskbarIntegration = chkWindows7TaskbarIntegration.Checked;
                Loader.MainForm.ZScreen_Windows7onlyTasks();
            }
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Config.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudHistoryMaxItems_ValueChanged(object sender, EventArgs e)
        {
            Config.HistoryMaxNumber = (int)nudHistoryMaxItems.Value;
        }

        private void tvOptions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tcMain.TabPages.Clear();
            var tabPage = TabPages.Where(x => x.Text == e.Node.Text);
            if (tabPage.Count() > 0)
            {
                tcMain.TabPages.Add(tabPage.First<TabPage>());
            }
        }

        private void txtImagesFolderPattern_TextChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.SaveFolderPattern = txtImagesFolderPattern.Text;
            lblImagesFolderPatternPreview.Text =
                new NameParser(NameParserType.SaveFolder).Convert(Engine.ConfigWorkflow.SaveFolderPattern);
            txtImagesDir.Text = Engine.ImagesDir;
        }

        private void ZScreenOptionsUI_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Options";

            AddNodesToList(tvOptions.Nodes);

            for (int i = 0; i < Nodes.Count; i++)
            {
                TabPages.Add(tcMain.TabPages[i]);
                TabPages[i].Text = Nodes[i].Text;
            }

            tvOptions.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvOptions_NodeMouseClick);

            tcMain.TabPages.Clear();
            tcMain.TabPages.Add(TabPages[0]);

            CheckUpdates();
        }

        private void ZScreenOptionsUI_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void ZScreenOptionsUI_Shown(object sender, EventArgs e)
        {
            tvOptions.ExpandAll();
            tvOptions.Focus(); // automatically selects first node
        }
    }
}