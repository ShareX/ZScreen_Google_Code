using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ZScreenLib;
using System.Windows.Forms;
using System.Threading;

namespace ZScreenGUI
{
    public static class Loader
    {
        // internal static ZSS.Forms.SplashScreen Splash = null;
        public static WorkerPrimary Worker;
        public static WorkerSecondary Worker2;
        public const string ZScreenCLI = "ZScreenCLI.exe";
        public static Queue<string> AsmLoads = new Queue<string>();
        public static List<string> LibNames = new List<string>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Splash = new ZSS.Forms.SplashScreen();
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            // Splash.Show();

            string[] args = Environment.GetCommandLineArgs();

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
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Path.Combine(Application.StartupPath, ZScreenCLI));
                psi.Arguments = args[1];
                p.StartInfo = psi;
                p.Start();
            }
            else
            {
                RunZScreen();
            }
        }

        private static void RunZScreen()
        {
            try
            {
                Program.Load(true);
                Application.Run(new ZScreen());
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex);
            }
            finally
            {
                Program.Unload();
            }
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            AsmLoads.Enqueue("Loading " + args.LoadedAssembly.GetName().Name);
            LibNames.Add(Path.GetFileName(args.LoadedAssembly.FullName));
        }
    }
}