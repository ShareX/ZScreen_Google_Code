using System.Windows.Forms;
using UploadersLib.HelperClasses;
using ZScreenLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {
        public void OpenHistory()
        {
            new HistoryLib.HistoryForm(Engine.HistoryDbPath, Engine.conf.HistoryMaxNumber, string.Format("{0} - History", Engine.GetProductName())).Show();
        }

        private void OpenLastSource(ImageFileManager.SourceType sType)
        {
            OpenSource(UploadManager.LinkManagerLast, sType);
        }

        private bool OpenSource(ImageFileManager ifm, ImageFileManager.SourceType sType)
        {
            if (ifm != null)
            {
                string path = ifm.GetSource(Engine.TempDir, sType);
                if (!string.IsNullOrEmpty(path))
                {
                    if (sType == ImageFileManager.SourceType.TEXT || sType == ImageFileManager.SourceType.HTML)
                    {
                        Process.Start(path);
                        return true;
                    }

                    if (sType == ImageFileManager.SourceType.STRING)
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

        public void UploadFiles(string[] filePaths)
        {
            List<string> files = new List<string>();
            files.AddRange(filePaths);
            Loader.Worker.UploadUsingFileSystem(files);
        }
    }
}