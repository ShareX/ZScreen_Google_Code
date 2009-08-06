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
        public ClipboardModePicker(MainAppTask task)
        {
            InitializeComponent();

            if (task.ImageManager != null)
            {
                int count = 0;
                foreach (ClipboardUriType type in Enum.GetValues(typeof(ClipboardUriType)))
                {
                    int yOffset = 20;
                    int yGap = 25;

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

                    this.Height = count * yGap + yOffset * 3;

                }

                Adapter.AddToClipboardByDoubleClick(this);
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
