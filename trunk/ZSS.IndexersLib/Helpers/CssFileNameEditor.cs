using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace ZSS.IndexersLib.Helpers
{
    class CssFileNameEditor : FileNameEditor
    {

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || provider == null)
            {
                return base.EditValue(context, provider, value);
            }
            OpenFileDialog dlg = new OpenFileDialog();            
            dlg.FileName = "Default.css";
            dlg.Title = "Browse for a Cascading Style Sheet...";
            dlg.Filter = "Cascading Style Sheets (*.css)|*.css";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                value = dlg.FileName;
            }
            return value;
        }
    }
}
