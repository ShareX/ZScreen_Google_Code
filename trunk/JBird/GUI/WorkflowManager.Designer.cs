namespace ZScreenLib
{
    partial class WorkflowManager
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
            this.lvWorkflows = new HelpersLib.MyListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHotkey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnProfileCreate = new System.Windows.Forms.Button();
            this.btnProfileEdit = new System.Windows.Forms.Button();
            this.btnProfileDelete = new System.Windows.Forms.Button();
            this.btnProfileDuplicate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpButtons.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvWorkflows
            // 
            this.lvWorkflows.AllowColumnReorder = true;
            this.lvWorkflows.CheckBoxes = true;
            this.lvWorkflows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chTask,
            this.chHotkey});
            this.lvWorkflows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWorkflows.FullRowSelect = true;
            this.lvWorkflows.GridLines = true;
            this.lvWorkflows.Location = new System.Drawing.Point(3, 3);
            this.lvWorkflows.Name = "lvWorkflows";
            this.lvWorkflows.Size = new System.Drawing.Size(622, 332);
            this.lvWorkflows.TabIndex = 0;
            this.lvWorkflows.UseCompatibleStateImageBehavior = false;
            this.lvWorkflows.View = System.Windows.Forms.View.Details;
            this.lvWorkflows.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvWorkflows_ItemChecked);
            this.lvWorkflows.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvProfiles_MouseDoubleClick);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 224;
            // 
            // chTask
            // 
            this.chTask.Text = "Task";
            this.chTask.Width = 228;
            // 
            // chHotkey
            // 
            this.chHotkey.Text = "Hotkey";
            this.chHotkey.Width = 142;
            // 
            // btnProfileCreate
            // 
            this.btnProfileCreate.Location = new System.Drawing.Point(3, 3);
            this.btnProfileCreate.Name = "btnProfileCreate";
            this.btnProfileCreate.Size = new System.Drawing.Size(112, 24);
            this.btnProfileCreate.TabIndex = 1;
            this.btnProfileCreate.Text = "&Create";
            this.btnProfileCreate.UseVisualStyleBackColor = true;
            this.btnProfileCreate.Click += new System.EventHandler(this.btnProfileCreate_Click);
            // 
            // btnProfileEdit
            // 
            this.btnProfileEdit.Location = new System.Drawing.Point(3, 33);
            this.btnProfileEdit.Name = "btnProfileEdit";
            this.btnProfileEdit.Size = new System.Drawing.Size(112, 24);
            this.btnProfileEdit.TabIndex = 2;
            this.btnProfileEdit.Text = "&Edit";
            this.btnProfileEdit.UseVisualStyleBackColor = true;
            this.btnProfileEdit.Click += new System.EventHandler(this.btnProfileEdit_Click);
            // 
            // btnProfileDelete
            // 
            this.btnProfileDelete.Location = new System.Drawing.Point(3, 93);
            this.btnProfileDelete.Name = "btnProfileDelete";
            this.btnProfileDelete.Size = new System.Drawing.Size(112, 24);
            this.btnProfileDelete.TabIndex = 3;
            this.btnProfileDelete.Text = "&Delete";
            this.btnProfileDelete.UseVisualStyleBackColor = true;
            this.btnProfileDelete.Click += new System.EventHandler(this.btnProfileDelete_Click);
            // 
            // btnProfileDuplicate
            // 
            this.btnProfileDuplicate.Location = new System.Drawing.Point(3, 63);
            this.btnProfileDuplicate.Name = "btnProfileDuplicate";
            this.btnProfileDuplicate.Size = new System.Drawing.Size(112, 24);
            this.btnProfileDuplicate.TabIndex = 6;
            this.btnProfileDuplicate.Text = "D&uplicate";
            this.btnProfileDuplicate.UseVisualStyleBackColor = true;
            this.btnProfileDuplicate.Click += new System.EventHandler(this.btnProfileDuplicate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(3, 123);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Cl&ose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // flpButtons
            // 
            this.flpButtons.Controls.Add(this.btnProfileCreate);
            this.flpButtons.Controls.Add(this.btnProfileEdit);
            this.flpButtons.Controls.Add(this.btnProfileDuplicate);
            this.flpButtons.Controls.Add(this.btnProfileDelete);
            this.flpButtons.Controls.Add(this.btnClose);
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpButtons.Location = new System.Drawing.Point(631, 3);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.Size = new System.Drawing.Size(122, 332);
            this.flpButtons.TabIndex = 8;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tlpMain.Controls.Add(this.flpButtons, 1, 0);
            this.tlpMain.Controls.Add(this.lvWorkflows, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(756, 338);
            this.tlpMain.TabIndex = 9;
            // 
            // WorkflowManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 338);
            this.Controls.Add(this.tlpMain);
            this.Name = "WorkflowManager";
            this.Text = "Workflow Manager";
            this.Load += new System.EventHandler(this.WorkflowManager_Load);
            this.Shown += new System.EventHandler(this.ProfileManager_Shown);
            this.flpButtons.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HelpersLib.MyListView lvWorkflows;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chHotkey;
        private System.Windows.Forms.Button btnProfileCreate;
        private System.Windows.Forms.Button btnProfileEdit;
        private System.Windows.Forms.Button btnProfileDelete;
        private System.Windows.Forms.Button btnProfileDuplicate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader chTask;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
    }
}