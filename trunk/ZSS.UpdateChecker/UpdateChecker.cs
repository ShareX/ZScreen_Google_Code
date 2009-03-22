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

namespace ZSS.UpdateCheckerLib
{
    public class UpdateCheckerOptions
    {
        public bool CheckExperimental { get; set; }
        public UpdateCheckType UpdateCheckType { get; set; }
        public NewVersionWindowOptions MyNewVersionWindowOptions { get; set; }
    }

    public class UpdateChecker
    {
        private VersionInfo MyVersionInfo;
        public string Statistics { get; private set; }
        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value;
                DefaultDownloads = "http://code.google.com/p/" + projectName.ToLower() + "/downloads/list";
                AllDownloads = DefaultDownloads + "?can=1";
                CurrentDownloads = DefaultDownloads + "?can=2";
                FeaturedDownloads = DefaultDownloads + "?can=3";
                DeprecatedDownloads = DefaultDownloads + "?can=4";
                DownloadsSetupExe = DefaultDownloads + "?q=label:Type-Installer";
                DownloadsBinRar = DefaultDownloads + "?q=label:Type-Executable";
            }
        }

        private string projectName, DefaultDownloads, AllDownloads, CurrentDownloads,
            FeaturedDownloads, DeprecatedDownloads, DownloadsSetupExe, DownloadsBinRar;

        private UpdateCheckerOptions Options { get; set; }

        public UpdateChecker(string projectName, UpdateCheckerOptions options)
        {
            this.ProjectName = projectName;
            this.Options = options;
        }

        public string StartCheckUpdate()
        {
            try
            {
                if (this.Options.CheckExperimental)
                {
                    MyVersionInfo = CheckUpdate(AllDownloads);
                }
                else
                {
                    switch (this.Options.UpdateCheckType)
                    {
                        case UpdateCheckType.BIN_RAR:
                            MyVersionInfo = CheckUpdate(DownloadsBinRar);
                            break;
                        case UpdateCheckType.SETUP_EXE:
                            MyVersionInfo = CheckUpdate(DownloadsSetupExe);  // versionInfo = CheckUpdate(DownloadsSetupExe);
                            break;
                        default:
                            MyVersionInfo = CheckUpdate(DefaultDownloads);
                            break;
                    }
                }

                StringBuilder sbVersions = new StringBuilder();
                sbVersions.AppendLine("Current version: " + Application.ProductVersion);
                sbVersions.AppendLine(" Latest version: " + MyVersionInfo.Version);
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
                this.Options.MyNewVersionWindowOptions.Question = string.Format("New version of {0} is available.\n\nDo you want to download it now?\n\n{1}", Application.ProductName, this.Statistics);
                this.Options.MyNewVersionWindowOptions.VersionHistory = MyVersionInfo.Summary.Replace("|", "\r\n");
                this.Options.MyNewVersionWindowOptions.ProjectName = ProjectName;
                NewVersionWindow ver = new NewVersionWindow(this.Options.MyNewVersionWindowOptions);
                ver.Text = string.Format("{0} {1}", Application.ProductName, MyVersionInfo.Version);
                if (ver.ShowDialog() == DialogResult.Yes)
                {
                    Process.Start(MyVersionInfo.Link);
                }
            }
        }

        public VersionInfo CheckUpdate(string link)
        {
            VersionInfo returnValue = new VersionInfo();
            WebClient wClient = new WebClient();
            string source = wClient.DownloadString(link);
            returnValue.Link = Regex.Match(source, "(?<=<a href=\").+(?=\" style=\"white)").Value; //Link
            returnValue.Version = Regex.Match(returnValue.Link, @"(?<=.+)(?:\d+\.){3}\d+(?=.+)").Value; //Version
            returnValue.Summary = Regex.Match(source, "(?<=q=\">).+?(?=</a>)", RegexOptions.Singleline).Value.Replace("\n", "").
                Replace("\r", "").Trim(); //Summary
            return returnValue;
        }
    }
}