#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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

using System;
using UploadersLib;
using UploadersLib.Helpers;

namespace ZUploader
{
    [Serializable]
    public class Settings
    {
        // Main Form

        public int SelectedImageUploaderDestination = 0;
        public int SelectedTextUploaderDestination = 0;
        public int SelectedFileUploaderDestination = 0;

        // Settings Form

        public bool ClipboardAutoCopy = true;
        public bool AutoPlaySound = true;

        public EImageFormat ImageFormat = EImageFormat.PNG;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        public bool SaveHistory = true;
        public bool UseCustomHistoryPath = false;
        public string CustomHistoryPath = string.Empty;

        public FTPAccount FTPAccount = new FTPAccount();
        public ProxyInfo ProxySettings = new ProxyInfo();

        public bool Save()
        {
            using (new DebugTimer("Settings.Save", true))
            {
                return SettingHelpers.Save(this, Program.SettingsFilePath, SerializationType.Binary);
            }
        }

        public static Settings Load()
        {
            using (new DebugTimer("Settings.Load", true))
            {
                return SettingHelpers.Load<Settings>(Program.SettingsFilePath, SerializationType.Binary);
            }
        }
    }
}