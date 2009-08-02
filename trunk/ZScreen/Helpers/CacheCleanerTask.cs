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

using System.Collections.Generic;
using System.IO;
using ZSS.ImageUploadersLib;

namespace ZSS.Tasks
{
    /// <summary>
    /// This class is responsible to keep be cache size at its limit
    /// By default the cache size is 50 MiB
    /// If the cache dir is bigger than this size then the background worker will 
    /// remove the oldest files to keep the size down
    /// </summary>
    class CacheCleanerTask
    {
        private string mCacheDir = "";
        private decimal mCacheSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loc">Location of Cache</param>
        /// <param name="size">Size of Cache in Mebibytes (MiB). A 80 minute CDR has 700 MiB.</param>
        public CacheCleanerTask(string loc, decimal size)
        {
            this.mCacheDir = loc;
            this.mCacheSize = size;
        }

        public void CleanCache()
        {
            if (Directory.Exists(mCacheDir))
            {
                List<ImageFile> files = new List<ImageFile>();

                //include all supported file types
                foreach (string s in Program.zImageFileTypes)
                {
                    string[] tmpFiles = Directory.GetFiles(mCacheDir, "*." + s, SearchOption.AllDirectories);
                    foreach (string f in tmpFiles)
                    {
                        files.Add(new ImageFile(f));
                    }
                }

                // sort in dateModified
                files.Sort();

                // determine cache size 
                decimal dirSize = 0;
                foreach (ImageFile f in files)
                {
                    dirSize += f.Size;
                }

                if (dirSize > 0)
                {
                    FileSystem.AppendDebug(string.Format("Cache Size (before): {0} MiB", dirSize.ToString("0.00")));
                }

                while (dirSize > mCacheSize)
                {
                    if (files.Count > 0)
                    {
                        ImageFile f = files[0];
                        FileSystem.AppendDebug("Deleting: " + f.LocalFilePath);
                        dirSize -= f.Size;
                        File.Delete(f.LocalFilePath);
                        files.RemoveAt(0);
                    }
                }

                if (dirSize > 0)
                {
                    FileSystem.AppendDebug(string.Format("Cache Size (after): {0} MiB", dirSize.ToString("0.00")));
                }
            }
        }
    }
}