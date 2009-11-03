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
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using ZSS;
using ZScreenLib.Properties;
using UploadersLib;

namespace ZScreenLib
{
    public static class FileSystem
    {
        public static ImageFormat[] mImageFormats = { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif, ImageFormat.Bmp, ImageFormat.Tiff, ImageFormat.Icon };

        public static StringBuilder mDebug = new StringBuilder();

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
                    files.Add(fp);
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
        /// Function to return the file path of a captured image. ImageFormat is based on length of the image.
        /// </summary>
        /// <param name="img">The actual image</param>
        /// <param name="filePath">The path to where the image will be saved</param>
        /// <returns>Returns the file path to a screenshot</returns>
        public static string SaveImage(ref WorkerTask task)
        {
            Image img = task.MyImage;
            string filePath = task.LocalFilePath;

            if (!string.IsNullOrEmpty(filePath))
            {
                img = ImageEffects.ApplySizeChanges(img);
                img = ImageEffects.ApplyScreenshotEffects(img);
                img = ImageEffects.ApplyWatermark(img);

                long size = (long)Engine.conf.SwitchAfter * 1024;
                ImageFormat imgFormat = mImageFormats[Engine.conf.FileFormat];

                MemoryStream ms = new MemoryStream();
                try
                {
                    GraphicsMgr.SaveImageToMemoryStream(img, ms, imgFormat);

                    // Change PNG to JPG (Lossy) if file size is large
                    if (ms.Length > size && size != 0)
                    {
                        ms = new MemoryStream();
                        GraphicsMgr.SaveImageToMemoryStream(img, ms, mImageFormats[Engine.conf.SwitchFormat]);
                        filePath = Path.ChangeExtension(filePath, Engine.zImageFileTypes[Engine.conf.SwitchFormat]);
                    }

                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    int retry = 3;
                    while (retry > 0 && !File.Exists(filePath))
                    {
                        using (FileStream fi = File.Create(filePath))
                        {
                            if (retry < 3) { System.Threading.Thread.Sleep(1000); }
                            FileSystem.AppendDebug(string.Format("Writing image {0}x{1} to {2}", img.Width, img.Height, filePath));
                            ms.WriteTo(fi);
                            retry--;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug("Error while saving image", ex);
                }
                finally
                {
                    if (ms != null) ((IDisposable)ms).Dispose();
                }
            }
            task.UpdateLocalFilePath(filePath);
            return filePath;
        }

        public static string GetTextFromFile(string filePath)
        {
            string s = "";
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

        public static void AppendDebug(string descr, Exception ex)
        {
            AppendDebug(descr + " " + ex.ToString());
        }

        public static void AppendDebug(string msg)
        {
            // a modified http://iso.org/iso/en/prods-services/popstds/datesandtime.html - McoreD
            string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss - ") + msg;
            System.Diagnostics.Debug.WriteLine(line);
            mDebug.AppendLine(line);
        }

        public static void WriteDebugFile()
        {
            if (!string.IsNullOrEmpty(Engine.LogsDir))
            {
                AppendDebug("Writing Debug file");
                string fpDebug = Path.Combine(Engine.LogsDir, string.Format("{0}-{1}-debug.txt", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")));
                if (Engine.conf.WriteDebugFile)
                {
                    if (mDebug.Length > 0)
                    {
                        using (StreamWriter sw = new StreamWriter(fpDebug, true))
                        {
                            sw.WriteLine(mDebug.ToString());
                            mDebug = new StringBuilder();
                        }
                    }
                }
            }
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
                FileSystem.AppendDebug("Error while exporting text", ex);
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
                FileSystem.AppendDebug("Error while getting text from resource", ex);
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
                    FTPAccountManager fam = new FTPAccountManager(Engine.conf.FTPAccountList);
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
                            newFolderPath = NameParser.Convert(new NameParserInfo(NameParserType.SaveFolder) { CustomDate = time });
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
    }
}