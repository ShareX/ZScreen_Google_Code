using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS.ImageUploadersLib;

namespace ZScreenLib
{
    public partial class ClipboardModePicker : Form
    {
        private MainAppTask mTask = null;

        public ClipboardModePicker(MainAppTask task)
        {
            InitializeComponent();

            this.mTask = task;
            this.Text = task.FileName.ToString() + " - " + task.GetDescription();

            if (task.ImageManager != null)
            {
                int xGap = 10;
                int yOffset = 20;
                int yGap = 25;

                int count = 0;
                foreach (ClipboardUriType type in Enum.GetValues(typeof(ClipboardUriType)))
                {
                    Label lbl = new Label();
                    lbl.Location = new Point(20, count * yGap + yOffset);
                    lbl.AutoSize = true;
                    lbl.Text = type.GetDescription();
                    this.Controls.Add(lbl);

                    TextBox txtUrl = new TextBox();
                    txtUrl.Location = new Point(170, count * yGap + yOffset);
                    txtUrl.Size = new Size(320, 20);
                    txtUrl.Text = task.ImageManager.GetUrlByType(type);
                    this.Controls.Add(txtUrl);

                    Button btnCopy = new Button();
                    btnCopy.Text = "Copy";
                    btnCopy.Tag = txtUrl;
                    btnCopy.AutoSize = true;
                    btnCopy.Location = new Point(txtUrl.Size.Width + 180, count * yGap + yOffset);
                    btnCopy.Click += new EventHandler(btnCopy_Click);
                    this.Controls.Add(btnCopy);
                    count++;
                }

                int yBottomControl = count * yGap + yOffset * 2;

                Button btnPreview = new Button();
                btnPreview.Text = "Open &Preview";
                btnPreview.Location = new Point(20, yBottomControl);
                btnPreview.AutoSize = true;
                btnPreview.Click += new EventHandler(btnPreview_Click);
                this.Controls.Add(btnPreview);

                Button btnDeleteClose = new Button();
                btnDeleteClose.Text = "&Delete Local File and Close";
                btnDeleteClose.Location = new Point(20 + btnPreview.Width + xGap, yBottomControl);
                btnDeleteClose.AutoSize = true;
                btnDeleteClose.Click += new EventHandler(btnDeleteClose_Click);
                this.Controls.Add(btnDeleteClose);

                Button btnClose = new Button();
                btnClose.Text = "&Close";
                btnClose.Location = new Point(btnDeleteClose.Location.X + btnDeleteClose.Width + xGap, yBottomControl);
                btnClose.AutoSize = true;
                btnClose.Click += new EventHandler(btnClose_Click);
                this.Controls.Add(btnClose);

                this.Height = yBottomControl + btnPreview.Size.Height + yOffset * 2;
                Adapter.AddToClipboardByDoubleClick(this);
            }
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnDeleteClose_Click(object sender, EventArgs e)
        {
            if (mTask != null && System.IO.File.Exists(mTask.LocalFilePath))
            {
                System.IO.File.Delete(mTask.LocalFilePath);
            }
            btnClose_Click(sender, e);
        }

        void btnPreview_Click(object sender, EventArgs e)
        {
            if (mTask != null && !string.IsNullOrEmpty(mTask.LocalFilePath))
            {
                System.Diagnostics.Process.Start(mTask.LocalFilePath);
            }
        }

        void btnCopy_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TextBox txtUrl = btn.Tag as TextBox;
            Clipboard.SetText(txtUrl.Text);
        }

        private void ClipboardModePicker_Shown(object sender, EventArgs e)
        {
            User32.ActivateWindow(this.Handle);
        }
    }
}
