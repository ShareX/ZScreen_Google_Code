#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    public partial class AboutBox : Form
    {
        private int saturation = 200;
        private int step = 10;
        private int multiply = 1;

        public AboutBox()
        {
            InitializeComponent();

            this.Icon = Resources.zss_main;
            Bitmap bmp = Resources.main;
            bmp = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
            bmp = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(-250));
            pbLogo.Image = bmp;
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", Application.ProductVersion);
            this.lblRev.Location = new Point(this.labelVersion.Left + this.labelVersion.Width + 10, this.labelVersion.Top);
            this.lblRev.Text = string.Format("Rev. {0}", Adapter.AppRevision);
            this.labelCopyright.Text = AssemblyCopyright;
            lblDevelopers.Text = string.Format("{0} is developed by:", AssemblyTitle);
            Timer timer = new Timer { Interval = 100 };
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            this.FormClosing += (v1, v2) => timer.Dispose();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (saturation > 400)
            {
                step = -20;
            }
            else if (saturation < 200)
            {
                step = 20;
            }

            saturation += step;

            Bitmap bmp = Resources.main;
            bmp = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
            bmp = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(-saturation * multiply));
            pbLogo.Image = bmp;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion Assembly Attribute Accessors

        private void AboutBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical))
            {
                brush.SetSigmaBellShape(0.20f);
                g.FillRectangle(brush, rect);
            }
        }

        private void labelProductName_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen");
        }

        private void lblRev_Click_1(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen/source/detail?r=" + Adapter.AppRevision);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblBrandon_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/u/rgrthat");
        }

        private void lblMike_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/u/mcored");
        }

        private void lblBerk_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/u/flexy123");
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            multiply = -multiply;
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            StringBuilder sbDesc = new StringBuilder();
            sbDesc.AppendLine("Acknowledgements:");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Starksoft FTP Component, Starksoft SOCKS and HTTP Proxy Component by Benton Stark: http://www.starksoft.com");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Silk Icons 1.3 by Mark James: http://www.famfamfam.com/lab/icons/silk");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Fugue Icons 3.1 by Yusuke Kamiyamane: http://p.yusukekamiyamane.com");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Image Editor is based on a modified version of Greenshot Image Editor 0.7.009: https://sourceforge.net/projects/greenshot");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Webpage Capture includes modified code from IECapt by Björn Höhrmann: http://iecapt.sourceforge.net");
            sbDesc.AppendLine();
            sbDesc.AppendLine("GIF file creation uses Image Quantizer example project by Brendan Tompkins: http://codebetter.com/media/p/164230.aspx");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Running from:");
            sbDesc.AppendLine(Application.ExecutablePath);
            sbDesc.AppendLine();
            sbDesc.AppendLine("Settings file:");
            sbDesc.AppendLine(Engine.SettingsFilePath);

            textBoxDescription.Text = sbDesc.ToString();
        }

        private void textBoxDescription_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}