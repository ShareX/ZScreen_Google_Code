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
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ToolbarWindow : Form
    {
        public event JobsEventHandler EventJob;
        private bool mGuiReady = false;

        public ToolbarWindow()
        {
            InitializeComponent();
            NativeMethods.ActivateWindow(this.Handle);
        }

        private void DoJob(object sender, WorkerTask.Jobs e)
        {
            EventJob(sender, e);
            if (Engine.conf.CloseQuickActions)
            {
                this.Close();
            }
        }

        private void tsbEntireScreen_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.TAKE_SCREENSHOT_SCREEN);
        }

        private void tsbSelectedWindow_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.TakeScreenshotWindowSelected);
        }

        private void tsbCropShot_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.TakeScreenshotCropped);
        }

        private void tsbLastCropShot_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED);
        }

        private void tsbAutoCapture_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.AUTO_CAPTURE);
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.UploadFromClipboard);
        }

        private void tsbDragDropWindow_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.PROCESS_DRAG_N_DROP);
        }

        private void tsbLanguageTranslator_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.LANGUAGE_TRANSLATOR);
        }

        private void tsbScreenColorPicker_Click(object sender, EventArgs e)
        {
            DoJob(this, WorkerTask.Jobs.SCREEN_COLOR_PICKER);
        }

        private void tsQuickActions_MouseEnter(object sender, EventArgs e)
        {
            NativeMethods.SetActiveWindow(this.Handle);
        }

        private void ToolbarWindow_Move(object sender, EventArgs e)
        {
            if (mGuiReady)
            {
                Engine.conf.ActionToolbarLocation = this.Location;
            }
        }

        private void ToolbarWindow_Shown(object sender, EventArgs e)
        {
            mGuiReady = true;
        }

    }
}