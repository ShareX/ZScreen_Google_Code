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
            this.MyCollection = new System.Windows.Forms.ListBox();
            this.btnItemTest = new System.Windows.Forms.Button();
            this.btnItemRemove = new System.Windows.Forms.Button();
            this.btnItemAdd = new System.Windows.Forms.Button();
            this.SettingsGrid = new System.Windows.Forms.PropertyGrid();
            this.Templates = new System.Windows.Forms.ComboBox();
            this.TextUploadersPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TextUploadersPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyCollection
            // 
            this.MyCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyCollection.FormattingEnabled = true;
            this.MyCollection.Location = new System.Drawing.Point(3, 3);
            this.MyCollection.Name = "MyCollection";
            this.MyCollection.Size = new System.Drawing.Size(217, 329);
            this.MyCollection.TabIndex = 14;
            this.MyCollection.SelectedIndexChanged += new System.EventHandler(this.MyCollection_SelectedIndexChanged);
            // 
            // btnItemTest
            // 
            this.btnItemTest.Location = new System.Drawing.Point(320, 8);
            this.btnItemTest.Name = "btnItemTest";
            this.btnItemTest.Size = new System.Drawing.Size(75, 23);
            this.btnItemTest.TabIndex = 13;
            this.btnItemTest.Text = "Test";
            this.btnItemTest.UseVisualStyleBackColor = true;
            // 
            // btnItemRemove
            // 
            this.btnItemRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemRemove.Location = new System.Drawing.Point(672, 9);
            this.btnItemRemove.Name = "btnItemRemove";
            this.btnItemRemove.Size = new System.Drawing.Size(75, 23);
            this.btnItemRemove.TabIndex = 12;
            this.btnItemRemove.Text = "Remove";
            this.btnItemRemove.UseVisualStyleBackColor = true;
            // 
            // btnItemAdd
            // 
            this.btnItemAdd.Location = new System.Drawing.Point(240, 8);
            this.btnItemAdd.Name = "btnItemAdd";
            this.btnItemAdd.Size = new System.Drawing.Size(75, 23);
            this.btnItemAdd.TabIndex = 11;
            this.btnItemAdd.Text = "Add";
            this.btnItemAdd.UseVisualStyleBackColor = true;
            // 
            // SettingsGrid
            // 
            this.SettingsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsGrid.HelpVisible = false;
            this.SettingsGrid.Location = new System.Drawing.Point(226, 3);
            this.SettingsGrid.Name = "SettingsGrid";
            this.SettingsGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.SettingsGrid.Size = new System.Drawing.Size(515, 333);
            this.SettingsGrid.TabIndex = 9;
            this.SettingsGrid.ToolbarVisible = false;
            // 
            // Templates
            // 
            this.Templates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Templates.FormattingEnabled = true;
            this.Templates.Location = new System.Drawing.Point(8, 8);
            this.Templates.Name = "Templates";
            this.Templates.Size = new System.Drawing.Size(224, 21);
            this.Templates.TabIndex = 10;
            // 
            // TextUploadersPanel
            // 
            this.TextUploadersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextUploadersPanel.ColumnCount = 2;
            this.TextUploadersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.TextUploadersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.TextUploadersPanel.Controls.Add(this.MyCollection, 0, 0);
            this.TextUploadersPanel.Controls.Add(this.SettingsGrid, 1, 0);
            this.TextUploadersPanel.Location = new System.Drawing.Point(8, 40);
            this.TextUploadersPanel.Name = "TextUploadersPanel";
            this.TextUploadersPanel.RowCount = 1;
            this.TextUploadersPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TextUploadersPanel.Size = new System.Drawing.Size(744, 339);
            this.TextUploadersPanel.TabIndex = 15;
            // 
            // TextUploadersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextUploadersPanel);
            this.Controls.Add(this.btnItemTest);
            this.Controls.Add(this.btnItemRemove);
            this.Controls.Add(this.btnItemAdd);
            this.Controls.Add(this.Templates);
            this.Name = "TextUploadersControl";
            this.Size = new System.Drawing.Size(760, 393);
            this.TextUploadersPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox MyCollection;
        internal System.Windows.Forms.Button btnItemTest;
        internal System.Windows.Forms.Button btnItemRemove;
        internal System.Windows.Forms.Button btnItemAdd;
        internal System.Windows.Forms.PropertyGrid SettingsGrid;
        internal System.Windows.Forms.ComboBox Templates;
        private System.Windows.Forms.TableLayoutPanel TextUploadersPanel;
    }
}
