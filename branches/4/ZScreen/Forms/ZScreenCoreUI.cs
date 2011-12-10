using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Properties;
using UploadersLib.OtherServices;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreenCoreUI : HotkeyForm
    {
        public bool IsReady;

        public ZScreenCoreUI()
        {
            InitializeComponent();
            tsCoreMainTab.Visible = false;
        }

        private void HideFormTemporary(MethodInvoker method, int executeTime = 500, int showTime = 2000)
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
                NativeMethods.ShowWindow(Handle, (int)WindowShowStyle.ShowNormalNoActivate);
            };

            Hide();
            timer.Start();
        }

        private void ExecuteTimer(MethodInvoker method, ToolStripItem control, int executeTime = 3000)
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

        public virtual WorkerTask CreateTask(WorkerTask.JobLevel2 job, TaskInfo tiCreateTask = null)
        {
            return new WorkerTask(Engine.ConfigWorkflow);
        }

        #region Worker Events

        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwApp_RunWorkerCompleted);
            return bwApp;
        }

        public virtual void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            // override by inherited form
        }

        public virtual void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // override by inherited form
        }

        public virtual void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // override by inherited form
        }

        #endregion Worker Events

        #region ToolStrip methods

        public virtual void CaptureEntireScreen() { }

        public virtual void CaptureActiveMonitor() { }

        public virtual void CaptureActiveWindow() { }

        public virtual void CaptureSelectedWindow() { }

        public virtual void CaptureSelectedWindowGetList() { }

        public virtual void CaptureRectRegion() { }

        public virtual void CaptureRectRegionClipboard() { }

        public virtual void CaptureRectRegionLast() { }

        public virtual void CaptureFreeHandRegion() { }

        public virtual void ShowAutoCapture() { }

        public virtual void FileUpload() { }

        public virtual void ClipboardUpload() { }

        public virtual void ShowDropWindow() { }

        public virtual void ShowGTGUI() { }

        public virtual void ShowScreenColorPicker() { }

        private void tsbFullscreenCapture_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureEntireScreen());
        }

        private void tsbActiveWindow_Click(object sender, EventArgs e)
        {
            ExecuteTimer(() => CaptureActiveWindow(), tsbActiveWindow);
        }

        private void tsbSelectedWindow_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureSelectedWindow());
        }

        private void tsddbSelectedWindow_DropDownOpening(object sender, EventArgs e)
        {
            CaptureSelectedWindowGetList();
        }

        private void tsbCropShot_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureRectRegion());
        }

        private void tsbLastCropShot_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureRectRegionLast());
        }

        private void tsbFreehandCropShot_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureFreeHandRegion());
        }

        private void tsbAutoCapture_Click(object sender, EventArgs e)
        {
            ShowAutoCapture();
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
            FileUpload();
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            ClipboardUpload();
        }

        private void tsbDragDropWindow_Click(object sender, EventArgs e)
        {
            ShowDropWindow();
        }

        private void tsbLanguageTranslator_Click(object sender, EventArgs e)
        {
            ShowGTGUI();
        }

        private void tsbScreenColorPicker_Click(object sender, EventArgs e)
        {
            ShowScreenColorPicker();
        }

        private void tsbOpenHistory_Click(object sender, EventArgs e)
        {
            OpenHistory(sender, e);
        }

        private void tsbImageDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(ZLinks.URL_DONATE_ZS);
        }

        #endregion ToolStrip methods

        #region Helper Methods

        protected void ShowDirectory(string dir)
        {
            Process.Start("explorer.exe", dir);
        }

        public static void OpenHistory(object sender = null, EventArgs e = null)
        {
            // if Engine.conf is null then open use default amount
            int maxNum = 100;
            if (Engine.ConfigUI != null)
            {
                maxNum = Engine.ConfigOptions.HistoryMaxNumber;
            }
            new HistoryLib.HistoryForm(Engine.HistoryPath, maxNum, string.Format("{0} - History", Engine.GetProductName())).Show();
        }

        #endregion Helper Methods
    }
}