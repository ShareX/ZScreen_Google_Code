using System;
using UploadersLib;
using ZScreenLib;

namespace ZScreenTesterCLI
{
    public static class TesterCLI
    {
        public static void Test()
        {
            #region Image Uploaders

            foreach (ImageDestType uploader in Enum.GetValues(typeof(UploadersLib.ImageDestType)))
            {
                Console.WriteLine("Starting: " + uploader.GetDescription());
                WorkerTask task = new WorkerTask(WorkerTask.Jobs.UPLOAD_IMAGE);
                task.MyImageUploader = uploader;
                task.SetLocalFilePath(Tester.TestFile);
                if (uploader != ImageDestType.TWITSNAPS)
                {
                    new TaskManager(ref task).UploadImage();
                }
            }

            #endregion

            #region Text Uploaders
            /*
            foreach (TextDestType uploader in Enum.GetValues(typeof(UploadersLib.TextDestType)))
            {
                WorkerTask task = new WorkerTask(WorkerTask.Jobs.UploadFromClipboard);

                task.MyText = TextInfo.FromString(uploader);
                task.MakeTinyURL = false; // preventing Error: TinyURL redirects to a TinyURL.
                task.MyTextUploader = uploader;
                task.RunWorker();
            }
            */
            #endregion

            #region File Uploaders

            #endregion

            #region URL Shorteners

            #endregion
        }
    }
}