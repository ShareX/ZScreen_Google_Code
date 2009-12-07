namespace ImageQueue
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tvPlugins = new System.Windows.Forms.TreeView();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.pgSettings = new System.Windows.Forms.PropertyGrid();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lvEffects = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.btnRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tvPlugins
            // 
            this.tvPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvPlugins.HideSelection = false;
            this.tvPlugins.Location = new System.Drawing.Point(8, 280);
            this.tvPlugins.Name = "tvPlugins";
            this.tvPlugins.Size = new System.Drawing.Size(264, 288);
            this.tvPlugins.TabIndex = 0;
            // 
            // pbPreview
            // 
            this.pbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbPreview.Location = new System.Drawing.Point(280, 280);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(320, 288);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPreview.TabIndex = 1;
            this.pbPreview.TabStop = false;
            // 
            // pgSettings
            // 
            this.pgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgSettings.HelpVisible = false;
            this.pgSettings.Location = new System.Drawing.Point(280, 8);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgSettings.Size = new System.Drawing.Size(320, 264);
            this.pgSettings.TabIndex = 3;
            this.pgSettings.ToolbarVisible = false;
            this.pgSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgSettings_PropertyValueChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(16, 240);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 24);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lvEffects
            // 
            this.lvEffects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvEffects.FullRowSelect = true;
            this.lvEffects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvEffects.HideSelection = false;
            this.lvEffects.Location = new System.Drawing.Point(8, 8);
            this.lvEffects.Name = "lvEffects";
            this.lvEffects.Size = new System.Drawing.Size(264, 264);
            this.lvEffects.TabIndex = 5;
            this.lvEffects.UseCompatibleStateImageBehavior = false;
            this.lvEffects.View = System.Windows.Forms.View.Details;
            this.lvEffects.SelectedIndexChanged += new System.EventHandler(this.lvEffects_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 250;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(96, 240);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(72, 24);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 580);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.pgSettings);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.tvPlugins);
            this.Controls.Add(this.lvEffects);
            this.Name = "Form1";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvPlugins;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.PropertyGrid pgSettings;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView lvEffects;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnRemove;
    }
}

