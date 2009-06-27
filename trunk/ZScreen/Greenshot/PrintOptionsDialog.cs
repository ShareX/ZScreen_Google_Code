/*
 * Erstellt mit SharpDevelop.
 * Benutzer: jens
 * Datum: 11.02.2008
 * Zeit: 22:53
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using Greenshot.Configuration;

namespace Greenshot.Forms
{
    /// <summary>
    /// Description of PrintOptionsDialog.
    /// </summary>
    public partial class PrintOptionsDialog : Form
    {
        AppConfig conf;

        public bool AllowPrintCenter;
        public bool AllowPrintEnlarge;
        public bool AllowPrintRotate;
        public bool AllowPrintShrink;
        public PrintOptionsDialog()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //

            conf = AppConfig.GetInstance();

            this.AllowPrintCenter = this.checkboxAllowCenter.Checked = (bool)conf.Output_Print_Center;
            this.AllowPrintEnlarge = this.checkboxAllowEnlarge.Checked = (bool)conf.Output_Print_AllowEnlarge;
            this.AllowPrintRotate = this.checkboxAllowRotate.Checked = (bool)conf.Output_Print_AllowRotate;
            this.AllowPrintShrink = this.checkboxAllowShrink.Checked = (bool)conf.Output_Print_AllowShrink;
            this.checkbox_dontaskagain.Checked = false;

        }

        void Button_okClick(object sender, EventArgs e)
        {
            this.AllowPrintCenter = this.checkboxAllowCenter.Checked;
            this.AllowPrintEnlarge = this.checkboxAllowEnlarge.Checked;
            this.AllowPrintRotate = this.checkboxAllowRotate.Checked;
            this.AllowPrintShrink = this.checkboxAllowShrink.Checked;
            if (this.checkbox_dontaskagain.Checked)
            {
                conf.Output_Print_Center = (bool?)this.AllowPrintCenter;
                conf.Output_Print_AllowEnlarge = (bool?)this.AllowPrintEnlarge;
                conf.Output_Print_AllowRotate = (bool?)this.AllowPrintRotate;
                conf.Output_Print_AllowShrink = (bool?)this.AllowPrintShrink;
                conf.Output_Print_PromptOptions = false;
                conf.Store();
            }
        }
    }
}