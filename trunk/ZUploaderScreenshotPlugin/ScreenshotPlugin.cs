using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ZUploaderPluginBase;
using ZUploaderScreenshotPlugin.Properties;

namespace ZUploaderScreenshotPlugin
{
    public class ScreenshotPlugin : ZUploaderPlugin
    {
        public override string Name
        {
            get { return "Screenshot capture"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Author
        {
            get { return "Jaex"; }
        }

        public override string Description
        {
            get { return "Fullscreen capture, Crop capture"; }
        }

        public override void Initialize()
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