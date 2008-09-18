#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

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
using System.Text;
using System.ComponentModel;

namespace ZSS.Tasks
{
    public class MainAppTask
    {
        public MainAppTask(BackgroundWorker worker, Jobs job)
        {
            this.myWorker = worker;
            this.Job = job;
        }

        public enum Jobs
        {            
            TAKE_SCREENSHOT_WINDOW,
            TAKE_SCREENSHOT_CROPPED,
            TAKE_SCREENSHOT_SCREEN

        }

        public enum ProgressType
        {
            ADD_FILE_TO_LISTBOX,            
            COPY_TO_CLIPBOARD_URL,
            COPY_TO_CLIPBOARD_IMAGE,
            FLASH_ICON,
            INCREMENT_PROGRESS,
            SET_ICON_BUSY,
            UPDATE_STATUS_BAR_TEXT,
            UPDATE_PROGRESS_MAX            
        }

        public Jobs Job { get; private set; }

        public BackgroundWorker myWorker { get; private set; }
        public StringBuilder ScreenshotName { get; set; }
        public string ScreenshotLocalPath { get; set; }
        public string ScreenshotRemotePath { get; set; }
        public List<ImageUploader.ImageFile> ScreenshotList { get; set; }
    }
}
