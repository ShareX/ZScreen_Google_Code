using System;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreenSnap : ZScreenCoreUI
    {
        public ZScreenSnap()
        {
            InitializeComponent();
            tsCoreMainTab.Visible = true;
        }

        #region Main ToolStrip Events

        private void tsbFullscreenCapture_Click(object sender, EventArgs e)
        {
        }

        private void tsbActiveWindow_Click(object sender, EventArgs e)
        {
        }

        private void tsbSelectedWindow_Click(object sender, EventArgs e)
        {
        }

        private void tsbCropShot_Click(object sender, EventArgs e)
        {
        }

        private void tsbLastCropShot_Click(object sender, EventArgs e)
        {
        }

        private void tsbFreehandCropShot_Click(object sender, EventArgs e)
        {
        }

        private void tsbAutoCapture_Click(object sender, EventArgs e)
        {
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
        }

        private void tsbDragDropWindow_Click(object sender, EventArgs e)
        {
        }

        private void tsbScreenColorPicker_Click(object sender, EventArgs e)
        {
        }

        private void tsbOpenHistory_Click(object sender, EventArgs e)
        {
        }

        private void tsbImageDirectory_Click(object sender, EventArgs e)
        {
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
        }

        #endregion Main ToolStrip Events

        private void ZScreenSnap_Load(object sender, EventArgs e)
        {
            Engine.LoadSettings();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowOptionsUI();
        }

        #region ToolStrip Helper Methods

        public override WorkerTask CreateTask(WorkerTask.JobLevel2 job, TaskInfo tiCreateTask = null)
        {
            if (tiCreateTask == null) tiCreateTask = new TaskInfo();

            tiCreateTask.Job = job;

            WorkerTask createTask = new WorkerTask(CreateWorker(), tiCreateTask);

            return createTask;
        }

        #endregion ToolStrip Helper Methods

        #region ToolStrip Methods

        public override void CaptureActiveWindow()
        {
            WorkerTask hkawTask = CreateTask(WorkerTask.JobLevel2.CaptureActiveWindow);
            hkawTask.PublishData();
        }

        #endregion ToolStrip Methods
    }
}