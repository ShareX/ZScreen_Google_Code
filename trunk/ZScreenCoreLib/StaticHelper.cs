using System;
using System.Windows.Forms;
using HelpersLib;

namespace ZScreenCoreLib
{
    public static class ZScreenCoreHelper
    {
        public static DialogResult ShowFontDialog(WatermarkConfig Config)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                var fDialog = new FontDialog
                {
                    ShowColor = true
                };
                try
                {
                    fDialog.Color = Config.WatermarkFontArgb;
                    fDialog.Font = Config.WatermarkFont;
                }
                catch (Exception err)
                {
                    DebugHelper.WriteException(err, "Error while initializing Font and Color");
                }

                result = fDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Config.WatermarkFont = fDialog.Font;
                    Config.WatermarkFontArgb = fDialog.Color;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex, "Error while setting Watermark Font");
            }
            return result;
        }
    }
}