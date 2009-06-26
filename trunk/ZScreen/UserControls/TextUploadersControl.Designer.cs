namespace ZSS.UserControls
{
    partial class TextUploadersControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextUploaders = new System.Windows.Forms.ListBox();
            this.btnTextUploaderTest = new System.Windows.Forms.Button();
            this.btnTextUploaderRemove = new System.Windows.Forms.Button();
            this.btnTextUploaderAdd = new System.Windows.Forms.Button();
            this.SettingsGrid = new System.Windows.Forms.PropertyGrid();
            this.Templates = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TextUploaders
            // 
            this.TextUploaders.Dock = System.Windows.Forms.DockStyle.Left;
            this.TextUploaders.FormattingEnabled = true;
            this.TextUploaders.Location = new System.Drawing.Point(0, 0);
            this.TextUploaders.Name = "lbTextUploaders";
            this.TextUploaders.Size = new System.Drawing.Size(197, 433);
            this.TextUploaders.TabIndex = 14;
            // 
            // btnTextUploaderTest
            // 
            this.btnTextUploaderTest.Location = new System.Drawing.Point(679, 9);
            this.btnTextUploaderTest.Name = "btnTestTextUploader";
            this.btnTextUploaderTest.Size = new System.Drawing.Size(75, 23);
            this.btnTextUploaderTest.TabIndex = 13;
            this.btnTextUploaderTest.Text = "Test";
            this.btnTextUploaderTest.UseVisualStyleBackColor = true;
            // 
            // btnTextUploaderRemove
            // 
            this.btnTextUploaderRemove.Location = new System.Drawing.Point(599, 9);
            this.btnTextUploaderRemove.Name = "btnRemoveTextUploader";
            this.btnTextUploaderRemove.Size = new System.Drawing.Size(75, 23);
            this.btnTextUploaderRemove.TabIndex = 12;
            this.btnTextUploaderRemove.Text = "Remove";
            this.btnTextUploaderRemove.UseVisualStyleBackColor = true;
            // 
            // btnTextUploaderAdd
            // 
            this.btnTextUploaderAdd.Location = new System.Drawing.Point(207, 9);
            this.btnTextUploaderAdd.Name = "btnAddTextUploader";
            this.btnTextUploaderAdd.Size = new System.Drawing.Size(75, 23);
            this.btnTextUploaderAdd.TabIndex = 11;
            this.btnTextUploaderAdd.Text = "Add";
            this.btnTextUploaderAdd.UseVisualStyleBackColor = true;
            // 
            // SettingsGrid
            // 
            this.SettingsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsGrid.HelpVisible = false;
            this.SettingsGrid.Location = new System.Drawing.Point(207, 40);
            this.SettingsGrid.Name = "pgTextUploaderSettings";
            this.SettingsGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.SettingsGrid.Size = new System.Drawing.Size(545, 393);
            this.SettingsGrid.TabIndex = 9;
            this.SettingsGrid.ToolbarVisible = false;
            // 
            // Templates
            // 
            this.Templates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Templates.FormattingEnabled = true;
            this.Templates.Location = new System.Drawing.Point(287, 9);
            this.Templates.Name = "cboTextUploaders";
            this.Templates.Size = new System.Drawing.Size(304, 21);
            this.Templates.TabIndex = 10;
            // 
            // TextUploadersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextUploaders);
            this.Controls.Add(this.btnTextUploaderTest);
            this.Controls.Add(this.btnTextUploaderRemove);
            this.Controls.Add(this.btnTextUploaderAdd);
            this.Controls.Add(this.SettingsGrid);
            this.Controls.Add(this.Templates);
            this.Name = "TextUploadersControl";
            this.Size = new System.Drawing.Size(760, 438);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox TextUploaders;
        internal System.Windows.Forms.Button btnTextUploaderTest;
        internal System.Windows.Forms.Button btnTextUploaderRemove;
        internal System.Windows.Forms.Button btnTextUploaderAdd;
        internal System.Windows.Forms.PropertyGrid SettingsGrid;
        internal System.Windows.Forms.ComboBox Templates;
    }
}
