#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GraphicsMgrLib;
using HelpersLib;
using UploadersLib;

namespace ZScreenLib
{
    public static class FileSystem
    {
        /// <summary>
        /// Returns a list of file paths from a collection of files and directories
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<string> GetExplorerFileList(string[] paths)
        {
            StringCollection sc = new StringCollection();
            foreach (string p in paths)
            {
                sc.Add(p);
            }
            return GetExplorerFileList(sc);
        }

        public static List<string> GetExplorerFileList(StringCollection paths)
        {
            List<string> files = new List<string>();
            foreach (string fp in paths)
            {
                if (File.Exists(fp))
                {
                    if (Path.GetTempPath().StartsWith(Path.GetDirectoryName(fp)))
                    {
                        string temp = Path.Combine(Engine.ImagesDir, Path.GetFileName(fp));
                        File.Copy(fp, temp);
                        files.Add(temp);
                    }
                    else
                    {
                        files.Add(fp);
                    }
                }
                else if (Directory.Exists(fp))
                {
                    string[] dirFiles = Directory.GetFiles(fp, "*.*", SearchOption.AllDirectories);
                    foreach (string f in dirFiles)
                    {
                        files.Add(f);
                    }
                }
            }
            return files;
        }

        /// <summary>
        /// Function to save and return the file path of a captured image. ImageFormat is based on length of the image.
        /// If the image exists it is overwritten.
        /// </summary>
        /// <param name="img">The actual image</param>
        /// <param name="filePath">The path to where the image will be saved</param>
        /// <returns>Returns the file path to a screenshot</returns>
        public static string WriteImage(WorkerTask task)
        {
            Image img = task.TempImage;
            string filePath = task.LocalFilePath;

            if (task.TaskOutputs.Contains(OutputEnum.LocalDisk) && !string.IsNullOrEmpty(filePath))
            {
                img = ImageEffects.ApplySizeChanges(img);
                img = ImageEffects.ApplyScreenshotEffects(img);
                if (task.Job2 != WorkerTask.JobLevel2.UploadFromClipboard || !Engine.conf.WatermarkExcludeClipboardUpload)
                {
                    img = ImageEffects.ApplyWatermark(img);
                }

                long size = (long)Engine.conf.SwitchAfter * 1024;

                MemoryStream ms = null;

                GraphicsMgr.SaveImageToMemoryStreamOptions opt = new GraphicsMgr.SaveImageToMemoryStreamOptions(img, Engine.zImageFileFormat);
                opt.GIFQuality = Engine.conf.GIFQuality;
                opt.JpgQuality = Engine.conf.JpgQuality;
                opt.MakeJPGBackgroundWhite = Engine.conf.MakeJPGBackgroundWhite;

                try
                {
                    ms = GraphicsMgr.SaveImageToMemoryStream(opt);

                    if (ms.Length > size && size != 0)
                    {
                        opt.MyImageFileFormat = Engine.zImageFileFormatSwitch;
                        ms = GraphicsMgr.SaveImageToMemoryStream(opt);
                        filePath = Path.ChangeExtension(filePath, Engine.zImageFileFormatSwitch.Extension);
                    }

                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    Engine.MyLogger.WriteLine(string.Format("Writing image {0}x{1} to {2}", img.Width, img.Height, filePath));
                    ms.WriteToFile(filePath);
                }
                catch (Exception ex)
                {
                    Engine.MyLogger.WriteException(ex, "Error while saving image");
                }
                finally
                {
                    if (ms != null) ms.Dispose();
                }
            }

            task.UpdateLocalFilePath(filePath);
            return filePath;
        }

