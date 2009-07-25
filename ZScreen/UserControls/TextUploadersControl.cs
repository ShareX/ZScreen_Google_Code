using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS.TextUploadersLib;

namespace ZSS.UserControls
{
    public partial class TextUploadersControl : UserControl
    {
        public ComboBox MyComboBox;

        public TextUploadersControl()
        {
            InitializeComponent();
        }

        private void SettingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.MyCollection.SelectedIndex > -1)
            {
                TextUploader textUploader = this.MyCollection.Items[this.MyCollection.SelectedIndex] as TextUploader;

                if (textUploader != null)
                {
                    if (textUploader.Settings.GetType() == typeof(TextUploaderSettings))
                    {
                        TextUploaderSettings settings = (TextUploaderSettings)textUploader.Settings;
                        if (string.IsNullOrEmpty(settings.Name))
                        {
                            settings.Name = (string)e.OldValue;
                        }
                    }
                    else if (textUploader.Settings.GetType() == typeof(FTPAccount))
                    {
                        FTPAccount acc = (FTPAccount)textUploader.Settings;
                        if (string.IsNullOrEmpty(acc.Name))
                        {
                            acc.Name = (string)e.OldValue;
                        }
                    }

                    this.MyCollection.Items[this.MyCollection.SelectedIndex] = textUploader;

                    if (this.MyCollection.SelectedIndex < MyComboBox.Items.Count)
                    {
                        MyComboBox.Items[this.MyCollection.SelectedIndex] = textUploader;
                    }
                }
            }
        }
    }
}