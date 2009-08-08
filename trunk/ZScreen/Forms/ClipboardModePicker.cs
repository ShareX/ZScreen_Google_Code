using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS.ImageUploadersLib;
using System.Diagnostics;
using System.IO;

namespace ZScreenLib
{
    public partial class ClipboardModePicker : Form
    {
        private MainAppTask mTask = null;

        public ClipboardModePicker(MainAppTask task)
        {
            InitializeComponent();

            if (task != null && task.ImageManager != null)
            {
                this.mTask = task;
                this.Text = task.FileName.ToString() + " - " + task.GetDescription();
                // this.pbPreview.Image = task.MyImage;
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

                int yBottomControl = yMargin+ count * yGap + yOffset * 2;

                Button btnOpenLocal = new Button();
                btnOpenLocal.Text = "Open &Local file";
                btnOpenLocal.Location = new Point(20, yBottomControl);
                btnOpenLocal.AutoSize = true;
                btnOpenLocal.Click += new EventHandler(btnPreview_Click);
                this.Controls.Add(btnOpenLocal);

                Button btnOpenRemote = new Button();
                btnOpenRemote.Text = "Open &Remote file";
                btnOpenRemote.Location = new Point(20 + btnOpenLocal.Width + xGap, yBottomControl);
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

        void btnPreview_Click(object sender, EventArgs e)
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
            tmrClose.Stop();
            tmrClose.Start();
        }

        private void ClipboardModePicker_Shown(object sender, EventArgs e)
        {
            User32.ActivateWindow(this.Handle);
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
