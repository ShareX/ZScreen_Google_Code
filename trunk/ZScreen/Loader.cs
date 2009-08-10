using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ZScreenLib;
using System.Windows.Forms;

namespace ZSS
{
    public static class Loader
    {
        internal static ZSS.Forms.SplashScreen Splash = null;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Splash = new SplashScreen();
            //AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            //Splash.Show();

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 2 && args[1] == "/doc")
            {
                string filePath = string.Join(" ", args, 2, args.Length - 2);
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
            }
            else
            {             
                Program.RunZScreen();
            }
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Splash.AsmLoads.Enqueue("Loading " + args.LoadedAssembly.GetName().Name);
        }
    }
}
