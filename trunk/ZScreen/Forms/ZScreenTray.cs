using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (Engine.conf.BalloonTipOpenLink)
            {
                try
                {
                    NotifyIcon ni = (NotifyIcon)sender;
                    ClickBalloonTip(ni.Tag as WorkerTask);
                }
                catch (Exception ex)
                {
                    Engine.MyLogger.WriteException(ex, "Error while clicking Balloon Tip");
                }
            }
        }

        public void ClickBalloonTip(WorkerTask task)
        {
            string cbString;
            switch (task.Job2)
            {
                case WorkerTask.JobLevel2.Translate:
                    cbString = task.TranslationInfo.Result;
                    if (!string.IsNullOrEmpty(cbString))
                    {
                        Clipboard.SetText(cbString); // ok
                    }
                    break;
                default:
                    if (task.UploadResults.Count > 0)
                    {
                        foreach (UploadResult ur in task.UploadResults)
                        {
                            if (!string.IsNullOrEmpty(ur.URL))
                            {
                                Process.Start(ur.URL);
                            }
                        }
                    }
                    else if (File.Exists(task.LocalFilePath))
                    {
                        Process.Start(task.LocalFilePath);
                    }
                    break;
            }
        }

        public string ShowBalloonTip(WorkerTask task)
        {
            StringBuilder sbMsg = new StringBuilder();
            ToolTipIcon tti = ToolTipIcon.Info;

            if (task.Job2 == WorkerTask.JobLevel2.Translate)
            {
                sbMsg.AppendLine(task.TranslationInfo.SourceLanguage + " -> " + task.TranslationInfo.TargetLanguage);
                sbMsg.AppendLine("Text: " + task.TranslationInfo.Text);
                sbMsg.AppendLine("Result: " + task.TranslationInfo.Result);
            }
            else
            {
                sbMsg.AppendLine("Name: " + task.GetDescription());

                switch (task.Job1)
                {
                    case JobLevel1.Text:
                        string dest = string.Empty;
                        switch (task.Job3)
                        {
                            case WorkerTask.JobLevel3.ShortenURL:
                                dest = task.GetActiveLinkUploadersDescription();
                                break;
                            default:
                                dest = task.GetActiveTextUploadersDescription();
                                break;
                        }
                        sbMsg.AppendLine(string.Format("Destination: {0}", dest));
                        break;
                    default:
                        sbMsg.Append("Outputs: ");
                        sbMsg.AppendLine(task.GetOutputsDescription());
                        break;
                }

                foreach (UploadResult ur in task.UploadResults)
                {
                    if (!string.IsNullOrEmpty(ur.URL))
                    {
                        sbMsg.AppendLine(ur.URL);
                        break;
                    }
                }

                if (task.UploadResults.Count == 0 && task.IsError)
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
    }
}