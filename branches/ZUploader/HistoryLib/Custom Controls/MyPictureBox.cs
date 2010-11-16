using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HistoryLib.Custom_Controls
{
    public partial class MyPictureBox : UserControl
    {
        public Image LoadingImage
        {
            set { pbMain.InitialImage = value; }
        }

        public PictureBoxSizeMode SizeMode
        {
            set { pbMain.SizeMode = value; }
        }

        private bool isReady;
        private bool isLoadLocal;

        public MyPictureBox()
        {
            InitializeComponent();
            pbMain.LoadCompleted += new AsyncCompletedEventHandler(pbMain_LoadCompleted);
            pbMain.LoadProgressChanged += new ProgressChangedEventHandler(pbMain_LoadProgressChanged);
        }

        public void LoadImage(string imagePath, string imageURL)
        {
            pbMain.Image = null;

            if (!string.IsNullOrEmpty(imagePath) && Helpers.IsImageFile(imagePath) && File.Exists(imagePath))
            {
                lblStatus.Text = "Loading local image...";
                isLoadLocal = true;
                LoadImage(imagePath);
            }
            else if (!string.IsNullOrEmpty(imageURL) && Helpers.IsImageFile(imageURL))
            {
                lblStatus.Text = "Downloading image from URL...";
                isLoadLocal = false;
                LoadImage(imageURL);
            }
        }

        private void LoadImage(string path)
        {
            isReady = false;
            lblStatus.Visible = true;
            this.Cursor = Cursors.Default;
            pbMain.LoadAsync(path);
        }

        private void pbMain_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            isReady = true;
            lblStatus.Visible = false;
            this.Cursor = Cursors.Hand;
        }

        private void pbMain_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string status;

            if (isLoadLocal)
            {
                status = "Loading local image - ";
            }
            else
            {
                status = "Downloading image from URL - ";
            }

            status += e.ProgressPercentage + "%";
            lblStatus.Text = status;
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isReady && pbMain.Image != null)
            {
                using (ImageViewer viewer = new ImageViewer(pbMain.Image))
                {
                    viewer.ShowDialog();
                }
            }
        }
    }
}