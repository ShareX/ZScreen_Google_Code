using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UploadersLib;
using ZScreenLib;
using UploadersLib.Helpers;

namespace UploaderTester
{
    class Tester
    {
        private static string testFile = @"C:\Users\Mihajlo\Pictures\lion_250px.jpg";

        private static void Main(string[] args)
        {
            Engine.TurnOn();
            Engine.LoadSettings();

            #region Image Uploaders
            foreach (ImageDestType uploader in Enum.GetValues(typeof(UploadersLib.ImageDestType)))
            {
                WorkerTask task = new WorkerTask(WorkerTask.Jobs.UPLOAD_IMAGE);
                task.MyImageUploader = uploader;
                task.SetLocalFilePath(testFile);
                if (uploader != ImageDestType.TWITSNAPS)
                {
                    new TaskManager(ref task).UploadImage();
                }
            }
            #endregion

            #region Text Uploaders
            foreach (TextDestType uploader in Enum.GetValues(typeof(UploadersLib.TextDestType)))
            {
                WorkerTask task = new WorkerTask(WorkerTask.Jobs.UploadFromClipboard);

                //task.MyText = TextInfo.FromString(uploader);
                //task.MakeTinyURL = false; // preventing Error: TinyURL redirects to a TinyURL.
                //task.MyTextUploader = uploader;
                //task.RunWorker();
            }
            #endregion

            #region File Uploaders

            #endregion

            #region URL Shorteners

            #endregion

            FTPAccount account = new FTPAccount();

            //using (FTPUploader ftp = new FTPUploader(account))
            //{
            //    //ftp.ProgressChanged += new FTPUploader.FTPProgressEventHandler(ftp_ProgressChanged);
            //    ftp.UploadFile(@"C:\Users\PC\Desktop\xchat-2.8.7f.exe", "UploadTest.exe");
            //}

            Console.ReadLine();
            Engine.TurnOff();
        }

        private static void ftp_ProgressChanged(float percentage)
        {
            Console.WriteLine(percentage + "%");
        }
    }
}