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
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace ZSS.Forms
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.zss_main;
            Bitmap bmp = Properties.Resources.main;
            bmp = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
            bmp = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(-200));
            pbLogo.Image = bmp;
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", Application.ProductVersion);
            this.lblRev.Location = new Point(this.labelVersion.Left + this.labelVersion.Width + 10, this.labelVersion.Top);
            this.lblRev.Text = string.Format("Rev. {0}", this.Revision);
            this.labelCopyright.Text = AssemblyCopyright;
            this.lblCompanyName.Text = AssemblyCompany;
            lblDevelopers.Text = string.Format("{0} is developed by:", AssemblyTitle);
            StringBuilder sbDesc = new StringBuilder();
            sbDesc.AppendLine("Acknowledgements:");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Silk icon set 1.3 by Mark James: http://www.famfamfam.com/lab/icons/silk");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Modified version of Greenshot Image Editor 0.7.009 and Portions of Selected Window code from Greenshot: https://sourceforge.net/projects/greenshot");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Running from:");
            sbDesc.AppendLine(Application.ExecutablePath);
            sbDesc.AppendLine();
            sbDesc.AppendLine("Settings file:");
            sbDesc.AppendLine(Program.DefaultXMLFilePath);
            this.textBoxDescription.Text = sbDesc.ToString();
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

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string Revision
        {
            get
            {
                return AssemblyVersion.Split('.')[3];
            }
        }
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
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

        #endregion


        private void AboutBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical);
            brush.SetSigmaBellShape(0.20f);
            g.FillRectangle(brush, rect);
        }

        private void lblCompanyName_Click(object sender, EventArgs e)
        {
            Process.Start("www.brandonz.net");
        }

        private void lblRev_Click_1(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen/source/detail?r=" + this.Revision);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}