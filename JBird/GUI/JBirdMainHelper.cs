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

namespace JBirdGUI
{
    public partial class JBirdMain : HotkeyForm
    {
        public List<Profile> CreateDefaultProfiles()
        {
            List<Profile> profiles = new List<Profile>();

            Profile entireScreen = new Profile("Desktop to file");
            entireScreen.FileNamePattern = "Screenshot";
            entireScreen.Job = WorkerTask.JobLevel2.CaptureEntireScreen;
            entireScreen.Outputs.Add(OutputEnum.Clipboard);
            entireScreen.Outputs.Add(OutputEnum.LocalDisk);
            profiles.Add(entireScreen);

            return profiles;
        }

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

        private void HideFormTemporary(MethodInvoker method, int executeTime = 500, int showTime = 2000)
        {
            var timer = new System.Windows.Forms.Timer { Interval = executeTime };
            var timer2 = new System.Windows.Forms.Timer { Interval = showTime };

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                method();
                timer2.Start();
            };

            timer2.Tick += (sender, e) =>
            {
                timer2.Stop();
                NativeMethods.ShowWindow(Handle, (int)NativeMethods.WindowShowStyle.ShowNormalNoActivate);
            };

            Hide();
            timer.Start();
        }

        private void ExecuteTimer(MethodInvoker method, ToolStripItem control, int executeTime = 3000)
        {
            var timer = new System.Windows.Forms.Timer { Interval = executeTime };

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                method();
                control.Enabled = true;
            };

            control.Enabled = false;
            timer.Start();
        }
    }
}
