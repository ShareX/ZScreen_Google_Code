using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ZSS.Global;
using ZSS.TextUploadersLib;
using System.Windows.Forms;

namespace ZSS.Helpers
{
    public class WorkerSecondary
    {
       
        private ZScreen mZScreen;

        public WorkerSecondary(ZScreen myZScreen)
        {
            this.mZScreen = myZScreen;
        }

        public void PerformOnlineTasks()
        {
            BackgroundWorker bwOnlineWorker = new BackgroundWorker();
            bwOnlineWorker.DoWork += new DoWorkEventHandler(bwOnlineTasks_DoWork);
            bwOnlineWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwOnlineTasks_RunWorkerCompleted);
            bwOnlineWorker.RunWorkerAsync();
        }

        private void bwOnlineTasks_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Program.mGTranslator = new GoogleTranslate();
                Adapter.UpdateTinyPicShuk();
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
        }

        private void bwOnlineTasks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Program.mGTranslator != null)
            {
                mZScreen.cbFromLanguage.Items.Clear();
                mZScreen.cbToLanguage.Items.Clear();
                foreach (GoogleTranslate.GTLanguage gtLang in Program.mGTranslator.LanguageOptions.SourceLangList)
                {
                    mZScreen.cbFromLanguage.Items.Add(gtLang.Name);
                }
                foreach (GoogleTranslate.GTLanguage gtLang in Program.mGTranslator.LanguageOptions.TargetLangList)
                {
                    mZScreen.cbToLanguage.Items.Add(gtLang.Name);
                }
                SelectLanguage(Program.conf.FromLanguage, Program.conf.ToLanguage, Program.conf.HelpToLanguage);
                GoogleTranslate.GTLanguage secondLang = GoogleTranslate.FindLanguage(Program.conf.ToLanguage2,
                    Program.mGTranslator.LanguageOptions.TargetLangList);
                if (secondLang != null)
                {
                    mZScreen.btnTranslateTo1.Text = "To " + secondLang.Name;
                }
                if (mZScreen.cbFromLanguage.Items.Count > 0) mZScreen.cbFromLanguage.Enabled = true;
                if (mZScreen.cbToLanguage.Items.Count > 0) mZScreen.cbToLanguage.Enabled = true;
            }
            if (!string.IsNullOrEmpty(Program.conf.TinyPicShuk) && Program.conf.TinyPicShuk != mZScreen.txtTinyPicShuk.Text)
            {
                mZScreen.txtTinyPicShuk.Text = Program.conf.TinyPicShuk;
            }
        }


        public void SelectLanguage(string srcLangValue, string targetLangValue, string helpTargetLangValue)
        {
            for (int i = 0; i < Program.mGTranslator.LanguageOptions.SourceLangList.Count; i++)
            {
                if (Program.mGTranslator.LanguageOptions.SourceLangList[i].Value == srcLangValue)
                {
                    if (mZScreen.cbFromLanguage.Items.Count > i) mZScreen.cbFromLanguage.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < Program.mGTranslator.LanguageOptions.TargetLangList.Count; i++)
            {
                if (Program.mGTranslator.LanguageOptions.TargetLangList[i].Value == targetLangValue)
                {
                    if (mZScreen.cbToLanguage.Items.Count > i) mZScreen.cbToLanguage.SelectedIndex = i;
                }
            }
        }


    }
}
