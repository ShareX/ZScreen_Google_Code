using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HelpersLib;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using UploadersLib.OtherServices;
using ZScreenLib;
using System.IO;
using System.Threading;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        protected override void WndProc(ref Message m)
        {
            if (IsReady)
            {
                switch (m.Msg)
                {
                    case NativeMethods.WM_SYSCOMMAND:
                        if (m.WParam.ToInt32() == NativeMethods.SC_MINIMIZE) // Minimize button handling
                        {
                            switch (Engine.AppConf.WindowButtonActionMinimize)
                            {
                                case WindowButtonAction.ExitApplication:
                                    CloseMethod = CloseMethod.MinimizeButton;
                                    Close();
                                    break;
                                case WindowButtonAction.MinimizeToTaskbar:
                                    WindowState = FormWindowState.Minimized;
                                    break;
                                case WindowButtonAction.MinimizeToTray:
                                    Hide();
                                    break;
                            }

                            m.Result = IntPtr.Zero;
                            return;
                        }
                        break;
                }
            }

            base.WndProc(ref m);
        }

        public void DisableFeatures()
        {
            tcMain.TabPages.Remove(tpQueue);
            tcCapture.TabPages.Remove(tpFreehandCropShot);
        }

        public GoogleTranslateGUI GetGTGUI()
        {
            if (Loader.MyGTGUI == null || Loader.MyGTGUI.IsDisposed)
            {
                Loader.MyGTGUI = new GoogleTranslateGUI(Engine.MyGTConfig) { Icon = this.Icon };
            }

            if (string.IsNullOrEmpty(Engine.MyGTConfig.APIKey))
            {
                StaticHelper.LoadBrowser("http://code.google.com/apis/language/translate/overview.html");
            }

            if (Engine.MyGTConfig.GoogleLanguages == null || Engine.MyGTConfig.GoogleLanguages.Count < 1)
            {
                Engine.MyGTConfig.GoogleLanguages = new GoogleTranslate(ZKeys.GoogleApiKey).GetLanguages();
            }

            return Loader.MyGTGUI;
        }

        public static void OpenHistory()
        {
            // if Engine.conf is null then open use default amount
            int maxNum = 100;
            if (Engine.conf != null)
            {
                maxNum = Engine.conf.HistoryMaxNumber;
            }
            new HistoryLib.HistoryForm(Engine.HistoryPath, maxNum, string.Format("{0} - History", Engine.GetProductName())).Show();
        }

        private void OpenLastSource(UploadResult.SourceType sType)
        {
            OpenSource(UploadManager.UploadResultLast, sType);
        }

        private bool OpenSource(UploadResult ifm, UploadResult.SourceType sType)
        {
            if (ifm != null)
            {
                string path = ifm.GetSource(Engine.TempDir, sType);
                if (!string.IsNullOrEmpty(path))
                {
                    if (sType == UploadResult.SourceType.TEXT || sType == UploadResult.SourceType.HTML)
                    {
                        StaticHelper.LoadBrowser(path);
                        return true;
                    }

                    if (sType == UploadResult.SourceType.STRING)
                    {
                        Clipboard.SetText(path); // ok
                        return true;
                    }
                }
            }

            return false;
        }

        public void ProxyAdd(ProxyInfo acc)
        {
            Engine.conf.ProxyList.Add(acc);
            ucProxyAccounts.AccountsList.Items.Add(acc);
            ucProxyAccounts.AccountsList.SelectedIndex = ucProxyAccounts.AccountsList.Items.Count - 1;
        }

        #region Backup & Restore

        private void AppSettingsImport()
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Engine.FILTER_XML_FILES };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLSettings temp = XMLSettings.Read(dlg.FileName);
                temp.FirstRun = false;
                Engine.conf = temp;
                ZScreen_ConfigGUI();
            }
        }

        private void AppSettingsExport()
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = Engine.FILTER_XML_FILES };
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dlg.FileName = Engine.SettingsFileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Engine.conf.Write(dlg.FileName);
            }
        }

        private void OutputsConfigImport()
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Engine.FILTER_XML_FILES };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Workflow temp = Workflow.Read(dlg.FileName);
                Engine.MyWorkflow = temp;
            }
        }

        private void OutputsConfigExport()
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = Engine.FILTER_XML_FILES };
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dlg.FileName = Engine.WorkflowConfigFileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Engine.MyWorkflow.Write(dlg.FileName);
            }
        }

        #endregion
    }
}