using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
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
        public bool IsConnected { get { return Client.IsConnected; } }
        public string HomeDir { get; set; }

        //Variables
        SftpClient Client;


        //Misc
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
            {
                Client.Connect();
                HomeDir = Client.WorkingDirectory;
            }
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
            }
        }

        public void CreateDirectory(string Path)
        {
            try
            {
                Client.CreateDirectory(Path);
                StaticHelper.WriteLine("Created Directory: " + Path);
            }
            catch (SftpPathNotFoundException)
            {
                StaticHelper.WriteLine("Failed to create directory " + Path);
                StaticHelper.WriteLine("Attempting to fix...");
                CreateMultipleDirectorys(FTPHelpers.GetPaths(Path));
            }
            catch (SftpPermissionDeniedException)
            {
            }
        }

        public bool DirectoryExists(string Dir)
        {
            Connect();
            try
            {
                string cdir = Client.WorkingDirectory;
                Client.ChangeDirectory(Dir);
                Client.ChangeDirectory(cdir);
                return true;
            }
            catch (SftpPathNotFoundException)
            {
                return false;
            }
        }

        public List<string> CreateMultipleDirectorys(List<string> Directories)
        {
            List<string> CreatedList = new List<string>();
            for (int x = 0; x <= Directories.Count - 1; x++)
            {
                Directories[x] = Directories[x].Substring(1);
                if (!DirectoryExists(Directories[x]))
                {
                    CreateDirectory(Directories[x]);
                    CreatedList.Add(Directories[x]);
                }
            }
            return CreatedList;
        }

        public SftpFile[] DirectoryListing(string RootDir)
        {
            Connect();
            IEnumerable<SftpFile> dirlist = Client.ListDirectory(RootDir);
            return dirlist.ToArray<SftpFile>();
            
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