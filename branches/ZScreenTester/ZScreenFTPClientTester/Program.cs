using System;
using System.Windows.Forms;
using UploadersLib;
using ZScreenFTPClientTester.Properties;
using ZSS.FTPClientLib;

namespace ZScreenFTPClientTester
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

            Application.Run(new FTPClient2(acc));
        }
    }
}