using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using ZScreenLib;

namespace ZScreenLib
{
    public partial class DestSelector : UserControl
    {
        public DestSelector()
        {
            InitializeComponent();
        }

        private void cbImageUploaderUseFile_CheckedChanged(object sender, EventArgs e)
        {
            cboImageUploaders.Enabled = chkImageUploaderEnabled.Checked;
        }

        private void cbTextUploaderUseFile_CheckedChanged(object sender, EventArgs e)
        {
            cboTextUploaders.Enabled = chkTextUploaderEnabled.Checked;
        }
    }
}