using System;
using System.Drawing;
using System.Windows.Forms;
using ZUploaderPlugin;

namespace ZUploaderCropPlugin
{
    public class CropPlugin : IPlugin
    {
        public IPluginHost Host { get; set; }

        public string Name
        {
            get { return "Crop Plugin"; }
        }

        public string Description
        {
            get { return "For testing."; }
        }

        public string Author
        {
            get { return "Jaex"; }
        }

        public string Version
        {
            get { return "1.0.0.0"; }
        }

        public void Init()
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = "Crop";
            tsmi.Click += new EventHandler(tsmi_Click);
            Host.AddPluginButton(tsmi);
        }

        private void tsmi_Click(object sender, EventArgs e)
        {
            Image screenshot = Helpers.GetScreenshot();

            using (CropLight crop = new CropLight(screenshot))
            {
                if (crop.ShowDialog() == DialogResult.OK)
                {
                    screenshot = Helpers.CropImage(screenshot, crop.SelectionRectangle);
                    Host.UploadImage(screenshot);
                }
            }
        }
    }
}