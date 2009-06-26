using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            btnItemRemove.Enabled = this.MyCollection.SelectedIndex > 0;
        }
    }
}
