namespace RegionCaptureTest
{
    partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.tsRegionTools = new System.Windows.Forms.ToolStrip();
            this.tsbRectangle = new System.Windows.Forms.ToolStripButton();
            this.tsbRoundedRectangle = new System.Windows.Forms.ToolStripButton();
            this.tsbEllipse = new System.Windows.Forms.ToolStripButton();
            this.tsbTriangle = new System.Windows.Forms.ToolStripButton();
            this.tsbPolygon = new System.Windows.Forms.ToolStripButton();
            this.tsbFreeHand = new System.Windows.Forms.ToolStripButton();
            this.pbResult = new System.Windows.Forms.PictureBox();
            this.cbShowChecker = new System.Windows.Forms.CheckBox();
            this.tsRegionTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            this.SuspendLayout();
            // 
            // tsRegionTools
            // 
            this.tsRegionTools.AutoSize = false;
            this.tsRegionTools.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsRegionTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRectangle,
            this.tsbRoundedRectangle,
            this.tsbEllipse,
            this.tsbTriangle,
            this.tsbPolygon,
            this.tsbFreeHand});
            this.tsRegionTools.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.tsRegionTools.Location = new System.Drawing.Point(0, 0);
            this.tsRegionTools.Name = "tsRegionTools";
            this.tsRegionTools.Padding = new System.Windows.Forms.Padding(10, 10, 1, 0);
            this.tsRegionTools.Size = new System.Drawing.Size(168, 649);
            this.tsRegionTools.TabIndex = 0;
            this.tsRegionTools.Text = "toolStrip1";
            // 
            // tsbRectangle
            // 
            this.tsbRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRectangle.Image = ((System.Drawing.Image)(resources.GetObject("tsbRectangle.Image")));
            this.tsbRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRectangle.Name = "tsbRectangle";
            this.tsbRectangle.Size = new System.Drawing.Size(63, 19);
            this.tsbRectangle.Text = "Rectangle";
            this.tsbRectangle.Click += new System.EventHandler(this.tsbRectangle_Click);
            // 
            // tsbRoundedRectangle
            // 
            this.tsbRoundedRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRoundedRectangle.Image = ((System.Drawing.Image)(resources.GetObject("tsbRoundedRectangle.Image")));
            this.tsbRoundedRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRoundedRectangle.Name = "tsbRoundedRectangle";
            this.tsbRoundedRectangle.Size = new System.Drawing.Size(114, 19);
            this.tsbRoundedRectangle.Text = "Rounded Rectangle";
            this.tsbRoundedRectangle.Click += new System.EventHandler(this.tsbRoundedRectangle_Click);
            // 
            // tsbEllipse
            // 
            this.tsbEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEllipse.Image = ((System.Drawing.Image)(resources.GetObject("tsbEllipse.Image")));
            this.tsbEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEllipse.Name = "tsbEllipse";
            this.tsbEllipse.Size = new System.Drawing.Size(44, 19);
            this.tsbEllipse.Text = "Ellipse";
            this.tsbEllipse.Click += new System.EventHandler(this.tsbEllipse_Click);
            // 
            // tsbTriangle
            // 
            this.tsbTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbTriangle.Image = ((System.Drawing.Image)(resources.GetObject("tsbTriangle.Image")));
            this.tsbTriangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTriangle.Name = "tsbTriangle";
            this.tsbTriangle.Size = new System.Drawing.Size(54, 19);
            this.tsbTriangle.Text = "Triangle";
            this.tsbTriangle.Click += new System.EventHandler(this.tsbTriangle_Click);
            // 
            // tsbPolygon
            // 
            this.tsbPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPolygon.Image = ((System.Drawing.Image)(resources.GetObject("tsbPolygon.Image")));
            this.tsbPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPolygon.Name = "tsbPolygon";
            this.tsbPolygon.Size = new System.Drawing.Size(55, 19);
            this.tsbPolygon.Text = "Polygon";
            this.tsbPolygon.Click += new System.EventHandler(this.tsbPolygon_Click);
            // 
            // tsbFreeHand
            // 
            this.tsbFreeHand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFreeHand.Image = ((System.Drawing.Image)(resources.GetObject("tsbFreeHand.Image")));
            this.tsbFreeHand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFreeHand.Name = "tsbFreeHand";
            this.tsbFreeHand.Size = new System.Drawing.Size(65, 19);
            this.tsbFreeHand.Text = "Free Hand";
            this.tsbFreeHand.Click += new System.EventHandler(this.tsbFreeHand_Click);
            // 
            // pbResult
            // 
            this.pbResult.BackColor = System.Drawing.Color.Gray;
            this.pbResult.Location = new System.Drawing.Point(176, 32);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(928, 608);
            this.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbResult.TabIndex = 1;
            this.pbResult.TabStop = false;
            // 
            // cbShowChecker
            // 
            this.cbShowChecker.AutoSize = true;
            this.cbShowChecker.ForeColor = System.Drawing.Color.White;
            this.cbShowChecker.Location = new System.Drawing.Point(176, 8);
            this.cbShowChecker.Name = "cbShowChecker";
            this.cbShowChecker.Size = new System.Drawing.Size(167, 17);
            this.cbShowChecker.TabIndex = 2;
            this.cbShowChecker.Text = "Show checkered background";
            this.cbShowChecker.UseVisualStyleBackColor = true;
            this.cbShowChecker.CheckedChanged += new System.EventHandler(this.cbShowChecker_CheckedChanged);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1123, 649);
            this.Controls.Add(this.cbShowChecker);
            this.Controls.Add(this.pbResult);
            this.Controls.Add(this.tsRegionTools);
            this.Name = "TestForm";
            this.Text = "RegionCapture Test";
            this.tsRegionTools.ResumeLayout(false);
            this.tsRegionTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsRegionTools;
        private System.Windows.Forms.ToolStripButton tsbFreeHand;
        private System.Windows.Forms.ToolStripButton tsbEllipse;
        private System.Windows.Forms.ToolStripButton tsbRectangle;
        private System.Windows.Forms.ToolStripButton tsbRoundedRectangle;
        private System.Windows.Forms.ToolStripButton tsbTriangle;
        private System.Windows.Forms.ToolStripButton tsbPolygon;
        private System.Windows.Forms.PictureBox pbResult;
        private System.Windows.Forms.CheckBox cbShowChecker;
    }
}