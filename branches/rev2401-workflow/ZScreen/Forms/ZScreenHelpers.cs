using System.Diagnostics;
using System.Windows.Forms;
using HelpersLib;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        public GoogleTranslateGUI GetGTGUI()
        {
            if (Loader.MyGTGUI == null || Loader.MyGTGUI.IsDisposed)
            {
                Loader.MyGTGUI = new GoogleTranslateGUI(Engine.MyGTConfig, ZKeys.GetAPIKeys()) { Icon = this.Icon };
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
                        Process.Start(path);
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
    }
}