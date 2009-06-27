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
 * Benutzer: thomas
 * Datum: 22.03.2007
 * Zeit: 23:09
 * 
 * Sie k�nnen diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader �ndern.
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using Greenshot.Configuration;
using Greenshot.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Resources;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Greenshot.Helpers;

namespace Greenshot
{
    /// <summary>
    /// Description of ImageEditorForm.
    /// </summary>
    public partial class ImageEditorForm : Form
    {
        private ColorDialog colorDialog = ColorDialog.GetInstance();
        private string lastSaveFullPath;
        private AppConfig conf = AppConfig.GetInstance();
        private Surface surface;

        public ImageEditorForm()
        {
            InitializeComponent();

            surface = new Surface();
            surface.SizeMode = PictureBoxSizeMode.AutoSize;
            surface.TabStop = false;
            surface.MovingElementChanged += new SurfaceElementEventHandler(surfaceMovingElementChanged);
            panel1.Controls.Add(surface);

            if (conf.Editor_WindowSize != null)
            {
                Size = (Size)conf.Editor_WindowSize;
            }

            Bitmap imgBorder = DrawColorButton(surface.ForeColor, btnBorderColor.ContentRectangle, ColorType.Border);
            btnBorderColor.Image = imgBorder;
            borderColorToolStripMenuItem.Image = imgBorder;
            Bitmap imgBackground = DrawColorButton(surface.BackColor, btnBackgroundColor.ContentRectangle, ColorType.Background);
            btnBackgroundColor.Image = imgBackground;
            backgroundColorToolStripMenuItem.Image = imgBackground;

            this.colorDialog.RecentColors = conf.Editor_RecentColors;
            this.cbThickness.Text = conf.Editor_Thickness.ToString();
        }

        public void SetImage(Image img)
        {
            surface.Image = img;
        }

        public void SetImagePath(string fullpath)
        {
            this.lastSaveFullPath = fullpath;
            if (fullpath == null) return;
            updateStatusLabel("Image saved to %storagelocation%.".Replace("%storagelocation%", fullpath), fileSavedStatusContextMenu);
            this.Text = "Image Editor" + " - " + Path.GetFileName(fullpath);
            this.saveToolStripMenuItem.Enabled = true;
        }

        private void surfaceMovingElementChanged(object sender, DrawableContainerList selectedElements)
        {
            bool elementSelected = (selectedElements.Count > 0);
            this.btnCopy.Enabled = elementSelected;
            this.btnCut.Enabled = elementSelected;
            this.btnDelete.Enabled = elementSelected;
            this.copyToolStripMenuItem.Enabled = elementSelected;
            this.cutToolStripMenuItem.Enabled = elementSelected;
            this.duplicateToolStripMenuItem.Enabled = elementSelected;
            this.removeObjectToolStripMenuItem.Enabled = elementSelected;

            //this.btnBorderColor.Enabled = this.borderColorToolStripMenuItem.Enabled =
            //(elementSelected && selectedElements.PropertySupported(DrawableContainer.Property.LINECOLOR));
            //this.btnBackgroundColor.Enabled = this.backgroundColorToolStripMenuItem.Enabled =
            //(elementSelected && selectedElements.PropertySupported(DrawableContainer.Property.FILLCOLOR));
            //this.comboBoxThickness.Enabled = this.lineThicknessToolStripMenuItem.Enabled =
            //(elementSelected && selectedElements.PropertySupported(DrawableContainer.Property.THICKNESS));
            //this.btnArrowHeads.Enabled = this.arrowHeadsToolStripMenuItem.Enabled =
            //(elementSelected && selectedElements.PropertySupported(DrawableContainer.Property.ARROWHEADS));

            bool push = surface.CanPushSelectionDown();
            bool pull = surface.CanPullSelectionUp();
            this.arrangeToolStripMenuItem.Enabled = (push || pull);
            if (this.arrangeToolStripMenuItem.Enabled)
            {
                this.upToTopToolStripMenuItem.Enabled = pull;
                this.upOneLevelToolStripMenuItem.Enabled = pull;
                this.downToBottomToolStripMenuItem.Enabled = push;
                this.downOneLevelToolStripMenuItem.Enabled = push;
            }
        }

        #region Filesystem options

