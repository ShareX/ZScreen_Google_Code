#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2010 ZScreen Developers

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
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SingleInstanceApplication;

namespace ZUploader
{
    static class Program
    {
        public static Settings Settings;

        public static string ZUploaderPersonalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.ProductName);

        private const string SettingsFileName = "Settings.bin"; // "Settings.xml";
        public static string SettingsFilePath
        {
            get { return Path.Combine(ZUploaderPersonalPath, SettingsFileName); }
        }

        private const string HistoryFileName = "History.db3";
        public static string HistoryFilePath
        {
            get
            {
                if (Settings != null && Settings.UseCustomHistoryPath && !string.IsNullOrEmpty(Program.Settings.CustomHistoryPath))
                {
                    return Settings.CustomHistoryPath;
                }
                else
                {
                    return Path.Combine(ZUploaderPersonalPath, HistoryFileName);
                }
            }
        }

        public const string URL_WEBSITE = "http://code.google.com/p/zscreen";
        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";

        public const string ImageShackKey = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TinyPicID = "e2aabb8d555322fa";
        public const string TinyPicKey = "00a68ed73ddd54da52dc2d5803fa35ee";
        public const string ImgurKey = "63499468bcc5d2d6aee1439e50b4e61c";
        public const string UploadScreenshotKey = "2807828f377649572393126680";

        public static Stopwatch StartTimer;

        private static MainForm mainForm;

        [STAThread]
        static void Main(string[] args)
        {
            DebugTimer.WriteLine("Application started.");

            StartTimer = new Stopwatch();
            StartTimer.Start();

            string name = Assembly.GetExecutingAssembly().GetName().Name;
            if (!ApplicationInstanceManager.CreateSingleInstance(name, SingleInstanceCallback)) return;

            Thread settingThread = new Thread(() => Settings = Settings.Load());
            settingThread.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path = null;
            if (args.Length > 0) path = args[0];

            using (new DebugTimer("MainForm init", true)) mainForm = new MainForm(path);

            settingThread.Join();

            Application.Run(mainForm);

            Settings.Save();

            DebugTimer.WriteLine("Application stopped.");
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (WaitFormLoad(5000))
            {
                Action d = () =>
                {
                    if (mainForm.WindowState == FormWindowState.Minimized)
                    {
                        mainForm.WindowState = FormWindowState.Normal;
                    }

                    mainForm.Activate();
                    mainForm.BringToFront();

                    if (args != null && args.CommandLineArgs.Length > 1)
                    {
                        UploadManager.Upload(args.CommandLineArgs[1]);
                    }
                };

                mainForm.Invoke(d);
            }
        }

        private static bool WaitFormLoad(int wait)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (timer.ElapsedMilliseconds < wait)
            {
                if (mainForm != null && mainForm.IsReady) return true;

                Thread.Sleep(10);
            }

            return false;
        }
    }
}