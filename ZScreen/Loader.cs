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
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SingleInstanceApplication;
using UploadersLib;
using ZScreenLib;
using Timer = System.Windows.Forms.Timer;

namespace ZScreenGUI
{
    public static class Loader
    {
        public static bool IsMultiInstance { get; private set; }
        public static string CommandLineArg { get; private set; }
        public static ZScreen MainForm { get; private set; }
        public static WorkerTask LastWorkerTask { get; set; }
        public static GoogleTranslateGUI MyGTGUI { get; set; }

        [STAThread]
        private static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                CommandLineArg = args[0];

                if (CommandLineArg.Contains("-m"))
                {
                    IsMultiInstance = true;
                }
            }

            if (!IsMultiInstance)
            {
                string name = Assembly.GetExecutingAssembly().GetName().Name;
                if (!ApplicationInstanceManager.CreateSingleInstance(name, SingleInstanceCallback)) return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Engine.TurnOn(new Engine.EngineOptions { KeyboardHook = true, ShowConfigWizard = true }))
            {
                Engine.LoadSettings();
                Application.Run(MainForm = new ZScreen());
                Engine.TurnOff();
            }
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (WaitFormLoad(20000))
            {
                Action d = () =>
                {
                    if (MainForm.WindowState == FormWindowState.Minimized)
                    {
                        MainForm.WindowState = FormWindowState.Normal;
                    }

                    MainForm.BringToFront();
                    MainForm.Activate();

                    if (args != null && args.CommandLineArgs.Length > 1)
                    {
                        MainForm.UseCommandLineArg(args.CommandLineArgs[1]);
                    }
                };

                MainForm.Invoke(d);
            }
        }

        private static bool WaitFormLoad(int wait)
        {
            Stopwatch timer = Stopwatch.StartNew();

            while (timer.ElapsedMilliseconds < wait)
            {
                if (MainForm != null && MainForm.IsReady) return true;

                Thread.Sleep(10);
            }

            return false;
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
            Engine.ZScreenKeyboardHook.KeyDown += new KeyEventHandler(MainForm.CheckHotkeys);
        }
    }
}