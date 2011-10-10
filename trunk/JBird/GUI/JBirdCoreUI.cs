using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using UploadersLib;
using ZScreenLib;

namespace JBirdGUI
{
    public partial class JBirdCoreUI : HotkeyForm
    {
        public JBirdCoreUI()
        {
            InitializeComponent();
            this.Text = Application.ProductName + " " + Application.ProductVersion;
            this.niApp.Text = this.Text;
        }

        #region Helper Methods

        protected void HideFormTemporary(MethodInvoker method, int executeTime = 500, int showTime = 2000)
        {
            var timer = new System.Windows.Forms.Timer { Interval = executeTime };
            var timer2 = new System.Windows.Forms.Timer { Interval = showTime };

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                method();
                timer2.Start();
            };

            timer2.Tick += (sender, e) =>
            {
                timer2.Stop();
                NativeMethods.ShowWindow(Handle, (int)WindowShowStyle.ShowNormal);
            };

            Hide();
            timer.Start();
        }

        protected void ExecuteTimer(MethodInvoker method, ToolStripItem control, int executeTime = 3000)
        {
            var timer = new System.Windows.Forms.Timer { Interval = executeTime };

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                method();
                control.Enabled = true;
            };

            control.Enabled = false;
            timer.Start();
        }

        protected void bwConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.WorkflowConfig = WorkflowConfig.Read();
        }

        protected void bwConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Program.WorkflowConfig != null)
            {
                if (Program.WorkflowConfig.Workflows98.Count == 0)
                {
                    Program.WorkflowConfig.Workflows98.AddRange(CreateDefaultWorkflows());
                }
                if (Program.HotkeyMgrs.Count == 0)
                {
                    Program.HotkeysUpdate();
                }
                Load_TrayMenuItems();
            }
        }

        public List<Workflow> CreateDefaultWorkflows()
        {
            List<Workflow> workflows = new List<Workflow>();

            Workflow entireScreen = new Workflow("Desktop to file");
            entireScreen.Job = WorkerTask.JobLevel2.CaptureEntireScreen;
            entireScreen.Outputs.Add(OutputEnum.Clipboard);
            entireScreen.Outputs.Add(OutputEnum.LocalDisk);
            entireScreen.Hotkey = new HelpersLib.Hotkey.HotkeySetting(Keys.PrintScreen);
            workflows.Add(entireScreen);

            Workflow activeWindow = new Workflow("Active Window to clipboard");
            activeWindow.Job = WorkerTask.JobLevel2.CaptureActiveWindow;
            activeWindow.Outputs.Add(OutputEnum.Clipboard);
            activeWindow.Hotkey = new HelpersLib.Hotkey.HotkeySetting(Keys.Alt | Keys.PrintScreen);
            workflows.Add(activeWindow);

            return workflows;
        }

        protected void Load_TrayMenuItems()
        {
            if (tsmiWorkflows.DropDownItems.Count == 0)
            {
                foreach (Workflow p in Program.WorkflowConfig.Workflows98)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(p.Description);
                    tsmi.Tag = p;
                    tsmi.Click += new System.EventHandler(tsmi_Click);
                    tsmiWorkflows.DropDownItems.Add(tsmi);
                }
            }
        }

        protected void StartWorkflow(Workflow p)
        {
            switch (p.Job)
            {
                case WorkerTask.JobLevel2.AutoCapture:
                    break;
                case WorkerTask.JobLevel2.CaptureActiveWindow:
                    HideFormTemporary(() => CaptureActiveWindow(), 3000);
                    break;
                case WorkerTask.JobLevel2.CaptureEntireScreen:
                    CaptureEntireScreen();
                    break;
            }
        }

        #endregion Helper Methods

        #region Task Methods

        public WorkerTask CreateTask(Workflow profile)
        {
            WorkerTask tempTask = new WorkerTask(CreateWorker(), profile);
            return tempTask;
        }

        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwTask_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwTask_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwTask_RunWorkerCompleted);
            return bwApp;
        }

        public void BwTask_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask bwTask = e.Argument as WorkerTask;

            if (bwTask.WorkflowConfig.Outputs.Contains(UploadersLib.OutputEnum.RemoteHost))
            {
                bwTask.PublishData();
            }

            e.Result = bwTask;
        }

        protected void BwTask_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // update gui
        }

        protected void BwTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask bwTask = e.Result as WorkerTask;

            UploadManager.ShowUploadResults(bwTask, true);
            bwTask.Dispose();
        }

        #endregion Task Methods

        #region Capture Methods

        public void CaptureEntireScreen()
        {
            Workflow wfCaptureScreen = Program.GetProfile(WorkerTask.JobLevel2.CaptureEntireScreen);
            if (wfCaptureScreen != null)
            {
                WorkerTask esTask = CreateTask(wfCaptureScreen);
                esTask.WasToTakeScreenshot = true;
                esTask.RunWorker();
            }
        }

        public void CaptureActiveWindow()
        {
            Workflow wfActiveWindow = Program.GetProfile(WorkerTask.JobLevel2.CaptureActiveWindow);
            if (wfActiveWindow != null)
            {
                WorkerTask awTask = CreateTask(wfActiveWindow);
                awTask.WasToTakeScreenshot = true;
                awTask.RunWorker();
            }
        }

        #endregion Capture Methods

        #region Capture Events

        protected virtual void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureEntireScreen());
        }

        protected virtual void btnCaptureActiveWindow_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureActiveWindow(), 3000);
        }

        protected virtual void btnWorkflows_Click(object sender, EventArgs e)
        {
            if (Program.WorkflowConfig != null)
            {
                WorkflowManager pm = new WorkflowManager(Program.WorkflowConfig.Workflows98) { Icon = this.Icon };
                pm.ShowDialog();
            }
        }

        #endregion Capture Events

        #region Form Events

        private void tsmi_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            Workflow p = tsmi.Tag as Workflow;
            StartWorkflow(p);

        }
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();

        }
        protected void JBirdCoreUI_Shown(object sender, EventArgs e)
        {

        }

        protected void JBirdCoreUI_Load(object sender, EventArgs e)
        {
            BackgroundWorker bwConfig = new BackgroundWorker();
            bwConfig.DoWork += new DoWorkEventHandler(bwConfig_DoWork);
            bwConfig.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwConfig_RunWorkerCompleted);
            bwConfig.RunWorkerAsync();
        }

        protected void JBirdCoreUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.WorkflowConfig.Write();
        }

        #endregion Form Events
    }
}