using System;
using System.Windows.Forms;

namespace ZSS.Forms
{
    public partial class ToolbarWindow : Form
    {
        public event JobsEventHandler EventJob;

        public ToolbarWindow()
        {
            InitializeComponent();
            User32.ActivateWindow(this.Handle);
        }

        private void DoJob(object sender, Tasks.MainAppTask.Jobs e)
        {
            EventJob(sender, e);
            if (Program.conf.CloseQuickActions)
            {
                this.Close();
            }
        }

        private void tsbEntireScreen_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN);
        }

        private void tsbSelectedWindow_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED);
        }

        private void tsbCropShot_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED);
        }

        private void tsbLastCropShot_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED);
        }

        private void tsbAutoCapture_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.AUTO_CAPTURE);
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD);
        }

        private void tsbDragDropWindow_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.PROCESS_DRAG_N_DROP);
        }

        private void tsbLanguageTranslator_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.LANGUAGE_TRANSLATOR);
        }

        private void tsbScreenColorPicker_Click(object sender, EventArgs e)
        {
            DoJob(this, Tasks.MainAppTask.Jobs.SCREEN_COLOR_PICKER);
        }

        private void tsQuickActions_MouseEnter(object sender, EventArgs e)
        {
            User32.SetActiveWindow(this.Handle);
        }

        private void ToolbarWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.conf.ActionToolbarLocation = this.Location;
        }
    }
}