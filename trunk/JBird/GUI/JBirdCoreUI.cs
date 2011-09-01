using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using HelpersLib;
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

        protected void JBirdCoreUI_Load(object sender, EventArgs e)
        {
            BackgroundWorker bwConfig = new BackgroundWorker();
            bwConfig.DoWork += new DoWorkEventHandler(bwConfig_DoWork);
            bwConfig.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwConfig_RunWorkerCompleted);
            bwConfig.RunWorkerAsync();

            this.Text = Application.ProductName + " " + Application.ProductVersion;
        }

        protected void bwConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.ProfilesConfig = ProfileSettings.Read();
        }

        protected void bwConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Program.ProfilesConfig.Profiles.Count == 0)
            {
                Program.ProfilesConfig.Profiles.AddRange(CreateDefaultProfiles());
            }
            Load_TrayMenuItems();
        }

        protected void Load_TrayMenuItems()
        {
            foreach (Workflow p in Program.ProfilesConfig.Profiles)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(p.Description);
                tsmi.Tag = p;
                tsmi.Click += new System.EventHandler(tsmi_Click);
                tsmiWorkflows.DropDownItems.Add(tsmi);
            }
        }

        private void tsmi_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            Workflow p = tsmi.Tag as Workflow;
            StartWorkflow(p);
        }

        public List<Workflow> CreateDefaultProfiles()
        {
            List<Workflow> profiles = new List<Workflow>();

            Workflow entireScreen = new Workflow("Desktop to file");
            entireScreen.Job = WorkerTask.JobLevel2.CaptureEntireScreen;
            entireScreen.Outputs.Add(OutputEnum.Clipboard);
            entireScreen.Outputs.Add(OutputEnum.LocalDisk);
            profiles.Add(entireScreen);

            Workflow activeWindow = new Workflow("Active Window to clipboard");
            activeWindow.Job = WorkerTask.JobLevel2.CaptureActiveWindow;
            activeWindow.Outputs.Add(OutputEnum.Clipboard);
            profiles.Add(activeWindow);

            return profiles;
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

        public WorkerTask CreateTask(Workflow profile)
        {
            WorkerTask tempTask = new WorkerTask(CreateWorker(), profile);
            return tempTask;
        }

        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwApp_RunWorkerCompleted);
            return bwApp;
        }

        public void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask bwTask = e.Argument as WorkerTask;

            if (bwTask.Profile.Outputs.Contains(UploadersLib.OutputEnum.RemoteHost))
            {
                bwTask.PublishData();
            }

            e.Result = bwTask;
        }

        protected void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // update gui
        }

        protected void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask bwTask = e.Result as WorkerTask;

            UploadManager.ShowUploadResults(bwTask, true);
            bwTask.Dispose();
        }

        public void CaptureEntireScreen()
        {
            WorkerTask esTask = CreateTask(Program.GetProfile(WorkerTask.JobLevel2.CaptureEntireScreen));
            esTask.WasToTakeScreenshot = true;
            esTask.RunWorker();
        }

        public void CaptureActiveWindow()
        {
            WorkerTask awTask = CreateTask(Program.GetProfile(WorkerTask.JobLevel2.CaptureActiveWindow));
            awTask.WasToTakeScreenshot = true;
            awTask.RunWorker();
        }

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
                NativeMethods.ShowWindow(Handle, (int)NativeMethods.WindowShowStyle.ShowNormalNoActivate);
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
            ProfileManager pm = new ProfileManager(Program.ProfilesConfig.Profiles) { Icon = this.Icon };
            pm.ShowDialog();
        }

        protected void JBirdCoreUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.ProfilesConfig.Write();
        }

        protected void JBirdCoreUI_Shown(object sender, EventArgs e)
        {
        }
    }
}