using System;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib;
using UploadersLib.HelperClasses;
using UploadersLib.OtherServices;
using ZScreenCoreLib;
using ZScreenLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        protected override void WndProc(ref Message m)
        {
            if (IsReady)
            {
                switch (m.Msg)
                {
                    case (int)WindowsMessages.SYSCOMMAND:
                        if (m.WParam.ToInt32() == NativeMethods.SC_MINIMIZE) // Minimize button handling
                        {
                            switch (Engine.ConfigApp.WindowButtonActionMinimize)
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

        public override void ShowGTGUI()
        {
            GetGTGUI().Show();
        }

        private void ShowConfigureActionsUI()
        {
            var ui = new ActionsUI(Engine.ConfigUI.ConfigActions) { Icon = this.Icon, Text = Application.ProductName + @" - Actions" };
            ui.Show();
        }

        private void ShowOptions()
        {
            FormsMgr.ShowOptionsUI();
        }

        private void ShowWatermarkUI()
        {
            WatermarkUI ui = new WatermarkUI(Engine.ConfigWorkflow.ConfigWatermark) { Icon = this.Icon };
            ui.Show();
        }

        private void ShowFileNamingUI()
        {
            FileNamingUI ui = new FileNamingUI(Engine.ConfigWorkflow.ConfigFileNaming) { Icon = this.Icon };
            ui.Show();
        }

        private void ShowImageFormatUI()
        {
            var wfwgui = new WorkflowWizardGUIOptions
            {
                ShowQualityTab = true,
                ShowResizeTab = true
            };
            var wfw = new WorkflowWizard(new WorkerTask(Engine.ConfigWorkflow, false), wfwgui) { Icon = Icon };
            wfw.Show();
        }

        public GoogleTranslateGUI GetGTGUI()
        {
            if (Loader.MyGTGUI == null || Loader.MyGTGUI.IsDisposed)
            {
                Loader.MyGTGUI = new GoogleTranslateGUI(Engine.ConfigGT) { Icon = this.Icon };
            }

            if (string.IsNullOrEmpty(Engine.ConfigGT.APIKey))
            {
                ZAppHelper.LoadBrowserAsync("http://code.google.com/apis/language/translate/overview.html");
            }

            if (Engine.ConfigGT.GoogleLanguages == null || Engine.ConfigGT.GoogleLanguages.Count < 1)
            {
                Engine.ConfigGT.GoogleLanguages = new GoogleTranslate(ZKeys.GoogleApiKey).GetLanguages();
            }

            return Loader.MyGTGUI;
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
                        ZAppHelper.LoadBrowserAsync(path);
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
    }
}