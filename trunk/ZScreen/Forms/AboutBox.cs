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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;

namespace ZSS.Forms
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.zss_main;
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", Application.ProductVersion);
            this.lblRev.Text = string.Format("Rev. {0}", this.Revision);
            this.labelCopyright.Text = AssemblyCopyright;
            this.llblCompanyName.Text = AssemblyCompany;
            StringBuilder sbDesc = new StringBuilder();
            sbDesc.AppendLine(string.Format("{0} is developed by:", AssemblyTitle));
            sbDesc.AppendLine();
            sbDesc.AppendLine("inf1ni (Brandon Zimmerman)");
            sbDesc.AppendLine("McoreD (Mike Delpach)");
            sbDesc.AppendLine("Jaex (Berk)");
            sbDesc.AppendLine();
            sbDesc.AppendLine(AssemblyDescription);
            sbDesc.AppendLine();
            sbDesc.AppendLine("Acknowledgements:");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Silk icon set 1.3 by Mark James: http://www.famfamfam.com/lab/icons/silk");
            sbDesc.AppendLine();
            sbDesc.AppendLine("Portions of Selected Window code from Greenshot: https://sourceforge.net/projects/greenshot");
            this.textBoxDescription.Text = sbDesc.ToString();

            //set translations for OK button
            okButton.Text = "OK";
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
                return AssemblyVersion.ToString().Split('.')[3];
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_ISSUES);
        }

        private void llblCompanyName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("www." + ((Control)sender).Text);
        }

        private void logoPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Image logo = logoPictureBox.Image;
            if (e.Button == MouseButtons.Left)
            {
                logo.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            else if (e.Button == MouseButtons.Right)
            {
                logo.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            logoPictureBox.Image = logo;
        }

        private void lblRev_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen/source/detail?r=" + this.Revision);
        }
    }
}