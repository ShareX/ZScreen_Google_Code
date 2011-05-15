#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using SingleInstanceApplication;

namespace ZUploader
{
    internal static class Program
    {
        public static Settings Settings { get; private set; }

        public static string ZUploaderPersonalPath { get; private set; }

        public static string SettingsFilePath
        {
            get { return Path.Combine(ZUploaderPersonalPath, SettingsFileName); }
        }

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

        public static string LogFilePath
        {
            get
            {
                DateTime now = FastDateTime.Now;
                string fileName = string.Format("Log_{0}_{1}.txt", now.Month, now.Year);
                return Path.Combine(ZUploaderPersonalPath, fileName);
            }
        }

        public static string PluginFolderPath
        {
            get { return Path.Combine(ZUploaderPersonalPath, "Plugins"); }
        }

        private static string ZUploaderDefaultPersonalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.ProductName);
        private static string ZUploaderPortablePersonalPath = Path.Combine(Application.StartupPath, Application.ProductName);

        private const string SettingsFileName = "Settings.xml";
        private const string HistoryFileName = "ZUploaderHistory.xml";

        // URLS
        public const string URL_WEBSITE = "http://code.google.com/p/zscreen";
        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";
        public const string URL_WIKIPAGES = "http://code.google.com/p/zscreen/w/list";
        public const string URL_HELP = "http://code.google.com/p/zscreen/wiki/Tutorials";
        public const string URL_UPDATE = "http://zscreen.googlecode.com/svn/trunk/Update.xml";
        public const string URL_BERK = "http://code.google.com/u/flexy123";
        public const string URL_MIKE = "http://code.google.com/u/mcored";
        public const string URL_BRANDON = "http://code.google.com/u/rgrthat";

        public static bool IsBeta { get { return true; } }
        public static bool IsPortable { get; private set; }
        public static string CommandLineArg { get; private set; }
        public static Stopwatch StartTimer { get; private set; }
        public static Logger MyLogger { get; private set; }

        public static string Title
        {
            get
            {
                string title = string.Format("{0} {1}", Application.ProductName, Application.ProductVersion);
                if (IsBeta) title += " Beta";
                if (IsPortable) title += " Portable";
                return title;
            }
        }

        private static MainForm mainForm;

        [STAThread]
        private static void Main(string[] args)
        {
            string name = Assembly.GetExecutingAssembly().GetName().Name;
            if (!ApplicationInstanceManager.CreateSingleInstance(name, SingleInstanceCallback)) return;

            StartTimer = Stopwatch.StartNew();
            MyLogger = new Logger();
            MyLogger.WriteLine("ZUploader started");

            IsPortable = CheckPortable();
            MyLogger.WriteLine("IsPortable = {0}", IsPortable);

            if (args != null && args.Length > 0) CommandLineArg = args[0];
            MyLogger.WriteLine("CommandLineArg = \"{0}\"", CommandLineArg);

            Thread settingThread = new Thread(() => Settings = Settings.Load());
            settingThread.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MyLogger.WriteLine("new MainForm() started");
            mainForm = new MainForm();
            MyLogger.WriteLine("new MainForm() finished");

            settingThread.Join();

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(mainForm);

            Settings.Save();

            MyLogger.WriteLine("ZUploader closing");
            MyLogger.SaveLog(LogFilePath);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnError((Exception)e.ExceptionObject);
        }

        private static void OnError(Exception e)
        {
            MyLogger.WriteException("Unhandled exception", e);
            MyLogger.WriteLine("ZUploader closing. Reason: Unhandled exception");
            MyLogger.SaveLog(LogFilePath);
            new ErrorForm("ZUploader - Error", e, LogFilePath, URL_ISSUES).ShowDialog();
            Application.Exit();
        }

        private static bool CheckPortable()
        {
            if (Directory.Exists(ZUploaderPortablePersonalPath))
            {
                ZUploaderPersonalPath = ZUploaderPortablePersonalPath;
                return true;
            }
            else
            {
                ZUploaderPersonalPath = ZUploaderDefaultPersonalPath;
                return false;
            }
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

                    mainForm.BringToFront();
                    mainForm.Activate();

                    if (args != null && args.CommandLineArgs.Length > 1)
                    {
                        mainForm.UseCommandLineArg(args.CommandLineArgs[1]);
                    }
                };

                mainForm.Invoke(d);
            }
        }

        private static bool WaitFormLoad(int wait)
        {
            Stopwatch timer = Stopwatch.StartNew();

            while (timer.ElapsedMilliseconds < wait)
            {
                if (mainForm != null && mainForm.IsReady) return true;

                Thread.Sleep(10);
            }

            return false;
        }
    }
}