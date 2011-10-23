using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using UploadersLib;
using ZScreenLib;

namespace JBirdGUI
{
    internal static class Program
    {
        public readonly static string ConfigUploadersFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName + @"\UploadersConfig.xml");

        public static Dictionary<string, HotkeyManager> HotkeyMgrs = new Dictionary<string, HotkeyManager>();
        public static UploadersConfig ConfigUploaders = new UploadersConfig();
        public static WorkflowConfig ConfigWorkflow = new WorkflowConfig();
        public static JBirdCoreUI CoreUI = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0] == "/toolbar")
            {
                Application.Run(CoreUI = new JBirdToolbar());
            }
            else
            {
                Application.Run(CoreUI = new JBirdMain());
            }
        }

        public static Workflow GetProfile(WorkerTask.JobLevel2 job)
        {
            foreach (Workflow profile in ConfigWorkflow.Workflows98)
            {
                if (profile.Job == job)
                {
                    return profile;
                }
            }
            return null;
        }

        public static void HotkeysUpdate()
        {
            HotkeyMgrs.Clear();
            foreach (Workflow wf in Program.ConfigWorkflow.Workflows98)
            {
                HotkeyManager hm = new HotkeyManager(Program.CoreUI, ZAppType.JBird);
                hm.AddHotkey(JBirdHotkey.Workflow, wf.Hotkey, wf.Start);
                HotkeyMgrs.Add(wf.ID, hm);
            }
        }
    }
}