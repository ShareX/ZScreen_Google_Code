using System;
using System.ComponentModel;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenCLI
{
    public partial class Form1 : GenericMain
    {
        public Form1(WorkerTask task)
        {
            InitializeComponent();
            new BalloonTipHelper(this.niTray, task).ShowBalloonTip();
        }

        private void niTray_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void niTray_BalloonTipClosed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (ZScreenLib.Engine.conf.BalloonTipOpenLink)
            {
                NotifyIcon ni = (NotifyIcon)sender;
                new BalloonTipHelper(ni).ClickBalloonTip();
            }
            this.Close();
        }

        #region Worker

        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new DoWorkEventHandler(bwApp_DoWork);
            bwApp.ProgressChanged += new ProgressChangedEventHandler(bwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
            return bwApp;
        }

        void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void bwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void bwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}