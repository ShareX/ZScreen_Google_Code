using System.Windows.Forms;

namespace ZScreenLib
{
    public partial class GenericMainWindow : Form
    {
        public GenericMainWindow()
        {
            InitializeComponent();
        }

        public NotifyIcon niTray = new NotifyIcon();
    }
}