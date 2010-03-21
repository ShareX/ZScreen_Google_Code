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
using System.Collections.Generic;
using System.IO;

namespace UploadersLib.Helpers
{
    [Serializable]
    public class ImageHostingServiceManager
    {
        public List<ImageHostingService> ImageHostingServices { get; set; }

        public ImageHostingServiceManager()
        {
            this.ImageHostingServices = new List<ImageHostingService>();
        }

        public void Save(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    System.Xml.Serialization.XmlSerializer xs =
                        new System.Xml.Serialization.XmlSerializer(typeof(ImageHostingServiceManager));
                    xs.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static ImageHostingServiceManager Read(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        System.Xml.Serialization.XmlSerializer xs =
                            new System.Xml.Serialization.XmlSerializer(typeof(ImageHostingServiceManager));
                        ImageHostingServiceManager set = xs.Deserialize(fs) as ImageHostingServiceManager;
                        fs.Close();
                        return set;
                    }
                }
                catch
                {
                    //just return blank settings
                }
            }

            return new ImageHostingServiceManager();
        }
    }
}