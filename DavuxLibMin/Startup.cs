/*
 * Author: David Amenta
 * Release Date: 12/12/2009
 * License: Free for any use.
 */

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DavuxLib
{
    public class Startup
    {
        static Mutex SingleInstanceMutex = null;

        public static bool AppAlreadyRunning(string app)
        {
            try
            {
                SingleInstanceMutex = Mutex.OpenExisting(app);
                return true;
            }
            catch (Exception)
            {
                SingleInstanceMutex = new Mutex(false, app);
            }
            finally
            {
                Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            }
            return false;
        }

        public static void CleanupMutex()
        {
            Application_ApplicationExit(null, null);
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                if (SingleInstanceMutex != null)
                {
                    SingleInstanceMutex.ReleaseMutex();
                }
            }
            catch (Exception)
            {
                // no recovery is required or possible.
            }
            finally
            {
                SingleInstanceMutex = null;
            }
        }

        static RegistryKey rkApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        public static bool WillRunAtStartup(string app)
        {
            try
            {
                return string.Equals(rkApp.GetValue(app), Environment.CommandLine);
            }
            catch (Exception)
            {
                // failure indicates lack of permissions, lack of platform support.
                return false;
            }
        }

        public static void RunAtStartup(string app, bool shouldRun)
        {
            try
            {
                if (shouldRun)
                {
                    rkApp.SetValue(app, Environment.CommandLine);
                }
                else
                {
                    rkApp.DeleteValue(app, false);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Unable to RunAtStartup: " + ex);
            }
        }
    }
}