using System;
using HelpersLib;

namespace ZScreenLib
{
    public class OCRHelper
    {
        public string Text { get; private set; }
        private string FilePath;

        public OCRHelper(string fp)
        {
            this.FilePath = fp;
            this.Text = OCR(fp);
        }

        private string OCR(string fp)
        {
            try
            {
                //OCR Operations ...
                MODI.Document md = new MODI.Document();
                md.Create(fp);
                md.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, true, true);
                MODI.Image image = (MODI.Image)md.Images[0];
                return image.Layout.Text;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex, "Error attempting to OCR " + fp);
                return string.Empty;
            }
        }
    }
}