/*
 * Created by SharpDevelop.
 * User: jens
 * Date: 19.07.2007
 * Time: 21:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using Greenshot.Configuration;

namespace Greenshot
{
    /// <summary>
    /// Description of JpegQualityDialog.
    /// </summary>
    public partial class JpegQualityDialog : Form
    {
        AppConfig conf;
        public int Quality = 0;
        public JpegQualityDialog()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            conf = AppConfig.GetInstance();

            this.trackBarJpegQuality.Value = conf.Output_File_JpegQuality;
            this.textBoxJpegQuality.Text = conf.Output_File_JpegQuality.ToString();

        }


        void Button_okClick(object sender, System.EventArgs e)
        {
            Quality = this.trackBarJpegQuality.Value;
            if (this.checkbox_dontaskagain.Checked)
            {
                conf.Output_File_JpegQuality = Quality;
                conf.Output_File_PromptJpegQuality = false;
                conf.Store();
            }

        }

        void TrackBarJpegQualityScroll(object sender, System.EventArgs e)
        {
            textBoxJpegQuality.Text = trackBarJpegQuality.Value.ToString();
        }
    }
}