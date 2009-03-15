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
using ZSS.ImageUploader.Helpers;
using ZSS.Tasks;

namespace ZSS.Helpers
{
    public class HistoryItem
    {
        public string JobName { get; private set; }
        public string FileName { get; private set; }
        public ImageFileManager ScreenshotManager { get; set; }
        public string LocalPath { get; private set; }
        public string RemotePath { get; private set; }
        /// <summary>
        /// Full Image, Active Window, Cropped Window etc..
        /// </summary>
        public string DestinationMode { get; private set; }
        /// <summary>
        /// ImageShack, TinyPic, xs.to, FTP...
        /// </summary>
        public string DestinationName { get; private set; }
        public JobCategoryType JobCategory { get; private set; }
        public ImageDestType ImageDestCategory { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string UploadDuration { get; private set; }

        public HistoryItem(MainAppTask task)
        {
            this.JobName = task.Job.ToDescriptionString();
            this.FileName = task.ImageName.ToString();
            this.LocalPath = task.ImageLocalPath;
            this.RemotePath = task.ImageRemotePath;
            this.DestinationMode = task.ImageDestCategory.ToDescriptionString();
            this.DestinationName = GetDestinationName(task);
            this.ScreenshotManager = task.ImageManager;
            this.JobCategory = task.JobCategory;
            this.ImageDestCategory = task.ImageDestCategory;
            this.StartTime = task.StartTime;
            this.EndTime = task.EndTime;
            this.UploadDuration = task.UploadDuration;
        }

        public override string ToString()
        {
            return EndTime.ToLongTimeString() + " - " + FileName;
        }

        private string GetDestinationName(MainAppTask t)
        {
            string name = "";

            switch (t.ImageDestCategory)
            {
                case ImageDestType.FTP:
                case ImageDestType.CUSTOM_UPLOADER:
                    name = string.Format("{0}: {1}", t.ImageDestCategory.ToDescriptionString(), t.ImageDestinationName);
                    break;
                default:
                    name = string.Format("{0}", t.ImageDestCategory.ToDescriptionString());
                    break;
            }

            return name;
        }
    }
}