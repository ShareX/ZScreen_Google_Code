using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS.TextUploaderLib;

namespace ZSS.UserControls
{
    public partial class TextUploadersControl : UserControl
    {
        public TextUploadersControl()
        {
            InitializeComponent();
        }

        internal virtual void MyCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnItemRemove.Enabled = this.MyCollection.Items.Count > 1;
        }

        private void SettingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.MyCollection.SelectedIndex > -1)
            {
                TextUploader textUploader = (TextUploader)this.MyCollection.Items[this.MyCollection.SelectedIndex];
                if (SettingsGrid.SelectedObject.GetType() != typeof(FTPAccount))
                {
                    TextUploaderSettings settings = (TextUploaderSettings)SettingsGrid.SelectedObject;
                    if (!string.IsNullOrEmpty(settings.ServiceName))
                    {
                        textUploader.Name = settings.ServiceName;                     
                    }
                }
                else
                {
                    FTPAccount acc = SettingsGrid.SelectedObject as FTPAccount;
                    if (!string.IsNullOrEmpty(acc.ToString()))
                    {
                        textUploader.Name = acc.ToString();
                    }
                }
                this.MyCollection.Items[this.MyCollection.SelectedIndex] = textUploader;
            }
        }
    }
}