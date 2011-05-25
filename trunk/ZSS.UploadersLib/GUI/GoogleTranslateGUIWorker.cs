using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using UploadersLib.OtherServices;

namespace UploadersLib
{
    public partial class GoogleTranslateGUI : Form
    {
        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new ProgressChangedEventHandler(bwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
            return bwApp;
        }

        public void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            GoogleTranslateInfo gti = e.Argument as GoogleTranslateInfo;
            gti = new GoogleTranslate(APIKeys.GoogleTranslateKey).TranslateText(gti);
            e.Result = gti;
        }

        private void bwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GoogleTranslateInfo gti = e.Result as GoogleTranslateInfo;
            UpdateGoogleTranslateGUI(gti);
        }

        private void UpdateGoogleTranslateGUI(GoogleTranslateInfo info)
        {
            btnTranslate.Enabled = true;
            btnTranslateTo.Enabled = true;

            txtTranslateText.Text = info.Text;
            txtLanguages.Text = info.SourceLanguage + " -> " + info.TargetLanguage;
            txtTranslateResult.Text = info.Result;
        }

    }
}
