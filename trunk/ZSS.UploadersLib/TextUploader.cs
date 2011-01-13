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
using System.IO;
using System.Text;
using UploadersLib.HelperClasses;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;

namespace UploadersLib
{
    [Serializable]
    public abstract class TextUploader : Uploader
    {
        #region ** THIS HAS TO BE UP-TO-DATE OTHERWISE XML SERIALIZING IS GOING TO FUCK UP **

        public static List<Type> Types = new List<Type> { typeof(Paste2Uploader), typeof(PastebinCaUploader), typeof(PastebinUploader),
            typeof(SlexyUploader), typeof(SniptUploader), typeof(TinyURLUploader), typeof(ThreelyUploader), typeof(KlamUploader),
            typeof(IsgdUploader), typeof(JmpUploader), typeof(BitlyUploader), typeof(TurlUploader), typeof(GoogleURLShortener), typeof(TextUploader) };

        #endregion ** THIS HAS TO BE UP-TO-DATE OTHERWISE XML SERIALIZING IS GOING TO FUCK UP **

        protected TextUploader() { }

        /// <summary>
        /// Descriptive name for the Text Uploader
        /// </summary>
        public override string ToString()
        {
            return "TextUploader";
        }

        /// <summary>
        /// String used to test the functionality
        /// </summary>
        public virtual string TesterString
        {
            get { return "http://code.google.com/p/zscreen"; }
        }

        public virtual object Settings { get; set; }

        public abstract string UploadText(TextInfo text);

        public string UploadText(Stream stream)
        {
            string text = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            return UploadText(TextInfo.FromString(text));
        }

        public string UploadTextFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return UploadText(TextInfo.FromFile(filePath));
            }

            return string.Empty;
        }
    }

    public abstract class TextUploaderSettings
    {
        public abstract string Name { get; set; }
        public abstract string URL { get; set; }
    }
}