using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class PrintTextForm : Form
    {
        private PrintHelper printHelper;
        private PrintSettings printSettings;

        public PrintTextForm(string text, PrintSettings settings)
        {
            InitializeComponent();
            printHelper = new PrintHelper(text);
            printHelper.Settings = printSettings = settings;
            LoadSettings();
        }

        private void LoadSettings()
        {
            Font font = (Font)printSettings.TextFont;
            lblFont.Text = "Name: " + font.Name + ", Size: " + font.Size;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printHelper.Print();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnShowPreview_Click(object sender, EventArgs e)
        {
            printHelper.ShowPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnChangeFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.Font = printSettings.TextFont;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    printSettings.TextFont = fd.Font;
                    LoadSettings();
                }
            }
        }
    }
}