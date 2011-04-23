using System.Windows.Forms;

namespace HistoryLib
{
    public partial class HistoryItemInfoForm : Form
    {
        public HistoryItemInfoForm(object hi)
        {
            InitializeComponent();
            olvMain.SelectObject(hi);
        }
    }
}