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

        public AutoScreenshots()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TimerTick);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            EventJob(this, job);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                IsRunning = false;
            }
            else
            {
                IsRunning = true;
                switch (cbScreenshotTypes.SelectedText)
                {
                    case "Entire Screen":
                        job = Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN;
                        break;
                    case "Last Crop Shot":
                        job = Tasks.MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED;
                        break;
                }
                timer.Interval = (int)(nudDelay.Value * 1000);
            }
            timer.Enabled = IsRunning;
        }
    }
}