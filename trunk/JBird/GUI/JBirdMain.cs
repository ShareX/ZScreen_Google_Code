using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using HelpersLib;
using ZScreenLib;
using UploadersAPILib;

namespace JBirdGUI
{
    public partial class JBirdMain : HotkeyForm
    {
        public JBirdMain()
        {
            InitializeComponent();
            this.Text = Application.ProductName + " " + Application.ProductVersion;
        }

        private void JBirdMain_Load(object sender, EventArgs e)
        {
            BackgroundWorker bwConfig = new BackgroundWorker();
            bwConfig.DoWork += new DoWorkEventHandler(bwConfig_DoWork);
            bwConfig.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwConfig_RunWorkerCompleted);
            bwConfig.RunWorkerAsync();
        }

        private void JBirdMain_Shown(object sender, EventArgs e)
        {

        }

        private void JBirdMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.ProfilesConfig.Write();
        }

        void bwConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.ProfilesConfig = ProfileSettings.Read();
        }

        void bwConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Program.ProfilesConfig.Profiles.Count == 0)
            {
                Program.ProfilesConfig.Profiles.AddRange(CreateDefaultProfiles());
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureEntireScreen());
        }

        private void btnCaptureCropShot_Click(object sender, EventArgs e)
        {

        }

        private void btnCaptureActiveWindow_Click(object sender, EventArgs e)
        {

        }

        private void btnCaptureSelectedWindow_Click(object sender, EventArgs e)
        {

        }

        private void btnDestinations_Click(object sender, EventArgs e)
        {
            //  new UploadersConfigForm(Engine.conf.UploadersConfig2, ZKeys.GetAPIKeys()) { Icon = this.Icon }.Show();
        }

        private void btnProfiles_Click(object sender, EventArgs e)
        {
            ProfileManager pm = new ProfileManager(Program.ProfilesConfig.Profiles) { Icon = this.Icon };
            pm.ShowDialog();
        }
    }
}
