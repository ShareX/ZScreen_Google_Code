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
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using System.IO;

namespace ZScreenLib
{
    public partial class DestOptions : Form
    {
        public string Title { get; set; }

        public string FilePath { get; set; }

        public WorkerTask Task { get; set; }

        public DestOptions(WorkerTask task)
        {
            InitializeComponent();
            this.Task = task;
            btnBrowse.Enabled = !task.States.Contains(WorkerTask.TaskState.ThreadMode);
            txtFilePath.Enabled = task.Job2 != WorkerTask.JobLevel2.UploadFromExplorer;
            DestSelectorHelper dsh = new DestSelectorHelper(ucDestOptions);
            dsh.AddEnumOutputsWithConfigSettings(Task.MyWorkflow.Outputs);
            dsh.AddEnumClipboardContentWithRuntimeSettings(Task.TaskClipboardContent);
            dsh.AddEnumLinkFormatWithRuntimeSettings(Task.MyLinkFormat.Cast<int>().ToList());
            dsh.AddEnumDestImageToMenuWithRuntimeSettings(Task.MyImageUploaders.Cast<int>().ToList());
            dsh.AddEnumDestTextToMenuWithRuntimeSettings(Task.MyTextUploaders.Cast<int>().ToList());
            dsh.AddEnumDestFileToMenuWithRuntimeSettings(Task.MyFileUploaders.Cast<int>().ToList());
            dsh.AddEnumDestLinkToMenuWithRuntimeSettings(Task.MyLinkUploaders.Cast<int>().ToList());
        }

        private void DestOptions_Load(object sender, EventArgs e)
        {
            this.Text = Title;
            txtFilePath.Text = FilePath;
            txtFilePath.KeyDown += new KeyEventHandler(txtInputText_KeyDown);
        }

        private void txtInputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // cancel with Escape
                this.Close();
            }
        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            txtFilePath.Focus();
            txtFilePath.SelectionLength = txtFilePath.Text.Length;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilePath.Text))
            {
                FilePath = txtFilePath.Text;
                this.DialogResult = DialogResult.OK;

                Adapter.SaveMenuConfigToList<OutputEnum>(ucDestOptions.tsddbOutputs, Task.MyWorkflow.Outputs);
                Adapter.SaveMenuConfigToList<ClipboardContentEnum>(ucDestOptions.tsddbClipboardContent, Task.TaskClipboardContent);
                Adapter.SaveMenuConfigToList<ImageUploaderType>(ucDestOptions.tsddbDestImage, Task.MyImageUploaders);
                Adapter.SaveMenuConfigToList<TextUploaderType>(ucDestOptions.tsddbDestText, Task.MyTextUploaders);
                Adapter.SaveMenuConfigToList<FileUploaderType>(ucDestOptions.tsddbDestFile, Task.MyFileUploaders);
                Adapter.SaveMenuConfigToList<UrlShortenerType>(ucDestOptions.tsddbDestLink, Task.MyLinkUploaders);

                this.Hide();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        public DestSelector ucDestOptions;
        private Button btnBrowse;

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
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.ucDestOptions = new ZScreenLib.DestSelector();
            this.SuspendLayout();
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(280, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(360, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // txtFilePath
            //
            this.txtFilePath.Location = new System.Drawing.Point(16, 16);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(408, 20);
            this.txtFilePath.TabIndex = 2;
            //
            // btnBrowse
            //
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowse.Location = new System.Drawing.Point(432, 16);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(26, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // ucDestOptions
            //
            this.ucDestOptions.BackColor = System.Drawing.Color.White;
            this.ucDestOptions.Location = new System.Drawing.Point(16, 48);
            this.ucDestOptions.Name = "ucDestOptions";
            this.ucDestOptions.Size = new System.Drawing.Size(440, 200);
            this.ucDestOptions.TabIndex = 3;
            //
            // DestOptions
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(472, 300);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.ucDestOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DestOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Destination Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DestOptions_Load);
            this.Shown += new System.EventHandler(this.InputBox_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtFilePath;

        #endregion Windows Form Designer generated code

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = Path.GetFileNameWithoutExtension(FilePath);
            dlg.Filter = string.Format("{0} files (*{0})|*{0}", Path.GetExtension(FilePath));
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dlg.FileName;
                FilePath = dlg.FileName;
            }
        }
    }
}