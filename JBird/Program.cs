using System;
using System.Windows.Forms;
using ZScreenLib;

namespace JBirdGUI
{
    internal static class Program
    {
        public static WorkflowConfig WorkflowConfig = new WorkflowConfig();
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
            foreach (Workflow profile in WorkflowConfig.Workflows98)
            {
                if (profile.Job == job)
                {
                    return profile;
                }
            }
            return null;
        }
    }
}