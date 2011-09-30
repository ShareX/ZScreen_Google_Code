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
using System.IO;

namespace ZScreenGUI
{
    public static class Loader
    {
        public static bool IsMultiInstance { get; private set; }

        public static string CommandLineArg { get; private set; }

        public static ZScreen MainForm { get; private set; }

        public static GoogleTranslateGUI MyGTGUI { get; set; }

        [STAThread]
        private static void Main(string[] args)
        {
            // Check for multi instance
            if (args != null && args.Length > 0)
            {
                CommandLineArg = args[0];

                if (CommandLineArg.Contains("-m"))
                {
                    IsMultiInstance = true;
                }

                else if (args.Length > 1 && args[0] == "/doc")
                {
                    string fp = args[1];
                    if (File.Exists(fp))
                    {
                        Process.Start(fp);
                    }
                    return;
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
                Application.Run(MainForm = new ZScreen());
            }
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (WaitFormLoad(20000))
            {
                Action d = () =>
                {
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
    }
}