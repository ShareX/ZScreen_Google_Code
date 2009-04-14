using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZSS.Forms
{
    public partial class AutoScreenshots : Form
    {
        public event JobsEventHandler EventJob;
        public bool IsRunning;

        private Timer timer = new Timer();
        private Tasks.MainAppTask.Jobs job;
        private int delay;
        private bool waitUploads;

        public AutoScreenshots()
        {
            InitializeComponent();
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (waitUploads && ClipboardManager.Workers > 0)
            {
                timer.Interval = 1000;
            }
            else
            {
                timer.Interval = delay;
                EventJob(this, job);
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                IsRunning = false;
                btnExecute.Text = "Start";
            }
            else
            {
                IsRunning = true;
                btnExecute.Text = "Stop";
                switch (cbScreenshotTypes.SelectedText)
                {
                    case "Entire Screen":
                        job = Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN;
                        break;
                    case "Active Window":
                        job = Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE;
                        break;
                    case "Last Crop Shot":
                        job = Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED;
                        break;
                }
                timer.Interval = 1000;
                delay = (int)(nudDelay.Value * 1000);
                waitUploads = cbWaitUploads.Checked;
            }
            timer.Enabled = IsRunning;
        }
    }
}