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
using ZSS.Forms;
using ZSS.Properties;
using ZSS.Helpers;
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ZSS
{
    public static class FileSystem
    {
        public static ImageFormat[] mImageFormats = { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif, ImageFormat.Bmp, ImageFormat.Tiff, ImageFormat.Emf, ImageFormat.Wmf, ImageFormat.Icon };

        private static StringBuilder mDebug = new StringBuilder();
        //private static string mFilePathDebug = Path.Combine(Program.LocalAppDataFolder, string.Format("{0}-{1}-debug.txt",Application.ProductName, DateTime.Now.ToString("yyyyMMdd")));

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
        public static string SaveImage(Image img, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (Program.conf.ShowWatermark) img = WatermarkMaker.GetImage(img);

                long size = Program.conf.SwitchAfter * 1024;

                MemoryStream ms = new MemoryStream();
                MyGraphics.SaveImageToMemoryStream(img, ms, mImageFormats[Program.conf.FileFormat]);

                long len = ms.Length;

                if (len > size && size != 0)
                {
                    ms = new MemoryStream();
                    MyGraphics.SaveImageToMemoryStream(img, ms, mImageFormats[Program.conf.SwitchFormat]);

                    filePath = Path.ChangeExtension(filePath, Program.zImageFileTypes[Program.conf.SwitchFormat]);
                }

                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                FileStream fi = File.Create(filePath);

                ms.WriteTo(fi);

                fi.Close();

                ms.Dispose();
                fi.Dispose();
            }

            return filePath;
        }

        //public static string DebugFilePath
        //{
        //    get
        //    { return mFilePathDebug; }
        //    private set { ;}
        //}

        public static string getTextFromFile(string filePath)
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

        public static string getConfigFilePath()
        {

            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            return config.FilePath;

        }

        public static string getTempFilePath(string fileName)
        {

            string dir = Program.conf.CacheDir;
            if (string.IsNullOrEmpty(dir))
                dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, fileName);

        }

        public static void appendDebug(string msg)
        {
            mDebug.AppendLine(DateTime.Now.Ticks.ToString() + " " + msg);
        }

        //public static void writeDebugFile()
        //{
        //    if (mDebug.Length > 0)
        //    {
        //        using (StreamWriter sw = new StreamWriter(mFilePathDebug, true))
        //        {
        //            sw.WriteLine(mDebug.ToString());
        //            mDebug = new StringBuilder();
        //        }
        //    }
        //}

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
                Console.WriteLine(ex.ToString());
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
                Console.WriteLine(ex.Message);
            }

            return text;
        }

        /// <summary>
        /// Gets a fully qualified File Path for a file name provided as a StringBuilder. Save Location is set by the GUI. 
        /// </summary>
        /// <param name="file">File Name</param>
        /// <returns>Full qualitied File Path</returns>
        public static string GetFilePath(string fileName, bool manualNaming)
        {
            string filePath = Path.Combine(Program.conf.ImagesDir, fileName + "." + Program.zImageFileTypes[Program.conf.FileFormat]);

            if (manualNaming)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Path.Combine(Path.GetDirectoryName(filePath), GetScreenshotName(Path.GetFileNameWithoutExtension(filePath))));
                sb.Append(Path.GetExtension(filePath));
                filePath = sb.ToString();
            }

            return filePath;
        }

        /// <summary>
        /// Function get 
        /// </summary>
        /// <param name="fName"></param>
        /// <returns></returns>
        private static string GetScreenshotName(string fName)
        {
            if (Program.conf.ManualNaming)
            {
                InputBox ib = new InputBox();
                ib.Question = "Specify a Screenshot Name...";
                ib.Answer = fName;
                ib.Icon = Properties.Resources.zss_main;
                //Program.MyInputBox.BringToFront();
                ib.ShowDialog();
                if (ib.DialogResult == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder(ib.Answer);
                    sb = NameParser.Normalize(sb);
                    if (!fName.Equals(ib.Answer))
                    {
                        //fName = string.Format("{0}-{1}", sb.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
                        fName = sb.ToString();
                    }
                }
            }

            return fName;
        }

        /// <summary>
        /// Function to check if file is a valid Image file by checking its extension
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static bool IsValidImageFile(string fp)
        {
            bool b = false;

            foreach (string s in Program.zImageFileTypes)
            {
                if (fp.EndsWith(s))
                    b = true;
            }

            return b;
        }

        /// <summary>
        /// Function to check if file is a valid Text file by checking its extension
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static bool IsValidTextFile(string fp)
        {
            bool b = false;

            foreach (string s in Program.zTextFileTypes)
            {
                if (fp.EndsWith(s))
                    b = true;
            }

            return b;
        }
    }
}