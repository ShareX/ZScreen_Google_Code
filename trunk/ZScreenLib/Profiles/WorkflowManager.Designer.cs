namespace ZScreenLib
{
    partial class ProfileManager
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
            this.lvProfiles = new HelpersLib.MyListView();
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHotkey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnProfileCreate = new System.Windows.Forms.Button();
            this.btnProfileEdit = new System.Windows.Forms.Button();
            this.btnProfileDelete = new System.Windows.Forms.Button();
            this.btnProfileEnable = new System.Windows.Forms.Button();
            this.btnProfileDisable = new System.Windows.Forms.Button();
            this.btnProfileDuplicate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvProfiles
            // 
            this.lvProfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDescription,
            this.chTask,
            this.chHotkey,
            this.chEnabled});
            this.lvProfiles.FullRowSelect = true;
            this.lvProfiles.Location = new System.Drawing.Point(8, 8);
            this.lvProfiles.Name = "lvProfiles";
            this.lvProfiles.Size = new System.Drawing.Size(560, 320);
            this.lvProfiles.TabIndex = 0;
            this.lvProfiles.UseCompatibleStateImageBehavior = false;
            this.lvProfiles.View = System.Windows.Forms.View.Details;
            this.lvProfiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvProfiles_MouseDoubleClick);
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 224;
            // 
            // chTask
            // 
            this.chTask.Text = "Task";
            // 
            // chHotkey
            // 
            this.chHotkey.Text = "Hotkey";
            // 
            // chEnabled
            // 
            this.chEnabled.Text = "Enabled";
            this.chEnabled.Width = 76;
            // 
            // btnProfileCreate
            // 
            this.btnProfileCreate.Location = new System.Drawing.Point(576, 8);
            this.btnProfileCreate.Name = "btnProfileCreate";
            this.btnProfileCreate.Size = new System.Drawing.Size(112, 24);
            this.btnProfileCreate.TabIndex = 1;
            this.btnProfileCreate.Text = "&Create";
            this.btnProfileCreate.UseVisualStyleBackColor = true;
            this.btnProfileCreate.Click += new System.EventHandler(this.btnProfileCreate_Click);
            // 
            // btnProfileEdit
            // 
            this.btnProfileEdit.Location = new System.Drawing.Point(576, 40);
            this.btnProfileEdit.Name = "btnProfileEdit";
            this.btnProfileEdit.Size = new System.Drawing.Size(112, 24);
            this.btnProfileEdit.TabIndex = 2;
            this.btnProfileEdit.Text = "&Edit";
            this.btnProfileEdit.UseVisualStyleBackColor = true;
            this.btnProfileEdit.Click += new System.EventHandler(this.btnProfileEdit_Click);
            // 
            // btnProfileDelete
            // 
            this.btnProfileDelete.Location = new System.Drawing.Point(576, 72);
            this.btnProfileDelete.Name = "btnProfileDelete";
            this.btnProfileDelete.Size = new System.Drawing.Size(112, 24);
            this.btnProfileDelete.TabIndex = 3;
            this.btnProfileDelete.Text = "&Delete";
            this.btnProfileDelete.UseVisualStyleBackColor = true;
            // 
            // btnProfileEnable
            // 
            this.btnProfileEnable.Location = new System.Drawing.Point(576, 104);
            this.btnProfileEnable.Name = "btnProfileEnable";
            this.btnProfileEnable.Size = new System.Drawing.Size(112, 24);
            this.btnProfileEnable.TabIndex = 4;
            this.btnProfileEnable.Text = "E&nable";
            this.btnProfileEnable.UseVisualStyleBackColor = true;
            // 
            // btnProfileDisable
            // 
            this.btnProfileDisable.Location = new System.Drawing.Point(576, 136);
            this.btnProfileDisable.Name = "btnProfileDisable";
            this.btnProfileDisable.Size = new System.Drawing.Size(112, 24);
            this.btnProfileDisable.TabIndex = 5;
            this.btnProfileDisable.Text = "Di&sable";
            this.btnProfileDisable.UseVisualStyleBackColor = true;
            // 
            // btnProfileDuplicate
            // 
            this.btnProfileDuplicate.Location = new System.Drawing.Point(576, 168);
            this.btnProfileDuplicate.Name = "btnProfileDuplicate";
            this.btnProfileDuplicate.Size = new System.Drawing.Size(112, 24);
            this.btnProfileDuplicate.TabIndex = 6;
            this.btnProfileDuplicate.Text = "D&uplicate";
            this.btnProfileDuplicate.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(576, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Cl&ose";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // ProfileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 338);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnProfileDuplicate);
            this.Controls.Add(this.btnProfileDisable);
            this.Controls.Add(this.btnProfileEnable);
            this.Controls.Add(this.btnProfileDelete);
            this.Controls.Add(this.btnProfileEdit);
            this.Controls.Add(this.btnProfileCreate);
            this.Controls.Add(this.lvProfiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProfileManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProfileManager";
            this.Load += new System.EventHandler(this.ProfileManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private HelpersLib.MyListView lvProfiles;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ColumnHeader chHotkey;
        private System.Windows.Forms.ColumnHeader chEnabled;
        private System.Windows.Forms.Button btnProfileCreate;
        private System.Windows.Forms.Button btnProfileEdit;
        private System.Windows.Forms.Button btnProfileDelete;
        private System.Windows.Forms.Button btnProfileEnable;
        private System.Windows.Forms.Button btnProfileDisable;
        private System.Windows.Forms.Button btnProfileDuplicate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader chTask;
    }
}