namespace ZScreenCoreLib
{
    partial class ActionsUI
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
            this.lbSoftware = new System.Windows.Forms.CheckedListBox();
            this.pgEditorsImage = new System.Windows.Forms.PropertyGrid();
            this.btnActionsRemove = new System.Windows.Forms.Button();
            this.btnAddImageSoftware = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSoftware
            // 
            this.lbSoftware.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbSoftware.FormattingEnabled = true;
            this.lbSoftware.IntegralHeight = false;
            this.lbSoftware.Location = new System.Drawing.Point(0, 0);
            this.lbSoftware.Name = "lbSoftware";
            this.lbSoftware.Size = new System.Drawing.Size(293, 462);
            this.lbSoftware.TabIndex = 0;
            this.lbSoftware.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbSoftwareItemCheck);
            this.lbSoftware.SelectedIndexChanged += new System.EventHandler(this.lbSoftware_SelectedIndexChanged);
            // 
            // pgEditorsImage
            // 
            this.pgEditorsImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgEditorsImage.Location = new System.Drawing.Point(301, 45);
            this.pgEditorsImage.Name = "pgEditorsImage";
            this.pgEditorsImage.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgEditorsImage.Size = new System.Drawing.Size(571, 259);
            this.pgEditorsImage.TabIndex = 3;
            this.pgEditorsImage.ToolbarVisible = false;
            this.pgEditorsImage.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgEditorsImage_PropertyValueChanged);
            // 
            // btnActionsRemove
            // 
            this.btnActionsRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnActionsRemove.Location = new System.Drawing.Point(397, 13);
            this.btnActionsRemove.Name = "btnActionsRemove";
            this.btnActionsRemove.Size = new System.Drawing.Size(88, 24);
            this.btnActionsRemove.TabIndex = 2;
            this.btnActionsRemove.Text = "&Remove";
            this.btnActionsRemove.UseVisualStyleBackColor = true;
            this.btnActionsRemove.Click += new System.EventHandler(this.btnDeleteImageSoftware_Click);
            // 
            // btnAddImageSoftware
            // 
            this.btnAddImageSoftware.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddImageSoftware.BackColor = System.Drawing.Color.Transparent;
            this.btnAddImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddImageSoftware.Location = new System.Drawing.Point(301, 13);
            this.btnAddImageSoftware.Name = "btnAddImageSoftware";
            this.btnAddImageSoftware.Size = new System.Drawing.Size(88, 24);
            this.btnAddImageSoftware.TabIndex = 1;
            this.btnAddImageSoftware.Text = "Add...";
            this.btnAddImageSoftware.UseVisualStyleBackColor = false;
            this.btnAddImageSoftware.Click += new System.EventHandler(this.btnAddImageSoftware_Click);
            // 
            // ActionsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.lbSoftware);
            this.Controls.Add(this.pgEditorsImage);
            this.Controls.Add(this.btnActionsRemove);
            this.Controls.Add(this.btnAddImageSoftware);
            this.MaximumSize = new System.Drawing.Size(900, 500);
            this.Name = "ActionsUI";
            this.Text = "ActionsUI";
            this.Load += new System.EventHandler(this.ActionsUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.CheckedListBox lbSoftware;
        internal System.Windows.Forms.PropertyGrid pgEditorsImage;
        internal System.Windows.Forms.Button btnActionsRemove;
        internal System.Windows.Forms.Button btnAddImageSoftware;

    }
}