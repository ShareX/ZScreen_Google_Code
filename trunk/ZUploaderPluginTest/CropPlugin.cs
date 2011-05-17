using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ZUploaderCropPlugin.Properties;
using ZUploaderPlugin;

namespace ZUploaderCropPlugin
{
    public class CropPlugin : IPlugin
    {
        public IPluginHost Host { get; set; }

        public string Name
        {
            get { return "Crop Capture"; }
        }

        public string Description
        {
            get { return "..."; }
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
            tsmi.Text = "Crop capture";
            tsmi.Image = Resources.layer_shape;
            tsmi.Click += new EventHandler(tsmi_Click);
            Host.AddPluginButton(tsmi);
        }

        private void tsmi_Click(object sender, EventArgs e)
        {
            Host.Hide();

            Thread.Sleep(500);

            try
            {
                Image screenshot = Helpers.GetScreenshot();

                using (CropLight crop = new CropLight(screenshot))
                {
                    if (crop.ShowDialog() == DialogResult.OK)
                    {
                        screenshot = Helpers.CropImage(screenshot, crop.SelectionRectangle);
                        Host.Show();
                        Host.UploadImage(screenshot);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                Host.Show();
            }
        }
    }
}