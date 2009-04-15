using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ZSS.Tasks;

namespace ZSS.Forms
{

    public enum AutoScreenshotterJobs
    {
        [Description("Entire Screen")]
        TAKE_SCREENSHOT_SCREEN,
        [Description("Active Window")]
        TAKE_SCREENSHOT_WINDOW_ACTIVE,
        [Description("Last Crop Shot")]
        TAKE_SCREENSHOT_LAST_CROPPED
    }

    public partial class AutoCapture : Form
    {
        public event JobsEventHandler EventJob;
        public bool IsRunning;

        private Timer timer = new Timer();
        private Timer statusTimer = new Timer { Interval = 250 };
        private Tasks.MainAppTask.Jobs mJob;
        /// <summary>
        /// Delay in milliseconds
        /// </summary>
        private int mDelay;
        private bool waitUploads;
        private int count;
        private Stopwatch stopwatch = new Stopwatch();

        public AutoCapture()
        {
            InitializeComponent();
            timer.Tick += TimerTick;
            statusTimer.Tick += StatusTimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (waitUploads && ClipboardManager.Workers > 0)
            {
                timer.Interval = 1000;
            }
            else
            {
                StartTimer();
                timer.Interval = mDelay;
                EventJob(this, mJob);
                count++;
            }
        }

        private void StatusTimerTick(object sender, EventArgs e)
        {
            UpdateStatus();
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

                AutoScreenshotterJobs job = (AutoScreenshotterJobs)cbScreenshotTypes.SelectedIndex;
                switch (job)
                {
                    case AutoScreenshotterJobs.TAKE_SCREENSHOT_SCREEN:
                        mJob = MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN;
                        break;
                    case AutoScreenshotterJobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                        mJob = MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE;
                        break;
                    case AutoScreenshotterJobs.TAKE_SCREENSHOT_LAST_CROPPED:
                        mJob = MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED;
                        break;
                }

                timer.Interval = 1000;
                mDelay = (int)(nudDelay.Value * 1000);
                waitUploads = cbWaitUploads.Checked;
                count = 0;
            }
            timer.Enabled = IsRunning;
            statusTimer.Enabled = IsRunning;
        }

        private void StartTimer()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        private void UpdateStatus()
        {
            tspbBar.Maximum = mDelay;
            tspbBar.Value = Math.Min(tspbBar.Maximum, (int)stopwatch.ElapsedMilliseconds);
                        
            tsslStatus.Text = "Count: " + count;
        }

        private void AutoScreenshots_Load(object sender, EventArgs e)
        {
            cbScreenshotTypes.Items.AddRange(typeof(AutoScreenshotterJobs).GetDescriptions());
            cbScreenshotTypes.SelectedIndex = 0;
        }
    }
}