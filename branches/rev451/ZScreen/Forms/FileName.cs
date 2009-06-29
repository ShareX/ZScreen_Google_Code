using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZSS.Forms
{
    public partial class FileName : Form
    {
        public string filename;
        public int destination;

        public FileName()
        {
            InitializeComponent();

            User32.ActivateWindow(this.Handle);

            // Populate destinations
            cbDestinations.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            cbDestinations.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdateDestination();
            filename = txtFileName.Text;
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void FileName_Load(object sender, EventArgs e)
        {
            txtFileName.Text = filename;
        }

        private void UpdateDestination()
        {
            ImageDestType sdt = (ImageDestType)cbDestinations.SelectedIndex;
            Program.conf.ScreenshotDestMode = sdt;
        }

        private void FileName_Shown(object sender, EventArgs e)
        {
            txtFileName.Focus();
            txtFileName.SelectionLength = txtFileName.Text.Length;
        }
    }
}
