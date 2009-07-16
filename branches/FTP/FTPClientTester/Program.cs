using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ZSS.FTPClientLib;
using ZSS;
using FTPClientTester.Properties;


namespace FTPClientTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginDialog dlg = null;

            if (string.IsNullOrEmpty(Settings.Default.Server) || string.IsNullOrEmpty(Settings.Default.UserName) || string.IsNullOrEmpty(Settings.Default.Password))
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

            FTPClientOptions ftpClientOptions = new FTPClientOptions();
            ftpClientOptions.Account = acc;
            Application.Run(new ZSS.FTPClientLib.FTPClient(ftpClientOptions));

        }
    }
}
