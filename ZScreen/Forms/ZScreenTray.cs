using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        private void TrayImageEditor_MouseLeave(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.AutoClose = true;
        }

        private void TrayImageEditor_MouseEnter(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.AutoClose = false;
        }

        private void tsmiTab_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tcMain.SelectedTab = tcMain.TabPages[(string)tsmi.Tag];

            ShowWindow();
            tcMain.Focus();
        }

        private void tsmExitZScreen_Click(object sender, EventArgs e)
        {
            CloseMethod = CloseMethod.TrayButton;
            Close();
        }

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMainWindow();
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (Engine.ConfigUI != null && Engine.ConfigOptions.BalloonTipOpenLink)
            {
                try
                {
                    NotifyIcon ni = (NotifyIcon)sender;
                    ClickBalloonTip(ni.Tag as WorkerTask);
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteException(ex, "Error while clicking Balloon Tip");
                }
            }
        }

        private void niTray2_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        public void ClickBalloonTip(WorkerTask task)
        {
            string cbString;
            if (task != null)
            {
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
                                    try
                                    {
                                        ThreadPool.QueueUserWorkItem(x => Process.Start(ur.URL));
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        DebugHelper.WriteException(ex);
                                    }
                                }
                            }
                        }
                        else if (File.Exists(task.Info.LocalFilePath))
                        {
                            Process.Start(task.Info.LocalFilePath);
                        }
                        break;
                }
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
                    case EDataType.Text:
                        string dest = string.Empty;
                        switch (task.Job3)
                        {
                            case WorkerTask.JobLevel3.ShortenURL:
                                dest = task.WorkflowConfig.DestConfig.ToStringLinkUploaders();
                                break;
                            default:
                                dest = task.WorkflowConfig.DestConfig.ToStringTextUploaders();
                                break;
                        }
                        sbMsg.AppendLine(string.Format("Destination: {0}", dest));
                        break;
                    default:
                        sbMsg.Append("Outputs: ");
                        sbMsg.AppendLine(task.WorkflowConfig.DestConfig.ToStringOutputs());
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

                if (Engine.ConfigOptions.ShowUploadDuration && task.UploadDuration > 0)
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