using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ZScreenLib.Helpers
{
    internal class SoundFileNameEditor : FileNameEditor
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
