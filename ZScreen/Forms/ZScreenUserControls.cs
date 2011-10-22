using System;
using HelpersLib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        private void ProxyAccountTestButton_Click(object sender, EventArgs e)
        {
            ProxyInfo proxy = GetSelectedProxy();
            if (proxy != null)
            {
                Adapter.TestProxyAccount(proxy);
            }
        }

        private void ProxyAccountsRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (ucProxyAccounts.RemoveItem(sel))
            {
                Engine.ConfigUI.ProxyList.RemoveAt(sel);
            }
        }

        private void ProxyAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (Engine.ConfigUI.ProxyList != null && sel != -1 && sel < Engine.ConfigUI.ProxyList.Count && Engine.ConfigUI.ProxyList[sel] != null)
            {
                ProxyInfo acc = Engine.ConfigUI.ProxyList[sel];
                ucProxyAccounts.SettingsGrid.SelectedObject = acc;
                Engine.ConfigUI.ProxyActive = acc;
                Engine.ConfigUI.ProxySelected = ucProxyAccounts.AccountsList.SelectedIndex;
            }
            if (IsReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void ProxyAccountsAddButton_Click(object sender, EventArgs e)
        {
            ProxyAdd(new ProxyInfo(Environment.UserName, "", ZAppHelper.GetDefaultWebProxyHost(), ZAppHelper.GetDefaultWebProxyPort()));
            cboProxyConfig.SelectedIndex = (int)ProxyConfigType.ManualProxy;
        }
    }
}