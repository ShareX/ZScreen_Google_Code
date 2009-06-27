using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZSS.Forms;

namespace ZSS.Global
{
    public static class FormsMgr
    {
        public static void ShowLicense()
        {
            string lic = FileSystem.GetTextFromFile(Path.Combine(Application.StartupPath, "License.txt"));
            lic = lic != string.Empty ? lic : FileSystem.GetText("License.txt");
            if (lic != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "License"), lic) { Icon = Properties.Resources.zss_main };
                v.ShowDialog();
            }
        }

        public static void ShowVersionHistory()
        {
            string h = FileSystem.GetTextFromFile(Path.Combine(Application.StartupPath, "VersionHistory.txt"));
            if (h == string.Empty)
            {
                h = FileSystem.GetText("VersionHistory.txt");
            }
            if (h != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "Version History"), h) { Icon = Properties.Resources.zss_main };
                v.ShowDialog();
            }
        }

        public static void ShowAboutWindow()
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

    }
}
