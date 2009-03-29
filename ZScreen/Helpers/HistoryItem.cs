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
using System.Text;

namespace ZSS.Helpers
{
    public class HistoryItem
    {
        public string JobName { get; set; }
        public string FileName { get; set; }
        public ImageFileManager ScreenshotManager { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }
        /// <summary>
        /// Full Image, Active Window, Cropped Window etc..
        /// </summary>
        public string DestinationMode { get; set; }
        /// <summary>
        /// ImageShack, TinyPic, xs.to, FTP...
        /// </summary>
        public string DestinationName { get; set; }
        public JobCategoryType JobCategory { get; set; }
        public ImageDestType ImageDestCategory { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UploadDuration { get; set; }

        public HistoryItem() { }

        public HistoryItem(MainAppTask task)
        {
            this.JobName = task.Job.GetDescription();
            this.FileName = task.FileName.ToString();
            this.LocalPath = task.LocalFilePath;
            this.RemotePath = task.RemoteFilePath;
            this.DestinationMode = task.ImageDestCategory.GetDescription();
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
            return FileName;
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Date Uploaded:   {0}", this.EndTime.ToShortDateString()));
            sb.AppendLine(String.Format("Time Uploaded:   {0}", this.EndTime.ToLongTimeString()));
            sb.AppendLine(String.Format("Upload Duration: {0}", this.UploadDuration));
            return sb.ToString().TrimEnd();
        }

        private string GetDestinationName(MainAppTask t)
        {
            switch (t.ImageDestCategory)
            {
                case ImageDestType.FTP:
                case ImageDestType.CUSTOM_UPLOADER:
                    return string.Format("{0}: {1}", t.ImageDestCategory.GetDescription(), t.DestinationName);
                default:
                    return string.Format("{0}", t.ImageDestCategory.GetDescription());
            }
        }
    }
}