namespace HistoryLib
{
    partial class HistoryForm
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
            this.lvHistory = new HistoryLib.ListViewNF();
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFilepath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chThumbnailURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDeletionURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            //
            // lvHistory
            //
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chFilename,
            this.chFilepath,
            this.chDateTime,
            this.chType,
            this.chHost,
            this.chURL,
            this.chThumbnailURL,
            this.chDeletionURL});
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.HideSelection = false;
            this.lvHistory.Location = new System.Drawing.Point(8, 8);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(808, 312);
            this.lvHistory.TabIndex = 0;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            //
            // chID
            //
            this.chID.Text = "ID";
            this.chID.Width = 30;
            //
            // chFilename
            //
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 100;
            //
            // chFilepath
            //
            this.chFilepath.Text = "Filepath";
            //
            // chDateTime
            //
            this.chDateTime.Text = "DateTime";
            //
            // chType
            //
            this.chType.Text = "Type";
            //
            // chHost
            //
            this.chHost.Text = "Host";
            //
            // chURL
            //
            this.chURL.Text = "URL";
            //
            // chThumbnailURL
            //
            this.chThumbnailURL.Text = "ThumbnailURL";
            //
            // chDeletionURL
            //
            this.chDeletionURL.Text = "DeletionURL";
            //
            // HistoryForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 327);
            this.Controls.Add(this.lvHistory);
            this.Name = "HistoryForm";
            this.Text = "HistoryFormTest";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private ListViewNF lvHistory;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chFilepath;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.ColumnHeader chThumbnailURL;
        private System.Windows.Forms.ColumnHeader chDeletionURL;
    }
}