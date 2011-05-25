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
        public void TranslateFromTextBox()
        {
            StartBW_LanguageTranslator(new GoogleTranslateInfo
            {
                Text = txtTranslateText.Text,
                SourceLanguage = Config.GoogleAutoDetectSource ? null : Config.GoogleSourceLanguage,
                TargetLanguage = Config.GoogleTargetLanguage
            });
        }

        public void TranslateTo1()
        {
            if (Config.GoogleTargetLanguage2 == "?")
            {
                lblToLanguage.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("Drag n drop 'To:' label to this button for be able to set button language.", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblToLanguage.BorderStyle = BorderStyle.None;
            }
            else
            {
                TranslateFromTextBox();
            }
        }

        public void StartBW_LanguageTranslator(GoogleTranslateInfo gti)
        {
            btnTranslate.Enabled = false;
            btnTranslateTo1.Enabled = false;
            CreateWorker().RunWorkerAsync(gti);
        }

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
            FillGoogleTranslateInfo(gti);
        }

        private void FillGoogleTranslateInfo(GoogleTranslateInfo info)
        {
            btnTranslate.Enabled = true;
            btnTranslateTo1.Enabled = true;

            txtTranslateText.Text = info.Text;
            txtLanguages.Text = info.SourceLanguage + " -> " + info.TargetLanguage;
            txtTranslateResult.Text = info.Result;
        }

    }
}
