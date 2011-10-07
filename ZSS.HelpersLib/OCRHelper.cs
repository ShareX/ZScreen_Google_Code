using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace HelpersLib
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
            /*
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
                StaticHelper.WriteException(ex, "Error attempting to OCR " + fp);
                return string.Empty;
            }
            */
            return string.Empty;
        }
    }
}