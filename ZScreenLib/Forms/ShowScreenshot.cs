#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using GraphicsMgrLib;

namespace ZScreenLib
{
    public class ShowScreenshot : Form
    {
        private Image screenshot;

        public ShowScreenshot(Image image)
        {
            this.screenshot = (Image)image.Clone();
            this.BackColor = Color.FloralWhite;
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            this.Bounds = GraphicsMgr.GetScreenBounds();
            this.BackgroundImage = screenshot;
        }

        private void ShowScreenshot_Load(object sender, EventArgs e)
        {
            if (this.Bounds.Width > this.BackgroundImage.Width && this.Bounds.Height > this.BackgroundImage.Height)
            {
                this.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                this.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void ShowScreenshot_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ShowScreenshot_Shown(object sender, EventArgs e)
        {
            NativeMethods.SetForegroundWindow(this.Handle);
            NativeMethods.SetActiveWindow(this.Handle);
        }

        private void ShowScreenshot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #region Windows Form Designer generated code

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
            screenshot.Dispose();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ShowScreenshot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Name = "ShowScreenshot";
            this.Text = "ShowScreenshot";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.ShowScreenshot_Deactivate);
            this.Load += new System.EventHandler(this.ShowScreenshot_Load);
            this.Shown += new System.EventHandler(this.ShowScreenshot_Shown);
            this.Leave += new System.EventHandler(this.ShowScreenshot_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowScreenshot_MouseDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShowScreenshot_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private void ShowScreenshot_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowScreenshot_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}