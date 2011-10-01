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

namespace UploadersLib.FileUploaders
{
    public sealed class SFTP
    {
        SftpClient sftp;


        //properties
        public FTPAccount FtpAccount { get; set; }
        public bool IsConnected { get { return sftp.IsConnected; } }


        static Logger logger = new Logger();

        public SFTP(FTPAccount ftpAccount)
        {
            this.FtpAccount = ftpAccount;

            if (FtpAccount.UserName.Contains("@"))
            {
                FtpAccount.UserName = FtpAccount.UserName.Substring(0, FtpAccount.UserName.IndexOf('@'));
            }
            if (!string.IsNullOrEmpty(FtpAccount.Password) && (string.IsNullOrEmpty(FtpAccount.Keypath)))
            {
                sftp = new SftpClient(FtpAccount.Host, FtpAccount.Port, FtpAccount.UserName, FtpAccount.Password);
            }
            else if (string.IsNullOrEmpty(FtpAccount.Password) && (File.Exists(FtpAccount.Keypath)) && (string.IsNullOrEmpty(FtpAccount.Passphrase)))
            {
                sftp = new SftpClient(FtpAccount.Host, FtpAccount.Port, FtpAccount.UserName, new PrivateKeyFile(FtpAccount.Keypath));
            }
            else if (string.IsNullOrEmpty(FtpAccount.Password) && (File.Exists(FtpAccount.Keypath)) && (!string.IsNullOrEmpty(FtpAccount.Passphrase)))
            {
                sftp = new SftpClient(FtpAccount.Host, FtpAccount.Port, FtpAccount.UserName, new PrivateKeyFile(FtpAccount.Keypath, FtpAccount.Passphrase));
            }
        }

        public void Connect()
        {
            if (!IsConnected)
                 sftp.Connect();
        }
        public void Disconnect()
        {
            if (IsConnected)
                sftp.Disconnect();
        }

        public string Upload(Stream Inputstream, string RemoteName)
        {
            Connect();
            RemoteName = ZAppHelper.ReplaceIllegalChars(RemoteName, '_');

            while (RemoteName.IndexOf("__") != -1)
            {
                RemoteName = RemoteName.Replace("__", "_");
            }
            ChangeDirectory(FtpAccount.GetSubFolderPath());
            object s = new object();
            AsyncCallback ac = new AsyncCallback(CallBack);
            var result = sftp.BeginUploadFile(Inputstream, RemoteName, ac, s);
            var progress = result as SftpUploadAsyncResult;
            while (!progress.IsCompleted)
            {
                logger.WriteLine("Percent Uploaded: " + (progress.UploadedBytes / 1024));
            }
            Disconnect();
            return FtpAccount.GetUriPath(RemoteName);
        }
        public string Upload(string Filepath, string RemoteName)
        {
            Connect();
            RemoteName = ZAppHelper.ReplaceIllegalChars(RemoteName, '_');
            while (RemoteName.IndexOf("__") != -1)
            {
                RemoteName = RemoteName.Replace("__", "_");
            }
            ChangeDirectory(FtpAccount.GetSubFolderPath());
            object s = new object();
            AsyncCallback ac = new AsyncCallback(CallBack);
            using (var file = File.OpenRead(Filepath))
            {
                var result = sftp.BeginUploadFile(file, RemoteName, ac, s);
                var progress = result as SftpUploadAsyncResult;
                while (!progress.IsCompleted)
                {
                   logger.WriteLine("Percent Uploaded: " + (progress.UploadedBytes / 1024));
                }
            }
            Disconnect();
            return FtpAccount.GetUriPath(RemoteName);
        }
        public static void CallBack(IAsyncResult ia)
        {
            logger.WriteLine("Finished Uploading");
        }
        public void ChangeDirectory(string Path)
        {
            try
            {
                sftp.ChangeDirectory(Path);
            }
            catch (SftpPathNotFoundException e)
            {
                CreateDirectory(Path);
                ChangeDirectory(Path);
            }
        }
        public void CreateDirectory(string Path)
        {
            try
            {
                sftp.CreateDirectory(Path);
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
                logger.WriteLine("Directory " + Path + " probably already exists.");
            }
        }
    }
}
