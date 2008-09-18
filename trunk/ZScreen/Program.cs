#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

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
#endregion

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using ZSS.Properties;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace ZSS
{

    static class Program
    {
        public static readonly string LocalAppDataFolder = System.IO.Path.Combine(
                       System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                       System.Windows.Forms.Application.ProductName);

        public static readonly string XMLSettingsFile = LocalAppDataFolder + "\\Settings.xml";

        public static XMLSettings conf = XMLSettings.Read();

        //Full list of languages intended to be supported
        //public static string[] languages =   { "العربية (Arabic)", "Chinese", "Nederlands (Dutch)", "English", "Français (French)", "Deutsch (German)", "Ελληνικά (Greek)", "Русский (Russian)", "Español (Spanish)" };
        //public static string[] langCode =    { "ar", "zh-CN", "nl", "en", "fr", "de", "el", "ru", "es" };

        public static readonly string[] languages = { "Nederlands (Dutch)", "English", "Ελληνικά (Greek)", "Русский (Russian)" };
        public static readonly string[] langCode = { "nl", "en", "el", "ru" };

        public const string FILTER_ACCOUNTS = "ZScreen FTP Accounts(*.zfa)|*.zfa";
        public const string FILTER_SETTINGS = "ZScreen XML Settings(*.xml)|*.xml";

        public static Image returnedCroppedImage;

        private static ZScreen mZScreenInstance;

        //Keyboard Hook
        private const int mWH_KEYBOARD_LL = 13;

        private delegate IntPtr mLowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static mLowLevelKeyboardProc m_Proc;

        //Imported Functions for Keyboard Hook
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            mLowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string proc = Process.GetCurrentProcess().ProcessName;

            Process[] processes = Process.GetProcessesByName(proc);

            if (processes.Length == 1)
            {

                if (/*not english*/!System.Threading.Thread.CurrentThread.CurrentCulture.NativeName.Contains("english") && Program.conf.Culture == "en")
                {
                    int num = Array.IndexOf(langCode, System.Threading.Thread.CurrentThread.CurrentCulture.Name.Split('-')[0]);
                    if (num != -1)
                    {
                        Program.conf.Culture = Program.langCode[num];
                        Program.conf.Save();
                    }
                }
                else
                {
                    //make sure previously saved language exists, otherwise set the default of english
                    int num = Array.IndexOf(langCode, Program.conf.Culture);
                    if (num < 0)
                    {
                        Program.conf.Culture = "en";
                        Program.conf.Save();
                    }
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Program.conf.Culture);
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                mZScreenInstance = new ZScreen();

                m_Proc = mZScreenInstance.keyboardHookCallback;

                mZScreenInstance.m_hID = setHook(m_Proc);

                Application.Run(mZScreenInstance);

                UnhookWindowsHookEx(mZScreenInstance.m_hID);
            }
        }

        public static string replaceErrorMessages(string msg)
        {
            string tmp;

            List<string> errors = new List<string>();
            
            int x = 1;

            while ((tmp = Properties.Resources.ResourceManager.GetString("ftp" + x++, CultureInfo.GetCultureInfo("en-US"))) != null )
            {
                errors.Add(tmp);
            }

            msg = msg.Replace("\r", "");

            for(int cnt = 0; cnt < errors.Count; cnt++)
            {
                string err = errors[cnt];

                if (msg.Contains(err))
                    return (tmp = Properties.Resources.ResourceManager.GetString("ftp" + (cnt + 1))) != null ? msg.Replace(err, tmp) : msg;
            }

            return msg;
        }

        private static IntPtr setHook(mLowLevelKeyboardProc proc)
        {
            using (Process currentProc = Process.GetCurrentProcess())
            using (ProcessModule currentMod = currentProc.MainModule)
            {
                return SetWindowsHookEx(mWH_KEYBOARD_LL, proc, GetModuleHandle(currentMod.ModuleName), 0);
            }
        }

        /// <summary>
        /// Searches for an Image Software in settings and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ImageSoftware GetImageSoftware(string name)
        {
            foreach (ImageSoftware app in conf.ImageSoftwareList)
            {
                if (app.Name.Equals(name))
                    return app;
            }
            return null;
        }

    }


}