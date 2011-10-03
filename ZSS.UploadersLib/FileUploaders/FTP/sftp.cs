using System;
using System.IO;
using System.Threading;
using HelpersLib;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using ZUploader.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class SFTP : IDisposable
    {
        //properties

        public FTPAccount FTPAccount { get; set; }
        SftpClient Client;
        public bool IsConnected { get { return Client.IsConnected; } }
        public event Uploader.ProgressEventHandler ProgressChanged;
        private ProgressManager progress;

        public SFTP(FTPAccount account)
        {
            this.FTPAccount = account;

            if (FTPAccount.UserName.Contains("@"))
            {
                FTPAccount.UserName = FTPAccount.UserName.Substring(0, FTPAccount.UserName.IndexOf('@'));
            }
            if (!string.IsNullOrEmpty(FTPAccount.Password) && (string.IsNullOrEmpty(FTPAccount.Keypath)))
            {
                Client = new SftpClient(FTPAccount.Host, FTPAccount.Port, FTPAccount.UserName, FTPAccount.Password);
            }
            else if (string.IsNullOrEmpty(FTPAccount.Password) && (File.Exists(FTPAccount.Keypath)) && (string.IsNullOrEmpty(FTPAccount.Passphrase)))
            {
                Client = new SftpClient(FTPAccount.Host, FTPAccount.Port, FTPAccount.UserName, new PrivateKeyFile(FTPAccount.Keypath));
            }
            else if (string.IsNullOrEmpty(FTPAccount.Password) && (File.Exists(FTPAccount.Keypath)) && (!string.IsNullOrEmpty(FTPAccount.Passphrase)))
            {
                Client = new SftpClient(FTPAccount.Host, FTPAccount.Port, FTPAccount.UserName, new PrivateKeyFile(FTPAccount.Keypath, FTPAccount.Passphrase));
            }
        }

        public void Connect()
        {
            if (!IsConnected)
                Client.Connect();
        }

        public void Disconnect()
        {
            if (IsConnected)
                Client.Disconnect();
        }

        public static void CallBack(IAsyncResult ia)
        {
            StaticHelper.WriteLine("Finished Uploading");
        }

        public void ChangeDirectory(string Path)
        {
            try
            {
                Client.ChangeDirectory(Path);
            }
            catch (SftpPathNotFoundException e)
            {
                CreateDirectory(Path);
                ChangeDirectory(Path);

                StaticHelper.WriteException(e);
            }
        }

        public void CreateDirectory(string Path)
        {
            try
            {
                Client.CreateDirectory(Path);
                StaticHelper.WriteLine("Creating Directory: " + Path);
            }
            catch (SftpPathNotFoundException)
            {
                string[] Dirs = Path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                string s = string.Empty;
                for (int x = 0; x <= Dirs.Length - 2; x++)
                {
                    s += Dirs[x] + "/";
                }
                CreateDirectory(s);
            }
            catch (SftpPermissionDeniedException)
            {
            }
        }

        private void OnTransferProgressChanged(SftpUploadAsyncResult e)
        {
            if (ProgressChanged != null)
            {
                progress.ChangeProgress((int)e.UploadedBytes, true);
                ProgressChanged(progress);
            }
        }

        public void UploadData(Stream stream, string fileName)
        {
            Connect();

            progress = new ProgressManager(stream.Length);

            ChangeDirectory(FTPAccount.GetSubFolderPath());

            object s = new object();
            AsyncCallback ac = new AsyncCallback(CallBack);

            var result = Client.BeginUploadFile(stream, fileName, ac, s);
            SftpUploadAsyncResult sftpresult = result as SftpUploadAsyncResult;

            while (!sftpresult.IsCompleted)
            {
                if (sftpresult.UploadedBytes > 0)
                {
                    OnTransferProgressChanged(sftpresult);
                }
                Thread.Sleep(500);
            }

            Disconnect();
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}