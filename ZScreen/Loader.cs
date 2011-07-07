#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using HelpersLib;
using NDesk.Options;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenGUI
{
    public static class Loader
    {
        public static ZScreen MyApp = null;
        public static WorkerTask zLastTask;
        public static UploadersLib.GoogleTranslateGUI MyGTGUI;
        public static List<string> LibNames = new List<string>();

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);

            if (args.Length > 2 && args[1] == "/doc")
            {
                string filePath = string.Join(" ", args, 2, args.Length - 2);
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
            }
            else if (args.Length > 1 && !string.IsNullOrEmpty(args[1]))
            {
                string arg1 = args[1].ToLower();
                if (File.Exists(arg1))
                {
                    Process p = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(Application.ExecutablePath);
                    psi.Arguments = "upload " + arg1;
                    p.StartInfo = psi;
                    p.Start();
                }
                else if (arg1 == "history")
                {
                    Application.Run(new HistoryLib.HistoryForm(Engine.HistoryPath, 100, string.Format("{0} - History", Engine.GetProductName())));
                }
            }
            else if (args.Length > 0)
            {
                ProcessArgs(args);
            }
            else
            {
                if (Engine.TurnOn(new Engine.EngineOptions { KeyboardHook = true, ShowConfigWizard = true }))
                {
                    Engine.LoadSettings();
                    Application.Run(MyApp = new ZScreen());
                    Engine.TurnOff();
                }
            }
        }

        public static void KeyboardHook()
        {
            InitKeyboardHook();

            Engine.MyLogger.WriteLine("Keyboard Hook initiated");

            if (Engine.conf.EnableKeyboardHookTimer)
            {
                Timer keyboardTimer = new Timer() { Interval = 5000 };
                keyboardTimer.Tick += new EventHandler(keyboardTimer_Tick);
                keyboardTimer.Start();
            }
        }

        private static void keyboardTimer_Tick(object sender, EventArgs e)
        {
            if (Engine.conf.EnableKeyboardHookTimer)
            {
                InitKeyboardHook();
            }
            else
            {
                Timer timer = sender as Timer;
                if (timer != null) timer.Stop();
            }
        }

        private static void InitKeyboardHook()
        {
            if (Engine.ZScreenKeyboardHook != null)
            {
                Engine.ZScreenKeyboardHook.Dispose();
            }

            Engine.ZScreenKeyboardHook = new KeyboardHook();
            Engine.ZScreenKeyboardHook.KeyDown += new KeyEventHandler(MyApp.CheckHotkeys);
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LibNames.Add(Path.GetFileName(args.LoadedAssembly.FullName));
        }

        #region CLI

        private static void ProcessArgs(string[] args)
        {
            var p = new OptionSet()
            {
                { "h|help",  "show this message and exit",
                   v => bShowHelp = v != null },
                    { "v|verbose",  "debug output",
                   v => bVerbose = v != null },
                { "o|outputs=", "Outputs. This must be an integer.",
                    (int v) => listImageHosts.Add(v) },
                { "i|hi=", "Image uploader type. This must be an integer.",
                    (int v) => listImageHosts.Add(v) },
                { "t|ht=", "Text uploader type. This must be an integer.",
                    (int v) => listTextHosts.Add(v) },
                { "f|hf=", "File uploader type. This must be an integer.",
                    (int v) => listFileHosts.Add(v) },
                { "s|ws",  "Capture selected window",
                    v => bSelectedWindow = v != null },
                { "r|wc",  "Capture rectangular region",
                    v => bCropShot = v != null },
                { "d|wf",  "Capture entire screen",
                    v => bScreen = v != null },
                { "c|uc",  "Upload clipboard content.",
                    v => bClipboardUpload = v != null },
                { "u|uf=", "File path to upload.",
                    v => {
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
                Console.Write(string.Format("{0}: ", "ZScreenCLI"));
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'zscreencli.exe --help' for more information.");
                return;
            }

            if (bVerbose) Console.WriteLine(string.Format("Loading {0}", Engine.AppConf.UploadersConfigPath));
            Engine.MyUploadersConfig = UploadersConfig.Load(Engine.AppConf.UploadersConfigPath);

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
            tempTask.TaskOutputs.Add(OutputEnum.Clipboard);
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
            esTask.AssignJob(WorkerTask.JobLevel2.CaptureEntireScreen);
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
            csTask.AssignJob(job2);
            if (csTask.BwCaptureRegionOrWindow())
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
                fuTask.AssignJob(WorkerTask.JobLevel2.UploadFromClipboard);
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
            Console.WriteLine("Usage: zscreencli.exe [OPTIONS]+ message");
            Console.WriteLine("Upload screenshots, text or files.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
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