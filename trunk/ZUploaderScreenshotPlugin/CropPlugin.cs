using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ZUploaderPlugin;
using ZUploaderScreenshotPlugin.Properties;

namespace ZUploaderScreenshotPlugin
{
    public class CropPlugin : IPlugin
    {
        public IPluginHost Host { get; set; }

        public string Name
        {
            get { return "Screenshot capture"; }
        }

        public string Description
        {
            get { return "Fullscreen capture, Crop capture"; }
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
            ToolStripMenuItem tsmiFullscreen = new ToolStripMenuItem();
            tsmiFullscreen.Text = "Fullscreen capture";
            tsmiFullscreen.Image = Resources.layer;
            tsmiFullscreen.Click += new EventHandler(tsmiFullscreen_Click);

            ToolStripMenuItem tsmiCrop = new ToolStripMenuItem();
            tsmiCrop.Text = "Crop capture";
            tsmiCrop.Image = Resources.layer_shape;
            tsmiCrop.Click += new EventHandler(tsmiCrop_Click);

            Host.AddPluginButton(tsmiFullscreen);
            Host.AddPluginButton(tsmiCrop);
        }

        private void tsmiFullscreen_Click(object sender, EventArgs e)
        {
            Host.Hide();
            Thread.Sleep(250);

            try
            {
                Image screenshot = Helpers.GetScreenshot();
                Host.Show();
                Host.UploadImage(screenshot);
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

        private void tsmiCrop_Click(object sender, EventArgs e)
        {
            Host.Hide();
            Thread.Sleep(250);

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