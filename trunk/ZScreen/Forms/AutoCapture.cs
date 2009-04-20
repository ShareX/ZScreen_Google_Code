using System;
using System.Windows.Forms;
using System.Diagnostics;
using ZSS.Tasks;

namespace ZSS.Forms
{
    public partial class AutoCapture : Form
    {
        public event JobsEventHandler EventJob;
        public bool IsRunning;

        private Timer timer = new Timer();
        private Timer statusTimer = new Timer { Interval = 250 };
        private MainAppTask.Jobs mJob;
        private int mDelay;
        private bool waitUploads;
        private int count;
        private int timeleft;
        private int percentage;
        private Stopwatch stopwatch = new Stopwatch();

        public AutoCapture()
        {
            InitializeComponent();
            timer.Tick += TimerTick;
            statusTimer.Tick += StatusTimerTick;
            LoadSettings();
        }

        private void LoadSettings()
        {
            cbScreenshotTypes.Items.AddRange(typeof(AutoScreenshotterJobs).GetDescriptions());
            cbScreenshotTypes.SelectedIndex = (int)Program.conf.AutoCaptureScreenshotTypes;
            nudDelay.Time = Program.conf.AutoCaptureDelayTimes;
            nudDelay.Value = Program.conf.AutoCaptureDelayTime;
            cbAutoMinimize.Checked = Program.conf.AutoCaptureAutoMinimize;
            cbWaitUploads.Checked = Program.conf.AutoCaptureWaitUploads;
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
                tspbBar.Value = 0;
                btnExecute.Text = "Start";
            }
            else
            {
                IsRunning = true;
                btnExecute.Text = "Stop";
                switch (Program.conf.AutoCaptureScreenshotTypes)
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
                mDelay = (int)Program.conf.AutoCaptureDelayTime;
                waitUploads = Program.conf.AutoCaptureWaitUploads;
                if (Program.conf.AutoCaptureAutoMinimize) this.WindowState = FormWindowState.Minimized;
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
            timeleft = Math.Max(0, mDelay - (int)stopwatch.ElapsedMilliseconds);
            percentage = (int)(100 - (double)timeleft / mDelay * 100);
            tsslStatus.Text = " Timeleft: " + timeleft + "ms (" + percentage + "%)";
            this.Text = "Auto Capture - Count: " + count;
        }

        private void cbScreenshotTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.AutoCaptureScreenshotTypes = (AutoScreenshotterJobs)cbScreenshotTypes.SelectedIndex;
        }

        private void cbAutoMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoCaptureAutoMinimize = cbAutoMinimize.Checked;
        }

        private void cbWaitUploads_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoCaptureWaitUploads = cbWaitUploads.Checked;
        }

        private void nudDelay_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.AutoCaptureDelayTime = nudDelay.Value;
        }

        private void nudDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.AutoCaptureDelayTimes = nudDelay.Time;
        }
    }
}