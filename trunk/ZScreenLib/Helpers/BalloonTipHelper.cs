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

using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.HelperClasses;

namespace ZScreenLib
{
    public class BalloonTipHelper
    {
        private WorkerTask task;
        private NotifyIcon niTray;

        public BalloonTipHelper(NotifyIcon notifyIcon)
        {
            this.niTray = notifyIcon;
        }

        public BalloonTipHelper(NotifyIcon notifyIcon, WorkerTask task)
            : this(notifyIcon)
        {
            this.task = task;
        }

        public string ShowBalloonTip()
        {
            StringBuilder sbMsg = new StringBuilder();
            ToolTipIcon tti = ToolTipIcon.Info;

            niTray.Tag = task;

            if (task.Job2 == WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR)
            {
                sbMsg.AppendLine(task.TranslationInfo.SourceLanguage + " -> " + task.TranslationInfo.TargetLanguage);
                sbMsg.AppendLine("Text: " + task.TranslationInfo.Text);
                sbMsg.AppendLine("Result: " + task.TranslationInfo.Result);
            }
            else
            {
                sbMsg.AppendLine("Name: " + task.FileName);

                switch (task.Job1)
                {
                    case JobLevel1.Text:
                        string dest = string.Empty;
                        switch (task.Job3)
                        {
                            case WorkerTask.JobLevel3.ShortenURL:
                                dest = task.MyUrlShortener.GetDescription();
                                break;
                            default:
                                dest = task.GetActiveTextUploadersDescription();
                                break;
                        }
                        sbMsg.AppendLine(string.Format("Destination: {0}", dest));
                        break;
                    case JobLevel1.Image:
                          sbMsg.AppendLine("Destination(s):");
                        sbMsg.AppendLine(task.GetActiveImageUploadersDescription());
                        foreach (UploadResult ur in task.UploadResults)
                        {
                            if (!string.IsNullOrEmpty(ur.URL))
                            {
                                sbMsg.AppendLine(ur.URL);
                            }
                            if (!string.IsNullOrEmpty(ur.LocalFilePath))
                            {
                                sbMsg.AppendLine(ur.LocalFilePath);
                            }
                        }
                        
                        break;
                }

                sbMsg.AppendLine();

                if (string.IsNullOrEmpty(task.RemoteFilePath) && task.IsError)
                {
                    tti = ToolTipIcon.Warning;
                    sbMsg.AppendLine("Warnings: ");
                    foreach (string err in task.Errors)
                    {
                        sbMsg.AppendLine(err);
                    }
                }

                if (Engine.conf.ShowUploadDuration && task.UploadDuration > 0)
                {
                    sbMsg.AppendLine("Upload duration: " + task.UploadDuration + " ms");
                }
            }

            string message = sbMsg.ToString();

            if (!string.IsNullOrEmpty(message))
            {
                niTray.ShowBalloonTip(1000, Application.ProductName, message, tti);
            }

            return message;
        }

        public void ClickBalloonTip()
        {
            if (niTray.Tag != null)
            {
                WorkerTask task = (WorkerTask)niTray.Tag;
                string cbString;
                switch (task.Job2)
                {
                    case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                        cbString = task.TranslationInfo.Result;
                        if (!string.IsNullOrEmpty(cbString))
                        {
                            Clipboard.SetText(cbString); // ok
                        }
                        break;
                    default:
                        foreach (UploadResult ur in task.UploadResults)
                        {
                            if (!string.IsNullOrEmpty(ur.URL))
                            {
                                Process.Start(ur.URL);
                            }
                        }
                        break;
                }
            }
        }
    }
}