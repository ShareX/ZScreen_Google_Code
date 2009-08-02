using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS.Helpers;
using ZSS.Properties;

namespace ZScreenCLI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                try
                {
                    ZSS.Program.SetRootFolder(!string.IsNullOrEmpty(ZSS.Properties.Settings.Default.RootDir) ? ZSS.Properties.Settings.Default.RootDir : ZSS.Program.DefaultRootAppFolder);
                    ZSS.Program.InitializeDefaultFolderPaths();
                    ZSS.Program.conf = ZSS.XMLSettings.Read();
                    WorkerPrimary worker = new ZSS.Helpers.WorkerPrimary(null);
                    if (args[1] == "crop_shot")
                    {
                        worker.StartBW_CropShot();
                    }
                    else if (args[1] == "selected_window")
                    {
                        worker.StartBW_SelectedWindow();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        
    }
}