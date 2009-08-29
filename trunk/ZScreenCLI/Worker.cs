using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ZScreenLib;

namespace ZScreenCLI
{
    public class Worker
    {
        private GenericMain GUI = null;

        public Worker(GenericMain gui)
        {
            this.GUI = gui;
        }

        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new DoWorkEventHandler(bwApp_DoWork);
            bwApp.ProgressChanged += new ProgressChangedEventHandler(bwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
            return bwApp;
        }

        void bwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            // throw new NotImplementedException();
        }

        void bwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //  throw new NotImplementedException();
        }

        void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.GUI.Close();
        }
    }
}
