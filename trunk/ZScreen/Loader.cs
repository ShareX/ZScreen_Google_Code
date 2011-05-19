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
using ZScreenLib;

namespace ZScreenGUI
{
    public static class Loader
    {
        public static WorkerPrimary Worker;
        public static WorkerSecondary Worker2;
        public static WorkerTask zLastTask;
        public static List<string> LibNames = new List<string>();

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                string arg1 = args[1].ToLower();
                if (File.Exists(arg1))
                {
                    Process p = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(Path.Combine(Application.StartupPath, Engine.ZScreenCLI));
                    psi.Arguments = "upload " + arg1;
                    p.StartInfo = psi;
                    p.Start();
                }
                else if (arg1 == "history")
                {
                    Application.Run(new HistoryLib.HistoryForm(Engine.HistoryPath, 100, string.Format("{0} - History", Engine.GetProductName())));
                }
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
                Engine.MyLogger.WriteException("Running ZScreen", ex);
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
            if (Engine.TurnOn(new Engine.EngineOptions { KeyboardHook = true, ShowConfigWizard = true }))
            {
                Engine.LoadSettings();
                Application.Run(new ZScreen());

                Engine.conf.Write();
                Engine.TurnOff();
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
            Engine.ZScreenKeyboardHook.KeyDown += new KeyEventHandler(Loader.Worker.CheckHotkeys);
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LibNames.Add(Path.GetFileName(args.LoadedAssembly.FullName));
        }
    }
}