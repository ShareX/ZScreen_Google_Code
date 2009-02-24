#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

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
using System.Net;
using System.IO;
using System.Xml;

namespace ZSS.ImageUploader
{
    public class ImageShackUploader : HTTPUploader, IUploader
    {
        
        public List<ImageFile> UploadImage(string File)
        {
            //use a new guid as the boundary
            string boundary = Guid.NewGuid().ToString().Replace("-", "");

            byte[] fileBytes = null;

            List<ImageFile> imageFiles = new List<ImageFile>();

            try
            {
                //read the image into a stream
                using (FileStream fileStream = new FileStream(File, FileMode.Open))
                {
                    byte[] tmp = new byte[fileStream.Length];
                    fileStream.Read(tmp, 0, tmp.Length);
                    fileBytes = tmp;
                }
            }
            catch (FileNotFoundException e)
            {
                //MessageBox.Show(e.Message);
                return imageFiles; //empty
            }
            catch (IOException e)
            {
                //MessageBox.Show(e.Message);
                return imageFiles; //empty
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return imageFiles; //empty
            }

            try
            {
                //dumb fix... required so it doesn't give the expect 100 error
                ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("http://imageshack.us/index.php");
                MemoryStream memoryStream = new MemoryStream();
                StreamWriter streamWriter = new StreamWriter(memoryStream);

                httpRequest.Method = "POST";
                httpRequest.ContentType = String.Format("multipart/form-data; boundary={0}", boundary);
                httpRequest.UserAgent = "ZScreen [http://brandonz.net/projects/zscreen]";

                //write the file data
                base.WriteFile(streamWriter, memoryStream, boundary, "fileupload", File, fileBytes);

                //write the post data fields
                base.WritePost(streamWriter, boundary, "xml", "yes");
                base.WritePost(streamWriter, boundary, "MAX_FILE_SIZE", (1024 * 1024).ToString()); //1MB file limit

                //optimage, optsize may be used in the future for resizing

                httpRequest.ContentLength = memoryStream.Length;

                Stream requestStream = httpRequest.GetRequestStream();

                if (!base.Upload(memoryStream, requestStream))
                    return null;

                memoryStream.Close();
                requestStream.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());

                //write xml data to a string
                string xmlData = streamReader.ReadToEnd();
                streamReader.Close();

                //create an xml document from the string
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlData);

                //find matching elements and create new ImageFile of type FULLIMAGE/THUMBNAIL
                foreach(XmlElement xmlElement in xmlDocument.SelectNodes("/links/*"))
                {
                    string tmpElement = xmlElement.Name;

                    switch (tmpElement)
                    {
                        case "image_link":
                            imageFiles.Add(new ImageFile(xmlElement.InnerText, ImageFile.ImageType.FULLIMAGE));
                            break;
                        case "thumb_link":
                            imageFiles.Add(new ImageFile(xmlElement.InnerText, ImageFile.ImageType.THUMBNAIL));
                            break;
                    }
                }
                if (imageFiles.Count == 2)
                {
                    imageFiles.Add(ImageFile.getThumbnailForum1ImageFile(imageFiles[0].URI, imageFiles[1].URI));
                }
            }
            catch
            {
                //will return an empty list
            }

            return imageFiles;
        }
    }
}