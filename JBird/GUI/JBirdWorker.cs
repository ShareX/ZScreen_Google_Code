using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZScreenLib;
using HelpersLib;

namespace JBirdGUI
{
    public partial class JBirdMain : HotkeyForm
    {

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
            
            // process outputs e.g. upload

            e.Result = bwTask;
        }

        private void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // update gui
        }

        private void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask bwTask = e.Result as WorkerTask;

            UploadManager.ShowUploadResults(bwTask, true);
        }

        public WorkerTask CreateTask(Profile profile)
        {
            WorkerTask tempTask = new WorkerTask(CreateWorker(), profile);
            return tempTask;
        }

        public void CaptureEntireScreen()
        {
            WorkerTask esTask = CreateTask(Program.GetProfile(WorkerTask.JobLevel2.CaptureEntireScreen));
            esTask.WasToTakeScreenshot = true;
            esTask.RunWorker();
        }

        public void CaptureActiveWindow()
        {
            
        }

    }
}
