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
            LoadSettings(config);
        }

        public void LoadSettings(GoogleTranslatorConfig config)
        {
            cbLanguageAutoDetect.Checked = config.GoogleAutoDetectSource;
            cbAutoTranslate.Checked = config.AutoTranslate;
            txtAutoTranslate.Text = config.AutoTranslateLength.ToString();

            if (Config.GoogleLanguages != null && Config.GoogleLanguages.Count > 0)
            {
                cbFromLanguage.Items.Clear();
                cbToLanguage.Items.Clear();

                foreach (GoogleLanguage lang in Config.GoogleLanguages)
                {
                    cbFromLanguage.Items.Add(lang.Name);
                    cbToLanguage.Items.Add(lang.Name);
                }

                SelectLanguage(Config.GoogleSourceLanguage, Config.GoogleTargetLanguage, Config.GoogleTargetLanguage2);

                if (cbFromLanguage.Items.Count > 0)
                {
                    cbFromLanguage.Enabled = true;
                }

                if (cbToLanguage.Items.Count > 0)
                {
                    cbToLanguage.Enabled = true;
                }
            }
        }

        public void SelectLanguage(string sourceLanguage, string targetLanguage, string targetLanguage2)
        {
            for (int i = 0; i < Config.GoogleLanguages.Count; i++)
            {
                if (Config.GoogleLanguages[i].Language == sourceLanguage)
                {
                    if (cbFromLanguage.Items.Count > i)
                    {
                        cbFromLanguage.SelectedIndex = i;
                    }

                    break;
                }
            }

            for (int i = 0; i < Config.GoogleLanguages.Count; i++)
            {
                if (Config.GoogleLanguages[i].Language == targetLanguage)
                {
                    if (cbToLanguage.Items.Count > i)
                    {
                        cbToLanguage.SelectedIndex = i;
                    }

                    break;
                }
            }

            btnTranslateTo1.Text = "To " + GetLanguageName(targetLanguage2);
        }

        public string GetLanguageName(string language)
        {
            foreach (GoogleLanguage gl in Config.GoogleLanguages)
            {
                if (gl.Language == language) return gl.Name;
            }

            return string.Empty;
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTranslateText.Text))
            {
                StartBW_LanguageTranslator(new GoogleTranslateInfo()
                {
                    Text = txtTranslateText.Text,
                    SourceLanguage = Config.GoogleSourceLanguage,
                    TargetLanguage = Config.GoogleTargetLanguage
                });
            }
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

                if (!string.IsNullOrEmpty(txtTranslateText.Text))
                {
                    TranslateFromTextBox();
                }
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
            btnTranslateTo1.Text = "To " + GetLanguageName(Config.GoogleTargetLanguage2);
        }

        private void btnTranslateTo1_Click(object sender, EventArgs e)
        {
            TranslateTo1();
        }

        private void btnTranslateTo1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) && e.AllowedEffect == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void btnTranslate_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}