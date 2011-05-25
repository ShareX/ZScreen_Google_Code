using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib.OtherServices;

namespace UploadersLib
{
    public partial class GoogleTranslateGUI : Form
    {
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

        public void TranslateFromTextBox()
        {
            if (!string.IsNullOrEmpty(txtTranslateText.Text))
            {
                StartBW_LanguageTranslator(new GoogleTranslateInfo
                {
                    Text = txtTranslateText.Text,
                    SourceLanguage = Config.GoogleAutoDetectSource ? null : Config.GoogleSourceLanguage,
                    TargetLanguage = Config.GoogleTargetLanguage
                });
            }
        }

        public void TranslateTo1()
        {
            if (Config.GoogleTargetLanguage2 == "?")
            {
                lblToLanguage.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("Drag n drop 'To:' label to this button for be able to set button language.", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblToLanguage.BorderStyle = BorderStyle.None;
            }
            else
            {
                TranslateFromTextBox();
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

            btnTranslateTo.Text = "To " + GetLanguageName(targetLanguage2);
        }

        public string GetLanguageName(string language)
        {
            foreach (GoogleLanguage gl in Config.GoogleLanguages)
            {
                if (gl.Language == language) return gl.Name;
            }

            return string.Empty;
        }

        public void StartBW_LanguageTranslator(GoogleTranslateInfo gti)
        {
            btnTranslate.Enabled = false;
            btnTranslateTo.Enabled = false;
            CreateWorker().RunWorkerAsync(gti);
        }


    }
}