        private void SaveToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            try
            {
                ImageOutput.Save(surface.GetImageForExport(), lastSaveFullPath);
                updateStatusLabel("Image saved to %storagelocation%.".Replace("%storagelocation%", lastSaveFullPath), fileSavedStatusContextMenu);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (lastSaveFullPath != null) SaveToolStripMenuItemClick(sender, e);
            else SaveAsToolStripMenuItemClick(sender, e);
        }

        private void SaveAsToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            lastSaveFullPath = ImageOutput.SaveWithDialog(surface.GetImageForExport());
            if (lastSaveFullPath != null)
            {
                SetImagePath(lastSaveFullPath);
                updateStatusLabel("Image saved to %storagelocation%.".Replace("%storagelocation%", lastSaveFullPath), fileSavedStatusContextMenu);
            }
            else
            {
                clearStatusLabel();
            }
        }

        private void CopyImageToClipboardToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            try
            {
                ImageOutput.PrepareClipboardObject();
                ImageOutput.CopyToClipboard(surface.GetImageForExport());
                updateStatusLabel("Copy file path to clipboard every time an image is saved");
            }
            catch (Exception ex)
            {
                updateStatusLabel(ex.Message);
            }
        }

        private void BtnClipboardClick(object sender, EventArgs e)
        {
            this.CopyImageToClipboardToolStripMenuItemClick(sender, e);
        }

        private void PrintToolStripMenuItemClick(object sender, EventArgs e)
        {
            PrintHelper ph = new PrintHelper(surface.GetImageForExport());
            PrinterSettings ps = ph.PrintWithDialog();
            if (ps != null)
            {
                updateStatusLabel("Print job was sent to '%printername%'.".Replace("%printername%", ps.PrinterName));
            }
        }

        private void BtnPrintClick(object sender, EventArgs e)
        {
            PrintToolStripMenuItemClick(sender, e);
        }

        private void CloseToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Drawing options

        private void BtnEllipseClick(object sender, EventArgs e)
        {
            surface.DrawingMode = Surface.DrawingModes.Ellipse;
            btnCursor.Checked = false;
            btnRect.Checked = false;
            btnEllipse.Checked = true;
            btnText.Checked = false;
            btnLine.Checked = false;
            btnArrow.Checked = false;
        }

        private void BtnCursorClick(object sender, EventArgs e)
        {
            surface.DrawingMode = Surface.DrawingModes.None;
            btnCursor.Checked = true;
            btnRect.Checked = false;
            btnEllipse.Checked = false;
            btnText.Checked = false;
            btnLine.Checked = false;
            btnArrow.Checked = false;
        }

        private void BtnRectClick(object sender, EventArgs e)
        {
            surface.DrawingMode = Surface.DrawingModes.Rect;
            btnCursor.Checked = false;
            btnRect.Checked = true;
            btnEllipse.Checked = false;
            btnText.Checked = false;
            btnLine.Checked = false;
            btnArrow.Checked = false;
        }

        private void BtnTextClick(object sender, EventArgs e)
        {
            surface.DrawingMode = Surface.DrawingModes.Text;
            btnCursor.Checked = false;
            btnRect.Checked = false;
            btnEllipse.Checked = false;
            btnText.Checked = true;
            btnLine.Checked = false;
            btnArrow.Checked = false;
        }

        private void BtnLineClick(object sender, EventArgs e)
        {
            surface.DrawingMode = Surface.DrawingModes.Line;
            btnCursor.Checked = false;
            btnRect.Checked = false;
            btnEllipse.Checked = false;
            btnText.Checked = false;
            btnLine.Checked = true;
            btnArrow.Checked = false;
        }

        private void BtnArrowClick(object sender, EventArgs e)
        {
            surface.DrawingMode = Surface.DrawingModes.Arrow;
            btnCursor.Checked = false;
            btnRect.Checked = false;
            btnEllipse.Checked = false;
            btnText.Checked = false;
            btnLine.Checked = false;
            btnArrow.Checked = true;
        }

        private void AddRectangleToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            BtnRectClick(sender, e);
        }

        private void AddEllipseToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            BtnEllipseClick(sender, e);
        }

        private void AddTextBoxToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            BtnTextClick(sender, e);
        }

        private void DrawLineToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            BtnLineClick(sender, e);
        }

        private void DrawArrowToolStripMenuItemClick(object sender, EventArgs e)
        {
            BtnArrowClick(sender, e);
        }

        private void RemoveObjectToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            surface.RemoveSelectedElements();
        }

        private void BtnDeleteClick(object sender, EventArgs e)
        {
            RemoveObjectToolStripMenuItemClick(sender, e);
        }

        #endregion

        #region Copy & paste options

        private void CutToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            if (surface.CutSelectedElements())
            {
                this.btnPaste.Enabled = true;
                this.pasteToolStripMenuItem.Enabled = true;
            }
        }

        private void BtnCutClick(object sender, System.EventArgs e)
        {
            CutToolStripMenuItemClick(sender, e);
        }

        private void CopyToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            if (surface.CopySelectedElements())
            {
                this.btnPaste.Enabled = true;
                this.pasteToolStripMenuItem.Enabled = true;
            }
        }

        private void BtnCopyClick(object sender, System.EventArgs e)
        {
            CopyToolStripMenuItemClick(sender, e);
        }

        private void PasteToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            surface.PasteElementFromClipboard();
        }

        private void BtnPasteClick(object sender, System.EventArgs e)
        {
            PasteToolStripMenuItemClick(sender, e);
        }

        private void DuplicateToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            surface.DuplicateSelectedElements();
        }

        #endregion

        #region Element properties

        private void UpOneLevelToolStripMenuItemClick(object sender, EventArgs e)
        {
            surface.PullElementsUp();
        }

        private void DownOneLevelToolStripMenuItemClick(object sender, EventArgs e)
        {
            surface.PushElementsDown();
        }

        private void UpToTopToolStripMenuItemClick(object sender, EventArgs e)
        {
            surface.PullElementsToTop();
        }

        private void DownToBottomToolStripMenuItemClick(object sender, EventArgs e)
        {
            surface.PushElementsToBottom();
        }

        private void BtnBorderColorClick(object sender, System.EventArgs e)
        {
            SelectBorderColor();
        }

        private void SelectBorderColorToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            SelectBorderColor();
        }

        private void SelectBorderColor()
        {
            colorDialog.Color = surface.ForeColor;
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                conf.Editor_ForeColor = colorDialog.Color;
                conf.Editor_RecentColors = colorDialog.RecentColors;
                conf.Store();
                surface.ForeColor = colorDialog.Color;

                Bitmap img = DrawColorButton(colorDialog.Color, btnBorderColor.ContentRectangle, ColorType.Border);
                btnBorderColor.Image = img;
                borderColorToolStripMenuItem.Image = img;
            }
        }

        private void BtnBackColorClick(object sender, System.EventArgs e)
        {
            SelectBackgroundColor();
        }

        private void SelectBackgroundColorToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            SelectBackgroundColor();
        }

        private void SelectBackgroundColor()
        {
            colorDialog.Color = surface.BackColor;
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                conf.Editor_BackColor = colorDialog.Color;
                conf.Editor_RecentColors = colorDialog.RecentColors;
                conf.Store();
                surface.BackColor = colorDialog.Color;

                Bitmap img = DrawColorButton(colorDialog.Color, btnBorderColor.ContentRectangle, ColorType.Background);
                btnBackgroundColor.Image = img;
                backgroundColorToolStripMenuItem.Image = img;
            }
        }

        public enum ColorType { Border, Background }

        private Bitmap DrawColorButton(Color color, Rectangle rect, ColorType colorType)
        {
            Bitmap img = new Bitmap(rect.Width, rect.Height);
            img = DrawCheckersPattern(rect, 5);
            Graphics g = Graphics.FromImage(img);
            if (colorType == ColorType.Border)
            {
                g.DrawRectangle(new Pen(color), new Rectangle(0, 0, rect.Width - 1, rect.Height - 1));
            }
            else if (colorType == ColorType.Background)
            {
                g.FillRectangle(new SolidBrush(color), new Rectangle(0, 0, rect.Width, rect.Height));
            }
            return img;
        }

        private Bitmap DrawCheckersPattern(Rectangle rect, int size)
        {
            Bitmap img = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(img);
            Color color;
            for (int x = 0; x < rect.Width; x += size)
            {
                for (int y = 0; y < rect.Height; y += size)
                {
                    if ((x + y) % 2 == 0)
                    {
                        color = Color.LightGray;
                    }
                    else
                    {
                        color = Color.WhiteSmoke;
                    }
                    g.FillRectangle(new SolidBrush(color), new Rectangle(x, y, size, size));
                }
            }
            return img;
        }

        private void ThicknessComboBoxChanged(object sender, System.EventArgs e)
        {
            ToolStripComboBox cb = (ToolStripComboBox)sender;
            try
            {
                int t = Int32.Parse(cb.Text);
                surface.Thickness = t;
                conf.Editor_Thickness = t;
                conf.Store();
            }
            catch (Exception)
            {
                cb.Text = conf.Editor_Thickness.ToString();
            }
        }

        private void LineThicknessValueToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            ToolStripMenuItem cb = (ToolStripMenuItem)sender;
            try
            {
                int t = Int32.Parse(cb.Text);
                surface.Thickness = t;
                conf.Editor_Thickness = t;
                conf.Store();
            }
            catch (Exception)
            {
                cb.Text = conf.Editor_Thickness.ToString();
            }
        }

        private void ArrowHeadsStartPointToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            surface.ArrowHead = Surface.ArrowHeads.Start;
        }

        private void ArrowHeadsEndPointToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            surface.ArrowHead = Surface.ArrowHeads.End;
        }

        private void ArrowHeadsBothToolStripMenuItemClick(object sender, EventArgs e)
        {
            surface.ArrowHead = Surface.ArrowHeads.Both;
        }

        private void ArrowHeadsNoneToolStripMenuItemClick(object sender, EventArgs e)
        {
            surface.ArrowHead = Surface.ArrowHeads.None;
        }

        #endregion

        #region Help

        private void AboutToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            new AboutForm().Show();
        }

        #endregion

        #region Image editor event handlers

        private void ImageEditorFormActivated(object sender, EventArgs e)
        {
            bool b = false; // TODO: Greenshot
            this.btnPaste.Enabled = b;
            this.pasteToolStripMenuItem.Enabled = b;
        }

        private void ImageEditorFormFormClosing(object sender, FormClosingEventArgs e)
        {
            conf.Editor_WindowSize = Size;
            conf.Store();
            System.GC.Collect();
        }

        private void ImageEditorFormKeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Escape.Equals(e.KeyCode))
            {
                BtnCursorClick(sender, e);
            }
            else if (Keys.R.Equals(e.KeyCode))
            {
                BtnRectClick(sender, e);
            }
            else if (Keys.E.Equals(e.KeyCode))
            {
                BtnEllipseClick(sender, e);
            }
            else if (Keys.L.Equals(e.KeyCode))
            {
                BtnLineClick(sender, e);
            }
            else if (Keys.A.Equals(e.KeyCode))
            {
                BtnArrowClick(sender, e);
            }
            else if (Keys.T.Equals(e.KeyCode))
            {
                BtnTextClick(sender, e);
            }
        }

        #endregion

        #region Cursor key strokes

        protected override bool ProcessCmdKey(ref Message msg, Keys k)
        {
            surface.ProcessCmdKey(k);
            return base.ProcessCmdKey(ref msg, k);
        }

        #endregion

        #region Status label handling

        private void updateStatusLabel(string text, ContextMenuStrip contextMenu)
        {
            statusLabel.Text = text;
            statusStrip1.ContextMenuStrip = contextMenu;
        }

        private void updateStatusLabel(string text)
        {
            updateStatusLabel(text, null);
        }

        private void clearStatusLabel()
        {
            updateStatusLabel(null, null);
        }

        private void StatusLabelClicked(object sender, MouseEventArgs e)
        {
            ToolStrip ss = (StatusStrip)((ToolStripStatusLabel)sender).Owner;
            if (ss.ContextMenuStrip != null)
            {
                ss.ContextMenuStrip.Show(ss, e.X, e.Y);
            }
        }

        private void CopyPathMenuItemClick(object sender, EventArgs e)
        {
            Clipboard.SetText(lastSaveFullPath);
        }

        private void OpenDirectoryMenuItemClick(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo("explorer");
            psi.Arguments = Path.GetDirectoryName(lastSaveFullPath);
            psi.UseShellExecute = false;
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();
        }

        #endregion
    }
}