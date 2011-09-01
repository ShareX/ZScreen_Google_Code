using System;
using System.Windows.Forms;
using ZScreenLib;

namespace JBirdGUI
{
    internal static class Program
    {
        public static ProfileSettings ProfilesConfig = new ProfileSettings();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new JBirdMain());
        }

        public static Workflow GetProfile(WorkerTask.JobLevel2 job)
        {
            foreach (Workflow profile in ProfilesConfig.Profiles)
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