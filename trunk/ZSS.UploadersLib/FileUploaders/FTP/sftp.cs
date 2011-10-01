using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Renci.SshNet.Common;
using System.IO;
using HelpersLib;
using UploadersLib.HelperClasses;
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

        static Logger logger = new Logger();

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

        public string Upload(string Filepath, string RemoteName)
        {
            Connect();
            RemoteName = ZAppHelper.ReplaceIllegalChars(RemoteName, '_');
            while (RemoteName.IndexOf("__") != -1)
            {
                RemoteName = RemoteName.Replace("__", "_");
            }
            ChangeDirectory(FTPAccount.GetSubFolderPath());
            object s = new object();
            AsyncCallback ac = new AsyncCallback(CallBack);
            using (var file = File.OpenRead(Filepath))
            {
                var result = Client.BeginUploadFile(file, RemoteName, ac, s);
                var resultprogress = result as SftpUploadAsyncResult;
                ProgressManager pm = new ProgressManager(new FileInfo(Filepath).Length);
                while (!resultprogress.IsCompleted)
                {
                    pm.ChangeProgress((int)resultprogress.UploadedBytes);
                    ProgressChanged(pm);
                }
            }
            Disconnect();
            return FTPAccount.GetUriPath(RemoteName);
        }

        public static void CallBack(IAsyncResult ia)
        {
            logger.WriteLine("Finished Uploading");
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

                logger.WriteException(e);
            }
        }

        public void CreateDirectory(string Path)
        {
            try
            {
                Client.CreateDirectory(Path);
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
                progress.ChangeProgress((int)e.UploadedBytes);
                ProgressChanged(progress);
            }
        }

        public void UploadData(Stream stream, string remotePath)
        {
            Connect();
            progress = new ProgressManager(stream.Length);

            ChangeDirectory(FTPAccount.GetSubFolderPath());

            object s = new object();
            AsyncCallback ac = new AsyncCallback(CallBack);

            var result = Client.BeginUploadFile(stream, remotePath, ac, s);
            SftpUploadAsyncResult sftpresult = result as SftpUploadAsyncResult;

            while (!sftpresult.IsCompleted)
            {
                OnTransferProgressChanged(sftpresult);
            }

            Disconnect();
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
