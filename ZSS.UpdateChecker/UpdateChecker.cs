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
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;
using System.Xml.Serialization;
using System.IO;

namespace ZSS.UpdateCheckerLib
{
    public class UpdateCheckerOptions
    {
        public bool CheckBeta { get; set; }
        public NewVersionWindowOptions MyNewVersionWindowOptions { get; set; }
        public IWebProxy ProxySettings { get; set; }
    }

    public class UpdateChecker
    {
        private string projectName, downloadsList, labelFeatured, labelBeta;

        private UpdateCheckerOptions Options { get; set; }
        private VersionInfo MyVersionInfo;

        public string Statistics { get; private set; }

        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value.ToLower();
                downloadsList = "http://code.google.com/p/" + projectName + "/downloads/list";
                labelFeatured = "?q=label:Featured";
                labelBeta = "?q=label:Beta";
            }
        }

        public UpdateChecker(string projectName, UpdateCheckerOptions options)
        {
            this.ProjectName = projectName;
            this.Options = options;
        }

        public string StartCheckUpdate()
        {
            try
            {
                string url = downloadsList;

                if (!this.Options.CheckBeta)
                {
                    url += labelFeatured;
                }

                MyVersionInfo = CheckUpdate(url);

                StringBuilder sbVersions = new StringBuilder();
                sbVersions.AppendLine("Current version: " + Application.ProductVersion);
                sbVersions.AppendLine("Latest version:  " + MyVersionInfo.Version);
                this.Statistics = sbVersions.ToString();

                return sbVersions.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Update failed:\r\n" + ex.Message;
            }
        }

        public void ShowPrompt()
        {
            if (!string.IsNullOrEmpty(MyVersionInfo.Version) && new Version(MyVersionInfo.Version).CompareTo(new Version(Application.ProductVersion)) > 0)
            {
                this.Options.MyNewVersionWindowOptions.Question = string.Format("Do you want to download it now?\n\n{0}", this.Statistics);
                this.Options.MyNewVersionWindowOptions.VersionHistory = MyVersionInfo.Summary.Replace("|", "\r\n");
                this.Options.MyNewVersionWindowOptions.ProjectName = ProjectName;
                NewVersionWindow ver = new NewVersionWindow(this.Options.MyNewVersionWindowOptions)
                {
                    Text = string.Format("{0} {1} is available", Application.ProductName, MyVersionInfo.Version)
                };
                if (ver.ShowDialog() == DialogResult.Yes)
                {
                    string fnUpdater = "Updater.exe";
                    string dirUpdater = Path.Combine(Path.GetTempPath(), Application.ProductName);
                    string updater = Path.Combine(Application.StartupPath, fnUpdater);
                    string updater2 = Path.Combine(dirUpdater, fnUpdater);
                    if (!Directory.Exists(dirUpdater))
                    {
                        Directory.CreateDirectory(dirUpdater);
                    }
                    File.Copy(updater, updater2, true); // overwrite always to have to latest Updater copied

                    if (File.Exists(updater2))
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(updater2);
                        psi.Arguments = string.Format("--url {0} --filepath \"{1}\"", MyVersionInfo.Link, Process.GetCurrentProcess().MainModule.FileName);
                        Process p = new Process();
                        p.StartInfo = psi;
                        p.Start();
                    }
                    else
                    {
                        Process.Start(MyVersionInfo.Link);
                    }
                }
            }
        }

        public VersionInfo CheckUpdate(string link)
        {
            VersionInfo returnValue = new VersionInfo();
            WebClient wClient = new WebClient();
            if (this.Options.ProxySettings != null)
            {
                wClient.Proxy = this.Options.ProxySettings;
            }
            string source = wClient.DownloadString(link);
            returnValue.Link = Regex.Match(source, "(?<=<a href=\").+(?=\" style=\"white)").Value; //Link
            returnValue.Version = Regex.Match(returnValue.Link, @"(?<=.+)(?:\d+\.){3}\d+(?=.+)").Value; //Version
            returnValue.Summary = HttpUtility.HtmlDecode(Regex.Match(source, "(?<=&amp;q=.*\">).+?(?=</a>)", RegexOptions.Singleline).Value.
                Replace("\n", "").Replace("\r", "").Trim()); //Summary
            return returnValue;
        }
    }
}