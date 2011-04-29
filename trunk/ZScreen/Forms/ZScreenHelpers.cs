using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {

        public void OpenHistory()
        {
            new HistoryLib.HistoryForm(Engine.HistoryDbPath, Engine.conf.HistoryMaxNumber, string.Format("{0} - History", Engine.GetProductName())).Show();
        }

        public void ProxyAdd(ProxyInfo acc)
        {
            Engine.conf.ProxyList.Add(acc);
            ucProxyAccounts.AccountsList.Items.Add(acc);
            ucProxyAccounts.AccountsList.SelectedIndex = ucProxyAccounts.AccountsList.Items.Count - 1;
        }
    }
}
