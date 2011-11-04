using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.ComponentModel;
using System;

namespace ZSS
{
    public class RadioListBox : ListBox
    {
        private StringFormat Align;
        private bool IsTransparent = false;
        private Brush TransparentBrush = SystemBrushes.Control;

        [DefaultValue(false)]
        public bool Transparent
        {
            set
            {
                IsTransparent = value;

                if (IsTransparent)
                // Mimic parent form color, and hide border
                {
                    if (this.Parent != null)
                        //Prevent an exception if control still has no parent
                        this.BackColor = this.Parent.BackColor;
                    else
                        this.BackColor = SystemColors.Control;
                    this.TransparentBrush = new SolidBrush(this.BackColor);
                    this.BorderStyle = BorderStyle.None;
                }
                else
                {
                    this.BackColor = SystemColors.Window;
                    this.BorderStyle = BorderStyle.Fixed3D;
                }
            }
            get
            {
                return IsTransparent;
            }
        }

        public RadioListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;

            this.Align = new StringFormat(StringFormat.GenericDefault);
            this.Align.LineAlignment = StringAlignment.Center;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            if (e.Index > this.Items.Count - 1)
                return;

            int size = e.Font.Height; // button size depends on font height, not on item height

            if (IsTransparent && e.State != DrawItemState.Selected)
                e.Graphics.FillRectangle(TransparentBrush, e.Bounds);
            else
                e.DrawBackground();

            Brush textBrush;
            bool isChecked = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            RadioButtonState state = isChecked ? RadioButtonState.CheckedNormal : RadioButtonState.UncheckedNormal;
            if ((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)
            {
                textBrush = SystemBrushes.GrayText;
                state = isChecked ? RadioButtonState.CheckedDisabled : RadioButtonState.UncheckedDisabled;
            }
            else if ((e.State & DrawItemState.Grayed) == DrawItemState.Grayed)
            {
                textBrush = SystemBrushes.GrayText;
                state = isChecked ? RadioButtonState.CheckedDisabled : RadioButtonState.UncheckedDisabled;
            }
            else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && !Transparent)
            {
                textBrush = SystemBrushes.HighlightText;
            }
            else
            {
                textBrush = SystemBrushes.FromSystemColor(this.ForeColor);
            }

            // Draw radio button
            Rectangle bounds = e.Bounds;
            bounds.Width = size;
            RadioButtonRenderer.DrawRadioButton(e.Graphics, bounds.Location, state);

            // Draw text
            bounds = new Rectangle(e.Bounds.X+size+2, e.Bounds.Y, e.Bounds.Width-size-2, e.Bounds.Height);
            if (!string.IsNullOrEmpty(DisplayMember)) // Bound Datatable? Then show the column written in Displaymember
                e.Graphics.DrawString(((System.Data.DataRowView)this.Items[e.Index])[this.DisplayMember].ToString(),
                    e.Font, textBrush, bounds, this.Align);
            else
                e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, textBrush, bounds, this.Align);

            // If the ListBox has focus, 
        // draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
        protected override void OnParentChanged(EventArgs e)
        {
            // Force to change backcolor
            this.Transparent = this.IsTransparent;
        }
        protected override void OnParentBackColorChanged(EventArgs e)
        {
            // Force to change backcolor
            this.Transparent = this.IsTransparent;
        }
    }
}
