using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;

namespace ZScreenLib
{
    public partial class ClipboardOptions : Form
    {
        private WorkerTask mTask = null;

        public ClipboardOptions(WorkerTask task)
        {
            InitializeComponent();

            if (task != null && task.ImageManager != null)
            {
                this.mTask = task;
                this.Text = task.FileName.ToString() + " - " + task.GetDescription();
                this.pbPreview.LoadAsync(task.LocalFilePath);

                int xGap = 10;
                int yOffset = 20;
                int yGap = 25;
                int yMargin = pbPreview.Height + yOffset;

                int count = 0;
                foreach (ClipboardUriType type in Enum.GetValues(typeof(ClipboardUriType)))
                {
                    string url = task.ImageManager.GetUrlByType(type);
                    if (!string.IsNullOrEmpty(url))
                    {
                        // URL Label
                        Label lbl = new Label();
                        lbl.Location = new Point(20, count * yGap + yMargin + yOffset);
                        lbl.AutoSize = true;
                        lbl.Text = type.GetDescription();
                        this.Controls.Add(lbl);
                        // URL TextBox
                        TextBox txtUrl = new TextBox();
                        txtUrl.Location = new Point(170, count * yGap + yMargin + yOffset);
                        txtUrl.Size = new Size(320, 20);
                        txtUrl.Text = url;
                        txtUrl.ReadOnly = true;
                        this.Controls.Add(txtUrl);
                        // Copy Button
                        Button btnCopy = new Button();
                        btnCopy.Text = "Copy";
                        btnCopy.Tag = txtUrl;
                        btnCopy.AutoSize = true;
                        btnCopy.Location = new Point(txtUrl.Size.Width + 180, count * yGap + yMargin + yOffset);
                        btnCopy.Click += new EventHandler(btnCopy_Click);
                        this.Controls.Add(btnCopy);
                        // Offset
                        count++;
                    }
                }

                int yBottomControl = yMargin + count * yGap + yOffset * 2;

                Button btnCopyImage = new Button();
                btnCopyImage.Text = "Copy &Image";
                btnCopyImage.Location = new Point(20, yBottomControl);
                btnCopyImage.AutoSize = true;
                btnCopyImage.Click += new EventHandler(btnCopyImage_Click);
                this.Controls.Add(btnCopyImage);

                Button btnOpenLocal = new Button();
                btnOpenLocal.Text = "Open &Local file";
                btnOpenLocal.Location = new Point(btnCopyImage.Location.X + btnCopyImage.Width + xGap, yBottomControl);
                btnOpenLocal.AutoSize = true;
                btnOpenLocal.Click += new EventHandler(btnOpenLocal_Click);
                this.Controls.Add(btnOpenLocal);

                Button btnOpenRemote = new Button();
                btnOpenRemote.Text = "Open &Remote file";
                btnOpenRemote.Location = new Point(btnOpenLocal.Location.X + btnOpenLocal.Width + xGap, yBottomControl);
                btnOpenRemote.AutoSize = true;
                btnOpenRemote.Click += new EventHandler(btnOpenRemote_Click);
                this.Controls.Add(btnOpenRemote);

                Button btnDeleteClose = new Button();
                btnDeleteClose.Text = "&Delete Local file and Close";
                btnDeleteClose.Location = new Point(btnOpenRemote.Location.X + btnOpenRemote.Width + xGap, yBottomControl);
                btnDeleteClose.AutoSize = true;
                btnDeleteClose.Click += new EventHandler(btnDeleteClose_Click);
                this.Controls.Add(btnDeleteClose);

                Button btnClose = new Button();
                btnClose.Text = "&Close";
                btnClose.Location = new Point(btnDeleteClose.Location.X + btnDeleteClose.Width + xGap, yBottomControl);
                btnClose.AutoSize = true;
                btnClose.Click += new EventHandler(btnClose_Click);
                this.Controls.Add(btnClose);

                this.Height = yBottomControl + btnOpenLocal.Size.Height + yOffset * 2;
                Adapter.AddToClipboardByDoubleClick(this);
                ResetTimer();
            }
        }

        void btnCopyImage_Click(object sender, EventArgs e)
        {
            using (Image img = GraphicsMgr.GetImageSafely(mTask.LocalFilePath))
            {
                Clipboard.SetImage(img);
            }
        }

        void btnOpenRemote_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(mTask.RemoteFilePath))
            {
                Process.Start(mTask.RemoteFilePath);
            }
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnDeleteClose_Click(object sender, EventArgs e)
        {
            if (mTask != null && File.Exists(mTask.LocalFilePath))
            {
                File.Delete(mTask.LocalFilePath);
            }
            btnClose_Click(sender, e);
        }

        void btnOpenLocal_Click(object sender, EventArgs e)
        {
            if (mTask != null && !string.IsNullOrEmpty(mTask.LocalFilePath))
            {
                Process.Start(mTask.LocalFilePath);
            }
        }

        void btnCopy_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TextBox txtUrl = btn.Tag as TextBox;
            Clipboard.SetText(txtUrl.Text);
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetTimer()
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

        void Button_Click(object sender, EventArgs e)
        {
            tmrClose.Stop();
            tmrClose.Start();
        }
    }
}
