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
using System.IO;

namespace ZSS.ImageUploader
{
    public class HTTPUploader
    {
        private const string mEndStr = "\r\n";

        protected bool Upload(MemoryStream memoryStream, Stream requestStream)
        {
            try
            {
                int count;
                byte[] tmp = new byte[4096]; //4 KiB

                //position at beginning of the stream
                memoryStream.Position = 0L;

                //possibly add in a progress bar...
                do
                {
                    count = memoryStream.Read(tmp, 0, tmp.Length);
                    requestStream.Write(tmp, 0, count);
                }
                while (count > 0);

                //success
                return true;
            }
            catch
            {
                //failure
                return false;
            }
        }

        protected void WriteFile(StreamWriter streamWriter, MemoryStream memoryStream, string boundary, string field, string fileName, byte[] fileBytes)
        {
            streamWriter.Write("--" + boundary + mEndStr);
            streamWriter.Write("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", field, fileName, mEndStr);
            streamWriter.Write("Content-Type: image/png {0}{0}", mEndStr);
            streamWriter.Flush();

            memoryStream.Write(fileBytes, 0, fileBytes.Length);

            streamWriter.Write(mEndStr);
            streamWriter.Write("--{0}--{1}", boundary, mEndStr);
            streamWriter.Flush();
        }

        protected void WritePost(StreamWriter streamWriter, string boundary, string field, string data)
        {
            streamWriter.Write("--" + boundary + mEndStr);
            streamWriter.Write("Content-Disposition: form-data; name=\"{0}\"{1}", field, mEndStr);
            streamWriter.Write("Content-Type: text/plain; charset=utf-8{0}", mEndStr);
            streamWriter.Write("Content-Transfer-Encoding: 8bit{0}{0}", mEndStr);
            streamWriter.Write("{0}{1}", data, mEndStr);
            streamWriter.Write("--{0}--{1}", boundary, mEndStr);
            streamWriter.Flush();
        }
    }
}
