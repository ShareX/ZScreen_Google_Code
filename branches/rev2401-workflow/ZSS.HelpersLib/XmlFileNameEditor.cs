using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace HelpersLib
{
    public class XmlFileNameEditor : FileNameEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || provider == null)
            {
                return base.EditValue(context, provider, value);
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Browse for a XML Configuration file...";
            dlg.Filter = "XML Configuration Files(*.xml)|*.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                value = dlg.FileName;
            }
            return value;
        }
    }
}