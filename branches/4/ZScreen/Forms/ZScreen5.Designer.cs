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
            this.btnOptions = new System.Windows.Forms.Button();
            this.gbEffects = new System.Windows.Forms.GroupBox();
            this.chkEffectsShadow = new System.Windows.Forms.CheckBox();
            this.chkEffectsReflection = new System.Windows.Forms.CheckBox();
            this.flpEffects = new System.Windows.Forms.FlowLayoutPanel();
            this.chkBorder = new System.Windows.Forms.CheckBox();
            this.chkEffectsRotate = new System.Windows.Forms.CheckBox();
            this.chkWatermark = new System.Windows.Forms.CheckBox();
            this.chkEffectsResize = new System.Windows.Forms.CheckBox();
            this.ssApp = new System.Windows.Forms.StatusStrip();
            this.tslResolution = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.flpTasks.SuspendLayout();
            this.gbEffects.SuspendLayout();
            this.flpEffects.SuspendLayout();
            this.ssApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Controls.Add(this.pbPreview, 1, 0);
            this.tlpMain.Controls.Add(this.flpTasks, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(686, 500);
            this.tlpMain.TabIndex = 0;
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
            this.flpTasks.Controls.Add(this.gbEffects);
            this.flpTasks.Controls.Add(this.btnTasksActions);
            this.flpTasks.Controls.Add(this.btnOptions);
            this.flpTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTasks.Location = new System.Drawing.Point(3, 3);
            this.flpTasks.Name = "flpTasks";
            this.flpTasks.Size = new System.Drawing.Size(122, 494);
            this.flpTasks.TabIndex = 0;
            // 
            // btnTasksActions
            // 
            this.btnTasksActions.AutoSize = true;
            this.btnTasksActions.Location = new System.Drawing.Point(3, 174);
            this.btnTasksActions.Name = "btnTasksActions";
            this.btnTasksActions.Size = new System.Drawing.Size(101, 23);
            this.btnTasksActions.TabIndex = 1;
            this.btnTasksActions.Text = "Run Actions";
            this.btnTasksActions.UseVisualStyleBackColor = true;
            // 
            // btnOptions
            // 
            this.btnOptions.AutoSize = true;
            this.btnOptions.Location = new System.Drawing.Point(3, 203);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(101, 23);
            this.btnOptions.TabIndex = 2;
            this.btnOptions.Text = "Options...";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // gbEffects
            // 
            this.gbEffects.Controls.Add(this.flpEffects);
            this.gbEffects.Location = new System.Drawing.Point(3, 3);
            this.gbEffects.Name = "gbEffects";
            this.gbEffects.Size = new System.Drawing.Size(109, 165);
            this.gbEffects.TabIndex = 0;
            this.gbEffects.TabStop = false;
            this.gbEffects.Text = "Effects";
            // 
            // chkEffectsShadow
            // 
            this.chkEffectsShadow.AutoSize = true;
            this.chkEffectsShadow.Location = new System.Drawing.Point(3, 3);
            this.chkEffectsShadow.Name = "chkEffectsShadow";
            this.chkEffectsShadow.Size = new System.Drawing.Size(65, 17);
            this.chkEffectsShadow.TabIndex = 0;
            this.chkEffectsShadow.Text = "Shadow";
            this.chkEffectsShadow.UseVisualStyleBackColor = true;
            // 
            // chkEffectsReflection
            // 
            this.chkEffectsReflection.AutoSize = true;
            this.chkEffectsReflection.Location = new System.Drawing.Point(3, 26);
            this.chkEffectsReflection.Name = "chkEffectsReflection";
            this.chkEffectsReflection.Size = new System.Drawing.Size(74, 17);
            this.chkEffectsReflection.TabIndex = 1;
            this.chkEffectsReflection.Text = "Reflection";
            this.chkEffectsReflection.UseVisualStyleBackColor = true;
            // 
            // flpEffects
            // 
            this.flpEffects.Controls.Add(this.chkEffectsShadow);
            this.flpEffects.Controls.Add(this.chkEffectsReflection);
            this.flpEffects.Controls.Add(this.chkBorder);
            this.flpEffects.Controls.Add(this.chkEffectsRotate);
            this.flpEffects.Controls.Add(this.chkWatermark);
            this.flpEffects.Controls.Add(this.chkEffectsResize);
            this.flpEffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpEffects.Location = new System.Drawing.Point(3, 16);
            this.flpEffects.Name = "flpEffects";
            this.flpEffects.Size = new System.Drawing.Size(103, 146);
            this.flpEffects.TabIndex = 0;
            // 
            // chkBorder
            // 
            this.chkBorder.AutoSize = true;
            this.chkBorder.Location = new System.Drawing.Point(3, 49);
            this.chkBorder.Name = "chkBorder";
            this.chkBorder.Size = new System.Drawing.Size(57, 17);
            this.chkBorder.TabIndex = 2;
            this.chkBorder.Text = "Border";
            this.chkBorder.UseVisualStyleBackColor = true;
            // 
            // chkEffectsRotate
            // 
            this.chkEffectsRotate.AutoSize = true;
            this.chkEffectsRotate.Location = new System.Drawing.Point(3, 72);
            this.chkEffectsRotate.Name = "chkEffectsRotate";
            this.chkEffectsRotate.Size = new System.Drawing.Size(58, 17);
            this.chkEffectsRotate.TabIndex = 3;
            this.chkEffectsRotate.Text = "Rotate";
            this.chkEffectsRotate.UseVisualStyleBackColor = true;
            // 
            // chkWatermark
            // 
            this.chkWatermark.AutoSize = true;
            this.chkWatermark.Location = new System.Drawing.Point(3, 95);
            this.chkWatermark.Name = "chkWatermark";
            this.chkWatermark.Size = new System.Drawing.Size(78, 17);
            this.chkWatermark.TabIndex = 4;
            this.chkWatermark.Text = "Watermark";
            this.chkWatermark.UseVisualStyleBackColor = true;
            // 
            // chkEffectsResize
            // 
            this.chkEffectsResize.AutoSize = true;
            this.chkEffectsResize.Location = new System.Drawing.Point(3, 118);
            this.chkEffectsResize.Name = "chkEffectsResize";
            this.chkEffectsResize.Size = new System.Drawing.Size(58, 17);
            this.chkEffectsResize.TabIndex = 5;
            this.chkEffectsResize.Text = "Resize";
            this.chkEffectsResize.UseVisualStyleBackColor = true;
            // 
            // ssApp
            // 
            this.ssApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslResolution});
            this.ssApp.Location = new System.Drawing.Point(0, 478);
            this.ssApp.Name = "ssApp";
            this.ssApp.Size = new System.Drawing.Size(686, 22);
            this.ssApp.TabIndex = 1;
            this.ssApp.Text = "statusStrip1";
            // 
            // tslResolution
            // 
            this.tslResolution.Name = "tslResolution";
            this.tslResolution.Size = new System.Drawing.Size(60, 17);
            this.tslResolution.Text = "1920x1080";
            // 
            // ZScreenSnap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 500);
            this.Controls.Add(this.ssApp);
            this.Controls.Add(this.tlpMain);
            this.Name = "ZScreenSnap";
            this.Text = "ZScreenSnap";
            this.Load += new System.EventHandler(this.ZScreenSnap_Load);
            this.Controls.SetChildIndex(this.tlpMain, 0);
            this.Controls.SetChildIndex(this.ssApp, 0);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.flpTasks.ResumeLayout(false);
            this.flpTasks.PerformLayout();
            this.gbEffects.ResumeLayout(false);
            this.flpEffects.ResumeLayout(false);
            this.flpEffects.PerformLayout();
            this.ssApp.ResumeLayout(false);
            this.ssApp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.FlowLayoutPanel flpTasks;
        private System.Windows.Forms.Button btnTasksActions;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.GroupBox gbEffects;
        private System.Windows.Forms.FlowLayoutPanel flpEffects;
        private System.Windows.Forms.CheckBox chkEffectsShadow;
        private System.Windows.Forms.CheckBox chkEffectsReflection;
        private System.Windows.Forms.CheckBox chkBorder;
        private System.Windows.Forms.CheckBox chkEffectsRotate;
        private System.Windows.Forms.CheckBox chkWatermark;
        private System.Windows.Forms.CheckBox chkEffectsResize;
        private System.Windows.Forms.StatusStrip ssApp;
        private System.Windows.Forms.ToolStripStatusLabel tslResolution;
    }
}