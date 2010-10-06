#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2010 ZScreen Developers

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

using System.IO;
using UploadersLib.Helpers;

namespace ZUploader
{
    public class UploadInfo
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public ProgressManager Progress { get; set; }

        private string filePath;
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                FileName = Path.GetFileName(filePath);
            }
        }

        public string FileName { get; set; }

        private EDataType uploaderType;
        public EDataType UploaderType
        {
            get
            {
                return uploaderType;
            }
            set
            {
                uploaderType = value;
                switch (uploaderType)
                {
                    case EDataType.File:
                        UploaderName = UploadManager.FileUploader.GetDescription();
                        break;
                    case EDataType.Image:
                        UploaderName = UploadManager.ImageUploader.GetDescription();
                        break;
                    case EDataType.Text:
                        UploaderName = UploadManager.TextUploader.GetDescription();
                        break;
                }
            }
        }

        public string UploaderName { get; set; }
        public UploadResult Result { get; set; }

        public UploadInfo()
        {
            Result = new UploadResult();
        }
    }
}