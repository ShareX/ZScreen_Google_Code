#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;

namespace ZSS.ImageUploader.Helpers
{
    public class UpdateChecker
    {
        private const string DefaultDownloads = "http://code.google.com/p/zscreen/downloads/list";
        private const string AllDownloads = DefaultDownloads + "?can=1";
        private const string CurrentDownloads = DefaultDownloads + "?can=2";
        private const string FeaturedDownloads = DefaultDownloads + "?can=3";
        private const string DeprecatedDownloads = DefaultDownloads + "?can=4";
        public static bool IsBusy = false;

        public static void CheckUpdates()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Thread updateThread = new Thread(UpdateThread);
                updateThread.Start();
            }
        }

        private static string[] CheckUpdate(string link)
        {
            string[] returnValue = new string[3];
            WebClient wClient = new WebClient();
            string source = wClient.DownloadString(link);
            returnValue[0] = Regex.Match(source, "(?<=<a href=\").+(?=\" style=\"white)").Value; //Link
            returnValue[1] = Regex.Match(returnValue[0], @"(?<=.+)(?:\d+\.){3}\d+(?=.+)").Value; //Version
            returnValue[2] = Regex.Match(source, "(?<=q=\">).+?(?=</a>)", RegexOptions.Singleline).Value.Replace("\n", "").
                Replace("\r", "").Trim(); //Summary
            return returnValue;
        }

        private static void UpdateThread()
        {
            try
            {
                string[] updateValues;
                if (Program.conf.CheckExperimental)
                {
                    updateValues = CheckUpdate(AllDownloads);
                }
                else
                {
                    updateValues = CheckUpdate(CurrentDownloads);
                }
                if (!string.IsNullOrEmpty(updateValues[1]) && new Version(updateValues[1]).
                    CompareTo(new Version(Application.ProductVersion)) > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("New version available");
                    sb.AppendLine();
                    sb.AppendLine("Current version:\t" + Application.ProductVersion);
                    sb.AppendLine("Latest version:\t" + updateValues[1]);
                    sb.AppendLine();
                    sb.AppendLine(updateValues[2].Replace("|", "\n"));
                    sb.AppendLine();
                    sb.AppendLine("Press OK to download the latest version.");

                    if (MessageBox.Show(sb.ToString(), Application.ProductName, MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Process.Start(updateValues[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            IsBusy = false;
        }
    }
}