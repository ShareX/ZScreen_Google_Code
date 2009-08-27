using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ZSS;
using UploadersLib.FileUploaders.FTP;

namespace UploaderTester
{
    class Program
    {
        private static void Main(string[] args)
        {
            FTPAccount account = new FTPAccount();

            using (FTPUploader ftp = new FTPUploader(account))
            {
                //ftp.ProgressChanged += new FTPUploader.FTPProgressEventHandler(ftp_ProgressChanged);
                ftp.UploadFile(@"C:\Users\PC\Desktop\xchat-2.8.7f.exe", "UploadTest.exe");
            }

            Console.ReadLine();
        }

        private static void ftp_ProgressChanged(float percentage)
        {
            Console.WriteLine(percentage + "%");
        }
    }
}