using System;
using System.Windows.Forms;
using FTPClientTester.Properties;
using ZSS;
using ZSS.FTPClientLib;

namespace FTPClientTester
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginDialog dlg = null;

            if (string.IsNullOrEmpty(Settings.Default.Server) ||
                string.IsNullOrEmpty(Settings.Default.UserName) ||
                string.IsNullOrEmpty(Settings.Default.Password))
            {
                dlg = new LoginDialog();
                dlg.ShowDialog();

                Settings.Default.UserName = dlg.txtUserName.Text;
                Settings.Default.Password = dlg.txtPassword.Text;
                Settings.Default.Server = dlg.txtServer.Text;
                Settings.Default.Save();
            }

            FTPAccount acc = new FTPAccount("Default");
            acc.Username = Settings.Default.UserName;
            acc.Password = Settings.Default.Password;
            acc.Server = Settings.Default.Server;

            FTPOptions ftpClientOptions = new FTPOptions();
            ftpClientOptions.Account = acc;

            Application.Run(new FTPClient2(ftpClientOptions));
        }
    }
}