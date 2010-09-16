namespace ZScreenLib
{
    partial class DekiWikiPath
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DekiWikiPath));
            this.tvDekiWikiPath = new System.Windows.Forms.TreeView();
            this.tvImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnDekiWikiHome = new System.Windows.Forms.Button();
            this.btnDekiWikiUser = new System.Windows.Forms.Button();
            this.btnDekiWikiCancel = new System.Windows.Forms.Button();
            this.btnDekiWikiSave = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblPathLabel = new System.Windows.Forms.Label();
            this.lblDekiWikiPath = new System.Windows.Forms.Label();
            this.lblAccountLabel = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // tvDekiWikiPath
            //
            this.tvDekiWikiPath.ImageIndex = 0;
            this.tvDekiWikiPath.ImageList = this.tvImageList;
            this.tvDekiWikiPath.Location = new System.Drawing.Point(58, 29);
            this.tvDekiWikiPath.Name = "tvDekiWikiPath";
            this.tvDekiWikiPath.SelectedImageIndex = 0;
            this.tvDekiWikiPath.Size = new System.Drawing.Size(308, 163);
            this.tvDekiWikiPath.TabIndex = 0;
            this.tvDekiWikiPath.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDekiWikiPath_AfterSelect);
            //
            // tvImageList
            //
            this.tvImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tvImageList.ImageStream")));
            this.tvImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tvImageList.Images.SetKeyName(0, "page.png");
            //
            // btnDekiWikiHome
            //
            this.btnDekiWikiHome.Image = global::ZScreenLib.Properties.Resources.nav_home;
            this.btnDekiWikiHome.Location = new System.Drawing.Point(12, 29);
            this.btnDekiWikiHome.Name = "btnDekiWikiHome";
            this.btnDekiWikiHome.Size = new System.Drawing.Size(40, 40);
            this.btnDekiWikiHome.TabIndex = 2;
            this.btnDekiWikiHome.UseVisualStyleBackColor = true;
            this.btnDekiWikiHome.Click += new System.EventHandler(this.btnDekiWikiHome_Click);
            //
            // btnDekiWikiUser
            //
            this.btnDekiWikiUser.Image = global::ZScreenLib.Properties.Resources.nav_user;
            this.btnDekiWikiUser.Location = new System.Drawing.Point(12, 75);
            this.btnDekiWikiUser.Name = "btnDekiWikiUser";
            this.btnDekiWikiUser.Size = new System.Drawing.Size(40, 40);
            this.btnDekiWikiUser.TabIndex = 3;
            this.btnDekiWikiUser.UseVisualStyleBackColor = true;
            this.btnDekiWikiUser.Click += new System.EventHandler(this.btnDekiWikiUser_Click);
            //
            // btnDekiWikiCancel
            //
            this.btnDekiWikiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDekiWikiCancel.Location = new System.Drawing.Point(291, 226);
            this.btnDekiWikiCancel.Name = "btnDekiWikiCancel";
            this.btnDekiWikiCancel.Size = new System.Drawing.Size(75, 23);
            this.btnDekiWikiCancel.TabIndex = 5;
            this.btnDekiWikiCancel.Text = "Cancel";
            this.btnDekiWikiCancel.UseVisualStyleBackColor = true;
            this.btnDekiWikiCancel.Click += new System.EventHandler(this.btnDekiWikiCancel_Click);
            //
            // btnDekiWikiSave
            //
            this.btnDekiWikiSave.Location = new System.Drawing.Point(210, 226);
            this.btnDekiWikiSave.Name = "btnDekiWikiSave";
            this.btnDekiWikiSave.Size = new System.Drawing.Size(75, 23);
            this.btnDekiWikiSave.TabIndex = 6;
            this.btnDekiWikiSave.Text = "Save";
            this.btnDekiWikiSave.UseVisualStyleBackColor = true;
            this.btnDekiWikiSave.Click += new System.EventHandler(this.btnDekiWikiSave_Click);
            //
            // lblInstructions
            //
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(13, 13);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(256, 13);
            this.lblInstructions.TabIndex = 7;
            this.lblInstructions.Text = "Please select the page to attach this image to below:";
            //
            // lblPathLabel
            //
            this.lblPathLabel.AutoSize = true;
            this.lblPathLabel.Location = new System.Drawing.Point(12, 202);
            this.lblPathLabel.Name = "lblPathLabel";
            this.lblPathLabel.Size = new System.Drawing.Size(32, 13);
            this.lblPathLabel.TabIndex = 8;
            this.lblPathLabel.Text = "Path:";
            //
            // lblDekiWikiPath
            //
            this.lblDekiWikiPath.Location = new System.Drawing.Point(58, 202);
            this.lblDekiWikiPath.Name = "lblDekiWikiPath";
            this.lblDekiWikiPath.Size = new System.Drawing.Size(308, 13);
            this.lblDekiWikiPath.TabIndex = 9;
            //
            // lblAccountLabel
            //
            this.lblAccountLabel.AutoSize = true;
            this.lblAccountLabel.Location = new System.Drawing.Point(12, 219);
            this.lblAccountLabel.Name = "lblAccountLabel";
            this.lblAccountLabel.Size = new System.Drawing.Size(50, 13);
            this.lblAccountLabel.TabIndex = 10;
            this.lblAccountLabel.Text = "Account:";
            //
            // lblAccount
            //
            this.lblAccount.Location = new System.Drawing.Point(58, 220);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(146, 13);
            this.lblAccount.TabIndex = 11;
            //
            // DekiWikiPath
            //
            this.AcceptButton = this.btnDekiWikiSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnDekiWikiCancel;
            this.ClientSize = new System.Drawing.Size(378, 264);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.lblAccountLabel);
            this.Controls.Add(this.lblDekiWikiPath);
            this.Controls.Add(this.lblPathLabel);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnDekiWikiSave);
            this.Controls.Add(this.btnDekiWikiCancel);
            this.Controls.Add(this.btnDekiWikiUser);
            this.Controls.Add(this.btnDekiWikiHome);
            this.Controls.Add(this.tvDekiWikiPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DekiWikiPath";
            this.Text = "Where do you want to save to?";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DekiWikiPath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.TreeView tvDekiWikiPath;
        private System.Windows.Forms.Button btnDekiWikiHome;
        private System.Windows.Forms.Button btnDekiWikiUser;
        private System.Windows.Forms.Button btnDekiWikiCancel;
        private System.Windows.Forms.Button btnDekiWikiSave;
        private System.Windows.Forms.ImageList tvImageList;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblPathLabel;
        private System.Windows.Forms.Label lblDekiWikiPath;
        private System.Windows.Forms.Label lblAccountLabel;
        private System.Windows.Forms.Label lblAccount;
    }
}