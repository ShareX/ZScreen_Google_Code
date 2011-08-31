using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using ZScreenLib;

namespace JBirdGUI
{
    static class Program
    {
        public static ProfileSettings ProfilesConfig = new ProfileSettings();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new JBirdMain());
        }

        public static Profile GetProfile(WorkerTask.JobLevel2 job)
        {
            foreach (Profile profile in ProfilesConfig.Profiles)
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
