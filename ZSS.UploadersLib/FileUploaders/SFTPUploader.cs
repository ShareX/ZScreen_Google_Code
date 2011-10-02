using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib.HelperClasses;
using System.IO;
using Renci.SshNet;
using UploadersLib.FileUploaders;
using HelpersLib;

namespace UploadersLib
{
    public sealed class SFTPUploader : FileUploader
    {
        public FTPAccount FTPAccount;

        public SFTPUploader(FTPAccount account)
        {
            FTPAccount = account;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult result = new UploadResult();

            using (SFTP sftpClient = new SFTP(FTPAccount))
            {
                sftpClient.ProgressChanged += new Uploader.ProgressEventHandler(x => OnProgressChanged(x));

                fileName = ZAppHelper.ReplaceIllegalChars(fileName, '_');

                while (fileName.IndexOf("__") != -1)
                {
                    fileName = fileName.Replace("__", "_");
                }

                try
                {
                    stream.Position = 0;
                    sftpClient.UploadData(stream, fileName);
                }
                catch (Exception e)
                {
                    StaticHelper.WriteException(e);
                    Errors.Add(e.Message);
                }

                if (Errors.Count == 0)
                {
                    result.URL = FTPAccount.GetUriPath(fileName);
                }

            }

            return result;
        }


    }
}
