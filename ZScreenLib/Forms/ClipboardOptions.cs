using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GraphicsMgrLib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    public partial class ClipboardOptions : Form
    {
        private WorkerTask urTask = null;

        public ClipboardOptions(WorkerTask task)
        {
            InitializeComponent();

            //if (task.TempImage != null)
            //{
            //    this.pbPreview.BackgroundImage = task.TempImage;
            //}

            if (task != null && task.UploadResults.Count > 0)
            {
                this.urTask = task;
                this.Text = task.GetDescription();
                this.pbPreview.LoadingImage = Resources.Loading;
                foreach (UploadResult ur in task.UploadResults)
                {
                    if (!string.IsNullOrEmpty(ur.URL))
                    {
                        this.pbPreview.LoadImage(task.LocalFilePath, ur.URL);
                        break;
                    }
                }

                int count = 0;
                foreach (UploadResult ur in task.UploadResults)
                {
                    TreeNode tnUploadResult = new TreeNode(ur.Host);
                    foreach (LinkFormatEnum type in Enum.GetValues(typeof(LinkFormatEnum)))
                    {
                        string url = ur.GetUrlByType(type, ur.URL);
                        if (!string.IsNullOrEmpty(url))
                        {
                            TreeNode tnLink = new TreeNode(type.GetDescription());
                            tnLink.Nodes.Add(url);
                            tnUploadResult.Nodes.Add(tnLink);

                            count++;
                        }
                    }
                    tvLinks.Nodes.Add(tnUploadResult);
                }

                tvLinks.ExpandAll();

                Button btnCopyLink = new Button();
                btnCopyLink.Text = "Copy &Link";
                btnCopyLink.AutoSize = true;
                btnCopyLink.Click += new EventHandler(btnCopyLink_Click);
                flpButtons.Controls.Add(btnCopyLink);

                Button btnCopyImage = new Button();
                btnCopyImage.Text = "Copy &Image";
                btnCopyImage.AutoSize = true;
                btnCopyImage.Click += new EventHandler(btnCopyImage_Click);
                flpButtons.Controls.Add(btnCopyImage);

                this.MinimumSize = new Size(this.Width, this.Height);

                Button btnOpenLocal = new Button();
                btnOpenLocal.Text = "Open &Local file";
                btnOpenLocal.AutoSize = true;
                btnOpenLocal.Click += new EventHandler(btnOpenLocal_Click);
                flpButtons.Controls.Add(btnOpenLocal);

                Button btnOpenRemote = new Button();
                btnOpenRemote.Text = "Open &Remote file";
                btnOpenRemote.AutoSize = true;
                btnOpenRemote.Click += new EventHandler(btnOpenRemote_Click);
                flpButtons.Controls.Add(btnOpenRemote);

                Button btnDeleteClose = new Button();
                btnDeleteClose.Text = "&Delete Local file and Close";
                btnDeleteClose.AutoSize = true;
                btnDeleteClose.Click += new EventHandler(btnDeleteClose_Click);
                flpButtons.Controls.Add(btnDeleteClose);

                Button btnClose = new Button();
                btnClose.Text = "&Close";
                btnClose.AutoSize = true;
                btnClose.Click += new EventHandler(btnClose_Click);
                flpButtons.Controls.Add(btnClose);

                this.AddResetTimerToButtons();
            }
        }

        private void btnCopyLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tvLinks.SelectedNode.Text);
        }

        private void btnCopyImage_Click(object sender, EventArgs e)
        {
            if (File.Exists(urTask.LocalFilePath))
            {
                using (Image img = GraphicsMgr.GetImageSafely(urTask.LocalFilePath))
                {
                    Adapter.CopyImageToClipboard(img);
                }
            }
            else if (urTask.TempImage != null)
            {
                Adapter.CopyImageToClipboard(urTask.TempImage);
            }
        }

        private void btnOpenRemote_Click(object sender, EventArgs e)
        {
            string url = tvLinks.SelectedNode.Text;
            if (FileSystem.IsValidLink(url))
            {
                ThreadPool.QueueUserWorkItem(x => Process.Start(url));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteClose_Click(object sender, EventArgs e)
        {
            if (urTask != null && File.Exists(urTask.LocalFilePath))
            {
                File.Delete(urTask.LocalFilePath);
            }
            btnClose_Click(sender, e);
        }

        private void btnOpenLocal_Click(object sender, EventArgs e)
        {
            if (urTask != null && !string.IsNullOrEmpty(urTask.LocalFilePath))
            {
                Process.Start(urTask.LocalFilePath);
            }
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddResetTimerToButtons()
        {
            Control ctl = this.GetNextControl(this, true);
            while (ctl != null)
            {
                if (ctl.GetType() == typeof(Button))
                {
                    ctl.Click += new EventHandler(Button_Click);
                }
                ctl = this.GetNextControl(ctl, true);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ResetTimer();
        }

        private void ClipboardOptions_Resize(object sender, EventArgs e)
        {
            ResetTimer();
        }

        private void ResetTimer()
        {
            tmrClose.Stop();
            tmrClose.Start();
        }

        private void tvLinks_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (FileSystem.IsValidLink(e.Node.Text))
            {
                Process.Start(e.Node.Text);
            }

            btnCopyLink_Click(sender, e);
        }

        private void tvLinks_Click(object sender, EventArgs e)
        {
            ResetTimer();
        }
    }
}