        public static string GetTextFromFile(string filePath)
        {
            string s = string.Empty;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    s = sr.ReadToEnd();
                }
            }
            return s;
        }

        public static string GetImagesDir()
        {
            return Directory.Exists(Engine.ImagesDir) ? Engine.ImagesDir : Engine.RootImagesDir;
        }

        public static string GetTempFilePath(string fileName)
        {
            string dir = Engine.CacheDir;
            if (string.IsNullOrEmpty(dir))
                dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, fileName);
        }

        public static bool WriteText(string fp, string myText)
        {
            bool succ = false;
            try
            {
                File.WriteAllText(fp, myText);
                succ = true;
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "WriteText");
            }
            return succ;
        }

        public static bool ExportText(string name, string filePath)
        {
            bool succ = true;
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(GetText(name));
                }
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while exporting text");
                succ = false;
            }

            return succ;
        }

        public static string GetText(string name)
        {
            string text = "";
            try
            {
                System.Reflection.Assembly oAsm = System.Reflection.Assembly.GetExecutingAssembly();

                string fn = "";
                foreach (string n in oAsm.GetManifestResourceNames())
                {
                    if (n.Contains(name))
                    {
                        fn = n;
                        break;
                    }
                }
                Stream oStrm = oAsm.GetManifestResourceStream(fn);
                StreamReader oRdr = new StreamReader(oStrm);
                text = oRdr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while getting text from resource");
            }

            return text;
        }

        public static Image ImageFromFile(string fp)
        {
            return GraphicsMgr.GetImageSafely(fp);
        }

        public static bool IsValidImageFile(string fp)
        {
            if (!string.IsNullOrEmpty(fp))
            {
                foreach (string s in Engine.zImageFileTypes)
                {
                    if (Path.GetExtension(fp).ToLower().EndsWith(s)) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Function to check if file is a valid Text file by checking its extension
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static bool IsValidTextFile(string fp)
        {
            if (!string.IsNullOrEmpty(fp) && File.Exists(fp))
            {
                foreach (string s in Engine.zTextFileTypes)
                {
                    if (Path.GetExtension(fp).ToLower().EndsWith(s)) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Function to check if file is a valid Text file by checking its extension
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static bool IsValidWebpageFile(string fp)
        {
            if (!string.IsNullOrEmpty(fp) && File.Exists(fp))
            {
                foreach (string s in Engine.zWebpageFileTypes)
                {
                    if (Path.GetExtension(fp).ToLower().EndsWith(s)) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// If file exist then adding number end of file name. Example: directory/fileName(2).exe
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueFilePath(string fileName)
        {
            string filePath, fileExt, pattern = @"(^.+\()(\d+)(\)\.\w+$)";
            int num = 1;
            GroupCollection groups = Regex.Match(fileName, pattern).Groups;
            if (string.IsNullOrEmpty(groups[2].Value))
            {
                filePath = fileName.Substring(0, fileName.LastIndexOf('.')) + "(";
                fileExt = ")" + fileName.Remove(0, fileName.LastIndexOf('.'));
            }
            else
            {
                filePath = groups[1].Value;
                fileExt = groups[3].Value;
                num = Convert.ToInt32(groups[2].Value);
            }
            while (File.Exists(fileName))
            {
                fileName = filePath + ++num + fileExt;
            }
            return fileName;
        }

        public static string GetFileSize(long bytes)
        {
            if (bytes >= 1073741824)
            {
                return String.Format("{0:##.##} GiB", (decimal)bytes / 1073741824);
            }
            if (bytes >= 1048576)
            {
                return String.Format("{0:##.##} MiB", (decimal)bytes / 1048576);
            }
            if (bytes >= 1024)
            {
                return String.Format("{0:##.##} KiB", (decimal)bytes / 1024);
            }
            if (bytes > 0 & bytes < 1024)
            {
                return String.Format("{0:##.##} Bytes", bytes);
            }
            return "0 Bytes";
        }

        public static void BackupAppSettings()
        {
            if (Engine.conf != null)
            {
                string fp = Path.Combine(Engine.SettingsDir, string.Format("Settings-{0}-backup.xml", DateTime.Now.ToString("yyyyMM")));
                if (!File.Exists(fp))
                {
                    Engine.conf.Write(fp);
                }
            }
        }

        public static void BackupFTPSettings()
        {
            if (Adapter.CheckFTPAccounts())
            {
                string fp = Path.Combine(Engine.SettingsDir, string.Format("{0}-{1}-accounts.{2}", Application.ProductName, DateTime.Now.ToString("yyyyMM"), Engine.EXT_FTP_ACCOUNTS));
                if (!File.Exists(fp))
                {
                    FTPAccountManager fam = new FTPAccountManager(Engine.MyUploadersConfig.FTPAccountList);
                    fam.Save(fp);
                }
            }
        }

        /// <summary>
        /// Function to move a directory with overwriting existing files
        /// </summary>
        /// <param name="dirOld"></param>
        /// <param name="dirNew"></param>
        public static void MoveDirectory(string dirOld, string dirNew)
        {
            if (Directory.Exists(dirOld) && dirOld != dirNew)
            {
                if (MessageBox.Show("Would you like to move old Root folder content to the new location?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(dirOld, dirNew, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// Function to validate a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidLink(string url)
        {
            bool b = false;
            if (!string.IsNullOrEmpty(url))
            {
                b = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            }
            return b;
        }

        public static bool ManageImageFolders(string path)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                string[] images = Directory.GetFiles(path);

                List<string> imagesList = new List<string>();

                foreach (string image in images)
                {
                    foreach (string s in Engine.zImageFileTypes)
                    {
                        if (Path.HasExtension(image) && Path.GetExtension(image) == "." + s)
                        {
                            imagesList.Add(image);
                            break;
                        }
                    }
                }

                if (imagesList.Count > 0)
                {
                    if (MessageBox.Show(string.Format("{0} files found in {1}\nPlease wait until all the files are moved...",
                        imagesList.Count, path), Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        return false;
                    }

                    DateTime time;
                    string newFolderPath;
                    string movePath;

                    foreach (string image in imagesList)
                    {
                        if (File.Exists(image))
                        {
                            time = File.GetCreationTime(image);
                            newFolderPath = new NameParser(NameParserType.SaveFolder) { CustomDate = time }.Convert(Engine.conf.SaveFolderPattern);
                            newFolderPath = Path.Combine(Engine.RootImagesDir, newFolderPath);

                            if (!Directory.Exists(newFolderPath))
                            {
                                Directory.CreateDirectory(newFolderPath);
                            }

                            movePath = Path.Combine(newFolderPath, Path.GetFileName(image));
                            File.Move(image, movePath);
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public static string GetBrowserFriendlyUrl(string url)
        {
            url = Regex.Replace(url, " ", "%20");
            return url;
        }
    }
}