using System.Windows.Forms;

namespace ZUploader
{
    public partial class ResponseForm : Form
    {
        public string Response { get; private set; }

        private bool isOpened;

        public ResponseForm(string response)
        {
            InitializeComponent();
            Response = response;
            txtSource.Text = Response;
        }

        private void tcResponse_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1 && !isOpened)
            {
                wbResponse.DocumentText = Response;
                isOpened = true;
            }
        }
    }
}