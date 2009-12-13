using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DavuxLib.Controls
{
    public class SavedSizePositionForm : System.Windows.Forms.Form
    {

        public string SavedStateKey = "";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;

            Rectangle Saved = Settings.Get("#" + SavedStateKey + "|Size", Rectangle.Empty);

            if (Saved != Rectangle.Empty && IsVisibleOnAnyScreen(Saved))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.DesktopBounds = Saved;

                switch (Settings.Get("#" + SavedStateKey + "|WindowMax", 1))
                {
                    case 1:
                        this.WindowState = FormWindowState.Normal;
                        break;
                    case 2:
                        this.WindowState = FormWindowState.Maximized;
                        break;
                    case 3:
                        this.WindowState = FormWindowState.Minimized;
                        break;
                }
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;

                // we can still apply the saved size
                //this.Size = Settings.Default.WindowPosition.Size;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

            // only save the WindowState if Normal or Maximized
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                    Settings.Set("#" + SavedStateKey + "|WindowMax", 1);
                    break;
                case FormWindowState.Maximized:
                    Settings.Set("#" + SavedStateKey + "|WindowMax", 2);
                    break;
                default:
                    Settings.Set("#" + SavedStateKey + "|WindowMax", 3);
                    break;
            }

            // reset window state to normal to get the correct bounds
            // also make the form invisible to prevent distracting the user
            this.Visible = false;
            this.WindowState = FormWindowState.Normal;


            Settings.Set("#" + SavedStateKey + "|Size", this.DesktopBounds);
            base.OnClosing(e);
        }
 
        private bool IsVisibleOnAnyScreen(Rectangle rect)
        {
            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(rect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
