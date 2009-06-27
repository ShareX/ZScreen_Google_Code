#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

#region Source code: Greenshot (GPL)
/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/
#endregion

/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Administrator
 * Datum: 11.11.2007
 * Zeit: 15:07
 * 
 * Sie k�nnen diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader �ndern.
 */

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

using Greenshot.Configuration;
using Greenshot.Forms;

namespace Greenshot.Helpers
{
    /// <summary>
    /// Description of PrintHelper.
    /// </summary>
    public class PrintHelper
    {
        private Image image;
        private PrintDocument printDocument = new PrintDocument();
        private PrintDialog printDialog = new PrintDialog();

        public PrintHelper(Image image)
        {
            this.image = image;
            printDialog.UseEXDialog = true;
            printDocument.DocumentName = "New";
            printDocument.PrintPage += GetImageForPrint;
            printDialog.Document = printDocument;
        }

        public PrinterSettings PrintWithDialog()
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
                return printDialog.PrinterSettings;
            }
            else
            {
                return null;
            }
        }

        private void GetImageForPrint(object sender, PrintPageEventArgs e)
        {
            PrintOptionsDialog pod = new PrintOptionsDialog();
            pod.ShowDialog();

            ContentAlignment alignment = pod.AllowPrintCenter ? ContentAlignment.MiddleCenter : ContentAlignment.TopLeft;

            RectangleF pageRect = e.PageSettings.PrintableArea;
            GraphicsUnit gu = GraphicsUnit.Pixel;
            RectangleF imageRect = image.GetBounds(ref gu);
            // rotate the image if it fits the page better
            if (pod.AllowPrintRotate)
            {
                if ((pageRect.Width > pageRect.Height && imageRect.Width < imageRect.Height) ||
                   (pageRect.Width < pageRect.Height && imageRect.Width > imageRect.Height))
                {
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    imageRect = image.GetBounds(ref gu);
                    if (alignment.Equals(ContentAlignment.TopLeft)) alignment = ContentAlignment.TopRight;
                }
            }
            RectangleF printRect = new RectangleF(0, 0, imageRect.Width, imageRect.Height); ;
            // scale the image to fit the page better
            if (pod.AllowPrintEnlarge || pod.AllowPrintShrink)
            {
                SizeF resizedRect = ScaleHelper.GetScaledSize(imageRect.Size, pageRect.Size, false);
                if ((pod.AllowPrintShrink && resizedRect.Width < printRect.Width) ||
                   pod.AllowPrintEnlarge && resizedRect.Width > printRect.Width)
                {
                    printRect.Size = resizedRect;
                }

            }
            // align the image
            printRect = ScaleHelper.GetAlignedRectangle(printRect, new RectangleF(0, 0, pageRect.Width, pageRect.Height), alignment);

            e.Graphics.DrawImage(image, printRect, imageRect, GraphicsUnit.Pixel);
        }
    }
}