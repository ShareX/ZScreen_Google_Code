#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZScreenLib
{
    public partial class InputBox : Form
    {
        public string Title { get; set; }
        public string Question { get; set; }
        public string InputText { get; set; }

        public InputBox(string title, string question = "", string inputText = "")
        {
            InitializeComponent();
            Title = title;
            Question = question;
            InputText = inputText;

            Text = Title;
            lblInputLabel.Text = question;
            txtInputText.Text = InputText;
        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            BringToFront();
            txtInputText.Focus();
            txtInputText.SelectionLength = txtInputText.Text.Length;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInputText.Text))
            {
                InputText = txtInputText.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void InputBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical))
            {
                brush.SetSigmaBellShape(0.20f);
                g.FillRectangle(brush, rect);
            }
        }

        #region Windows Form Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtInputText = new System.Windows.Forms.TextBox();
            this.lblInputLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // btnOK
            //
            this.btnOK.BackColor = System.Drawing.Color.White;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(208, 64);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(288, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // txtInputText
            //
            this.txtInputText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInputText.Location = new System.Drawing.Point(8, 32);
            this.txtInputText.Name = "txtInputText";
            this.txtInputText.Size = new System.Drawing.Size(352, 20);
            this.txtInputText.TabIndex = 0;
            //
            // lblInputLabel
            //
            this.lblInputLabel.AutoSize = true;
            this.lblInputLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblInputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblInputLabel.ForeColor = System.Drawing.Color.White;
            this.lblInputLabel.Location = new System.Drawing.Point(8, 8);
            this.lblInputLabel.Name = "lblInputLabel";
            this.lblInputLabel.Size = new System.Drawing.Size(36, 16);
            this.lblInputLabel.TabIndex = 3;
            this.lblInputLabel.Text = "Input";
            //
            // InputBox
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(369, 96);
            this.Controls.Add(this.lblInputLabel);
            this.Controls.Add(this.txtInputText);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputBox";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.InputBox_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblInputLabel;
        private Button btnOK;
        private Button btnCancel;
        private TextBox txtInputText;

        #endregion Windows Form Designer generated code
    }
}