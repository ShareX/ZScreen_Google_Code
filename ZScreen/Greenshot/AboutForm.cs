/*
 * Erstellt mit SharpDevelop.
 * Benutzer: thomas
 * Datum: 21.04.2007
 * Zeit: 17:05
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Greenshot
{
	/// <summary>
	/// Description of AboutForm.
	/// </summary>
	public partial class AboutForm : Form
		
	{	
		public AboutForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			Version v = Assembly.GetExecutingAssembly().GetName().Version;
			lblTitle.Text = "Greenshot " + v.Major + "." + v.Minor + "." + v.Build.ToString("000");	
		}
		
	}
}
