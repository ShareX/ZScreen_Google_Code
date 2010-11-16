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
                lblStatus.Visible = true;
                lblStatus.Text = "Loading local image...";
                isLoadLocal = true;
                pbMain.LoadAsync(imagePath);
            }
            else if (!string.IsNullOrEmpty(imageURL) && Helpers.IsImageFile(imageURL))
            {
                lblStatus.Visible = true;
                lblStatus.Text = "Downloading image from URL...";
                isLoadLocal = false;
                pbMain.LoadAsync(imageURL);
            }
        }

        private void pbMain_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lblStatus.Visible = false;
        }

        private void pbMain_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (isLoadLocal)
            {
                lblStatus.Text = "Loading local image - ";
            }
            else
            {
                lblStatus.Text = "Downloading image from URL - ";
            }

            lblStatus.Text += e.ProgressPercentage + "%";
        }
    }
}