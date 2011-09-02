using System;
using System.Collections.Generic;
using System.IO;
using HelpersLib;
using NDesk.Options;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenCLI
{
    public static class Program
    {
        private static string ApplicationName = "zscreencli.exe";

        private static bool bVerbose = false;
        private static bool bShowHelp = false;
        private static bool bFileUpload = false;
        private static bool bClipboardUpload = false;
        private static bool bCropShot = false;
        private static bool bSelectedWindow = false;
        private static bool bScreen = false;

        private static List<int> listOutputTypes = new List<int>();
        private static int clipboardContent = (int)ClipboardContentEnum.Remote;
        private static List<int> listImageHosts = new List<int>();
        private static List<int> listTextHosts = new List<int>();
        private static List<int> listFileHosts = new List<int>();
        private static List<string> listPaths = new List<string>();

        [STAThread]
        private static void Main(string[] args)
        {
            ProcessArgs(args);
        }

        #region CLI

        private static void ProcessArgs(string[] args)
        {
            var p = new OptionSet()
            {
                { "h|help", "Show this message and exit",
                   v => bShowHelp = v != null },
                { "v|verbose", "Debug output",
                    v => bVerbose = v != null },
                { "o|outputs=", "Outputs. This must be an integer.",
                    (int v) => listOutputTypes.Add(v) },
                { "i|hi=", "Image uploader type. This must be an integer.",
                    (int v) => listImageHosts.Add(v) },
                { "t|ht=", "Text uploader type. This must be an integer.",
                    (int v) => listTextHosts.Add(v) },
                { "f|hf=", "File uploader type. This must be an integer.",
                    (int v) => listFileHosts.Add(v) },
                { "s|ws", "Capture selected window.",
                    v => bSelectedWindow = v != null },
                { "r|wc", "Capture rectangular region.",
                    v => bCropShot = v != null },
                { "d|wf", "Capture entire screen.",
                    v => bScreen = v != null },
                { "c|uc", "Upload clipboard content.",
                    v => bClipboardUpload = v != null },
                { "u|uf=", "File path to upload.",
                    v =>
                    {
                        if (File.Exists(v)) listPaths.Add (v);
                        else if (Directory.Exists(v)) listPaths.Add(v);
                        if (listPaths.Count>0) bFileUpload = true;
                    }
                },
            };

            if (args.Length == 0)
            {
                ShowHelp(p);
            }

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write(string.Format("{0}: ", ApplicationName));
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'ZScreen.exe -help' for more information.");
                return;
            }

            if (bVerbose) Console.WriteLine(string.Format("Loading {0}", Engine.AppConf.WorkflowConfigPath));
            Engine.MyWorkflow.OutputsConfig = UploadersConfig.Load(Engine.AppConf.WorkflowConfigPath);

            if (listOutputTypes.Count == 0)
            {
                // if user forgets to list output then copy to clipboard
                listOutputTypes.Add((int)OutputEnum.Clipboard);
            }

            if (bShowHelp)
            {
                ShowHelp(p);
                return;
            }

            if (bFileUpload && listPaths.Count > 0)
            {
                FileUpload();
            }

            if (bClipboardUpload)
            {
                ClipboardUpload();
            }

            if (bCropShot)
            {
                CaptureRectRegion(WorkerTask.JobLevel2.CaptureRectRegion);
            }

            if (bSelectedWindow)
            {
                CaptureRectRegion(WorkerTask.JobLevel2.CaptureSelectedWindow);
            }

            if (bScreen)
            {
                CaptureScreen();
            }
        }

        private static WorkerTask DefaultWorkerTask()
        {
            WorkerTask tempTask = new WorkerTask();
            foreach (int o in listOutputTypes)
            {
                tempTask.Profile.Outputs.Add((OutputEnum)o);
            }
            if (tempTask.Profile.Outputs.Count == 0)
            {
                tempTask.Profile.Outputs.Add(OutputEnum.RemoteHost);
            }
            tempTask.TaskClipboardContent.Add((ClipboardContentEnum)clipboardContent);
            foreach (int ut in listImageHosts)
            {
                if (bVerbose) Console.WriteLine(string.Format("Added {0}", ((ImageUploaderType)ut).GetDescription()));
                tempTask.MyImageUploaders.Add((ImageUploaderType)ut);
            }
            foreach (int ut in listFileHosts)
            {
                if (bVerbose) Console.WriteLine(string.Format("Added {0}", ((FileUploaderType)ut).GetDescription()));
                tempTask.MyFileUploaders.Add((FileUploaderType)ut);
            }
            return tempTask;
        }

        private static void CaptureScreen()
        {
            WorkerTask esTask = DefaultWorkerTask();
            esTask.StartWork(WorkerTask.JobLevel2.CaptureEntireScreen);
            Console.WriteLine();
            Console.WriteLine("Capturing entire screen in 3 seconds.");
            Console.WriteLine("If you would like to minimize this window, then do it now.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);
            esTask.CaptureScreen();
            esTask.PublishData();
            PostPublishTask(esTask);
        }

        private static void CaptureRectRegion(WorkerTask.JobLevel2 job2)
        {
            WorkerTask csTask = DefaultWorkerTask();
            csTask.StartWork(job2);
            if (csTask.CaptureRegionOrWindow())
            {
                csTask.PublishData();
            }
            Console.WriteLine(csTask.ToErrorString());
            PostPublishTask(csTask);
        }

        private static void ClipboardUpload()
        {
            WorkerTask cbTask = DefaultWorkerTask();
            cbTask.LoadClipboardContent();
            cbTask.PublishData();

            PostPublishTask(cbTask);
        }

        private static void FileUpload()
        {
            List<string> listFiles = new List<string>();
            foreach (string fdp in listPaths)
            {
                if (File.Exists(fdp))
                {
                    listFiles.Add(fdp);
                }
                else if (Directory.Exists(fdp))
                {
                    listFiles.AddRange(Directory.GetFiles(fdp, "*.*", SearchOption.AllDirectories));
                }
            }
            foreach (string fp in listFiles)
            {
                WorkerTask fuTask = DefaultWorkerTask();
                fuTask.StartWork(WorkerTask.JobLevel2.UploadFromClipboard);
                fuTask.UpdateLocalFilePath(fp);
                fuTask.PublishData();

                PostPublishTask(fuTask);
            }
        }

        private static void PostPublishTask(WorkerTask task)
        {
            if (task.UploadResults.Count > 0)
            {
                foreach (UploadResult ur in task.UploadResults)
                {
                    Console.WriteLine(ur.URL);
                }
                UploadManager.ShowUploadResults(task, true);
            }
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: zscreen.exe [OPTIONS]+ message");
            Console.WriteLine("Upload screenshots, text or files.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.Write(p.WriteOptionDescriptions().ToString());
            Console.WriteLine();
            Console.WriteLine("Outputs:\n");
            foreach (OutputEnum ut in Enum.GetValues(typeof(OutputEnum)))
            {
                Console.WriteLine(string.Format("{0}: {1}", (int)ut, ut.GetDescription()));
            }
            Console.WriteLine();
            Console.WriteLine("Image hosts:\n");
            foreach (ImageUploaderType ut in Enum.GetValues(typeof(ImageUploaderType)))
            {
                Console.WriteLine(string.Format("{0}: {1}", (int)ut, ut.GetDescription()));
            }
            Console.WriteLine();
            Console.WriteLine("Text hosts:\n");
            foreach (TextUploaderType ut in Enum.GetValues(typeof(TextUploaderType)))
            {
                Console.WriteLine(string.Format("{0}: {1}", (int)ut, ut.GetDescription()));
            }
            Console.WriteLine();
            Console.WriteLine("File hosts:\n");
            foreach (FileUploaderType ut in Enum.GetValues(typeof(FileUploaderType)))
            {
                Console.WriteLine(string.Format("{0}: {1}", (int)ut, ut.GetDescription()));
            }
        }

        #endregion CLI
    }
}