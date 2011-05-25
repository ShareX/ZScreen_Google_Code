using System;
using System.Windows.Forms;
using UploadersLib.OtherServices;
using System.ComponentModel;

namespace UploadersLib
{
    public partial class GoogleTranslateGUI : Form
    {
        public GoogleTranslatorConfig Config { get; private set; }
        public UploadersAPIKeys APIKeys { get; private set; }

        public GoogleTranslateGUI(GoogleTranslatorConfig config, UploadersAPIKeys uploadersAPIKeys)
        {
            InitializeComponent();
            this.Config = config;
            this.APIKeys = uploadersAPIKeys;
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            TranslateFromTextBox();
        }

        private void cbFromLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.GoogleSourceLanguage = Config.GoogleLanguages[cbFromLanguage.SelectedIndex].Language;
        }

        private void cbLanguageAutoDetect_CheckedChanged(object sender, EventArgs e)
        {
            Config.GoogleAutoDetectSource = cbLanguageAutoDetect.Checked;
        }

        private void cbToLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.GoogleTargetLanguage = Config.GoogleLanguages[cbToLanguage.SelectedIndex].Language;
        }

        private void cbAutoTranslate_CheckedChanged(object sender, EventArgs e)
        {
            Config.AutoTranslate = cbAutoTranslate.Checked;
        }

        private void txtTranslateText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TranslateFromTextBox();
            }
        }

        private void lblToLanguage_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbToLanguage.SelectedIndex > -1)
            {
                cbToLanguage.DoDragDrop(Config.GoogleTargetLanguage, DragDropEffects.Move);
            }
        }

        private void txtTranslateText_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtAutoTranslate.Text, out number))
            {
                Config.AutoTranslateLength = number;
            }
        }

        private void btnTranslateTo1_DragDrop(object sender, DragEventArgs e)
        {
            Config.GoogleTargetLanguage2 = e.Data.GetData(DataFormats.Text).ToString();
            btnTranslateTo.Text = "To " + GetLanguageName(Config.GoogleTargetLanguage2);
        }

        private void btnTranslateTo_Click(object sender, EventArgs e)
        {
            TranslateTo1();
        }

        private void btnTranslateTo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) && e.AllowedEffect == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void GoogleTranslateGUI_Load(object sender, EventArgs e)
        {
            LoadSettings(Config);
        }
    }
}