namespace ZScreenGUI
{
    partial class ZScreenSnap : ZScreenCoreUI
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.flpTasks = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTasksActions = new System.Windows.Forms.Button();
            this.btnTasksEffects = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.flpTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tlpMain.Controls.Add(this.pbPreview, 1, 0);
            this.tlpMain.Controls.Add(this.flpTasks, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(844, 500);
            this.tlpMain.TabIndex = 128;
            this.tlpMain.Controls.SetChildIndex(this.flpTasks, 0);
            this.tlpMain.Controls.SetChildIndex(this.pbPreview, 0);
            // 
            // pbPreview
            // 
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(131, 3);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(552, 494);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 128;
            this.pbPreview.TabStop = false;
            // 
            // flpTasks
            // 
            this.flpTasks.Controls.Add(this.btnTasksActions);
            this.flpTasks.Controls.Add(this.btnTasksEffects);
            this.flpTasks.Controls.Add(this.btnOptions);
            this.flpTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTasks.Location = new System.Drawing.Point(3, 3);
            this.flpTasks.Name = "flpTasks";
            this.flpTasks.Size = new System.Drawing.Size(122, 494);
            this.flpTasks.TabIndex = 129;
            // 
            // btnTasksActions
            // 
            this.btnTasksActions.AutoSize = true;
            this.btnTasksActions.Location = new System.Drawing.Point(3, 3);
            this.btnTasksActions.Name = "btnTasksActions";
            this.btnTasksActions.Size = new System.Drawing.Size(101, 23);
            this.btnTasksActions.TabIndex = 0;
            this.btnTasksActions.Text = "Run Actions";
            this.btnTasksActions.UseVisualStyleBackColor = true;
            // 
            // btnTasksEffects
            // 
            this.btnTasksEffects.AutoSize = true;
            this.btnTasksEffects.Location = new System.Drawing.Point(3, 32);
            this.btnTasksEffects.Name = "btnTasksEffects";
            this.btnTasksEffects.Size = new System.Drawing.Size(101, 23);
            this.btnTasksEffects.TabIndex = 1;
            this.btnTasksEffects.Text = "Apply Effects";
            this.btnTasksEffects.UseVisualStyleBackColor = true;
            // 
            // btnOptions
            // 
            this.btnOptions.AutoSize = true;
            this.btnOptions.Location = new System.Drawing.Point(3, 61);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(101, 23);
            this.btnOptions.TabIndex = 2;
            this.btnOptions.Text = "Options...";
            this.btnOptions.UseVisualStyleBackColor = true;
            // 
            // ZScreenSnap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 500);
            this.Controls.Add(this.tlpMain);
            this.Name = "ZScreenSnap";
            this.Text = "ZScreenSnap";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.flpTasks.ResumeLayout(false);
            this.flpTasks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.FlowLayoutPanel flpTasks;
        private System.Windows.Forms.Button btnTasksActions;
        private System.Windows.Forms.Button btnTasksEffects;
        private System.Windows.Forms.Button btnOptions;
    }
}