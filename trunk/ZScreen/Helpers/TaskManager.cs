using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.Tasks;
using System.IO;
using System.Diagnostics;

namespace ZSS.Helpers
{
    public class TaskManager
    {
        private MainAppTask task; 

        public TaskManager(ref MainAppTask task)
        {
            this.task = task;
        }

        public void TextEdit()
        {
            if (File.Exists(task.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.TextEditorActive.Path);
                psi.Arguments = string.Format("{0}{1}{0}", "\"", task.LocalFilePath);
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();
            }
        }

        /// <summary>
        /// Edit Image in selected Image Editor
        /// </summary>
        /// <param name="task"></param>
        public void ImageEdit()
        {
            if (File.Exists(task.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.ImageSoftwareActive.Path);
                psi.Arguments = string.Format("{0}{1}{0}", "\"", task.LocalFilePath);
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();
            }
        }
    }
}
