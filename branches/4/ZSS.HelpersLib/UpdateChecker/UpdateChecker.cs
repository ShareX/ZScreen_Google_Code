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
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using HelpersLib;

namespace ZSS.UpdateCheckerLib
{
    public enum ReleaseChannelType
    {
        [Description("Stable version")]
        Stable,
        [Description("Beta version")]
        Beta,
        [Description("Dev version")]
        Dev
    }

    public enum UpdateStatus
    {
        UpdateCheckFailed,
        UpdateRequired,
        UpToDate
    }

    public class UpdateChecker
    {
        public string URL { get; private set; }
        public string ApplicationName { get; private set; }
        public Version ApplicationVersion { get; private set; }
        public ReleaseChannelType ReleaseChannel { get; private set; }
        public UpdateInfo UpdateInfo { get; private set; }
        public bool AutoDownloadSummary { get; set; }

        private IWebProxy proxy;
        private NewVersionWindowOptions nvwo;

        public UpdateChecker(string url, string applicationName, Version applicationVersion, ReleaseChannelType channel, IWebProxy proxy, NewVersionWindowOptions nvwo = null)
        {
            URL = url;
            ApplicationName = applicationName;
            ApplicationVersion = applicationVersion;
            ReleaseChannel = channel;
            AutoDownloadSummary = true;
            this.proxy = proxy;
            this.nvwo = nvwo;
        }

        public bool CheckUpdate()
        {
            UpdateInfo = new UpdateInfo(ReleaseChannel);
            UpdateInfo.ApplicationVersion = ApplicationVersion;

            try
            {
                using (WebClient wc = new WebClient { Proxy = proxy })
                using (MemoryStream ms = new MemoryStream(wc.DownloadData(URL)))
                using (XmlTextReader xml = new XmlTextReader(ms))
                {
                    XDocument xd = XDocument.Load(xml);

                    if (xd != null)
                    {
                        string node;

                        switch (ReleaseChannel)
                        {
                            default:
                            case ReleaseChannelType.Stable:
                                node = "Stable";
                                break;
                            case ReleaseChannelType.Beta:
                                node = "Beta|Stable";
                                break;
                            case ReleaseChannelType.Dev:
                                node = "Dev|Beta|Stable";
                                break;
                        }

                        string path = string.Format("Update/{0}/{1}", ApplicationName, node);
                        XElement xe = xd.GetNode(path);

                        if (xe != null)
                        {
                            UpdateInfo.LatestVersion = new Version(xe.GetValue("Version"));
                            UpdateInfo.URL = xe.GetValue("URL");
                            UpdateInfo.Date = DateTime.Parse(xe.GetValue("Date"), CultureInfo.InvariantCulture);
                            UpdateInfo.Summary = xe.GetValue("Summary");

                            if (UpdateInfo.IsUpdateRequired)
                            {
                                UpdateInfo.Status = UpdateStatus.UpdateRequired;

                                if (AutoDownloadSummary && !string.IsNullOrEmpty(UpdateInfo.Summary) && UpdateInfo.Summary.IsValidUrl())
                                {
                                    try
                                    {
                                        UpdateInfo.Summary = wc.DownloadString(UpdateInfo.Summary.Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        StaticHelper.WriteException(ex);
                                    }
                                }
                            }
                            else
                            {
                                UpdateInfo.Status = UpdateStatus.UpToDate;
                            }

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex);
            }

            UpdateInfo.Status = UpdateStatus.UpdateCheckFailed;

            return false;
        }

        public bool ShowPrompt()
        {
            if (UpdateInfo != null && UpdateInfo.IsUpdateRequired)
            {
                nvwo.Question = string.Format("Do you want to download it now?\n\n{0}", UpdateInfo.ToString());
                nvwo.UpdateInfo = UpdateInfo;
                nvwo.ProjectName = ApplicationName;

                using (UpdaterForm ver = new UpdaterForm(nvwo))
                {
                    if (ver.ShowDialog() == DialogResult.Yes)
                    {
                        StaticHelper.LoadBrowser(UpdateInfo.URL);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}