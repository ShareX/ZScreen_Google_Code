using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenGUI
{
    public static class Loader
    {
        // internal static ZSS.Forms.SplashScreen Splash = null;
        public static WorkerPrimary Worker;
        public static WorkerSecondary Worker2;
        public static Queue<string> AsmLoads = new Queue<string>();
        public static List<string> LibNames = new List<string>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // FileSystem.AppendDebug("Starting Splash Screen");
            // Splash = new ZSS.Forms.SplashScreen();
            // Splash.Show();
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);

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
                ProcessStartInfo psi = new ProcessStartInfo(Path.Combine(Application.StartupPath, Engine.ZScreenCLI));
                psi.Arguments = args[1];
                p.StartInfo = psi;
                p.Start();
            }
            else if (Engine.mAppInfo.ApplicationState == McoreSystem.AppInfo.SoftwareCycle.Beta)
            {
                RunZScreenBeta();
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
                if (Engine.TurnOn(new ZScreenLib.Engine.EngineOptions { KeyboardHook = true, ShowConfigWizard = true }))
                {
                    Engine.LoadSettings();
                    Application.Run(new ZScreen());
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Running ZScreen", ex);
                Engine.conf.Write();
                throw ex;
            }
            finally
            {
                Engine.TurnOff();
            }
        }

        private static void RunZScreenBeta()
        {
            if (Engine.TurnOn(new ZScreenLib.Engine.EngineOptions { KeyboardHook = true, ShowConfigWizard = true }))
            {
                Engine.LoadSettings();
                Application.Run(new ZScreen());

                Engine.conf.Write();
                Engine.TurnOff();
            }
        }

        public static void KeyboardHook()
        {
            System.Windows.Forms.Timer keyboardTimer = new System.Windows.Forms.Timer() { Interval = 1500, Enabled = true };
            keyboardTimer.Tick += new EventHandler(keyboardTimer_Tick);
            FileSystem.AppendDebug("Keyboard Hook initiated");
        }

        static void keyboardTimer_Tick(object sender, EventArgs e)
        {
            if (Engine.ZScreenKeyboardHook != null)
            {
                Engine.ZScreenKeyboardHook.Dispose();
            }
            Engine.ZScreenKeyboardHook = new KeyboardHook();
            Engine.ZScreenKeyboardHook.KeyDownEvent += new KeyEventHandler(Loader.Worker.CheckHotkeys);
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            AsmLoads.Enqueue("Loading " + args.LoadedAssembly.GetName().Name);
            LibNames.Add(Path.GetFileName(args.LoadedAssembly.FullName));
        }
    }
}