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

#region Source code: Greenshot (GPL)
/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/
#endregion

using Greenshot.Configuration;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Greenshot.Helpers
{
    /// <summary>
    /// Description of ImageOutput.
    /// </summary>
    public static class ImageOutput
    {
        private static IDataObject ido = new DataObject();

        /// <summary>
        /// prepares an IDataObject which might be used by CopyToClipboard() and/or SaveToFile()
        /// should be called whenever there might be need to place one or more dataformats in the clipboard
        /// this method leaves the actual clipboard untouched - its contents are not replaced as long as
        /// another functionality demands it
        /// </summary>
        public static void PrepareClipboardObject()
        {
            ido = new DataObject();
        }

        #region Save

        /// <summary>
        /// Saves image to specific path with specified quality
        /// </summary>
        public static void Save(Image img, string fullPath, int quality)
        {
            // check whether path exists - if not create it
            DirectoryInfo di = new DirectoryInfo(fullPath.Substring(0, fullPath.LastIndexOf(Path.DirectorySeparatorChar)));
            if (!di.Exists)
            {
                Directory.CreateDirectory(di.FullName);
            }

            ImageFormat imfo = null;
            string extension = fullPath.Substring(fullPath.LastIndexOf(".") + 1);
            if (extension.Equals("jpg")) extension = "jpeg"; // we need jpeg string with e for reflection
            extension = extension.Substring(0, 1).ToUpper() + extension.Substring(1).ToLower();
            try
            {
                Type t = typeof(ImageFormat);
                PropertyInfo pi = t.GetProperty(extension, typeof(ImageFormat));
                imfo = (ImageFormat)pi.GetValue(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                MessageBox.Show("Could not use " + extension + " as image format. Using Jpeg.");
                imfo = ImageFormat.Jpeg;
                extension = imfo.ToString();
            }
            PropertyItem pit = PropertyItemProvider.GetPropertyItem(0x0131, "Greenshot");
            img.SetPropertyItem(pit);
            if (extension.Equals("Jpeg"))
            {
                EncoderParameters parameters = new EncoderParameters(1);
                parameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(Encoder.Quality, quality);
                ImageCodecInfo[] ies = ImageCodecInfo.GetImageEncoders();
                img.Save(fullPath, ies[1], parameters);
            }
            else
            {
                img.Save(fullPath, imfo);
            }
            if ((bool)AppConfig.GetInstance().Output_File_CopyPathToClipboard)
            {
                ido.SetData(DataFormats.Text, true, fullPath);
                Clipboard.SetDataObject(ido, true);
            }
        }

        /// <summary>
        /// saves img to fullpath
        /// </summary>
        /// <param name="img">the image to save</param>
        /// <param name="fullPath">the absolute destination path including file name</param>
        public static void Save(Image img, string fullPath)
        {
            AppConfig conf = AppConfig.GetInstance();
            int q;
            if ((bool)conf.Output_File_PromptJpegQuality)
            {
                JpegQualityDialog jqd = new JpegQualityDialog();
                jqd.ShowDialog();
                q = jqd.Quality;
            }
            else
            {
                q = AppConfig.GetInstance().Output_File_JpegQuality;
            }
            Save(img, fullPath, q);
        }

        #endregion

        #region Save As

        //private static string eagerlyCreatedDir = null;

        public static string SaveWithDialog(Image img)
        {
            string ret = null;
            AppConfig conf = AppConfig.GetInstance();
            SaveFileDialog sfd = CreateSaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fn = GetFileNameWithExtension(sfd);
                    ImageOutput.Save(img, fn);
                    ret = fn;
                    conf.Output_FileAs_Fullpath = fn;
                    conf.Store();
                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return ret;
        }

        private static SaveFileDialog CreateSaveFileDialog()
        {
            AppConfig conf = AppConfig.GetInstance();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // prepare file dialog filter
            string fdf = "";
            int preselect = -1;
            for (int i = 0; i < RuntimeConfig.SupportedImageFormats.Length; i++)
            {
                string ifo = RuntimeConfig.SupportedImageFormats[i];
                if (ifo.ToLower().Equals("jpeg")) ifo = "Jpg"; // we dont want no jpeg files, so let the dialog check for jpg
                if (conf.Output_FileAs_Fullpath.EndsWith(ifo, StringComparison.CurrentCultureIgnoreCase)) preselect = i;
                fdf += ifo + "|*." + ifo.ToLower() + "|";
            }
            fdf = fdf.Substring(0, fdf.Length - 1);
            saveFileDialog.Filter = fdf;
            saveFileDialog.AddExtension = false;
            saveFileDialog.FilterIndex = preselect + 1;
            //applyPresetValues(saveFileDialog);
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(conf.Output_FileAs_Fullpath);
            saveFileDialog.CheckPathExists = false;
            return saveFileDialog;
        }

        /// <summary>
        /// provides saveFileDialog's FileName property with extension matching the
        /// selected filter item's extension
        /// </summary>
        /// <param name="saveFileDialog">a SaveFileDialogInstance</param>
        /// <returns>just saveFileDialog's FileName property, if it already ends with the correct extension, otherwise the correct extension is added</returns>
        private static string GetFileNameWithExtension(SaveFileDialog saveFileDialog)
        {
            // extract extension of currently selected filter
            int ix = (2 * (saveFileDialog.FilterIndex - 1)) + 1; // FilterIndex starts with 1!
            string selectedFilter = saveFileDialog.Filter.Split('|')[ix];
            string selectedExt = Path.GetExtension(selectedFilter);

            string fn = saveFileDialog.FileName;
            string ext = Path.GetExtension(fn);
            // if the filename contains a valid extension, which is the same like the selected filter item's extension, the filename is okay
            if (fn.EndsWith(selectedExt, System.StringComparison.CurrentCultureIgnoreCase)) return fn;
            // otherwise we just add the selected filter item's extension
            else return fn + selectedExt;
        }

        /*
        /// <summary>
        /// sets InitialDirectory and FileName property of a SaveFileDialog smartly, considering default pattern and last used path
        /// </summary>
        /// <param name="sfd">a SaveFileDialog instance</param>
        private static void applyPresetValues(SaveFileDialog sfd) {
			
            AppConfig conf = AppConfig.GetInstance();
            string path = conf.Output_FileAs_Fullpath;
            if(path.Length == 0) {
                // first save -> apply default storage location and pattern
                sfd.InitialDirectory = conf.Output_File_Path;
                sfd.FileName = FilenameHelper.GetFilenameWithoutExtensionFromPattern(conf.Output_File_FilenamePattern);
            } else {
                // check whether last used path matches default pattern
                string patternStr = conf.Output_File_FilenamePattern;
                patternStr = patternStr.Replace(@"\",@"\\"); // escape backslashes for regex
                IDictionaryEnumerator en = FilenameHelper.Placeholders.GetEnumerator();
                while(en.MoveNext()) {
                    patternStr = patternStr.Replace(en.Key.ToString(),en.Value.ToString());
                }
                Regex rg = new Regex(patternStr);
                Match m = rg.Match(path);
                // rootDir serves as root for pattern based saving
                String rootDir = (m.Success) ? path.Substring(0, m.Index) : Path.GetDirectoryName(path);
                String fileNameFromPattern = FilenameHelper.GetFilenameWithoutExtensionFromPattern(conf.Output_File_FilenamePattern);
                String appendDir = Path.GetDirectoryName(fileNameFromPattern);
                String fileName = Path.GetFileName(fileNameFromPattern);
				
                String recommendedDir = Path.Combine(rootDir, appendDir);
				
                // due to weird behavior of SaveFileDialog, we cannot use a path in the FileName property (causes InitialDirectory to be ignored)
                // thus we create the recommended dir eagerly, assign it to InitialDirectory, and clean up afterwards, if it has not been used
                DirectoryInfo di = new DirectoryInfo(recommendedDir);
                if(!di.Exists) {
                     Directory.CreateDirectory(recommendedDir);
                     eagerlyCreatedDir = recommendedDir;
                }
                sfd.InitialDirectory = recommendedDir;
                sfd.FileName = fileName;
            }
        }
        */

        #endregion

        #region Clipboard

        /// <summary>
        /// copies the specified image bitmap data to the clipboard
        /// </summary>
        /// <param name="img">the image to be copied to the clipboard</param>
        public static void CopyToClipboard(Image img)
        {
            MemoryStream source = new MemoryStream();
            MemoryStream dest = new MemoryStream();

            img.Save(source, ImageFormat.Bmp);
            byte[] b = source.GetBuffer();
            dest.Write(b, 14, (int)source.Length - 14); // why the hell 14 ??
            source.Position = 0;

            ido.SetData(DataFormats.Dib, true, dest);
            Clipboard.SetDataObject(ido, true);
            dest.Close();
            source.Close();
        }

        #endregion
    }
}