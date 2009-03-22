using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;

namespace ZSS.UpdateCheckerLib
{
    public partial class NewVersionWindow : Form
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool SetForegroundWindow(int hWnd);
        private NewVersionWindowOptions Options { get; set; }

        public NewVersionWindow(NewVersionWindowOptions options)
        {
            InitializeComponent();
            this.Options = options;

            if (this.Options.MyIcon != null)
                this.Icon = this.Options.MyIcon;
            if (this.Options.MyImage != null)
                this.pbApp.Image = this.Options.MyImage;

            this.lblVer.Text = this.Options.Question;
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.Options.VersionHistory) &&
                 this.Options.VersionHistory.StartsWith("http://" + this.Options.ProjectName + ".googlecode.com/"))
            {
                WebClient wClient = new WebClient();
                string versionHistory = wClient.DownloadString(this.Options.VersionHistory);
                if (!string.IsNullOrEmpty(versionHistory))
                {
                    this.txtVer.Text = versionHistory;
                }
            }
            else
            {
                this.txtVer.Text = this.Options.VersionHistory;
            }
            SetForegroundWindow(this.Handle.ToInt32());
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }

    public class NewVersionWindowOptions
    {
        public Icon MyIcon { get; set; }
        public Image MyImage { get; set; }
        public string VersionHistory { get; set; }
        public string Question { get; set; }
        public string ProjectName { get; set; }
    }
}