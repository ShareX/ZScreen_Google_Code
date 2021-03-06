﻿namespace JBirdGUI
{
    partial class JBirdCoreUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JBirdCoreUI));
            this.niApp = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsApp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiWorkflows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // niApp
            // 
            this.niApp.ContextMenuStrip = this.cmsApp;
            this.niApp.Icon = ((System.Drawing.Icon)(resources.GetObject("niApp.Icon")));
            this.niApp.Text = "notifyIcon1";
            this.niApp.Visible = true;
            // 
            // cmsApp
            // 
            this.cmsApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiWorkflows,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.cmsApp.Name = "cmsApp";
            this.cmsApp.Size = new System.Drawing.Size(114, 54);
            // 
            // tsmiWorkflows
            // 
            this.tsmiWorkflows.Name = "tsmiWorkflows";
            this.tsmiWorkflows.Size = new System.Drawing.Size(113, 22);
            this.tsmiWorkflows.Text = "Profiles";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(113, 22);
            this.tsmiExit.Text = "E&xit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // JBirdCoreUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 264);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(64, 64);
            this.Name = "JBirdCoreUI";
            this.Text = "JBirdCoreUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JBirdCoreUI_FormClosing);
            this.Load += new System.EventHandler(this.JBirdCoreUI_Load);
            this.Shown += new System.EventHandler(this.JBirdCoreUI_Shown);
            this.cmsApp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tsmiWorkflows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        protected System.Windows.Forms.NotifyIcon niApp;
        protected System.Windows.Forms.ContextMenuStrip cmsApp;
    }
}