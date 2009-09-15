using System;
using System.Windows.Forms;
using UploadersLib;
using ZScreenFTPClientTester.Properties;
using ZSS.FTPClientLib;
using ZScreenLib;

namespace ZScreenFTPClientTester
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Engine.TurnOn();
            Engine.LoadSettingsLatest();

            if (Adapter.CheckFTPAccounts())
            {
                Application.Run(new FTPClient2(Engine.conf.FTPAccountList[Engine.conf.FTPSelected]));
            }
            else
            {
                MessageBox.Show("An FTP account does not exist or not selected properly.");
            }

            Engine.TurnOff();
        }
    }
}