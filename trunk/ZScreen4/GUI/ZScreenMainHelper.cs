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

namespace ZScreen4
{
    public partial class ZScreenMain : Form
    {
        public void TrayMenuLoadItems()
        {
            foreach (ImageUploaderType t in Enum.GetValues(typeof(ImageUploaderType)))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                tsmi.Tag = t;
                tsmi.CheckOnClick = true;
                tsmi.Click += new EventHandler(tsmi_Click);
                tsmiDestImages.DropDownItems.Add(tsmi);
            }
            foreach (FileUploaderType t in Enum.GetValues(typeof(FileUploaderType)))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                tsmi.Tag = t;
                tsmi.Click += new EventHandler(tsmi_Click);
                tsmiDestFiles.DropDownItems.Add(tsmi);
            }
            foreach (TextUploaderType t in Enum.GetValues(typeof(TextUploaderType)))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                tsmi.Tag = t;
                tsmi.Click += new EventHandler(tsmi_Click);
                tsmiDestText.DropDownItems.Add(tsmi);
            }
        }

        void tsmi_Click(object sender, EventArgs e)
        {

        }
    }
}
